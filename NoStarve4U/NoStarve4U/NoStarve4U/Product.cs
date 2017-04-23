using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Products
{
    public class Product
    {
        public string Name { get; set; }
        public string Quantity { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public Product()
        {
        }

        public Product(string _name)
        {
            this.Name = _name;
        }
        
        //metodas skirtas produktu sulyginimui veikiant matcher'iui
        public bool Equals(Product _product)
        {
            bool result = this.Name == _product.Name;

            return (result);
        }              
    }
}