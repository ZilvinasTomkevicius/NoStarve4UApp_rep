using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using BusinessEntities;

namespace ClientServices
{
    /*
 * Using WB API Client: https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
 * Async programing: https://msdn.microsoft.com/en-us/library/mt674882.aspx
 * best practise asyncL https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
 */

    public class AsyncRecipeServices
    {
        public AsyncRecipeServices()
        {
            client.BaseAddress = new Uri("http://localhost:63421/");
            // client.
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private HttpClient client = new HttpClient();

        public void ShowProduct(RecipeEntity recipe)
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", recipe.Name, recipe.ID, recipe.Description, recipe.CookingTime);
        }

        public void ShowRecipeList(List<RecipeEntity> recipeList)
        {
            foreach (RecipeEntity r in recipeList)
            {
                Console.WriteLine("{0} {1} {2}", r.ID, r.Name, r.Description, r.CookingTime);
            }
        }

        public async Task<Uri> CreateProductAsync(RecipeEntity product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/recipe/add", product);

            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        public async Task<RecipeEntity> GetProductAsync(string path)
        {
            RecipeEntity product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<RecipeEntity>();
            }
            return product;
        }

        public async Task<List<RecipeEntity>> GetProductListAsync(string path)
        {
            List<RecipeEntity> productList = null;

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                productList = await response.Content.ReadAsAsync<List<RecipeEntity>>();
            }
            return productList;
        }

        public async Task<RecipeEntity> UpdateProductAsync(RecipeEntity product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/recipe/{product.ID}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<RecipeEntity>();
            return product;
        }

        public async Task<HttpStatusCode> DeleteProductAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/recipe/{id}");
            return response.StatusCode;
        }

        public async Task RunAsync()
        {

            try
            {
                // Create a new product

                RecipeEntity recipe;// = new ProductEntity { Name = "Gizmo", Kind = "Duona", ID = 10000 };

                List<RecipeEntity> recipeList;

                //var url = await CreateProductAsync(product);
                // Console.WriteLine($"Created at {url}");

                // Get the product
                //  product = await GetProductAsync("api/product/get?productID=2");//  url.PathAndQuery);
                //ShowProduct(product);

                recipeList = await GetProductListAsync("api/product/getList");

                ShowRecipeList(recipeList);
                /*
                // Update the product
                Console.WriteLine("Updating price...");
                product.Kind = "Skystis";
                await UpdateProductAsync(product);

                // Get the updated product
                product = await GetProductAsync(url.PathAndQuery);
                ShowProduct(product);

                // Delete the product
                var statusCode = await DeleteProductAsync(product.ID);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");
                */


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
