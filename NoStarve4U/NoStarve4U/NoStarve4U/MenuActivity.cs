using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using BusinessEntities;
using ClientServices;

namespace NoStarve4U
{
    [Activity(Label = "Nebūk alkanas", MainLauncher = true)]
    public class MenuActivity : Activity
    {
        protected HttpClient client = new HttpClient();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            /*
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            // http://217.117.27.42:8090/internal/api/product/1
            this.client.BaseAddress = new Uri("http://217.117.27.42:8090/internal/");
            this.client.MaxResponseContentBufferSize = 250000;
            this.client.DefaultRequestHeaders.Accept.Clear();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            */
            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            // button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };

            AsyncProductServices prservices = new AsyncProductServices();

            button.Click += async (sender, args) =>
            {

             //   EditText editID = FindViewById<EditText>(Resource.Id.editTextID);

               // int product_id = Int32.Parse("1");

                var prod = await prservices.GetProductAsync();

              //  EditText editName = FindViewById<EditText>(Resource.Id.editTextName);

                if (prod != null)
                    button.Text = prod.Name;
                else
                    button.Text = "try again...";
            };
        }
        /*
        public async Task<ProductEntity> GetProduct(int id)
        {
            HttpResponseMessage response = null;

            int attempts = 0;

            ProductEntity pr = null;

            do
            {
                try
                {
                    response = await this.client.GetAsync(string.Format("api/product/{0}", id));
                    break;
                }
                catch
                {
                    attempts++;
                }
            } while (attempts < 4);

            if (response != null)
            {
                var str = await response.Content.ReadAsStringAsync();
                pr = JsonConvert.DeserializeObject<ProductEntity>(str);
            }

            return pr;
        }


        public async Task SaveTodoItemAsync(ProductEntity item, bool isNewItem = false)
        {
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems{0}

            using (var client = new HttpClient())
            {
                var uri = new Uri("api/product/save");

                var json = JsonConvert.SerializeObject(item);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                    // client.DeleteAsync (uri);
                    // client.PutAsync (uri);
                }

                if (response.IsSuccessStatusCode)
                {
                    // cuccess
                }
            }
        }
        */

    }
}