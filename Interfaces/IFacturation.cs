using System;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IFacturation
    {
        void GenerateBill(List<Tuple<string,int,double>> products);
    }
}
