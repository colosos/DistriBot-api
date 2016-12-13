using System;

namespace Interfaces
{
    public interface IFinance
    {
        int ReturnBalance(int CliId, DateTime Date);
    }
}
