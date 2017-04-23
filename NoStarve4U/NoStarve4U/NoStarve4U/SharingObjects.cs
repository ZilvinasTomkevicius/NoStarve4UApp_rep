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

using Products;
using Recipes;

namespace NoStarve4U
{
    public static class SharingObjects
    {
        public static List<Product> finalCheckedProductList = new List<Product>();

        public static Recipe recipeDetails;
    }
}