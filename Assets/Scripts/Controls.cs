using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Controls
{
    public static Node selectedNode;
    public static Unit selectedUnit;

    public static void SelectNode(Node node)
    {
        selectedNode = node;
    }

    public static void SelectUnit(Unit unit)
    {
        selectedUnit = unit;
    }

    public static int DistanceTo(Node currNode, Node target)
    {
        List<Node> path = AStar(currNode, target);
        return path.Count;
    }

    public static List<Node> AStar(Node start, Node goal)
    {
        //Initialize open and closed lists
        List<Node> openList = new List<Node>() { start }; // Nodes to be evaluated
        List<Node> closedList = new List<Node>(); // Nodes already evaluated

        // Initialize node properties
        start.g = 0; // Cost from start to start is 0
        start.h = Heuristic(start, goal); // Estimate to goal
        start.f = start.g + start.h; // Total estimated cost
        start.parent = null; // For path reconstruction
        while (openList.Any())
        {
            // Get node with lowest f value - implement using a priority queue for faster retrieval of the best node
            openList = openList.OrderBy(o => o.f).ToList();
            Node current = openList[0]; // node in openList with lowest f value

            // Check if we've reached the goal
            if (current == goal)
            {
                return ReconstructPath(current);
            }

            // Move current node from open to closed list
            // Add current to closedList
            closedList.Add(current);
            // Remove current from openList
            openList.RemoveAt(0);

            // Check all neighbouring nodes
            foreach (Node node in current.GetAdjacentNodes())
            {
                if (closedList.Contains(node))
                {
                    continue; // Skip already evaluated nodes
                }

                // Calculate tentative g score
                float tentative_g = current.g + Heuristic(current, node);

                if (!openList.Contains(node))
                {
                    openList.Add(node);
                }
                else if (tentative_g >= node.g)
                {
                    continue; // The path is not better
                }

                // This path is the best so far
                node.parent = current;
                node.g = tentative_g;
                node.h = Heuristic(node, goal);
                node.f = node.g + node.h;
            }
        }
        return null; // No path exists
    }
    // Calculates distance from start to goal node
    private static float Heuristic(Node start, Node goal)
    {
        Vector2 startPos = start.transform.position;
        Vector2 goalPos = goal.transform.position;

        float a = (goalPos.x - startPos.x);
        float b = (goalPos.y - startPos.y);

        return Mathf.Sqrt((a * a) + (b * b));
    }
    // WILL RETURN THE FULL PATH AS A LIST FROM GOAL TO START
    private static List<Node> ReconstructPath(Node current)
    {
        List<Node> path = new List<Node>();
        while (current != null)
        {
            //Debug.Log(current);
            //current.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
            path.Add(current);
            current = current.parent;
        }
        path.Reverse();
        path.RemoveAt(0);
        return path;
    }
}
