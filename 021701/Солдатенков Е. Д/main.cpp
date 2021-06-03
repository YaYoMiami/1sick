#include <iostream>
#include <fstream>
#include <vector>
#include <string>
#include <utility>
using namespace std;
ifstream in("test_1.txt");
int main()
{
	int l = 0;
	vector<vector<vector<int>>> pairs;
	int k = 0;
	int vertex_kol = 0;
	in >> vertex_kol;
	int sum = 0;
	for (int i = 0; i < vertex_kol; i++)
	{
		int kol = 0;
		in >> kol;
		sum += kol;
		vector<vector<int>> temp;
		for (int j = 0; j < kol; j++)
		{
			vector<int> tmp;
			k++;
			tmp.push_back(i + 1);
			int m = 0;
			in >> m;
			tmp.push_back(m);
			if (m == ' ')
			{
				l++;
			}
			temp.push_back(tmp);
		}
		pairs.push_back(temp);
	}
	if (l != sum)
	{
		cout << "--------------------------------------------------------------------" << endl;
		cout << "\tAll edges:" << endl;
		for (int i = 0; i < vertex_kol; i++)
		{
			for (int j = 0; j < pairs[i].size(); j++)
			{
				cout << "{" << pairs[i][j][0] << ", " << pairs[i][j][1] << "} ";
			}
			cout << endl;
		}
		cout << "--------------------------------------------------------------------" << endl;
		bool check = true;
		cout << "\tEdge graph adjacency list:" << endl;
		vector<pair<int, int>> temp;
		for (int i = 0; i < vertex_kol; i++)
		{
			for (int j = 0; j < pairs[i].size(); j++)
			{
				vector<vector<int>> edges;
				vector<int> curr = pairs[i][j];
				for (int h = 0; h < pairs[curr[1] - 1].size(); h++)
				{
					if ((curr[1] == pairs[curr[1] - 1][h][0]) && (curr[0] == pairs[curr[1] - 1][h][1]))
					{
						check = false;
					}
					if (check)
					{
						edges.push_back(pairs[curr[1] - 1][h]);
					}
					check = true;
				}
				pair<int, int> temp_pair_2 = make_pair(curr[1], curr[0]);
				temp.push_back(temp_pair_2);
				int c = 0;
				for (int i = 0; i < temp.size(); i++)
				{
					if (curr[0] == temp[i].first && curr[1] == temp[i].second)
					{
						c++;
					}
				}
				if (c == 0)
				{
					cout << "Edge " << "{" << curr[0] << ", " << curr[1] << "}: ";


				}
				for (int h = 0; h < pairs[curr[0] - 1].size(); h++)
				{
					if (curr != pairs[i][h])
					{
						edges.push_back(pairs[i][h]);
					}
				}
				if (c == 0)
				{
					for (auto x : edges)
					{
						cout << "{" << x[0] << ", " << x[1] << "} ";

					}
					cout << endl;
				}
			}
		}
		cout << "--------------------------------------------------------------------" << endl;
	}
	return 0;
}