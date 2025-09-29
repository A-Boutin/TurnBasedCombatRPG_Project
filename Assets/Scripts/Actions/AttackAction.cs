using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;

[Serializable]
public class AttackAction : Action
{
    [SerializeField] public Target target;
    [SerializeField] public DiceSet dmgSet; // determines the amount of damage the performer deals
    [Range(-1,10)][SerializeField] public int range = 0;      // determines how far the performer is allowed to attack
    [SerializeField] public bool AOE = false;       // deal damage to the entire node instead of a single target
    [SerializeField] public bool shaft = false;     // cannot attack at range 0
    [SerializeField] public bool magic = false;     // deal magical damage (goes against resistance instead of block)

    [SerializeField] public bool push;

    public IEnumerator Perform(Encounter encounter, Unit performer)
    {
        Debug.Log("Attack Action");
        // Select a target IN RANGE (Node if AOE, Unit if not)
        // Damage the target
        if (AOE)
        {
            List<Node> selectableNodes = GetAOENodes(encounter, performer);

            if (selectableNodes.Count == 0) yield break;

            yield return this.WaitForSelection(selectableNodes);

            int dmg = dmgSet.Roll();

            // Loop for each Unit on target node, deal damage.
            foreach (Unit unit in this.GetSelectedNode().GetUnits())
            {
                if(unit.GetType() != performer.GetType())
                {
                    // Have to give option to the player to roll out of the way OR to block
                    DealDamage(unit, dmg);
                    if (push)
                        yield return new MoveAction(Target.Any, Direction.Back, 1, performer.GetCurrentPosition()).Perform(encounter, unit);
                }
            }

            Controls.SelectNode(null);
        }
        else
        {
            List<Unit> selectableUnits = GetTargets(encounter, performer);

            if (selectableUnits.Count == 0) yield break;

            yield return this.WaitForSelection(selectableUnits);

            int dmg = dmgSet.Roll();

            DealDamage(this.GetSelectedUnit(), dmg);
            if (push)
                yield return new MoveAction(Target.Any, Direction.Back, 1, performer.GetCurrentPosition()).Perform(encounter, this.GetSelectedUnit());

            Controls.SelectUnit(null);

            //deal damage
            //target.GetComponent<IDamageable>().Damage(dmg, magic);
        }
        yield return null;
    }

    // Make player select a valid node
    private List<Node> GetAOENodes(Encounter encounter, Unit performer)
    {
        Node performerPos = performer.GetCurrentPosition();
        List<Node> selectableNodes = new List<Node>();

        Node nearest = GetNearest(performer).GetCurrentPosition();

        List<Node> map = performerPos.GetMap().nodes;
        foreach(Node node in map)
        {
            if (Controls.DistanceTo(performerPos, node) == 0 && shaft) continue;

            if (Controls.DistanceTo(performerPos, node) <= range) // need to add that it has units as children as well
            {
                if (performer.GetType() != typeof(Player))
                {
                    if (target == Target.Nearest && node == nearest)
                    {
                        selectableNodes.Add(node);
                        break;
                    }
                    else if (target == Target.Aggro && node == encounter.GetAggro().GetCurrentPosition())
                    {
                        selectableNodes.Add(node);
                        break;
                    }
                }

                foreach (Unit unit in node.GetUnits())
                {
                    if (unit.GetType() != performer.GetType() && target == Target.Any)
                    {
                        selectableNodes.Add(node);
                        break;
                    }
                }
            }
        }

        return selectableNodes;
    }
    // Make player select a valid unit
    private List<Unit> GetTargets(Encounter encounter, Unit performer)
    {
        Node performerPos = performer.GetCurrentPosition();

        List<Unit> selectableTargets = new List<Unit>();

        Unit nearest = GetNearest(performer);

        List<Node> map = performerPos.GetMap().nodes;
        foreach(Node node in map)
        {
            if (Controls.DistanceTo(performerPos, node) == 0 && shaft) continue;

            if (Controls.DistanceTo(performerPos, node) <= range)
            {
                foreach(Unit unit in node.GetUnits())
                {
                    if(performer.GetType() != typeof(Player))
                    {
                        if (target == Target.Nearest && unit == nearest)
                        {
                            selectableTargets.Add(unit);
                            break;
                        }
                        else if(target == Target.Aggro && unit == encounter.GetAggro())
                        {
                            selectableTargets.Add(unit);
                            break;
                        }
                    }

                    if(unit.GetType() != performer.GetType() && target == Target.Any)
                    {
                        selectableTargets.Add(unit);
                    }
                }
            }
        }

        return selectableTargets;
    }

    private void DealDamage(Unit unit, int dmg)
    {
        int hp;

        if (unit is Player p) hp = p.Damage(dmg, magic);
        else if (unit is Enemy e) hp = e.Damage(dmg, magic);
        else hp = unit.Damage(dmg, magic);

        Debug.Log(unit.GetType() + ": " + hp);
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
}
