using ClientServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessServices;
using BusinessEntities;

namespace APITesting
{
    [TestClass]
    public class UnitTestAsyncRecipeSerivices
    {
        private AsyncRecipeServices reServices = new AsyncRecipeServices();

        [TestMethod]
        public async Task Test_AddRecipe()
        {
            RecipeEntity recipe = new RecipeEntity();

            recipe.Name = "hahahhaa";
            recipe.Description = "pasijuokt garsiai";
            recipe.CookingTime = 3;

            await reServices.AddRecipeAsync(recipe);

            //TODO: Add asserts!
        }

        [TestMethod]
        public async Task Test_UpdateRecipe()
        {
            RecipeEntity recipe = new RecipeEntity();

            recipe.ID = 38;
            recipe.Name = "boboboboo";
            recipe.Description = "paliudet";
            recipe.CookingTime = 50;

            await reServices.UpdateRecipeAsync(recipe);

            //TODO: Add asserts!
        }

        [TestMethod]
        public async Task Test_DeleteRecipe()
        {
            int recipeID = 38;

            await reServices.DeleteRecipeAsync(recipeID);
        }

        [TestMethod]
        public async Task Test_GetRecipe()
        {
            RecipeEntity recipe = new RecipeEntity();

            recipe.ID = 1;
            recipe.Name = "Sumuðtinis su sûriu";
            recipe.Description = "Ant duonos uþtepti sviesto ir uþdëti sûrio.";
            recipe.CookingTime = 3;

            RecipeEntity recipe2 = await reServices.GetRecipeAsync("api/recipe/get?recipeID=1");

            Assert.AreEqual(recipe.ID, recipe2.ID);
            Assert.AreEqual(recipe.Name, recipe2.Name);
            Assert.AreEqual(recipe.Description, recipe2.Description);
            Assert.AreEqual(recipe.CookingTime, recipe2.CookingTime);
        }

        [TestMethod]
        public async Task Test_GetRecipeList()
        {
            List<RecipeEntity> recipeList = new List<RecipeEntity>();

            recipeList = await reServices.GetRecipeListAsync("api/recipe/getList");

            //TODO: Add asserts!
        }
    }
}
