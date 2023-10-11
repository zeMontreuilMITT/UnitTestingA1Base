using System.ComponentModel;
using System.Xml.Linq;
using UnitTestingA1Base.Models;

namespace UnitTestingA1Base.Data
{
    public class BusinessLogicLayer
    {
        private AppStorage _appStorage;

        public BusinessLogicLayer(AppStorage appStorage)
        {
            _appStorage = appStorage;
        }


        public HashSet<Recipe> GetRecipesByIngredient(int? id, string? name)
        {
            Ingredient ingredient;
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            // Check if both ID and name are provided
            if (id != null && !string.IsNullOrEmpty(name))
            {
                // First, try to find the ingredient using ID
                ingredient = _appStorage.Ingredients.FirstOrDefault(x => x.Id == id);

                if (ingredient != null)
                {
                    HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients
                        .Where(ri => ri.IngredientId == ingredient.Id)
                        .ToHashSet();

                    recipes = _appStorage.Recipes
                        .Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id))
                        .ToHashSet();
                }
                else
                {
                    // If no match is found using ID, try to find the ingredient using name
                    ingredient = _appStorage.Ingredients.FirstOrDefault(x => x.Name == name);

                    if (ingredient != null)
                    {
                        HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients
                            .Where(ri => ri.IngredientId == ingredient.Id)
                            .ToHashSet();

                        recipes = _appStorage.Recipes
                            .Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id))
                            .ToHashSet();
                    }
                    else
                    {
                        // Ingredient with the provided ID or name was not found, return null
                        return null;
                    }
                }
            }
            else if (id != null)
            {
                ingredient = _appStorage.Ingredients.FirstOrDefault(x => x.Id == id);

                if (ingredient != null)
                {
                    HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients
                        .Where(ri => ri.IngredientId == ingredient.Id)
                        .ToHashSet();

                    recipes = _appStorage.Recipes
                        .Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id))
                        .ToHashSet();
                }
                else
                {
                    // Ingredient with the provided ID was not found, return null.
                    return null;
                }
            }
            else if (name != null)
            {
                ingredient = _appStorage.Ingredients.FirstOrDefault(x => x.Name.StartsWith(name));

                if (ingredient != null)
                {
                    HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients
                        .Where(ri => ri.IngredientId == ingredient.Id)
                        .ToHashSet();

                    recipes = _appStorage.Recipes
                        .Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id))
                        .ToHashSet();
                }
                else
                {
                    // Ingredient with the provided name was not found, return null.
                    return null;
                }
            }
            return recipes;
        }

        ///// Returns a HashSet of all Recipes that only contain ingredients that belong 
        /// to the Dietary Restriction provided by name or Primary Key
        public HashSet<Recipe> GetRecipesByDiet(int? id, string? name)
        {
            HashSet<Ingredient> ingredients = new HashSet<Ingredient>();
            DietaryRestriction dietaryRestriction;
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            // Check if both ID and name are provided
            if (id != null && !string.IsNullOrEmpty(name))
            {
                // First, try to find dietary restriction using ID
                dietaryRestriction = _appStorage.DietaryRestrictions.FirstOrDefault(x => x.Id == id);

                if (dietaryRestriction != null)
                {
                    // Get list of all ingredients that belong to that dietary restriction
                    HashSet<IngredientRestriction> ingredientRestriction = _appStorage.IngredientRestrictions
                        .Where(ir => ir.DietaryRestrictionId == dietaryRestriction.Id)
                        .ToHashSet();

                    // List of recipes-ingredients that contains the ingredients with that dietary restriction
                    HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients
                        .Where(ri => ingredientRestriction.Any(ir => ir.IngredientId == ri.IngredientId))
                        .ToHashSet();

                    // Get the recipes
                    recipes = _appStorage.Recipes
                        .Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id))
                        .ToHashSet();
                }
                else
                {
                    // If no match is found using ID, try to find dietary restriction using name
                    dietaryRestriction = _appStorage.DietaryRestrictions.FirstOrDefault(x => x.Name.StartsWith(name));

                    if (dietaryRestriction != null)
                    {
                        // Get list of all ingredients that belong to that dietary restriction
                        HashSet<IngredientRestriction> ingredientRestriction = _appStorage.IngredientRestrictions
                            .Where(ir => ir.DietaryRestrictionId == dietaryRestriction.Id)
                            .ToHashSet();

                        // List of recipes-ingredients that contains the ingredients with that dietary restriction
                        HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients
                            .Where(ri => ingredientRestriction.Any(ir => ir.IngredientId == ri.IngredientId))
                            .ToHashSet();

                        // Get the recipes
                        recipes = _appStorage.Recipes
                            .Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id))
                            .ToHashSet();
                    }
                    else
                    {
                        // Dietary Restriction with the provided ID or name was not found, return null
                        return null;
                    }
                }
            }
            else if (id != null)
            {
                //  Find dietary restriction
                dietaryRestriction = _appStorage.DietaryRestrictions.FirstOrDefault(x => x.Id == id);

                if (dietaryRestriction != null)
                {
                    // Get list of all ingredients that belongs to that dietary restriction
                    HashSet<IngredientRestriction> ingredientRestriction = _appStorage.IngredientRestrictions
                        .Where(ir => ir.DietaryRestrictionId == dietaryRestriction.Id)
                        .ToHashSet();


                    // List of recipes-ingredients that contains the ingredients with that dietary restriction
                    HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients
                    .Where(ri => ingredientRestriction.Any(ir => ir.IngredientId == ri.IngredientId))
                    .ToHashSet();

                    // Get the recipes
                    recipes = _appStorage.Recipes
                       .Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id))
                       .ToHashSet();


                    /*
                    // Join RecipeIngredients, Ingredients, and IngredientRestrictions to find matching recipes
                    recipes = _appStorage.Recipes
                        .Where(r => _appStorage.RecipeIngredients
                            .Join(_appStorage.Ingredients, ri => ri.IngredientId, i => i.Id, (ri, i) => new { ri, i })
                            .Join(ingredientRestriction, x => x.i.Id, ir => ir.IngredientId, (x, ir) => new { x.ri, ir })
                            .All(x => x.ri.RecipeId == r.Id))
                        .ToHashSet();

                    */

                }
                else
                {
                    // Dietary Restriction with the provided ID was not found, return null.
                    return null;
                }
            }
            else if (name != null)
            {
                //  Find dietary restriction
                dietaryRestriction = _appStorage.DietaryRestrictions.FirstOrDefault(x => x.Name.StartsWith(name));

                if (dietaryRestriction != null)
                {
                    // Get list of all ingredients that belongs to that dietary restriction
                    HashSet<IngredientRestriction> ingredientRestriction = _appStorage.IngredientRestrictions
                        .Where(ir => ir.DietaryRestrictionId == dietaryRestriction.Id)
                        .ToHashSet();


                    // List of recipes-ingredients that contains the ingredients with that dietary restriction
                    HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients
                    .Where(ri => ingredientRestriction.Any(ir => ir.IngredientId == ri.IngredientId))
                    .ToHashSet();

                    // Get the recipes
                    recipes = _appStorage.Recipes
                       .Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id))
                       .ToHashSet();


                    /*
                    // Join RecipeIngredients, Ingredients, and IngredientRestrictions to find matching recipes
                    recipes = _appStorage.Recipes
                        .Where(r => _appStorage.RecipeIngredients
                            .Join(_appStorage.Ingredients, ri => ri.IngredientId, i => i.Id, (ri, i) => new { ri, i })
                            .Join(ingredientRestriction, x => x.i.Id, ir => ir.IngredientId, (x, ir) => new { x.ri, ir })
                            .All(x => x.ri.RecipeId == r.Id))
                        .ToHashSet();

                    */

                }
                else
                {
                    // Dietary Restriction with the provided ID was not found, return null.
                    return null;
                }
            }
            return recipes;
        }

        public HashSet<Recipe> GetAllRecipes(int? id, string? name)
        {
            HashSet<Recipe> recipes = _appStorage.Recipes.ToHashSet();

            if (id != null && !string.IsNullOrEmpty(name))
            {
                // First try to find the recipe by Id
                recipes = recipes.Where(r => r.Id == id).ToHashSet();

                if (recipes.Count == 0)
                {
                    // Try to find recipes using name
                    recipes = _appStorage.Recipes
                        .Where(r => r.Name.StartsWith(name))
                        .ToHashSet();
                }
            }
            else if (id != null)
            {
                recipes = recipes.Where(r => r.Id == id).ToHashSet();
            }
            else if (!string.IsNullOrEmpty(name))
            {
                recipes = recipes
                    .Where(r => r.Name.StartsWith(name))
                    .ToHashSet();
            }

            if (recipes.Count == 0)
            {
                // If no matches are found, return null.
                return null;
            }

            return recipes;
        }

        public Recipe CreateRecipeWithIngredients(RecipeWithIngredients recipeWithIngredients)
        {
            // Check if a Recipe with the same name already exists
            Recipe existingRecipe = _appStorage.Recipes.FirstOrDefault(r => r.Name == recipeWithIngredients.Recipe.Name);
            if (existingRecipe != null)
            {
                // Throw an exception if a Recipe with the same name exists
                throw new InvalidOperationException("Recipe with the same name already exists.");
            }

            // Create Recipe and Ingredients, and add them to the database
            Recipe newRecipe = recipeWithIngredients.Recipe;
            newRecipe.Id = _appStorage.GeneratePrimaryKey();
            _appStorage.Recipes.Add(newRecipe);

            // Create RecipeIngredient objects and add them to the database
            foreach (Ingredient ingredient in recipeWithIngredients.Ingredients)
            {
                Ingredient existingIngredient = _appStorage.Ingredients.FirstOrDefault(i => i.Name == ingredient.Name);
                if (existingIngredient == null)
                {
                    // If the Ingredient does not exist, create it and add it to the database
                    ingredient.Id = _appStorage.GeneratePrimaryKey();
                    _appStorage.Ingredients.Add(ingredient);
                }
                else
                {
                    // Use the existing Ingredient's ID
                    ingredient.Id = existingIngredient.Id;
                }

                // Create a RecipeIngredient object and add it to the database
                RecipeIngredient recipeIngredient = new RecipeIngredient
                {
                    RecipeId = newRecipe.Id,
                    IngredientId = ingredient.Id
                };

                _appStorage.RecipeIngredients.Add(recipeIngredient);
            }

            return newRecipe;
        }

        // Delete an Ingredient
        public Ingredient DeleteIngredient(int? ingredientId, string? ingredientName)
        {
            if (!ingredientId.HasValue && string.IsNullOrEmpty(ingredientName))
            {
                throw new InvalidOperationException("Please provide either ingredientId " +
                    "or ingredientName for deletion.");
            }

            Ingredient ingredientToDelete = null;

            if (ingredientId.HasValue)
            {
                // Try to find the ingredient by ID
                ingredientToDelete = _appStorage.Ingredients.FirstOrDefault(i => i.Id == ingredientId);
            }

            // If the ingredient is not found by ID and ingredientName is provided, try to find it by name
            if (ingredientToDelete == null && !string.IsNullOrEmpty(ingredientName))
            {
                ingredientToDelete = _appStorage.Ingredients.FirstOrDefault(i => i.Name == ingredientName);
            }

            if (ingredientToDelete == null)
            {
                // Ingredient not found
                throw new InvalidOperationException("Ingredient not found.");
            }

            // Check how many recipes use this ingredient
            int recipeCount = _appStorage.RecipeIngredients.Count(ri => ri.IngredientId == ingredientToDelete.Id);

            if (recipeCount == 1)
            {
                // If only one recipe uses the ingredient, delete that recipe and associated RecipeIngredients
                int recipeIdToDelete = _appStorage.RecipeIngredients.First(ri => ri.IngredientId == ingredientToDelete.Id).RecipeId;

                // Store items to remove
                List<RecipeIngredient> itemsToRemove = _appStorage.RecipeIngredients
                    .Where(ri => ri.IngredientId == ingredientToDelete.Id)
                    .ToList();
                foreach (RecipeIngredient recipeIngredient in itemsToRemove)
                {
                    _appStorage.RecipeIngredients.Remove(recipeIngredient);
                }

                // Remove the recipe
                _appStorage.Recipes.Remove(_appStorage.Recipes.First(r => r.Id == recipeIdToDelete));
            }
            else if (recipeCount > 1)
            {
                // If multiple recipes use the ingredient, return a Forbidden response
                throw new InvalidOperationException("Cannot delete ingredient used in multiple recipes.");
            }

            // Delete the ingredient
            _appStorage.Ingredients.Remove(ingredientToDelete);

            // Return the deleted ingredient
            return ingredientToDelete;
        }

        public void DeleteRecipe(int? recipeId, string? recipeName)
        {
            if (!recipeId.HasValue && string.IsNullOrEmpty(recipeName))
            {
                throw new InvalidOperationException("Please provide either recipeId or recipeName for deletion.");
            }

            Recipe recipeToDelete = null;

            if (recipeId.HasValue)
            {
                recipeToDelete = _appStorage.Recipes.FirstOrDefault(r => r.Id == recipeId);
            }

            if (recipeToDelete == null && !string.IsNullOrEmpty(recipeName))
            {
                recipeToDelete = _appStorage.Recipes.FirstOrDefault(r => r.Name == recipeName);
            }

            if (recipeToDelete == null)
            {
                throw new InvalidOperationException("Recipe not found.");
            }

            // Delete associated IngredientRecipe objects
            List<RecipeIngredient> ingredientRecipes = _appStorage.RecipeIngredients
                .Where(ri => ri.RecipeId == recipeToDelete.Id)
                .ToList();

            foreach (RecipeIngredient ingredientRecipe in ingredientRecipes)
            {
                _appStorage.RecipeIngredients.Remove(ingredientRecipe);
            }

            // Delete the recipe
            _appStorage.Recipes.Remove(recipeToDelete);
        }
    }
}

