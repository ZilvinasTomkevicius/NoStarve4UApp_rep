using ClassLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
//using static Android.Provider.SyncStateContract;

/*
 * Using WB API Client: https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
 * Async programing: https://msdn.microsoft.com/en-us/library/mt674882.aspx
 * best practise asyncL https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
 */

namespace ClientServices
{
    public class AsyncProductServices
    {
        HttpClient client;

        public AsyncProductServices()
        {
            client = new HttpClient();
          //  client.BaseAddress = new Uri("http://localhost:63421/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.MaxResponseContentBufferSize = 256000;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

      //  private HttpClient client = new HttpClient();

        public async Task AddProductAsync(ProductEntity product)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                                
                response = await client.PostAsync("http://localhost:63421/api/product/add", content);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<ProductEntity> UpdateProductAsync(ProductEntity product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"api/product/{product.ID}", product);
            response.EnsureSuccessStatusCode();

            product = await response.Content.ReadAsAsync<ProductEntity>();
            return product;
        }

        public async Task<HttpStatusCode> DeleteProductAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:63421/");

                HttpResponseMessage response = null;

                response = await client.DeleteAsync($"api/product/{id}");

                return response.StatusCode;              
            }                           
        }

        public async Task<ProductEntity> GetProductAsync()
        {
            var restUrl = "http://localhost:63421/api/product/1";

            var uri = new Uri(string.Format(restUrl, string.Empty));

            ProductEntity product = null;

            HttpResponseMessage response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var str = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<ProductEntity>(str);
            }
            return product;
        }

        public async Task<List<ProductEntity>> GetProductListAsync(string path)
        {
            List<ProductEntity> productList = null;

            HttpResponseMessage response = await client.GetAsync(path);

            if (response.IsSuccessStatusCode)
            {
                productList = await response.Content.ReadAsAsync<List<ProductEntity>>();
            }

            return productList;
        }       
    }
}
