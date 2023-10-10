using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestingA1Base.Data;
using UnitTestingA1Base.Models;
using System.Linq;

namespace RecipeUnitTests
{
    [TestClass]
    public class BusinessLogicLayerTests
    {
        private BusinessLogicLayer _initializeBusinessLogic()
        {
            return new BusinessLogicLayer(new AppStorage());
        }

        #region GetRecipesByIngredient
        
        [TestMethod]
        public void GetRecipesByIngredients_ValidId_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 6;  
            int expectedRecipeCount = 2;  
            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredients(ingredientId, null);

            // assert
            Assert.AreEqual(expectedRecipeCount, recipes.Count());
        }

        [TestMethod]
        public void GetRecipesByIngredients_ValidName_ReturnsRecipesWithIngredient()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string ingredientName = "Salmon"; 
            int expectedRecipeCount = 2;  
            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredients(null, ingredientName);

            // assert
            Assert.AreEqual(expectedRecipeCount, recipes.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRecipesByIngredients_NullIdAndName_ThrowsException()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // act
            bll.GetRecipesByIngredients(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetRecipesByIngredients_InvalidId_ThrowsException()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int invalidIngredientId = 9999;  

            // act
            bll.GetRecipesByIngredients(invalidIngredientId, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetRecipesByIngredients_InvalidName_ThrowsException()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string invalidIngredientName = "XYZIngredient";  // A name that doesn't exist in storage.

            // act
            bll.GetRecipesByIngredients(null, invalidIngredientName);
        }

        [TestMethod]
        public void GetRecipesByIngredients_ValidIdAndName_ReturnsRecipes()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int ingredientId = 6;  
            string ingredientName = "Salmon";  
            int expectedRecipeCount = 2;  

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByIngredients(ingredientId, ingredientName);

            // assert
            Assert.AreEqual(expectedRecipeCount, recipes.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetRecipesByIngredients_ConflictingIdAndName_ThrowsException()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int validIngredientId = 6;  
            string conflictingIngredientName = "AnotherIngredient";  

            // act
            bll.GetRecipesByIngredients(validIngredientId, conflictingIngredientName);
        }
        #endregion

        #region GetRecipesByDietary
        [TestMethod]
        public void GetRecipesByDietary_ValidId_ReturnsRecipesWithDietaryRestriction()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int dietaryId = 3;  
            int expectedRecipeCount = 3; 

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDietary(dietaryId, null);

            // assert
            Assert.AreEqual(expectedRecipeCount, recipes.Count());
        }

        [TestMethod]
        public void GetRecipesByDietary_ValidName_ReturnsRecipesWithDietaryRestriction()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string dietaryName = "Vegan";  
            int expectedRecipeCount = 3;  

            // act
            HashSet<Recipe> recipes = bll.GetRecipesByDietary(null, dietaryName);

            // assert
            Assert.AreEqual(expectedRecipeCount, recipes.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRecipesByDietary_NullIdAndName_ThrowsException()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // act
            bll.GetRecipesByDietary(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetRecipesByDietary_InvalidId_ThrowsException()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int invalidDietaryId = 9999;  // Adjust this to an invalid ID.

            // act
            bll.GetRecipesByDietary(invalidDietaryId, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetRecipesByDietary_InvalidName_ThrowsException()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string invalidDietaryName = "XYZDiet";  // A name that doesn't exist in your storage.

            // act
            bll.GetRecipesByDietary(null, invalidDietaryName);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetRecipesByDietary_ConflictingIdAndName_ThrowsException()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int validDietaryId = 3;  // Assuming this is a valid ID.
            string conflictingDietaryName = "AnotherDiet";  // Adjust this based on your dataset.

            // act
            bll.GetRecipesByDietary(validDietaryId, conflictingDietaryName);
        }
        #endregion

        #region GetRecipes
        [TestMethod]
        public void GetRecipes_ValidId_ReturnsRecipes()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 1;

            // act
            HashSet<Recipe> recipes = bll.GetRecipes(recipeId, null);

            // assert
            int expectedResult = 1;
            int actualResult = recipes.Count();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetRecipes_ValidName_ReturnsRecipes()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string recipeName = "Salmon";

            // act
            HashSet<Recipe> recipes = bll.GetRecipes(null, recipeName);

            // assert
            int expectedResult = 2;
            int actualResult = recipes.Count();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetRecipes_InvalidId_ThrowsArgumentOutOfRange()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            int recipeId = 0;

            // act and assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                HashSet<Recipe> recipes = bll.GetRecipes(recipeId, null);
            });
        }

        [TestMethod]
        public void GetRecipes_InvalidName_ThrowsArgumentOutOfRange()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();
            string recipeName = "Invalid Name";

            // act and assert
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            {
                HashSet<Recipe> recipes = bll.GetRecipes(null, recipeName);
            });
        }

        [TestMethod]
        public void GetRecipes_NullParameters_ThrowsArgumentNullException()
        {
            // arrange
            BusinessLogicLayer bll = _initializeBusinessLogic();

            // act and assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                HashSet<Recipe> recipes = bll.GetRecipes(null, null);
            });
        }

        #endregion

    }

}
