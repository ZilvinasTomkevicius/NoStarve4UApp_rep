using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Products;

namespace Recipes
{
    public class Recipe
    {
        public string Name; // recepto pavadinimas

        public List<Product> Ingridients = new List<Product>();// produktu sarashas

        public string Description; // recepto aprasas
        public string CookingTime;

        // reserved for reading from xml
        public string ProductForRecipeName;
        public string ProductForRecipeQuantity;

        //TODO: 
        public Recipe()
        {
        }

        public Recipe(string _name, string _description, string _cookingTime)
        {
            this.Name = _name;
            this.CookingTime = _cookingTime;
            this.Description = _description;

        }

        public void PrintOut()
        {
            Console.WriteLine("Recipe Name: {0}", this.Name);
            Console.WriteLine("Description: {0}", this.Description);
            Console.WriteLine("Cooking time: {0} mins", this.CookingTime);
            Console.WriteLine("Needed Products: ");
            foreach (Product p in this.Ingridients)
            {
                p.PrintOut();
            }
            Console.WriteLine();
        }
        
        }

    }

