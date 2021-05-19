using System;

namespace CourseTask
{
    class Program
    {
        public static void Main(string[] args)
        {
            // First test

            int N = 10;
            int[,] adjacencyMatrix = new int[10, 10]
            {
                //0   1   2   3   4   5   6   7   8   9
                { 0 , 1 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 }, // 0
                { 1 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 }, // 1
                { 0 , 0 , 0 , 6 , 2 , 0 , 0 , 0 , 0,  0 }, // 2
                { 0 , 0 , 6 , 0 , 0 , 2 , 0 , 0 , 0 , 0 }, // 3
                { 0 , 0 , 2 , 0 , 0 , 1 , 3 , 0 , 0 , 0 }, // 4
                { 0 , 0 , 0 , 2 , 1 , 0 , 0 , 0 , 4 , 0 }, // 5
                { 0 , 0 , 0 , 0 , 3 , 0 , 0 , 5 , 0 , 0 }, // 6
                { 0 , 0 , 0 , 0 , 0 , 0 , 5 , 0 , 1 , 0 }, // 7
                { 0 , 0 , 0 , 0 , 0 , 4 , 0 , 1 , 0 , 0 }, // 8
                { 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 , 0 }  // 9
            };

            int[] firstSetOfVertexes = new int[3] { 0, 1, 2 };
            int[] secondSetOfVertexes = new int[3] { 5, 6, 7 };

            // Second test

            //int N = 6;
            //int[,] adjacencyMatrix = new int[6, 6]
            //{
            //    //0   1   2   3   4   5
            //    { 0 , 0 , 0 , 0 , 0 , 0}, // 0
            //    { 0 , 0 , 1 , 0 , 0 , 1}, // 1
            //    { 0 , 1 , 0 , 0 , 1 , 0}, // 2
            //    { 0 , 0 , 0 , 0 , 0 , 0}, // 3
            //    { 0 , 0 , 1 , 0 , 0 , 0}, // 4
            //    { 0 , 1 , 0 , 0 , 0 , 0}  // 5
            //};

            //int[] firstSetOfVertexes = new int[2] { 0, 5 };
            //int[] secondSetOfVertexes = new int[2] { 3, 4 };

            // Third test

            //int N = 6;
            //int[,] adjacencyMatrix = new int[6, 6]
            //{
            //    //0   1   2    3    4   5
            //    { 0 , 0 , 0  , 0  , 0 , 0},  // 0
            //    { 0 , 0 , 1  , 0  , 0 , 0},  // 1
            //    { 0 , 1 , 0  , 10 , 1 , 11}, // 2
            //    { 0 , 0 , 10 , 0  , 0 , 4},  // 3
            //    { 0 , 0 , 1  , 0  , 0 , 3},  // 4
            //    { 0 , 0 , 11 , 4  , 3 , 0}   // 5
            //};

            //int[] firstSetOfVertexes = new int[2] { 0, 1 };
            //int[] secondSetOfVertexes = new int[2] { 3, 5 };

            int result = 0;
            result = GetShortestDistance(adjacencyMatrix, N, firstSetOfVertexes, secondSetOfVertexes);
            if (result == int.MaxValue)
            {
                Console.WriteLine("No way!");
            }
            else
            {
                Console.WriteLine("Shortest distance: " + result);
            }
        }

        public static int GetShortestDistance(int[,] adjacencyMatrix, int numberOfVertexes, int[] firstSetOfVertexes, int[] secondSetOfVertexes)
        {
            int[] distanceArray;
            int minimalDistance = int.MaxValue;

            for (int i = 0; i < firstSetOfVertexes.Length; i++) // Перебираем вершины первого множества, используя их как "стартовые"
            {
                distanceArray = DijkstraAlgoritm(adjacencyMatrix, numberOfVertexes, firstSetOfVertexes[i]); // Получаем массив расстояний до стартовой вершины
                for (int j = 0; j < secondSetOfVertexes.Length; j++) // И из него находим кратчайшее расстояние среди вершин второго множества
                {
                    minimalDistance = Math.Min(minimalDistance, distanceArray[secondSetOfVertexes[j]]);
                }
            }

            return minimalDistance;
        }

        public static int[] DijkstraAlgoritm(int[,] adjacencyMatrix, int numberOfVertexes, int startVertex)
        {
            int[] shortestDistanceToStartVertex = new int[numberOfVertexes]; // Создаем массив расстояний от вершины [i] до стартовой вершины
            for (int i = 0; i < numberOfVertexes; i++) // И заполняем его максимальными значениями, указывающими, что расстояние от этой вершины мы еще не определили
            {
                shortestDistanceToStartVertex[i] = int.MaxValue;
            }

            shortestDistanceToStartVertex[startVertex] = 0; // Расстояние от стартовой вершины до самой себя равно 0
            bool[] isVisited = new bool[numberOfVertexes]; // Создаем массив, указывающий, посещали ли мы данную вершину (false - не посещали)

            Console.WriteLine("Shortest distances to start vertex [" + startVertex + "]: ");
            for (int count = 0; count < numberOfVertexes; count++) // Начинаем просматривать все вершины графа
            {
                int m = MinimumDistance(shortestDistanceToStartVertex, isVisited, numberOfVertexes); // Находим вершину с минимальным расстоянием до стартовой вершины
                isVisited[m] = true; // Помечаем ее как посещенную

                for (int n = 0; n < numberOfVertexes; n++)
                {
                    if (!isVisited[n] && adjacencyMatrix[m,n] != 0) // Если мы не посещали вершину n, и она имеет связь с вершиной m (вершина с минимальным расстоянием до стартовой вершины)
                    {
                        if (shortestDistanceToStartVertex[m] != int.MaxValue) // И если минимальное расстояние до стартовой вершины из нее известно
                        {
                            if (shortestDistanceToStartVertex[m] + adjacencyMatrix[m, n] < shortestDistanceToStartVertex[n]) // И если минимальное расстояние до нынешней вершины было до этого больше, чем сейчас (минимальное расстояние от вершины m + пропускная способность ребра между ней и вершиной n)
                            {
                                shortestDistanceToStartVertex[n] = shortestDistanceToStartVertex[m] + adjacencyMatrix[m, n]; // То меняем минимальное расстояние от до ныненей вершины n на новое минимальное
                            }
                        }
                    }
                }

                Console.WriteLine(count + ": " + shortestDistanceToStartVertex[count]);
            }

            Console.WriteLine("========================================");
            return shortestDistanceToStartVertex;
        }

        public static int MinimumDistance(int[] shortestDistanceToStartVertex, bool[] isVisited, int numberOfVertexes)
        {
            int currentMinDistance = int.MaxValue;
            int minDistanceVertex = 0;

            for (int i = 0; i < numberOfVertexes; i++) // Начинаем искать среди всех вершин графа
            {
                if (!isVisited[i] && shortestDistanceToStartVertex[i] <= currentMinDistance) // Если эту вершину мы еще не посещали, и расстояние от нее до стартовой вершины меньше или равно уже известному минимальному расстоянию
                {
                    currentMinDistance = shortestDistanceToStartVertex[i]; // То сохраняем расстояние от нее до стартовой вершины
                    minDistanceVertex = i; // И записываем эту вершину, как вершину с самым минимальным расстоянием до стартовой вершины
                }
            }

            return minDistanceVertex;
        }
    }
}
