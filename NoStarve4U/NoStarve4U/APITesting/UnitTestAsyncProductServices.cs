using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessEntities;
using BusinessServices;
using ClientServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace APITesting
{
    [TestClass]
    public class UnitTestAsyncProductServices
    {
        private AsyncProductServices prServices = new AsyncProductServices();

        [TestMethod]
        public async Task Test_AddProduct()
        {
            ProductEntity product = new ProductEntity();
            product.Name = "siuksle";
            product.Kind = "Vaisius";

            await prServices.AddProductAsync(product);

            //TODO: Add asserts!

     
        }

        [TestMethod]
        public async Task Test_UpdateProduct()
        {
            ProductEntity product = new ProductEntity();

            product.ID = 31;
            product.Name = "Paprika";
            product.Kind = "Darzove";

            await prServices.UpdateProductAsync(product);

            //TODO: Add asserts!
        }

        [TestMethod]
        public async Task Test_DeleteProduct()
        {
          //  ProductEntity product = new ProductEntity();

            int product = 36;

            await prServices.DeleteProductAsync(product);
        }

        [TestMethod]
        public async Task Test_GetProduct()
        {
            ProductEntity product = new ProductEntity();
            product.ID = 1;
            product.Name = "Bananas";
            product.Kind = "Vaisius";

            ProductEntity product2 = new ProductEntity();          

            product2 = await prServices.GetProductAsync("api/product/get?productID=1");         

            Assert.AreEqual(product.ID, product2.ID);
            Assert.AreEqual(product.Name, product2.Name);
            Assert.AreEqual(product.Kind, product2.Kind);
        }    

        [TestMethod]
        public async Task Test_GetProductList()
        {
            List<ProductEntity> productList = new List<ProductEntity>();

            productList = await prServices.GetProductListAsync("api/product/getList");

            //TODO: Add asserts!          
        }
    }
}
