using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Products;

namespace Products
{
    public class Fruit: Product
    {

        public Fruit()
        {
        }

        public Fruit(string _name)
        {
            this.Name = _name;
        }
        
    }
}
