using UnitTestingA1Base.Models;

namespace UnitTestingA1Base.Data
{
    public class BusinessLogicLayer
    {
        private AppStorage _appStorage;

        public BusinessLogicLayer(AppStorage appStorage) {
            _appStorage = appStorage;
        }
        public HashSet<Recipe> GetRecipesByIngredient(int? id, string? name)
        {
            Ingredient ingredient;
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            if (id != null)
            {
                ingredient = _appStorage.Ingredients.First(i => i.Id == id);

                HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients.Where(rI => rI.IngredientId == ingredient.Id).ToHashSet();

                recipes = _appStorage.Recipes.Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id)).ToHashSet();
            }

            return recipes;
        }

        public HashSet<Recipe> GetRecipesByDiet(string name, int id)
        {
            // Find the dietary restriction based on either name or ID
            DietaryRestriction dietaryRestriction = null;
            if (!string.IsNullOrEmpty(name))
            {
                dietaryRestriction = _appStorage.DietaryRestrictions.FirstOrDefault(dr => dr.Name == name);
            }
            else if (id > 0)
            {
                dietaryRestriction = _appStorage.DietaryRestrictions.FirstOrDefault(dr => dr.Id == id);
            }

            if (dietaryRestriction == null)
            {
                // Handle the case when the dietary restriction is not found.
                // You can throw an exception or return an appropriate response.
                // For simplicity, I'll return an empty HashSet here.
                return new HashSet<Recipe>();
            }

            // Find all recipes that contain ingredients satisfying the dietary restriction
            var recipesWithDietaryRestriction = _appStorage.RecipeIngredients
                .Where(ri => _appStorage.IngredientRestrictions
                    .Any(ir => ir.DietaryRestrictionId == dietaryRestriction.Id &&
                               ir.IngredientId == ri.IngredientId))
                .Select(ri => ri.Recipe)
                .ToHashSet();

            return recipesWithDietaryRestriction;
        }

        public HashSet<Recipe> GetRecipesByNameOrId(string name, int id)
        {
            // If an ID is provided, search for the recipe by ID
            if (id > 0)
            {
                var recipeById = _appStorage.Recipes.FirstOrDefault(recipe => recipe.Id == id);
                if (recipeById != null)
                {
                    var result = new HashSet<Recipe> { recipeById };
                    return result;
                }
            }

            // If a name is provided, search for recipes by name (case-insensitive)
            if (!string.IsNullOrEmpty(name))
            {
                var recipesByName = _appStorage.Recipes
                    .Where(recipe => recipe.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToHashSet();

                return recipesByName;
            }

            // If neither ID nor name is provided or no matching recipe is found, return an empty HashSet
            return new HashSet<Recipe>();
        }

        public HashSet<Ingredient> GetIngredientsByNameOrId(string name, int id)
        {
            // If an ID is provided, search for the ingredient by ID
            if (id > 0)
            {
                var ingredientById = _appStorage.Ingredients.FirstOrDefault(ingredient => ingredient.Id == id);
                if (ingredientById != null)
                {
                    var result = new HashSet<Ingredient> { ingredientById };
                    return result;
                }
            }

            // If a name is provided, search for ingredients by name (case-insensitive)
            if (!string.IsNullOrEmpty(name))
            {
                var ingredientsByName = _appStorage.Ingredients
                    .Where(ingredient => ingredient.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                    .ToHashSet();

                return ingredientsByName;
            }

            // If neither ID nor name is provided or no matching ingredient is found, return an empty HashSet
            return new HashSet<Ingredient>();
        }

        public bool DeleteIngredient(int id, string name)
        {
            // Find the ingredient by ID or name
            Ingredient ingredientToDelete = _appStorage.Ingredients
                .FirstOrDefault(i => i.Id == id || i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (ingredientToDelete == null)
            {
                return false; // Ingredient not found
            }

            // Check how many recipes use this ingredient
            int recipeCount = _appStorage.RecipeIngredients.Count(ri => ri.IngredientId == ingredientToDelete.Id);

            if (recipeCount > 1)
            {
                return false; // Ingredient is used in multiple recipes and cannot be deleted
            }

            // Delete the ingredient
            _appStorage.Ingredients.Remove(ingredientToDelete);
            return true; // Ingredient deleted successfully
        }


        public bool DeleteRecipe(int id, string name)
        {
            // Find the recipe by ID or name
            Recipe recipeToDelete = null;

            if (id > 0)
            {
                recipeToDelete = _appStorage.Recipes.FirstOrDefault(recipe => recipe.Id == id);
            }
            else if (!string.IsNullOrEmpty(name))
            {
                recipeToDelete = _appStorage.Recipes.FirstOrDefault(recipe => recipe.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            }

            if (recipeToDelete == null)
            {
                return false; // Recipe not found
            }

            // Delete associated RecipeIngredient entries
            var recipeIngredientsToDelete = _appStorage.RecipeIngredients.Where(ri => ri.RecipeId == recipeToDelete.Id).ToList();
            foreach (var recipeIngredient in recipeIngredientsToDelete)
            {
                _appStorage.RecipeIngredients.Remove(recipeIngredient);
            }

            // Delete the recipe
            _appStorage.Recipes.Remove(recipeToDelete);

            return true; // Recipe deleted successfully
        }


    }
}
