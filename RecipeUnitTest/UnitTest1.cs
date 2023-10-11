using System.Security.Cryptography.X509Certificates;
using UnitTestingA1Base.Models;
using UnitTestingA1Base.Data;

namespace RecipeUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private BusinessLogicLayer _initializeBusinessLogic()
        {
            return new BusinessLogicLayer(new AppStorage());
        }

        #region GetRecipesByIngredient()
        [TestMethod]
        public void GetRecipesByIngredient_ValidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 6;
            int recipeCount = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, null);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);

        }


        [TestMethod]
        public void GetRecipesByIngredient_ValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Parmesan Cheese";
            int recipeCount = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, ingredientName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);

        }

        [TestMethod]
        public void GetRecipesByIngredient_ValidNameAndId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Parmesan Cheese";
            int ingredientId = 6; ;
            int recipeCount = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(ingredientId, ingredientName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);

        }

        [TestMethod]
        public void GetRecipesByIngredient_ValidNameAndInvalidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Parmesan Cheese";
            int invalidIngredientId = 99; ;
            int recipeCount = 2;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(invalidIngredientId, ingredientName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);

        }


        [TestMethod]
        public void GetRecipesByIngredient_InvalidId_ReturnsNull()
        {
            // Arrange: Create an instance of your business logic layer (BLL).
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // Define an invalid ingredientId that doesn't exist in your data.
            int invalidIngredientId = 999;

            // Act: Call the method with the invalid ingredientId and a null name.
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(invalidIngredientId, null);

            // Assert: Verify that the method returns null.
            Assert.IsNull(recipes);

        }

        [TestMethod]
        public void GetRecipesByIngredient_InvalidName_ReturnsNull()
        {
            // Arrange: Create an instance of your business logic layer (BLL).
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // Define an invalid ingredientId that doesn't exist in your data.
            string invalidIngredientName = "Invalid-Name";

            // Act: 
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, invalidIngredientName);

            // Assert: Verify that the method returns null.
            Assert.IsNull(recipes);

        }

        [TestMethod]
        public void GetRecipesByIngredient_InvalidIdAndInvalidName_ReturnsNull()
        {
            // Arrange: Create an instance of your business logic layer (BLL).
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // Define an invalid ingredientId that doesn't exist in your data.
            int invalidIngredientId = 999;
            string invalidIngredientName = "Invalid-Name";


            // Act: 
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(invalidIngredientId, invalidIngredientName);

            // Assert: Verify that the method returns null.
            Assert.IsNull(recipes);

        }



        #endregion

        #region GetRecipesByDiet()

        [TestMethod]
        public void GetRecipesByDiet_ValidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietId = 1;
            int recipeCount = 3;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietId, null);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);

        }

        [TestMethod]
        public void GetRecipesByDiet_ValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string dietName = "Vegetarian"; // Id = 1
            int recipeCount = 3;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(null, dietName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);

        }

        [TestMethod]
        public void GetRecipesByDiet_ValidNameAndId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string dietName = "Vegetarian";
            int dietId = 1; ;
            int recipeCount = 3;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietId, dietName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);

        }

        [TestMethod]
        public void GetRecipesByDiet_ValidNameAndInvalidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string dietName = "Vegetarian";
            int dietId = 99;
            int recipeCount = 3;

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(dietId, dietName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);

        }


        [TestMethod]
        public void GetRecipesByDiet_InvalidId_ReturnsNull()
        {
            // Arrange: Create an instance of your business logic layer (BLL).
            BusinessLogicLayer bll = _initializeBusinessLogic();

            int invalidDietId = 99;

            // Act: Call the method with the invalid dietId and a null name.
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(invalidDietId, null);

            // Assert: Verify that the method returns null.
            Assert.IsNull(recipes);
        }

        [TestMethod]
        public void GetRecipesByDiet_InvalidName_ReturnsNull()
        {
            // Arrange: Create an instance of your business logic layer (BLL).
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // Define an invalid ingredientId that doesn't exist in your data.
            string invalidDietName = "Invalid-Diet";

            // Act: 
            HashSet<Recipe> recipes = bll.GetRecipesByIngredient(null, invalidDietName);

            // Assert: Verify that the method returns null.
            Assert.IsNull(recipes);
        }

        [TestMethod]
        public void GetRecipesByDiet_InvalidIdAndInvalidName_ReturnsNull()
        {
            // Arrange: Create an instance of your business logic layer (BLL).
            BusinessLogicLayer bll = _initializeBusinessLogic();

            int invalidDietId = 99;
            string invalidDietName = "Invalid-Diet";

            // Act: Call the method with both an invalid dietId and an invalid name.
            HashSet<Recipe> recipes = bll.GetRecipesByDiet(invalidDietId, invalidDietName);

            // Assert: Verify that the method returns null.
            Assert.IsNull(recipes);
        }


        #endregion

        #region GetAllRecipes()

        [TestMethod]
        public void GetRecipes_ValidId_ReturnsRecipes()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 1;
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetAllRecipes(recipeId, null);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipes_ValidPartialName_ReturnsRecipes()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string recipeName = "Spa";
            int recipeCount = 1;

            // act
            HashSet<Recipe> recipes = bll.GetAllRecipes(null, recipeName);

            // assert
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipes_InvalidIdAndValidName_ReturnsRecipes()
        {
            // Arrange: Create an instance of your business logic layer (BLL).
            BusinessLogicLayer bll = _initializeBusinessLogic();

            int invalidRecipeId = 99;
            string recipeName = "Spa";
            int recipeCount = 1;

            // Act: Call the method with both an invalid dietId and an invalid name.
            HashSet<Recipe> recipes = bll.GetAllRecipes(invalidRecipeId, recipeName);

            // Assert: Verify that the method returns null.
            Assert.AreEqual(recipeCount, recipes.Count);
        }

        [TestMethod]
        public void GetRecipes_InvalidIdAndInvalidName_ReturnsNull()
        {
            // Arrange: Create an instance of your business logic layer (BLL).
            BusinessLogicLayer bll = _initializeBusinessLogic();

            int invalidRecipeId = 99;
            string invalidRecipeName = "Invalid-Recipe";

            // Act: Call the method with both an invalid dietId and an invalid name.
            HashSet<Recipe> recipes = bll.GetAllRecipes(invalidRecipeId, invalidRecipeName);

            // Assert: Verify that the method returns null.
            Assert.IsNull(recipes);
        }


        #endregion

        #region CreateRecipeWithIngredients()


        #endregion

        #region DeleteIngredient()
        [TestMethod]
        public void DeleteIngredient_ById_IngredientDeleted()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 4;
            string ingredientName = "Tomatoes";


            Ingredient ingredient = new Ingredient { Id = ingredientId, Name = ingredientName };

            // Act
            Ingredient deletedIngredient = bll.DeleteIngredient(ingredientId, null);

            // Assert
            Assert.IsNotNull(deletedIngredient);
            Assert.AreEqual(ingredientId, deletedIngredient.Id);
        }
       
        [TestMethod]
        public void DeleteIngredient_ByName_IngredientDeleted()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 4;
            string ingredientName = "Tomatoes";
           

            Ingredient ingredient = new Ingredient { Id = ingredientId, Name = ingredientName };
            
            // Act
            Ingredient deletedIngredient = bll.DeleteIngredient(null, ingredientName);

            // Assert
            Assert.IsNotNull(deletedIngredient);
            Assert.AreEqual(ingredientName, deletedIngredient.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteIngredient_InvalidIdAndName_ExceptionThrown()
        {
            // Arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // Act
            bll.DeleteIngredient(null, null);
        }

        #endregion

        #region DeleteRecipe()
        
        [TestMethod]
        public void DeleteRecipe_ById_SuccessfullyDeletesRecipe()
        {
            // Arrange: 
            AppStorage appStorage = new AppStorage(); 
            BusinessLogicLayer bll = new BusinessLogicLayer(appStorage);

            // Add some recipes to the storage
            Recipe recipeToDelete = new Recipe { Id = 99, Name = "Test Recipe" };
            appStorage.Recipes.Add(recipeToDelete);

            int recipeId = 99;

            // Act: 
            bll.DeleteRecipe(recipeId, null);

            // Assert: Check if the recipe is removed from the storage
            Recipe deletedRecipe = appStorage.Recipes.FirstOrDefault(r => r.Id == recipeId);
            Assert.IsNull(deletedRecipe); // Ensure that the recipe is not found
        }

        [TestMethod]
        public void DeleteRecipe_ByName_SuccessfullyDeletesRecipe()
        {
            // Arrange: 
            AppStorage appStorage = new AppStorage();
            BusinessLogicLayer bll = new BusinessLogicLayer(appStorage);


            Recipe recipeToDelete = new Recipe { Id = 99, Name = "Test Recipe" };
            appStorage.Recipes.Add(recipeToDelete);
            string recipeName = "Test Recipe";

            // Act: 
            bll.DeleteRecipe(null, recipeName);

            // Assert: Check if the recipe is removed from the storage
            Recipe deletedRecipe = appStorage.Recipes.FirstOrDefault(r => r.Name == recipeName);
            Assert.IsNull(deletedRecipe); // Ensure that the recipe is not found
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteRecipe_RecipeNotFound_ThrowsException()
        {
            // Arrange: 
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // Act and Assert: Call the DeleteRecipe method with a non-existing recipe
            bll.DeleteRecipe(999, "Non-Existing Recipe");
        }

        [TestMethod]
        public void DeleteRecipe_InvalidInputs_ThrowsException()
        {
            // Arrange:
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // Act and Assert: Call the DeleteRecipe method with invalid inputs
            Assert.ThrowsException<InvalidOperationException>(() => bll.DeleteRecipe(null, null));
        }

        #endregion
    }
}