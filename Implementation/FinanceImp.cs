﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class FinanceImp : IFinance
    {
        public FinanceImp()
        {

        }

        public int ReturnBalance(int CliId, DateTime Date)
        {
            Random random = new Random();
            double randomNumber = random.NextDouble();
            randomNumber *= 2000;
            randomNumber -= 1000;
            return (int)randomNumber;
        }

    }
}
