using System;
using System.Collections.Generic;

namespace InterfacesDLL
{
    public interface IFacturation
    {
        void GenerateBill(List<Tuple<string,int,double>> products);
    }
}
