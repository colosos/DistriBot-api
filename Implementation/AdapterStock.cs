using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class AdapterStock : IStock
    {
        public AdapteeStock _adaptee { get; set; }

        public int RemainingStock(int PrdId)
        {
           return _adaptee.checkStock(PrdId);
        }

        public AdapterStock()
        {
            _adaptee = new AdapteeStock();
        }
    }
}
