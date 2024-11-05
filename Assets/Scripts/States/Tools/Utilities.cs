using System;

namespace Utilities
{
    public class Tool
    {
        private static Random _random = new Random();

        /// <summary>
        /// Creates a list containing the coordinates the player will travel. The coordinates range from `{ -3, -2, -1, 1, 2, 3 }`
        /// representing short (1), medium (2) and large (3) distances the player can teleport from it current position, and
        /// whether the platfrom is to the left (-) or to the right (+).
        /// The time complexity is O(2N)
        /// </summary>
        public static void CreateCoordinatesList(uint visitCount, out int[] coordinates)
        {
            if (visitCount == 0) // Makes sure of at leats one
                visitCount = 1;

            int listSize = (int)visitCount * 3 * 2; // 3 distances (small, medium, large), 2 direction (Left, Right)

            coordinates = new int[listSize];
            float half = listSize * 0.5f;

            // Step 1: Populate the list with coordinates  
            for (int i = 0; i < listSize; i++)
            {
                int coordinate = (1 + (i % 3)) * (i < half ? -1 : 1);
                coordinates[i] = coordinate;
            }

            // Step 2: shuffle the list to create a perception of randomness
            for (int i = 0; i < listSize; i++)
            {
                // cout << list[i] << "before swaping \n";
                int min = i;
                int max = listSize - 1;
                int random = (int)_random.Next(min, max);

                int temp = coordinates[i];
                coordinates[i] = coordinates[random];
                coordinates[random] = temp;
            }

        }
    }
}
