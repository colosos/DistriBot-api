using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class AdapterFinance : IFinance
    {
        public AdapteeFinance _adaptee = new AdapteeFinance();
        public int ReturnBalance(int CliId, DateTime Date)
        {
            return _adaptee.ReturnBalance(CliId, Date);
        }
    }
}
