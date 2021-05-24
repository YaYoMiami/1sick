#include <iostream> 
#include <fstream>
#include <vector>
using namespace std;

bool test(vector <int>* c, vector<vector<int>>* cycle) {
	int pt = 0;
	for (int i = 0; i < (*cycle).size(); i++) {//Поочерёдно выбираем циклы заданной длины 
		int p = 0;
		for (int j = 0; j < (*cycle)[i].size(); j++) //Поочерёдно выбираем вершину
			if (find((*c).begin(), (*c).end(), (*cycle)[i][j]) != (*c).end())//Ищем такую вершину в предполагаемом графе 
				p++;//Если есть, прибавляем счётчик 
		if (p == (*c).size())//Если счётчик равен размеру искомого цикла, то такой цикл уже есть 
			return false;
	}
	return true;//Иначе его нет
}

void d(int i, int first, int lenght, vector<pair<int, int>> graph, vector <int>* used, vector <int>* c, vector<vector<int>>* cycle) {
	if ((*used)[graph[i].second - 1] == 0 ) {//Если состояние вершины, в которое направлено ребро,0
		for (int j = 0; j < graph.size(); j++) {
			if (graph[j].first == graph[i].second) {//Находим ребро, которое исходит из этой вершины
				(*used)[graph[j].first - 1] = 1;//Меняем состояние вершины, в которое направлено ребро, на 1
				(*c).push_back(graph[j].first);//Записываем данную вершину в предполагаемый цикл 
				d(j, first, lenght, graph, used, c, cycle);//Вызываем функцию для найденного ребра 
			}
		}
	}
	if ((*used)[graph[i].second - 1] == 1 && (*c).size() == lenght && graph[i].second == first && test(c, cycle))//Усли состояние вершины - 1, длина цикла совпадает, first совпадает с вершиной, в которую направлено ребро, для которого последний раз была вызвана функция и такого цикла ещё нет
		(*cycle).push_back((*c));//Записываем данный цикл в список циклов искомой длины 
	(*used)[graph[i].first- 1] = 0;//Меняем состояние вершины,из которой исходит ребро, на 0
	(*c).pop_back();//Удаляем данную вершину из прелполагаемого цикла 
}

void main() {
	int frst, scnd, lenght, N;
	vector<pair<int, int>> graph;
	vector<vector<int>> cycle;
	fstream List;
	List.open("input7.txt");
	List >> lenght >> N;//Во вжодном файле сначала записывается длина скомого цикла,затем колтчество вершин в графе
	vector <int> used(N, 0);
	while(List>>frst>>scnd)
		graph.push_back(make_pair(frst, scnd));//Заполнение графа
	List.close();
	for (int i = 0; i < graph.size(); i++) {//Поочерёдно для каждого ребра графа 
		used[graph[i].first - 1] = 1;//Меняем состояние вершины, из которого исходит ребро, на 1
		vector <int> c;
		c.push_back(graph[i].first);//Записываем эту вершину в предполагаемый цикл 
		d(i,graph[i].first, lenght, graph, &used, &c, &cycle);//Вызываем функцию. В first записываем данную вершину 
		used[graph[i].first - 1] = 0;// Меняем состояние вершины на 0
	}
	ofstream List1;
	List1.open("output.txt");
	if(!cycle.size())
		List1 << "Нет цикла такой длины";
	else {//Вывод
		for (int i = 0; i < cycle.size(); i++) {
			for (int j = 0; j < cycle[i].size(); j++)
				List1 << cycle[i][j] << " ";
			List1 << "\n";
		}
	}
	List1.close();
	return;
}