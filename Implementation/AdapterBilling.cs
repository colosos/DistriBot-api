using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class AdapterBilling : IFacturation
    {
        public AdapteeBilling _adaptee = new AdapteeBilling();

        public void GenerateBill(List<Tuple<string, int, double>> products)
        {
            _adaptee.GenerateBill(products);
        }
    }
}
