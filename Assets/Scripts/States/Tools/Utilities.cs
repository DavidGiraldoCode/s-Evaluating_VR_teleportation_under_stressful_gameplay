using System;
using System.Collections.Generic;

namespace Utilities
{
    public class CycleGraph
    {
        private List<int[]> _nodes;
        public int NodeCount { get => _nodes.Count; }

        /// <summary>
        /// Creates the graph by adding an adjacency list at each index of a `List<int[]>`, `0` for left and `1` for right clockwise.
        /// It can only create graph with at least two nodes
        /// </summary>
        /// <param name="nodesCount"></param>
        public static void BuildGraph(uint nodesCount, out List<int[]> nodesGraph)
        {
            // Invariance
            if (nodesCount < 2) nodesCount = 2; // This makes sure the graph has at least two nodes.

            nodesGraph = new List<int[]>();
            for (int i = 0; i < nodesCount; i++)
            {
                int[] adjacencyList = new int[2];

                adjacencyList[0] = (i + ((int)nodesCount - 1)) % (int)nodesCount;
                adjacencyList[1] = (i + 1) % (int)nodesCount;

                nodesGraph.Add(adjacencyList);
                //Debug.Log(_nodes[i][0] + " <- left | node: " + i + " | right -> " + _nodes[i][1]);
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
        public static int GetDestinationNode(List<int[]> nodes, int currentNode, int coordinate)
        {
            // Invariances
            bool invalidCoordinates = coordinate == 0 || coordinate > 3 || coordinate < -3; // The coordinate can only be {-3, -2, -1, 1, 2, 3}
            bool invalidNode = currentNode < 0 || currentNode >= nodes.Count; // The user of this method inputed a location that does not exist.

            if (invalidNode)
                throw new ArgumentException($"Location {currentNode} that does not exist, must be between [0, n-1] , n being the number of platforms");
            if (invalidCoordinates)
                throw new ArgumentException($"Coordinate {coordinate} is invalid," + " they can only be {-3, -2, -1, 1, 2, 3}");


            bool clockWise = coordinate > 0;
            int steps = Math.Abs(coordinate);
            int j = 0;
            int nextNode;

            while (j < steps)
            {
                if (!clockWise) // If the turn is counter clockwise, move to the left (0) node
                    nextNode = nodes[currentNode][0];
                else // If the turn is clockwise, move to the right (1) node
                    nextNode = nodes[currentNode][1];

                currentNode = nextNode;
                j++;
            }

            return currentNode;
        }

    }

    public class CoordinatesGenerator
    {
        private static Random _random = new Random();

        /// <summary>
        /// Creates a list containing the coordinates the player will travel. The coordinates range from `{ -3, -2, -1, 1, 2, 3 }`
        /// representing short (1), medium (2) and large (3) distances the player can teleport from it current position, and
        /// whether the platfrom is to the left (-) or to the right (+).
        /// The time complexity is O(N)
        /// </summary>
        /// <param name="visitCount"></param>
        /// <param name="coordinates"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void CreateCoordinatesList(uint visitCount, out int[] coordinates)
        {
            if (visitCount == 0) // Makes sure of at leats one
                throw new ArgumentException("The visitCount to create coordinates needs to be at least 1");

            int listSize = (int)visitCount * 3 * 2; // 3 distances (small, medium, large), 2 direction (Left, Right)

            coordinates = new int[listSize];
            float directionSwitchPoint = listSize * 0.5f;

            // Step 1: Populate the list with coordinates  
            for (int i = 0; i < listSize; i++)
            {
                int orientation = i < directionSwitchPoint ? -1 : 1;
                int coordinate = (1 + (i % 3)) * orientation;
                coordinates[i] = coordinate;
            }

            // Step 2: shuffle the list to create a perception of randomness / Fisher-Yates
            for (int i = 0; i < listSize; i++)
            {
                int max = listSize - 1;
                int random = (int)_random.Next(i, max);

                int temp = coordinates[i];
                coordinates[i] = coordinates[random];
                coordinates[random] = temp;
            }

        }
    }
}
