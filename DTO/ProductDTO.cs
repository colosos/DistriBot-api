using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ProductDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string IdOrigen { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        public string Unit { get; set; }

        public double UnitQuantity { get; set; }
    }
}
