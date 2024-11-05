namespace Utilities
{
    public class Tool
    {
        /// @brief Creates a list containing the of distances the player will travel, in this case 3, due to the shape of the hexagon.
        /// And populates the list with negative and positive values representing left and right respectively.
        /// @param visitCount how many times each distance is going to be visited

        /// <summary>
        /// Creates a list containing the coordinates the player will travel. The coordinates range from `{ -3, -2, -1, 1, 2, 3 }`
        /// representing short (1), medium (2) and large (3) distances the player can teleport from it current position, and
        /// whether the platfrom is to the left (-) or to the right (+)
        /// </summary>
        public static void CreateCoordinatesList(uint visitCount, out int[] coordinates)
        {
            if (visitCount == 0) // Makes sure of at leats one
                visitCount = 1;

            int listSize = (int)visitCount * 3 * 2; // 3 distances (small, medium, large), 2 direction (Left, Right)
            coordinates = new int[listSize];
            float half = listSize * 0.5f;

            for (int i = 0; i < coordinates.Length; i++)
            {
                int coordinate = (1 + (i % 3)) * (i < half ? -1 : 1);
                coordinates[i] = coordinate;
            }

        }

        private static void ShuffleList(int var2)
        {
            var2 = 2;
        }
    }
}
