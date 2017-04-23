using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using Newtonsoft.Json;
using BusinessServices;
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
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private HttpClient client = new HttpClient();

        public async Task AddRecipeAsync(RecipeEntity recipe)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(recipe);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync("http://localhost:63421/api/recipe/add", content);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<RecipeEntity> UpdateRecipeAsync(RecipeEntity recipe)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/recipe/{recipe.ID}", recipe);
            response.EnsureSuccessStatusCode();

            recipe = await response.Content.ReadAsAsync<RecipeEntity>();
            return recipe;
        }

        public async Task<HttpStatusCode> DeleteRecipeAsync(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"api/recipe/{id}");
            return response.StatusCode;
        }

        public async Task<RecipeEntity> GetRecipeAsync(string path)
        {
            RecipeEntity recipe = null;

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                recipe = JsonConvert.DeserializeObject<RecipeEntity>(str);
            }
            return recipe;
        }

        public async Task<List<RecipeEntity>> GetRecipeListAsync(string path)
        {
            List<RecipeEntity> recipeList = null;

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                recipeList = await response.Content.ReadAsAsync<List<RecipeEntity>>();
            }
            return recipeList;
        }
    }
}
