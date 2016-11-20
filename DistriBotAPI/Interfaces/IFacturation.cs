﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistriBotAPI.Interfaces
{
    public interface IFacturation
    {
        void GenerateBill(List<Tuple<string,int,double>> products);
    }
}
