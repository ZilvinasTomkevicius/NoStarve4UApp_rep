using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Products;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Recipes
{
    //CLASS FOR RECIPE ENTITIES AND METHODS

    public class Recipe
    {
        public string Name { get; set; } 

        public override string ToString()
        {
            return Name;
        }

        public List<Product> Ingridients = new List<Product>(); 

        public string Description { get; set; }
        public string CookingTime { get; set; }        
 
        public Recipe()
        {
        }

        public Recipe(string _name, string _description, string _cookingTime)
        {
            this.Name = _name;
            this.CookingTime = _cookingTime;
            this.Description = _description;
        }

        public Recipe(string recipeName)
        {
            this.Name = recipeName;
        }        
    }

}