using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Задание 2.3 ог СИ:
/// Определить числовую характеристику графа: средний диаметр.
/// </summary>
namespace mid_diameter
{
    class Graph
    {
        private int V; // кол-во вершин
        private int E; // кол-во ребер
        private List<KeyValuePair<int, int>> list; // список инцидентности

        public double AverageDiameter()
        {
            int count = 0;
            double sum = 0;
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    int temp = FindWay(i, j);
                    if (temp > 0)
                    {
                        
                        count++;
                        sum += temp;
                    }
                }
             }
            Console.WriteLine("kek");
            return sum / count;
        }
        public int FindWay(int source, int destination)
        {
            bool[] visited = new bool[V];
            for (int i = 0; i < visited.Length; i++)
                visited[i] = false;
            int pathLen = int.MaxValue;
            int currentpath = 0;
            FindWayUtility(source, destination, ref visited, ref pathLen, ref currentpath);

            return pathLen == int.MaxValue ? -1 : --pathLen;

        }
        private void FindWayUtility(int source, int destination, ref bool[] visited, ref int pathlen, ref int currentpath)
        {
            visited[source] = true;
            currentpath++;
            if (source == destination)
            {
                if (currentpath < pathlen)
                    pathlen = currentpath;
            }
            else
            {
                for (int i = 0; i < E; i++)
                {
                    if (source == list[i].Key && (!visited[list[i].Value]))
                        FindWayUtility(list[i].Value, destination, ref visited, ref pathlen, ref currentpath);
                }
            }
            currentpath--;
            visited[source] = false;
        }
        public Graph(int verCount, int edgeCount) // конструктор для графа
        {
            this.E = edgeCount;
            this.V = verCount;
            list = new List<KeyValuePair<int, int>>(edgeCount);
        }
        public void AddEdge(int u, int w, int edgeCount)
        {
            // первая компонента - начало(key)
            // вторая компонента - конец(value)
            int key = u;
            int value = w;
            list.Add(new KeyValuePair<int, int>(key, value));
        }
    }
    class Program
    {
        public static void TestingSystem(int test)
        {
            Console.WriteLine($"Test number {test}:");
            StringBuilder FILEPATH = new StringBuilder("test");
            FILEPATH.Append(test);
            FILEPATH.Append(".txt");
            string path = FILEPATH.ToString();
            if (!File.Exists(path))
                File.Create(path).Dispose();
            var array = File.ReadAllLines(path, Encoding.UTF8);
            var VE = array[0].Split(' ');
            int V = int.Parse(VE[0]);
            int E = int.Parse(VE[1]);
            Graph g = new Graph(V, E);
            for (int i = 1; i < array.Length; i++)
            {
                var temp = array[i].Split(' ');
                g.AddEdge(int.Parse(temp[0]) - 1, int.Parse(temp[1]) - 1, int.Parse(temp[2]) - 1);
            }
            Console.WriteLine($"Average diameter for given graph is {g.AverageDiameter()}");
            Console.WriteLine("\n\n\n");
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            TestingSystem(2);

            
            Console.ReadKey(true);
        }
    }
}
