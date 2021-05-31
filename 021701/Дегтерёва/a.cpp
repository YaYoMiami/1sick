#include <iostream>
#include <fstream>
#include <string>
#include <vector>
#include <set>
#include <utility>
using namespace std;

const int infinity = 1e7;
vector <pair <int, int>> g[1000];
vector <int> dist;
set <pair < int, int>> q;
int length[1000];
double average = 0;

int main()
{
	int n, m, a, b, from, to, len;
	fstream file;
	file.open("graph2-5.txt");			
	file >> n >> m;
	while(!file.eof())
	{
		for (int i = 0; i < m; i++)
		{
			file >> from >> to >> len;
			g[to].push_back(make_pair(from, len));
			g[from].push_back(make_pair(to, len));
		}
	}
	file.close();

	for (a = 0; a < n - 1; a++)		
		for (b = a + 1; b < n; b++)
		{
			for (int i = 0; i < n; i++) length[i] = infinity;
			length[a] = 0;
			q.insert(make_pair(length[a], a));
			while (!q.empty())
			{
				int v = q.begin()->second;
				q.erase(q.begin());
				for (int i = 0; i < g[v].size(); i++)
				{
					int to = g[v][i].first, len = g[v][i].second;
					if (length[v] + len < length[to])
					{
						q.erase(make_pair(length[to], to));
						length[to] = length[v] + len;
						q.insert(make_pair(length[to], to));
						}
				}
			}
			cout << length[b] << " ";
			average += length[b];
			dist.push_back(length[b]);
		}
	int s = dist.size();
	average = average / s;
	cout << "\naverage diameter - " << average;
}