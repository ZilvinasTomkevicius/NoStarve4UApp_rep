using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientServices;
using BusinessEntities;
using Newtonsoft.Json;
namespace Test_app
{
    class Program
    {

       static List<ProductEntity> productList;

        static void Main(string[] args)
        {
            RunAsync().Wait();

            foreach (ProductEntity p in productList)
            {
                Console.WriteLine("{0} {1} {2}", p.ID, p.Name, p.Kind);
            }

            Console.ReadLine();
        }

        static async Task RunAsync()
        {
            AsyncProductServices services = new AsyncProductServices();

            productList = await services.GetProductListAsync();
        }
    }
}
