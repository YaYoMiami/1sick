#include<iostream>
#include<vector>
#include<conio.h>
#include<stdio.h>
#include<math.h>
#include <string>
#include<map>
#include <string.h>
#include <cstdlib>
#include <time.h>
#include<fstream>
#include<sstream>
using namespace std;

void printSolution(int path[], int size);

/* ��������� ������� ��� �������� ����,����� �� ����
������� v  ��������� �� ������� pos
� ������������� �����, �����������
�� ��� ��� (�������� � 'path []')*/
bool isSafe(int v, bool** graph, int path[], int pos)
{
    /* ���������, �������� �� ��� ������� �������
     ������� ����� ����������� �������. */
    if (graph[path[pos - 1]][v] == 0)
        return false;

    for (int i = 0; i < pos; i++)
        if (path[i] == v)
            return false;

    return true;
}


bool hamCycleUtil(bool** graph, int path[], int pos, int size)
{
    /* ������� ������: ���� ��� �������
     �������� � ����������� ���� */
    if (pos == size)
    {
        // � ���� ���� ����� �� ��������� ���������� ������� �� ������ �������
        if (graph[path[pos - 1]][path[0]] == 1)
            return true;
        else
            return false;
    }

    // ��������� ������ ������� � �������� ���������� �������� � ������������� �����. �� �� ���������� 0, ��������� �� �������� 0 � �������� ��������� ����� � hamCycle ()
    for (int v = 1; v < size; v++)
    {
        if (isSafe(v, graph, path, pos))
        {
            path[pos] = v;

            /*���������� ���������, ����� ��������� ���������� ����� ���� */
            if (hamCycleUtil(graph, path, pos + 1, size) == true)
                return true;
            path[pos] = -1;
        }
    }

    //���� �� ���� ������� �� ����� ���� ��������� � ������������ �����, ������������ �� ��� ���, ���������� false
    return false;
}


bool hamCycle(bool** graph, int size)
{
    int* path = new int[size];
    for (int i = 0; i < size; i++)
        path[i] = -1;

    /* �������� ������� 0 ��� ������ ������� ����. */
    path[0] = 0;
    if (hamCycleUtil(graph, path, 1, size) == false)
    {
        cout << "\nSolution does not exist" << endl;
        return false;
    }

    printSolution(path, size);
    return true;
    delete path;
}


void printSolution(int path[], int size)
{
    cout << "Solution Exists:"
        " Following is one Hamiltonian Cycle \n";
    for (int i = 0; i < size; i++)
        cout << path[i] << " ";

    cout << path[0] << " ";
    cout << endl;
}

vector<bool> GetTestingTable(int test)
{
    int n, link;
    string testname;
    testname = "test" + to_string(test) + ".txt";
    ifstream input(testname, ios::app);

    if (!input.is_open())
    {
        cout << "File is not opened" << endl;
    }
    input >> n;

    vector<bool> aa;
    for (int i = 0; i < n; i++)
    {
        for (int j = 0; j < n; j++)
        {
            input >> link;
            aa.push_back(link);
        }
    }
    return aa;
}

void TestRunner(int test)
{
    int size;
    cout << "Test " << test << endl;
    vector<bool> a = GetTestingTable(test);
    size = sqrt(a.size());

    bool** ary;
    ary = new bool* [size];
    for (int i = 0; i < size; i++) {
        ary[i] = new bool[size];
    }

    for (int i = 0; i < size; i++) {
        for (int j = 0; j < size; j++) {
            ary[i][j] = a[i * size + j];
        }
    }
    cout << "Table: " << endl;
    for (int i = 0; i < size; i++) {
        for (int j = 0; j < size; j++) {
            cout << ary[i][j] << " ";
        }
        cout << endl;
    }


    hamCycle(ary, size);

    for (int i = 0; i < size; i++)
        delete[]ary[i];
    delete[]ary;
    cout << endl;
    cout << endl;
}

// Test
int main()
{
    TestRunner(1);
    TestRunner(2);
    TestRunner(3);
    TestRunner(4);
    TestRunner(5);

    return 0;
}
