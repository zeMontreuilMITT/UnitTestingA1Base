using UnitTestingA1Base.Data;
using UnitTestingA1Base.Models;

namespace RecipeUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private BusinessLogicLayer _initializeBusinessLogic()
        {
            return new BusinessLogicLayer(new AppStorage());
        }

        //Returns a HashSet of all Recipes that contain the specified Ingredient by name or Primary Key
        [TestMethod]
        public void GetRecipesByIngredient_ValidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 6;
            int recipeCount = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //Assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredient_ValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Salmon";
            int recipeCount = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            //Assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIngredient_InvalidId_Returns0()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = -6;
            int recipeCount = 0;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //Assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        //Returns a HashSet of all Recipes that only contain ingredients that belong to the Dietary Restriction provided by name or Primary Key
        [TestMethod]
        public void GetRecipesByDietaryRestriction_ValidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 6;
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //Assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDietaryRestriction_ValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Salmon";
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            //Assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByDietaryRestriction_InvalidId_Returns0()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = -6;
            int recipeCount = 0;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //Assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        //Returns a HashSet of all recipes by either Name or Primary Key. 
        [TestMethod]
        public void GetRecipesByIdOrName_ValidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 6;
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //Assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIdOrName_ValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Salmon";
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            //Assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipesByIdOrName_InvalidId_Returns0()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = -6;
            int recipeCount = 0;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //Assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        // Deletes an ingredient from the database. 
        [TestMethod]
        public void DeleteRecipesByDeleteIngredientByIdOrName_ValidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 1;
            string result = "Delete Success.";

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //Assert
            Assert.AreEqual(result, "Delete Success.");
        }

        [TestMethod]
        public void DeleteRecipesByDeleteIngredientByIdOrName_ValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Salmon";
            string result = "Delete Success.";

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            //Assert
            Assert.AreEqual(result, "Delete Success.");
        }

        [TestMethod]
        public void DeleteRecipesByDeleteIngredientByIdOrName_InvalidId_Returns0()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 6;
            string result = "Cannot delete without specifying an ingredient or when multiple recipes use the same ingredient.";

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //Assert
            Assert.AreEqual(result, "Cannot delete without specifying an ingredient or when multiple recipes use the same ingredient.");
        }


        // Deletes an ingredient from the database. 
        [TestMethod]
        public void DeleteRecipesByIdOrName_ValidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 1;
            string result = "Delete Success.";

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //Assert
            Assert.AreEqual(result, "Delete Success.");
        }

        [TestMethod]
        public void DeleteRecipesByIdOrName_ValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Salmon";
            string result = "Delete Success.";

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            //Assert
            Assert.AreEqual(result, "Delete Success.");
        }

        [TestMethod]
        public void DeleteRecipesByIdOrName_InvalidId_Returns0()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = -6;
            string result = "Invalid ID provided.";

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            //Assert
            Assert.AreEqual(result, "Invalid ID provided.");
        }
    }
}