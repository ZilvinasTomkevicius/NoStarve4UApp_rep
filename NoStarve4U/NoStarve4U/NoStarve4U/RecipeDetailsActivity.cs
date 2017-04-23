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

using Recipes;

namespace NoStarve4U
{
    //ACTIVITY FOR DISPLAYING RECIPE DETAILS

    [Activity(Label = "Recepto aprašymas")]
    public class RecipeDetailsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.RecipeDetails);

            string recipeName = SharingObjects.recipeDetails.Name;

            ReadXml xml = new ReadXml(this.Assets);

            Recipe recipe = xml.GetRecipeFromXmlByName(recipeName);

            string recipeDescription = recipe.Description;

            string recipeCookingTime = recipe.CookingTime;

            TextView nameView = FindViewById<TextView>(Resource.Id.nameView);
            nameView.Text = recipeName;

            TextView descriptionView = FindViewById<TextView>(Resource.Id.desciptionView);
            descriptionView.Text = recipeDescription;

            TextView cookingTimeView = FindViewById<TextView>(Resource.Id.cookingTimeView);
            cookingTimeView.Text = recipeCookingTime;

            ListView productsView = FindViewById<ListView>(Resource.Id.productsView);
            productsView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.TestListItem, recipe.Ingridients);

            Button buttonToMenuActivity = FindViewById<Button>(Resource.Id.buttonToMenuActivity);

            buttonToMenuActivity.Click += delegate
            {
                var menuActivity = new Intent(this, typeof(MenuActivity));
                StartActivity(menuActivity);
            };           
        }
    }
}