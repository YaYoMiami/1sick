using System;
using System.IO;
using System.Collections.Generic;

namespace _4._16_СИ
{
    class Orgraf
    {
        private List<List<(int, int)>> list;
        private List<int> visit1, visit2;
      
        private void Dfs1(int v)
        {
            visit1[v] = -1;
            for(int i = 0;i < list[v].Count;i++)
            {
                if (list[v][i].Item2 == 0) continue;
                int n = V_num(list[v][i]);
                if(visit1[n]==0)
                Dfs1(n);
            }
            visit1[v] = ((Search_Max(visit1) + 1) > 1) ? (Search_Max(visit1) + 1) : 1;
        }
        private void Dfs2(int v,int k)
        {
            visit2[v] = -1;
            for (int i = 0; i < list[v].Count; i++)
            {
                if (list[v][i].Item2 == 0) continue;
                int n = V_num(list[v][i]);
                if (visit2[n] == 0)
                    Dfs2(n,k);
            }
            visit1[v]=0;
            visit2[v] = k;
        }
        private int V_num((int, int) n)
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (list[i][j] == (n.Item1, 0)) return i;
                }
            }
            return -1;
        }
        private bool Zero_Search(List<int> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i] == 0) return true;
            }
            return false;
        }
        private bool Not_All_Zero(List<int> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                if (l[i] != 0) return true;
            }
            return false;
        }
        private void Invert()
        {
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].Count; j++)
                {
                    if (list[i][j].Item2 == 1) list[i][j] = (list[i][j].Item1, 0);
                    else list[i][j] = (list[i][j].Item1, 1);
                }
            }
        }
        private int Search_Max(List<int> l)
        {
            int max = -1;
            for (int i = 0; i < l.Count; i++)
            {
                if (max < l[i]) max = l[i];
            }
            return max;
        }
        private int Search_Max_Index(List<int> l)
        {
            int max = -1;
            int index = -1;
            for (int i = 0; i < l.Count; i++)
            {
                if (max < l[i])
                {
                    max = l[i];
                    index = i;
                }
            }
            return index;
        }
        private List<(int,int)> Clear(List<(int,int)> l)
        {
            List<(int, int)> tmp = new List<(int, int)>();
            for(int i = 0;i < l.Count;i++)
            {
                bool check = true;
                for(int j = 0;j < l.Count;j++)
                {
                    if (l[i].Item1 == l[j].Item1&&i!=j)
                    {
                        check = false;
                        break;
                    }
                }
                if (check) tmp.Add(l[i]);
            }
            return tmp;
        }
        private List<int> Crossing(int n,int m)
        {
            List<int> res = new List<int>();
            for(int i = 0;i < list[n].Count;i++)
            {
                for(int j = 0;j < list[m].Count;j++)
                {
                    if(list[n][i].Item1==list[m][j].Item1)
                    {
                        res.Add(list[n][i].Item1);
                    }
                }
            }
            return res;
        }
        public Orgraf(StreamReader input)
        {
            int n = Convert.ToInt32(input.ReadLine());
            list = new List<List<(int, int)>>();
            for (int i = 0; i < n; i++)
            {
                List<(int, int)> list1 = new List<(int, int)>();
                string s = input.ReadLine();
                string[] tmp = s.Split(new char[] { ' ' });
                for (int j = 1; j < tmp.Length; j += 2)
                {
                    list1.Add((Convert.ToInt32(tmp[j]), Convert.ToInt32(tmp[j + 1])));
                }
                list.Add(list1);
            }
            visit1 = new List<int>();
            visit2 = new List<int>();
            for (int i = 0;i < n;i++)
            {
                visit1.Add(0);
                visit2.Add(0);
            }

        }
        public void Build_KonGr()
        {
            while (Zero_Search(visit1))
            {
                for (int i = 0; i < visit1.Count; i++)
                {
                    if (visit1[i] == 0)
                    {
                        Dfs1(i);
                        break;
                    }
                }
            }
            Invert();
            int k = 1;
            while (Not_All_Zero(visit1))
            {
                Dfs2(Search_Max_Index(visit1), k);
                k++;
            }
            Invert();
            while(Not_All_Zero(visit2))
            {
                int key = visit2[0];
                List<(int, int)> l = new List<(int, int)>();
                for(int i = 0;i < visit2.Count;i++)
                {
                    if(visit2[i] == key)
                    {
                        for(int j = 0;j < list[i].Count;j++)
                        {
                            l.Add(list[i][j]);
                        }
                    }
                }
                for (int i = visit2.Count-1; i>=0; i--)
                {
                    if (visit2[i] == key)
                    {
                        visit2.RemoveAt(i);
                        list.RemoveAt(i);
                    }
                }
                visit2.Add(0);
                list.Add(Clear(l));
            }

            for(int i = 0;i < list.Count;i++)
            {
                for(int j = i+1;j < list.Count;j++)
                {
                    List<int> tmp = Crossing(i, j);
                    if(tmp.Count > 1)
                    {
                        tmp.RemoveAt(0);
                        for(int i1 = 0;i1 < tmp.Count;i1++)
                        {
                            list[i].Remove((tmp[i1], 0));
                            list[i].Remove((tmp[i1], 1));
                            list[j].Remove((tmp[i1], 0));
                            list[j].Remove((tmp[i1], 1));
                        }
                    }
                }
            }
        }
        public void Print()
        {
            Console.WriteLine(list.Count);
            for(int i = 0;i < list.Count;i++)
            {
                string s = String.Join(' ',list[i]);
                Console.Write(list[i].Count + " ");
                Console.WriteLine(s);
            }    
        }
    }
}
