using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFileManagement.Service.Extra;

public class WokingWithDelegates
{
    public delegate bool Predicate<T1, T2, T3>(T1 item1, T2 item2, T3 item3);

    public bool Method1(int a, int b, int s) => true;
    public bool Method2(int a, int b, int s) => true;
    public bool Method3(int a, int b, int s) => true;
    public bool Method4(int a, int b, int s) => true;
    public bool Method5(int a, int b, int s) => true;

    public  void CheckDelegate()
    {
        Predicate<int, int, int> m1 = new Predicate<int, int, int >(Method1);
        m1 += Method2;
        m1 += Method2;
        m1 += Method3;
        m1 += Method4;
        m1 += Method5;
    }
}
