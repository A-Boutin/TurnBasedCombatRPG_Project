using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[Serializable]
public enum Direction
{
    Any,
    Forward,
    Back,
    Left,
    Right,
}

[Serializable]
public class MoveAction : Action
{
    [SerializeField] public Target target;
    [SerializeField] public Direction direction;
    [Range(0, 15)][SerializeField] public int moveAmount;
    [SerializeField] public bool push;
    [Range(0,15)][SerializeField] public int pushDamage;

    private Node customTarget = null;

    public MoveAction()
    {
        this.target = Target.Any;
        this.direction = Direction.Any;
        this.moveAmount = 0;
        this.push = false;
        this.pushDamage = 0;
    }
    public MoveAction(Target target, Direction direction, int moveAmount, Node customTarget = null)
    {
        this.target = target;
        this.direction = direction;
        this.moveAmount = moveAmount;
        this.customTarget = customTarget;
    }

    public IEnumerator Perform(Encounter encounter, Unit performer)
    {
        Debug.Log("Move Action");
        // Select a node
        // Move in direction relative to node (towards, away, left, or right of node).

        // Highlight movable nodes
        // Wait for player selection
        // Move performer Unit
        // Do this moveAmount of times

        for(int i = 0; i < moveAmount; i++)
        {
            //Highlight moveable nodes
            List<Node> selectableNodes = GetSelectableNodes(encounter, performer); // need something more sophisticated using targets and direction

            if (selectableNodes.Count == 0) yield break;

            yield return this.WaitForSelection(selectableNodes);

            Node oldPos = performer.GetCurrentPosition();

            performer.MoveTo(this.GetSelectedNode());
            Controls.SelectNode(null);

            if (push)
            {
                foreach(Unit pushedUnit in performer.GetCurrentPosition().GetUnits())
                {
                    if (pushedUnit == performer) continue;
                    yield return new MoveAction(Target.Any, Direction.Back, 1, oldPos).Perform(encounter, pushedUnit);
                    pushedUnit.Damage(pushDamage, false);
                }
            }

            yield return null;
        }
    }

    private List<Node> GetSelectableNodes(Encounter encounter, Unit performer)
    {
        Node currPos = performer.GetCurrentPosition();

        if (customTarget != null) return GetSelectableNodes(currPos, customTarget);

        if (target == Target.Any && direction == Direction.Any) return currPos.GetAdjacentNodes();
        else if (target == Target.Aggro) return GetSelectableNodes(currPos, encounter.GetAggro().GetCurrentPosition());
        else if (target == Target.Nearest) return GetSelectableNodes(currPos, GetNearest(performer).GetCurrentPosition());
        // Need to have something for if there's a target and a direction (if target, then direction is necessary)

        return null;
    }

    private List<Node> GetSelectableNodes(Node performerPos, Node relativePos)
    {
        List<Node> selectableNodes = new List<Node>();
        if (relativePos == performerPos)
        {
            // (if Direction is not forward, then go anywhere else
            if (direction == Direction.Forward)
            {
                selectableNodes.Add(performerPos);
                return selectableNodes;
            }
            else
            {
                return performerPos.GetAdjacentNodes();
            }
        }
        else
        {
            // change angle checks with distance checks
            float defAngle = GetAngle(performerPos, relativePos);
            float defDist = Controls.DistanceTo(performerPos, relativePos);
            List<Node> adjNodes = performerPos.GetAdjacentNodes();
            float nodeAngle;
            bool isEqual = false;
            bool isLess = false;
            foreach (Node node in adjNodes)
            {
                float nodeDist = Controls.DistanceTo(node, relativePos);
                switch (direction)
                {
                    // Need to change these to changing angle, not nodeAngle
                    case Direction.Forward:
                        if (nodeDist < defDist) selectableNodes.Add(node);
                        break;
                    case Direction.Back:
                        if (nodeDist > defDist) selectableNodes.Add(node);
                        break;
                    case Direction.Left:
                        nodeAngle = GetAngle(node, relativePos);
                        // Remove directly Forward and Backward Node
                        if (nodeAngle == defAngle || node == relativePos) continue;
                        // Remaining nodeDist < defDist is good (use angle to find if it's left or right of performerPos to relativePos
                        else if (!isEqual && nodeDist < defDist && IsLeft(performerPos, relativePos, node))
                        {
                            selectableNodes.Add(node);
                            isLess = true;
                        }
                        // If Empty, then nodeDist == defDist is good (use angle to find if it's left or right of performerPos to relativePos
                        else if (!isLess && nodeDist == defDist && IsLeft(performerPos, relativePos, node))
                        {
                            selectableNodes.Add(node);
                            isEqual = true;
                        }
                        break;
                    case Direction.Right:
                        nodeAngle = GetAngle(node, relativePos);
                        // Remove directly Forward and Backward Node
                        if (nodeAngle == defAngle || node == relativePos) continue;
                        // Remaining nodeDist < defDist is good (use angle to find if it's left or right of performerPos to relativePos
                        else if (!isEqual && nodeDist < defDist && !IsLeft(performerPos, relativePos, node)) 
                        { 
                            selectableNodes.Add(node); 
                            isLess = true; 
                        }
                        // If Empty, then nodeDist == defDist is good (use angle to find if it's left or right of performerPos to relativePos
                        else if (!isLess && nodeDist == defDist && !IsLeft(performerPos, relativePos, node)) 
                        { 
                            selectableNodes.Add(node); 
                            isEqual = true;  
                        } 
                        break;
                    default:
                        break;
                }
            }
            if (selectableNodes.Count == 0)
                selectableNodes.Add(performerPos);

            return selectableNodes;
        }
    }

    private bool IsLeft(Node origin, Node defPoint, Node relativePoint)
    {
        Vector3 o = origin.transform.position;
        Vector3 a = defPoint.transform.position - o;
        Vector3 b = relativePoint.transform.position - o;

        float det = (a.x * b.y) - (a.y * b.x);
        return det > 0;
    }

    private Unit GetNearest(Unit performer)
    {
        Player nearest = null;
        // Get nearest unit of opposite derived type (possibly have an enum that can be a parameter for this.
        if (performer.GetType() != typeof(Player))
        {
            List<Player> players = performer.GetCurrentPosition().gameObject.GetComponentInParent<Map>().GetMapPlayers();
            foreach (Player player in players)
            {
                Debug.Log(player.name);
            }
            players = players.OrderByDescending(f => f.aggro).ToList();
            // Check player distance to performer
            foreach (Player player in players)
            {
                if (nearest == null) nearest = player;
                else
                {
                    int nearestDist = Controls.DistanceTo(performer.GetCurrentPosition(), nearest.GetCurrentPosition());
                    int currDist = Controls.DistanceTo(performer.GetCurrentPosition(), player.GetCurrentPosition());
                    // Otherwise, check distance of player. If nearer than current player, replace as new nearest.
                    if (currDist < nearestDist) nearest = player;
                    // If same distance, but higher aggro, then replace as new nearest.
                    else if (currDist == nearestDist && player.aggro > nearest.aggro) nearest = player;
                    // Otherwise, do nothing.
                }
            }
        }

        return nearest;
    }

    private float GetAngle(Node X, Node Y)
    {
        float deltaX = (Y.transform.position.x - X.transform.position.x);
        float deltaY = (Y.transform.position.y - X.transform.position.y);

        float angle = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
        return angle + 180f;
    } 
}
