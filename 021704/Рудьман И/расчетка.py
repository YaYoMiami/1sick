import copy
import sys
class bond:
    def __init__(self,start,end,num):
        self.start=start
        self.end=end
        self.num=num
file = open("input3.txt","r")
spis=file.read().split("\n")
v=int(spis[0])
spis=spis[1:]
spis2=[]
for i in spis:
    k=i.split(" ")
    spis2.append([])
    for j in k:
        spis2[-1].append(j)
bonds=[[bond(int(i[0]),int(i[2]),1) for i in j] for j in spis2]
for i in bonds:
    for j in range(len(i)):
        i.append(bond(i[j].end,i[j].start,0))
visited=[]
way=[]
time=0
def f(start,end,sstart):
    global visited
    global way
    global cbonds
    global time 
    visited.append(start)
    for i in cbonds[start-1]:
        if (not (i.end in visited)) and i.num==1:
            way.append(i)
            break
        if cbonds[start-1].index(i)==len(cbonds[start-1])-1:
            if len(way)==0:
                return
            else:
                del way[-1]
                if (len(way)!=0):
                    f(way[-1].end,end,sstart)
                else:
                    f(sstart,end,sstart)
                return
    if way[-1].end==end:
        time+=1
        for i in way:
            for j in cbonds[i.start-1]:
                if j.end==i.end and j.num==1:
                    j.num=0
                    break
        for i in way:
            for j in cbonds[i.end-1]:
                if j.start==i.end and j.num==0:
                    j.num=1
                    break
        visited=[]
        way=[]
        f(sstart,end,sstart)
        return
    if len(way)!=0:  
        f(way[-1].end,end,sstart)
    else:
        f(start,end,sstart)
min=1000000
cbonds=copy.deepcopy(bonds)
for i in range(v):
    for j in range(v):
        if i!=j:
            f(i+1,j+1,i+1)
            if min>time:
                min=time
            cbonds=copy.deepcopy(bonds)
            visited=[]
            way=[]
            time=0
file=open("output3.txt","w")
file.write(str(min))
file.close()
        