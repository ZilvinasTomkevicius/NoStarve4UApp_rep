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
using BusinessEntities;
using System.Threading.Tasks;

namespace NoStarve4U
{
    [Activity(Label = "Kokius produktus turi?")]
    public class ProductListActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ProductsList);

            ReadXml ReadProducts = new ReadXml(this.Assets);

            List<Product> productsList = ReadProducts.ProductsFromXml();
            
            ListView productsListView;            
            
            productsListView = FindViewById<ListView>(Resource.Id.listView1);
            productsListView.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemMultipleChoice, productsList);
            productsListView.ChoiceMode = ChoiceMode.Multiple;
            Button button = FindViewById<Button>(Resource.Id.buttonMatches);
            
            
            //BY CLICKING A BUTTON APP SETS ANOTHER LAYOUT AND DISPLAYS A LIST OF PRODUCTS
            button.Click += delegate
            {
                //list view for checked products
                ListView productsLstView = FindViewById<ListView>(Resource.Id.listView1);

                //list for checked products
                List<Product> checkedProductList = new List<Product>();

                //checking products
                for (int i = 0; i <= productsList.Count; i++)
                {
                    if (productsLstView.IsItemChecked(i))
                    {
                        string productName = productsLstView.GetItemAtPosition(i).ToString();

                        Product product = new Product(productName);

                        checkedProductList.Add(product);
                    }
                }

                // remember checked products
                SharingObjects.finalCheckedProductList = checkedProductList;

                //intent to another activity
                var recipeListActivity = new Intent(this, typeof(RecipeListActivity));
                StartActivity(recipeListActivity);
            };
            
        }
    }
}