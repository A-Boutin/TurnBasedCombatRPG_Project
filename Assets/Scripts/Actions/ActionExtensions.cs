using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionExtensions
{
    public static Node selectedNode;
    public static Unit selectedUnit;

    public static IEnumerator WaitForSelection(this Action action, List<Node> selectableNodes)
    {
        HighlightNodes(selectableNodes, true);

        bool done = false;
        while (!done)
        {
            // IF get input is true Then done = true;
            selectedNode = Controls.selectedNode;
            if (selectedNode != null)
            {
                if (selectableNodes.Contains(selectedNode))
                {
                    done = true;
                }
            }
            yield return null;
        }

        HighlightNodes(selectableNodes, false);
    }
    public static IEnumerator WaitForSelection(this Action action, List<Unit> selectableUnits)
    {
        HighlightUnits(selectableUnits, true);

        bool done = false;
        while (!done)
        {
            // IF get input is true Then done = true;
            selectedUnit = Controls.selectedUnit;
            if (selectedUnit != null)
            {
                if (selectableUnits.Contains(selectedUnit))
                {
                    done = true;
                }
            }
            yield return null;
        }

        HighlightUnits(selectableUnits, false);
    }

    public static void HighlightNodes(List<Node> nodes, bool selectable)
    {
        foreach (Node node in nodes)
        {
            node.selectable = selectable;
        }
    }

    public static void HighlightUnits(List<Unit> units, bool selectable)
    {
        foreach (Unit unit in units)
        {
            unit.selectable = selectable;
        }
    }

    public static Node GetSelectedNode(this Action action)
    {
        return selectedNode;
    }
    public static Unit GetSelectedUnit(this Action action)
    {
        return selectedUnit;
    }
}
