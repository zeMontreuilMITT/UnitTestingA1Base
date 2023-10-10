using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestingA1Base.Data;
using UnitTestingA1Base.Models;
using System;
using System.Linq;

namespace RecipeUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        private AppStorage _appStorage;
        private BusinessLogicLayer _bll;

        [TestInitialize]
        public void Initialize()
        {
            // Initialize the AppStorage and BusinessLogicLayer for testing
            _appStorage = new AppStorage();
            _bll = new BusinessLogicLayer(_appStorage);
        }

        // Correct Functionality Tests

        [TestMethod]
        public void GetRecipesByIngredient_ShouldReturnMatchingRecipes()
        {
            // Arrange
            var ingredient = new Ingredient { Id = 1, Name = "Test Ingredient" };
            _appStorage.Ingredients.Add(ingredient);

            var recipe = new Recipe { Id = 1, Name = "Test Recipe" };
            var recipeIngredient = new RecipeIngredient { IngredientId = ingredient.Id, RecipeId = recipe.Id };
            _appStorage.Recipes.Add(recipe);
            _appStorage.RecipeIngredients.Add(recipeIngredient);

            // Act
            var recipes = _bll.GetRecipesByIngredient(ingredient.Id, null);

            // Assert
            Assert.AreEqual(1, recipes.Count);
            Assert.IsTrue(recipes.Contains(recipe));
        }

        [TestMethod]
        public void DeleteIngredient_ShouldDeleteIngredient()
        {
            // Arrange
            var ingredient = new Ingredient { Id = 1, Name = "Test Ingredient" };
            _appStorage.Ingredients.Add(ingredient);

            // Act
            bool deleted = _bll.DeleteIngredient(1, null);

            // Assert
            Assert.IsTrue(deleted);
            Assert.AreEqual(0, _appStorage.Ingredients.Count);
        }

        // Incorrect Functionality Tests

        [TestMethod]
        public void GetRecipesByIngredient_ShouldReturnEmptyResultForNonExistentIngredient()
        {
            // Act
            var recipes = _bll.GetRecipesByIngredient(999, null);

            // Assert
            Assert.AreEqual(0, recipes.Count);
        }

        [TestMethod]
        public void DeleteIngredient_ShouldFailForNonExistentIngredient()
        {
            // Act
            bool deleted = _bll.DeleteIngredient(999, null);

            // Assert
            Assert.IsFalse(deleted);
        }

        [TestMethod]
        public void DeleteIngredient_ShouldFailForIngredientUsedInMultipleRecipes()
        {
            // Arrange
            var ingredient = new Ingredient { Id = 1, Name = "Test Ingredient" };
            _appStorage.Ingredients.Add(ingredient);

            var recipe1 = new Recipe { Id = 1, Name = "Recipe 1" };
            var recipe2 = new Recipe { Id = 2, Name = "Recipe 2" };

            var recipeIngredient1 = new RecipeIngredient { IngredientId = ingredient.Id, RecipeId = recipe1.Id };
            var recipeIngredient2 = new RecipeIngredient { IngredientId = ingredient.Id, RecipeId = recipe2.Id };

            _appStorage.Recipes.Add(recipe1);
            _appStorage.Recipes.Add(recipe2);
            _appStorage.RecipeIngredients.Add(recipeIngredient1);
            _appStorage.RecipeIngredients.Add(recipeIngredient2);

            // Act
            bool deleted = _bll.DeleteIngredient(ingredient.Id, null);

            // Assert
            Assert.IsFalse(deleted);
            Assert.AreEqual(1, _appStorage.Ingredients.Count);
        }

        [TestMethod]
        public void GetRecipesByDiet_ShouldReturnRecipesWithDietaryRestriction()
        {
            // Arrange
            var dietaryRestriction = new DietaryRestriction { Id = 1, Name = "Vegan" };
            _appStorage.DietaryRestrictions.Add(dietaryRestriction);

            var ingredient1 = new Ingredient { Id = 1, Name = "Tofu" };
            var ingredient2 = new Ingredient { Id = 2, Name = "Spinach" };

            _appStorage.Ingredients.Add(ingredient1);
            _appStorage.Ingredients.Add(ingredient2);

            var ingredientRestriction1 = new IngredientRestriction { DietaryRestrictionId = dietaryRestriction.Id, IngredientId = ingredient1.Id };
            var ingredientRestriction2 = new IngredientRestriction { DietaryRestrictionId = dietaryRestriction.Id, IngredientId = ingredient2.Id };

            _appStorage.IngredientRestrictions.Add(ingredientRestriction1);
            _appStorage.IngredientRestrictions.Add(ingredientRestriction2);

            var recipe1 = new Recipe { Id = 1, Name = "Vegan Salad" };
            var recipe2 = new Recipe { Id = 2, Name = "Spinach Wrap" };

            var recipeIngredient1 = new RecipeIngredient { RecipeId = recipe1.Id, IngredientId = ingredient1.Id };
            var recipeIngredient2 = new RecipeIngredient { RecipeId = recipe2.Id, IngredientId = ingredient2.Id };

            _appStorage.Recipes.Add(recipe1);
            _appStorage.Recipes.Add(recipe2);
            _appStorage.RecipeIngredients.Add(recipeIngredient1);
            _appStorage.RecipeIngredients.Add(recipeIngredient2);

            // Act
            var recipes = _bll.GetRecipesByDiet("Vegan", 0);

            // Assert
            Assert.AreEqual(2, recipes.Count);
            Assert.IsTrue(recipes.Contains(recipe1));
            Assert.IsTrue(recipes.Contains(recipe2));
        }

        [TestMethod]
        public void GetRecipesByNameOrId_ShouldReturnRecipeById()
        {
            // Arrange
            var recipe = new Recipe { Id = 1, Name = "Test Recipe" };
            _appStorage.Recipes.Add(recipe);

            // Act
            var recipes = _bll.GetRecipesByNameOrId(null, 1);

            // Assert
            Assert.AreEqual(1, recipes.Count);
            Assert.IsTrue(recipes.Contains(recipe));
        }

        [TestMethod]
        public void GetRecipesByNameOrId_ShouldReturnRecipesByName()
        {
            // Arrange
            var recipe1 = new Recipe { Id = 1, Name = "Spaghetti" };
            var recipe2 = new Recipe { Id = 2, Name = "Lasagna" };

            _appStorage.Recipes.Add(recipe1);
            _appStorage.Recipes.Add(recipe2);

            // Act
            var recipes = _bll.GetRecipesByNameOrId("spa", 0);

            // Assert
            Assert.AreEqual(1, recipes.Count);
            Assert.IsTrue(recipes.Contains(recipe1));
        }

        [TestMethod]
        public void DeleteRecipe_ShouldDeleteRecipe()
        {
            // Arrange
            var recipe = new Recipe { Id = 1, Name = "Test Recipe" };
            _appStorage.Recipes.Add(recipe);

            // Act
            bool deleted = _bll.DeleteRecipe(1, null);

            // Assert
            Assert.IsTrue(deleted);
            Assert.AreEqual(0, _appStorage.Recipes.Count);
        }

        [TestMethod]
        public void DeleteRecipe_ShouldDeleteAssociatedRecipeIngredients()
        {
            // Arrange
            var recipe = new Recipe { Id = 1, Name = "Test Recipe" };
            var ingredient = new Ingredient { Id = 1, Name = "Test Ingredient" };
            var recipeIngredient = new RecipeIngredient { RecipeId = recipe.Id, IngredientId = ingredient.Id };

            _appStorage.Recipes.Add(recipe);
            _appStorage.Ingredients.Add(ingredient);
            _appStorage.RecipeIngredients.Add(recipeIngredient);

            // Act
            bool deleted = _bll.DeleteRecipe(1, null);

            // Assert
            Assert.IsTrue(deleted);
            Assert.AreEqual(0, _appStorage.Recipes.Count);
            Assert.AreEqual(0, _appStorage.RecipeIngredients.Count);
        }
    }
}