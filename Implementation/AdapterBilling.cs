using InterfacesDLL;
using System;
using System.Collections.Generic;

namespace Implementation
{
    public class AdapterBilling : IFacturation
    {
        public void GenerateBill(List<Tuple<string, int, double>> products)
        {
            //_adaptee.GenerateBill(products);
        }

        public AdapterBilling()
        {
        }
    }
}
