using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Products;
using Recipes;
using System.Xml;
using System.Xml.Linq;
using System.Configuration;

namespace Fridge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MANO SALDYTUVE(DUOM BAZEJE) YRA:\n");
            string recipeFP = System.Configuration.ConfigurationManager.AppSettings["xmlRecipePath"];

            Console.WriteLine(recipeFP);
            Console.ReadLine();
            //SPAUSDINAME PRODUKTUS

            List<Product> _productsList = ProductsFromXml();

            foreach(Product p in _productsList)
            {
                p.PrintOut();
            }

            Console.WriteLine("\nRECEPTAI DUOM BAZEJE:\n");

            //SPAUSDINAME RECEPTUS

            List<Recipe> _recipeList = RecipesFromXml();

            foreach (Recipe r in _recipeList)
            {
                r.PrintOut();
            }

            Console.WriteLine("KA GALI PASIGAMINTI:\n");

            //SPAUSDINAME SUMATCHINTUS RECEPTUS

            List<Recipe> _matches = getMatchedRecipes();

            if (_matches.Count() > 0)
            {
                foreach (Recipe r in _matches)
                {
                    r.PrintOut();
                }
            }
            else
            {
                Console.WriteLine("Deja, bet produktu neuztenka :( Tu, ubage - bek i parduotuve arba eik dirbti");
            }
           
            Console.ReadLine();                                                                                
        }

        //METODAS, KURIS NUSKAITO PRODUKTUS IS DUOMENU BAZES

        static List<Product> ProductsFromXml()
        {
            List<Product> productsList = new List<Product>(); 

            XmlDocument productsXml = new XmlDocument(); 
            productsXml.Load("Products.xml");

            XmlNode root = productsXml.SelectSingleNode("CurrentProducts");
            XmlNodeList nodeList = root.SelectNodes("Product");
            foreach (XmlNode n in nodeList) 
            {
                Product p = new Product(); 
                p.Name = n.SelectSingleNode("Name").InnerText; 
                p.Quantity = n.SelectSingleNode("Quantity").InnerText; 
                productsList.Add(p);
            }
            return productsList;
        }

        //METODAS, KURIS NUSKAITO RECEPTUS IS DUOMENU BAZES

        static List<Recipe> RecipesFromXml()
        {
            List<Recipe> recipeList = new List<Recipe>();
            List<Product> Ingridients = new List<Product>();

            string recipeFP = System.Configuration.ConfigurationManager.AppSettings["xmlRecipePath"];

            XmlDocument recipesXml = new XmlDocument();
            recipesXml.Load("recipes.xml");

            XmlNode _root = recipesXml.SelectSingleNode("Recipes");
            XmlNodeList _nodeList = _root.SelectNodes("Recipe");


            foreach (XmlNode _n in _nodeList)
            {
                Recipe r = new Recipe();
                r.Name = _n.SelectSingleNode("RecipeName").InnerText;
                r.Description = _n.SelectSingleNode("Procesas").InnerText;
                r.CookingTime = _n.SelectSingleNode("CookingTime").InnerText;

                XmlNode recipeProducts = _n.SelectSingleNode("RecipeProducts");
                XmlNodeList _products = recipeProducts.SelectNodes("Product");
                foreach (XmlNode _p in _products)
                {
                    Product p = new Product();
                    r.Ingridients.Add(new Product(_p.SelectSingleNode("Name").InnerText)); 
                }
               recipeList.Add(r);

            }
            return recipeList;
        }

        //METODAS, KURIS SUMATCHINA RECEPTUOSE ESANCIUS PRODUKTUS IR RECEPTUS

        static List<Recipe> getMatchedRecipes()
        {
            List<Product> _productsList = ProductsFromXml();

            List<Recipe> _recipeList = RecipesFromXml();

            List<Recipe> matches = new List<Recipe>();

            foreach (Recipe r in _recipeList)
            {
                int matchedIngridients = 0;
                foreach (Product r_p in r.Ingridients)
                {
                    foreach (Product p in _productsList)
                    {
                        if (r_p.Equals(p))
                        {
                            matchedIngridients++;
                        }
                    }
                }
                if (matchedIngridients == r.Ingridients.Count())
                {
                    matches.Add(r);
                }
            }
            return matches;
        }
    }
}
