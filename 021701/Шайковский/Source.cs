using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Задание 1.17 СИ в неориентированном графе:
/// Определить тип графа: Кактус
/// Кактус - Связный граф, в котором любые два простых цикла имеют не более одной общей вершины.
/// формат входных данных:
/// В первой строке содержатся два числа V и E, где E - кол-во ребер, V - кол-во вершин
/// В следующих k строках (k <= V) последовательно содержатся три числа 
/// u w l, где u, w - вершиные, соединенные ребром l
/// Очевидно, что число l не должно повторяться несколько раз (ребро может соединять только две вершнины)
/// Результат проверки для графа выводится в консоли
/// Если граф является кактусом, выводим "Cactus", иначе "Not cactus"
/// </summary>
namespace Rachetnaya_rabota
{
    class Graph
    {
        private int V; // кол-во вершин
        private List<int>[] list; // список инцидентности
        private int E; // кол-во вершин
        private bool isCactus = true;
        public List<int>[] cyclesPaths;

        private void DfsColoring(int u, int p,  int[] color,  int[] mark,  int[] par, ref int cycleNumber, ref List<int>[] cyclesPath)
        {
            if (color[u] == 2)
            {
                return; // если уже помечена вершина
            }
            // вершина уже была увидена, но не была полностью посещена, т.е цикл существует
            // возвращаемся к родителю чтобы найти полный цикл
            if (color[u] == 1)
            {
                cycleNumber++;
                int cur = p;
                mark[cur] = cycleNumber;
                // находим родителя из которой шел цикл, помечаем все вершины цикла цветом.
                while (cur != u)
                {
                    cur = par[cur];
                    mark[cur] = cycleNumber;
                }
                for (int i = 0; i < mark.Length; i++)
                {
                    if (mark[i] != 0)
                        cyclesPath[cycleNumber - 1].Add(i);
                }
                for (int i = 0; i < mark.Length; i++)
                {
                    mark[i] = 0;
                }
                return;
            }

            par[u] = p;
            color[u] = 1;

            // нужно пройти по вершинам смежным с данной, найти не отмеченную и посетить её
            for (int i = 0; i < list[u].Count; i++) // проходим по всем ребрам, инцидентным данной вершине
            {
                for (int j = 0; j < V; j++) // проходим по всем вершинам
                {
                    if (u == j) // пропускаем данную вершину
                        continue;
                    if (list[j].IndexOf(list[u][i]) != -1 && j != par[u]) // если ребро list[u][i] содержится в списке list[j] и эта вершина не была посещена до этого, делаем dfs
                    {
                        DfsColoring(j, u,  color,  mark,  par, ref cycleNumber, ref cyclesPath);
                    }
                }
            }
            // отмечаем как полностью  посещенную
            color[u] = 2;
        }
        public void FindAllCyclesAndCheckForCactus()
        {
            int[] color = new int[V]; // массив для пометки вершин
            int[] par = new int[V]; // для записи родителя некоторой вершины

            int[] mark = new int[V];

            int cyclenumber = 0;
            
            DfsColoring(0, -1, color, mark,  par, ref cyclenumber, ref cyclesPaths);
            // нашли все присутствующие циклы в графе...
            if (cyclenumber == 0)
            {
                Console.WriteLine("No cycles.");
            }
            for (int i = 0; i < cyclenumber; i++)
            {
                Console.WriteLine($"Cycle number {i + 1}");
                for (int j = 0; j < cyclesPaths[i].Count; j++)
                {
                    Console.Write($"{cyclesPaths[i][j] + 1} ");   
                }
                Console.WriteLine();
            }
            
            for (int i = 0; i < cyclenumber; i++)
            {
                if (!isCactus)
                    break;
                for (int j = i + 1; j < cyclenumber; j++)
                {
                    int countOfSameVert = 0;
                    for (int k = 0; k < cyclesPaths[i].Count; k++) // проверяем первый цикл
                    {
                        if (cyclesPaths[j].Contains(cyclesPaths[i][k])) // если содержит какую-то вершину
                            countOfSameVert++;
                    }
                    if (countOfSameVert >= 2)
                    {
                        isCactus = false;
                        break;
                    }
                }
            }

            if (isCactus)
            {
                Console.WriteLine("Answer: Cactus.");
            }
            else
            {
                Console.WriteLine("Answer: Not a cactus.");
            }

        }
        public Graph(int verCount, int edge) // количество вершин и количество ребер
        {
            this.E = edge;
            this.V = verCount;
            list = new List<int>[verCount];
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = new List<int>(V + E);
            }
            cyclesPaths = new List<int>[V];
            for (int i = 0; i < cyclesPaths.Length; i++)
            {
                cyclesPaths[i] = new List<int>();
            }
        }
        public void AddEdge(int u, int v, int edgeNum) // ребро из вершины u в v, ребро под номером edjeNum
        {
            list[u].Add(edgeNum);
            list[v].Add(edgeNum);
        }
        

    }

    class Program
    {
        static void TestingSystem(int test)
        {
            Console.WriteLine($"Test number {test}:");
            Console.WriteLine("This graph contains following cycles:");
            StringBuilder FILEPATH = new StringBuilder ("test");
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
            g.FindAllCyclesAndCheckForCactus();
            Console.WriteLine("\n\n\n");
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            TestingSystem(1);
            TestingSystem(2);
            TestingSystem(3);
            TestingSystem(4);
            TestingSystem(5);
            
        }
    }
}
