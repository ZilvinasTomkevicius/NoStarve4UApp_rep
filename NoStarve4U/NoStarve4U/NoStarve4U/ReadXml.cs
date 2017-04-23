using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Widget;
using Android.OS;
using Products;
using Recipes;
using System.Xml;
using System.Collections;
using Android.Database;
using Android.Views;
using Java.Lang;
using Android.Content.Res;
using System.IO;
using System.Xml.Linq;
using Android.Content;

namespace NoStarve4U
{
   public class ReadXml
    {
        public ReadXml(AssetManager ActivityAssets)
        {
            this.Assets = ActivityAssets;
        }        

        public string ProdContent;
        public string RecContent;

        public AssetManager Assets;

        public int numberOfProducts;

        //method for reading products form xml
        public List<Product> ProductsFromXml()
        {            
            using (StreamReader sr = new StreamReader(this.Assets.Open("products.xml")))
            {
                ProdContent = sr.ReadToEnd();
            }

            List<Product> productsList = new List<Product>();

            XmlDocument productsXml = new XmlDocument();
            productsXml.LoadXml(ProdContent);

            XmlNode root = productsXml.SelectSingleNode("CurrentProducts");
            XmlNodeList nodeList = root.SelectNodes("Product");
            foreach (XmlNode n in nodeList)
            {
                Product p = new Product();
                p.Name = n.SelectSingleNode("Name").InnerText;
                p.Quantity = n.SelectSingleNode("Quantity").InnerText;
                productsList.Add(p);

                numberOfProducts++;
            }
            return productsList;
        }

        //method for getting recipes from xml
        public List<Recipe> RecipesFromXml()
        {
            using (StreamReader sr = new StreamReader(Assets.Open("recipes.xml")))
            {
                RecContent = sr.ReadToEnd();
            }

            List<Recipe> recipeList = new List<Recipe>();
            List<Product> Ingridients = new List<Product>();

            XmlDocument recipesXml = new XmlDocument();
            recipesXml.LoadXml(RecContent);

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

        //method for getting actual recipe
        public Recipe GetRecipeFromXmlByName(string recipeName)
        {
            List<Product> Ingridients = new List<Product>();

            using (StreamReader sr = new StreamReader(Assets.Open("recipes.xml")))
            {
                RecContent = sr.ReadToEnd();
            }

            XmlDocument recipesXml = new XmlDocument();
            recipesXml.LoadXml(RecContent);

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

                if (r.Name.Equals(recipeName))
                {
                    return (r);
                }
            }
            return null; // not found
        }
    }
}