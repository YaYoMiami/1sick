#include <iostream>
#include <fstream>
#include<string>

using namespace std;
int parent[20]; //массив предков для DSU
int size_set[20]; //размер дерева для DSU
int matrix[20][20];// матрица смежности графа

struct edge //структура для представления ребра
{
    int ver1;
    int ver2;
};

//функции для системы непересекающихся множеств
//*******************************************************//
int find_set(int v)
{
    if (v == parent[v])
        return v;
    return parent[v] = find_set(parent[v]);
}

void union_sets(int x, int y)
{
    x = find_set(x);
    y = find_set(y);
    if (x != y)
    {
        if (size_set[x] < size_set[y])
        {
            int buf;
            buf = x;
            x = y;
            y = buf;
        }
        parent[y] = x;
        size_set[x] += size_set[y];
    }
}
//*******************************************************//

edge* minimal( edge* e)//при последовательном вызове, принимает прошлое ребро и возвращает следующее по возрастанию веса ребро
{
    int min=0;
    int v1, v2,prev;
    v1 = e->ver1;
    v2 = e->ver2;
    prev = matrix[v1][v2];
    for (int i = 0; i < 20; i++)  
    {
        for (int j = 0; j < 20; j++)
        {
            if (matrix[i][j] != 0 && matrix[i][j] >prev )
            {
                    min = matrix[i][j];
            }
        }
    }
    edge* t = new edge;
    t->ver1=e->ver1;
    t->ver2 = e->ver2;
    for (int i = 0; i < 20; i++)
    {
        for (int j = 0; j < 20; j++)
        {
            if (matrix[i][j] != 0 && matrix[i][j] <= min && matrix[i][j] > prev )
            {
                if (i == v1 && j == v2)
                    continue;
                else if (i == v2 && j == v1)
                    continue;
                else
                {
                    min = matrix[i][j];
                    t->ver1 = i;
                    t->ver2 = j;
                }
            }
        }
    }
    return t;
}
void initial(void) //инициализация глобальных переменных
{
    for (int i = 0; i < 20; i++)
    {
        size_set[i] = 1;
        parent[i] = i;
        for (int j = 0; j < 20; j++)
        {
            matrix[i][j] = 0;
        }
    }
}

int main()
{
    initial();
    for (int i = 1;i < 6; i++)
    {
        ifstream file;
        int weight = 0;
        int row = 1;
        int col = 1;
        string str="";
        file.open("Input"+to_string(i)+".txt");
        while (!file.eof())
        {
            file >> weight;
            str = file.get();
            matrix[row][col] = weight;
            if (str == "\n")
            {
                row++;
                col = 1;
            }
            else
                col++;
        }
        file.close();
        ofstream in;
        in.open("Output"+to_string(i)+".txt");
        edge* e = new edge;
        e->ver1 = 0;
        e->ver2 = 0;
        edge* t = new edge;
        do
        {
            e = minimal (e);
            if (find_set(e->ver1) != find_set(e->ver2))
            {
                union_sets(e->ver1, e->ver2);
                in << e->ver1 << " " << e->ver2 << endl;
            }
            t = minimal(e);
        } while (t->ver1 != e->ver1 || t->ver2 != e->ver2);
        in.close();
        initial();

    }
}
