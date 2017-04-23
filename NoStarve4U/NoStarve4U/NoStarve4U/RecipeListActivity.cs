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
using ClassLibrary;

namespace NoStarve4U
{
    [Activity(Label = "Pasirink receptą")]
    public class RecipeListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.RecipeList);

            ReadXml ReadRecipes = new ReadXml(this.Assets);

            List<Recipe> recipeList = ReadRecipes.RecipesFromXml();

            List<Recipe> matchedRecipes = new List<Recipe>();

            foreach (Recipe r in recipeList)
            {
                int numberOfCheckedProducts = SharingObjects.finalCheckedProductList.Count;
                int sk2 = r.Ingridients.Count;

                int matchedIngridients = 0;
                foreach (Product recipeProduct in r.Ingridients)
                {
                    foreach (Product p in SharingObjects.finalCheckedProductList)
                    {
                        if (recipeProduct.Equals(p))
                        {
                            matchedIngridients++;
                        }
                    }
                }
                if (matchedIngridients == r.Ingridients.Count())
                {
                    matchedRecipes.Add(r);
                }
            }

            //List view variable which is needed for old version
            ListView recipeListView;

            //creating a ListView within our products (old veersion)
            recipeListView = FindViewById<ListView>(Resource.Id.listView2);
            recipeListView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemSingleChoice, matchedRecipes);
            recipeListView.ChoiceMode = ChoiceMode.Single;

            Button buttonGoToUoutput = FindViewById<Button>(Resource.Id.buttonGoToOutput);

            buttonGoToUoutput.Click += delegate
            {
                ListView recipesLstView = FindViewById<ListView>(Resource.Id.listView2);
              
                for (int i = 0; i < matchedRecipes.Count ; i++)
                {
                    if (recipesLstView.IsItemChecked(i))
                    {
                        string recipeName = recipesLstView.GetItemAtPosition(i).ToString();

                        SharingObjects.recipeDetails = new Recipe(recipeName);

                        break;
                    }
                }
                    var recipeDetailsActivity = new Intent(this, typeof(RecipeDetailsActivity));
                    StartActivity(recipeDetailsActivity);               
            };
        }
    }
}