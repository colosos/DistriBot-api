using InterfacesDLL;
using System;


namespace Implementation
{
    public class AdapterFinance : IFinance
    {

        public int ReturnBalance(int CliId, DateTime Date)
        {
            // return _adaptee.ReturnBalance(CliId, Date);
            return 100;
        }

        public AdapterFinance()
        {
        }
    }
}
