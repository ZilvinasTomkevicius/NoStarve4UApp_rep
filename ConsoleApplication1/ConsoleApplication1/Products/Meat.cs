using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Products;

namespace Products
{
    public class Meat: Product
    {
        public Meat()
        {
        }

        public Meat(string _name)
        {
            this.Name = _name;
        }

    }
}
