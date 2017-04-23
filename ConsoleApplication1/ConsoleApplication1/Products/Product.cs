using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Products
{
    public enum MeasurementType {Kilogram, Gramm, Litr, ml};

    public class Product
    {
        public string Name;
        public string Quantity;
        public MeasurementType UnitMeasure;

        public Product()
        {
        }

        public Product(string _name)
        {
            this.Name = _name;
        }

        public bool Equals(Product _product)
        {
            bool result = this.Name == _product.Name;
            
            return (result);
        }
        public void PrintOut()
        {
            Console.WriteLine(this.Name);

        }


    }
}
