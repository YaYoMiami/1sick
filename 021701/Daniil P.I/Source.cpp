#include <iostream>
#include <algorithm>
using namespace std;
int const n = 3; // chislo vershin v grafe
int const f = 2; //chislo reber v grafe
int timer, tin[n], fup[n];
bool used[n];
int mi[n][f] = 
{  
    //intendention matrix
    //           a,b
                {1,0},
                {1,1},
                {0,1}                          
};


bool Checker(int a, int b)
{
    for (int i = 0; i < f; i++)
    {
        if (mi[a][i] == mi[b][i]) return true;
    }
    return false;
}


void dfs(int v, int p = -1) // poisk v glubinu
{
    used[v] = true;
    tin[v] = fup[v] = timer++;
    for (int i = 0; i < n; ++i)
    {
        if (Checker(v,i) == 1)                                
        {
            int to = i;
            if (to == p)  continue;
            if (used[to]) fup[v] = min(fup[v], tin[to]);
            else
            {
                dfs(to, v);
                fup[v] = min(fup[v], fup[to]);
                if (fup[to] > tin[v]) cout << "bridge: (" << v << " , " << to << ")\n";
            }
        }
    }
}


void find_bridges() // poisk mostov
{
    timer = 0;
    for (int i = 0; i < n; ++i)
    {
        used[i] = false;
    }
    for (int i = 0; i < n; ++i)
    {
        if (!used[i]) dfs(i);
    }
}


void main()
{
   
    find_bridges();
       
}
