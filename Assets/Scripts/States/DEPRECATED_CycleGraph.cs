using System;
using System.Collections.Generic;
using UnityEngine;

/* DEPRECATED, look into States/Tools/Utilities.cs for the graph definition

[CreateAssetMenu(fileName = "CycleGraph", menuName = "States/CycleGraph", order = 0)]
public class CycleGraph : ScriptableObject
{
    private List<int[]> _nodes;
    public int NodeCount { get => _nodes.Count; }
    
    /// <summary>
    /// Creates the graph by adding an adjacency list at each index of a `List<int[]>`, `0` for left and `1` for right clockwise.
    /// It can only create graph with at least two nodes
    /// </summary>
    /// <param name="nodesCount"></param>
    public void BuildGraph(uint nodesCount)
    {
        // Invariance
        if (nodesCount < 2) nodesCount = 2; // This makes sure the graph has at least two nodes.

        _nodes = new List<int[]>();
        for (int i = 0; i < nodesCount; i++)
        {
            int[] adjacencyList = new int[2];

            adjacencyList[0] = (i + ((int)nodesCount - 1)) % (int)nodesCount;
            adjacencyList[1] = (i + 1) % (int)nodesCount;

            _nodes.Add(adjacencyList);
            Debug.Log(_nodes[i][0] + " <- left | node: " + i + " | right -> " + _nodes[i][1]);
        }
    }

    /// <summary>
    /// The graphs retunrs the color of a node given a curren node (where the player is located) and coordinates.
    /// The coordnates have encoded the turn (left or right) with the sign and the steps to traverse the graph.
    /// It will retunr -1 if invalid coordinates or an inexistant node is inputed 
    /// </summary>
    /// <param name="currentNode">Valid coordinates {-3, -2, -1, 1, 2, 3}</param>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public int GetDestinationNode(int currentNode, int coordinate)
    {
        // Invariances
        bool invalidCoordinates = coordinate == 0 || coordinate > 3 || coordinate < -3; // The coordinate can only be {-3, -2, -1, 1, 2, 3}
        bool invalidNode = currentNode < 0 || currentNode >= _nodes.Count; // The user of this method inputed a location that does not exist.

        if ( invalidNode)
            throw new ArgumentException($"Location {currentNode} that does not exist, must be between [0, n-1] , n being the number of platforms");
        if (invalidCoordinates)
            throw new ArgumentException($"Coordinate {coordinate} is invalid,"+" they can only be {-3, -2, -1, 1, 2, 3}");


        bool clockWise = coordinate > 0;
        int steps = Math.Abs(coordinate);
        int j = 0;
        int nextNode;
        
        while (j < steps)
        {
            if (!clockWise) // If the turn is counter clockwise, move to the left (0) node
                nextNode = _nodes[currentNode][0];
            else // If the turn is clockwise, move to the right (1) node
                nextNode = _nodes[currentNode][1];

            #if UNITY_EDITOR
            //Debug.Log($"XXX moves to {nextNode}");
            #endif

            currentNode = nextNode;
            j++;
        }

        return currentNode;
    }

}
*/