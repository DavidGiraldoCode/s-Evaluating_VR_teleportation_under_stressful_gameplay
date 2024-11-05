using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CycleGraph", menuName = "States/CycleGraph", order = 0)]
public class CycleGraph : ScriptableObject
{
    private List<int[]> nodes;

    /// <summary>
    /// Creates the graph by adding an adjacency list at each index of a `List<int[]>`, `0` for left and `1` for right clockwize.
    /// </summary>
    /// <param name="nodesCount"></param>
    public void BuildGraph(uint nodesCount)
    {
        nodes = new List<int[]>();
        for (int i = 0; i < nodesCount; i++)
        {
            int[] adjacentyList = new int[2];

            adjacentyList[0] = (i + ((int)nodesCount - 1)) % (int)nodesCount;
            adjacentyList[1] = (i + 1) % (int)nodesCount;

            nodes.Add(adjacentyList);
            Debug.Log(nodes[i][0] + " <- left | node: " + i + " | right -> " + nodes[i][1]);
        }
    }

    /// <summary>
    /// The graphs retunrs the color of a node given a curren node (where the player is located) and coordinates.
    /// The coordnates have encoded the turn (left or right) with the sign and the steps to traverse the graph
    /// </summary>
    /// <param name="currentNode">Valid coordinates {-3, -2, -1, 1, 2, 3}</param>
    /// <param name="coordinates"></param>
    /// <returns></returns>
    public int GetDestinationNode(int currentNode, int coordinates)
    {
        // Invariances
        bool invalidCoordinates = coordinates == 0 || coordinates > 3 || coordinates < -3; // The coordinate can only be {-3, -2, -1, 1, 2, 3}
        bool invalidNode = currentNode < 0 || currentNode >= nodes.Count; // The user of this method inputed a location that does not exist.
        if (invalidCoordinates || invalidNode) return -1;

        bool clockWize = coordinates > 0;
        int steps = Math.Abs(coordinates);
        int j = 0;
        int nextNode;
        Debug.Log("starts at " + currentNode);
        while (j < steps)
        {
            if (!clockWize) // If the turn is counter clockwize, move to the left (0) node
                nextNode = nodes[currentNode][0];
            else // If the turn is clockwize, move to the right (1) node
                nextNode = nodes[currentNode][1];

            Debug.Log("moves to " + nextNode);
            currentNode = nextNode;
            j++;
        }

        return currentNode;
    }

}
