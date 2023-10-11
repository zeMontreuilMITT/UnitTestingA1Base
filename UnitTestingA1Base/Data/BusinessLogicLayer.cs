using System.Net;
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
            Ingredient ingredient = null;
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            if (id != null || name != null)
            {
                if (id != null)
                {
                    ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Id == id);
                }

                if (ingredient == null && name != null)
                {
                    ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Name == name);
                }

                if (ingredient != null)
                {
                    HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients.Where(rI => rI.IngredientId == ingredient.Id).ToHashSet();
                    recipes = _appStorage.Recipes.Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id)).ToHashSet();
                }
            }


            return recipes;
        }


        public HashSet<Recipe> GetRecipesByDietaryRestriction(int? id, string? name)
        {
            Ingredient ingredient = null;
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            if (id != null || name != null)
            {
                if (id != null)
                {
                    ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Id == id);
                }

                if (ingredient == null && name != null)
                {
                    ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Name == name);
                }

                if (ingredient != null)
                {
                    HashSet<IngredientRestriction> ingredientRestrictions = _appStorage.IngredientRestrictions.Where(iR => iR.IngredientId == ingredient.Id).ToHashSet();
                    recipes = _appStorage.Recipes.Where(r => ingredientRestrictions.Any(ir => ir.DietaryRestrictionId == r.Id)).ToHashSet();
                }
            }


            return recipes;
        }

        public HashSet<Recipe> GetRecipesByIdOrName(int? id, string? name)
        {
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            if (id != null || name != null)
            {
                if (id != null)
                {
                    Recipe recipe = _appStorage.Recipes.FirstOrDefault(i => i.Id == id);
                    if (recipe != null)
                    {
                        recipes.Add(recipe);
                    }
                }

                if (name != null)
                {
                    Recipe recipe = _appStorage.Recipes.FirstOrDefault(i => i.Name == name);
                    if (recipe != null)
                    {
                        recipes.Add(recipe);
                    }
                }
            }

            return recipes;
        }

        public string DeleteRecipesByDeleteIngredientByIdOrName(int? id, string? name)
        {
            Ingredient ingredient = null;
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            if (id != null || name != null)
            {
                if (id != null)
                {
                    ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Id == id);
                }

                if (ingredient == null && name != null)
                {
                    ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Name == name);
                }

                if (ingredient != null)
                {
                    HashSet<RecipeIngredient> recipeIngredients = _appStorage.RecipeIngredients.Where(rI => rI.IngredientId == ingredient.Id).ToHashSet();
                    recipes = _appStorage.Recipes.Where(r => recipeIngredients.Any(ri => ri.RecipeId == r.Id)).ToHashSet();

                    if (recipeIngredients.Count == 1)
                    {
                        RecipeIngredient singleRecipeIngredient = recipeIngredients.Single();
                        Recipe correspondingRecipe = _appStorage.Recipes.FirstOrDefault(r => r.Id == singleRecipeIngredient.RecipeId);
                        _appStorage.Recipes.Remove(correspondingRecipe);
                        _appStorage.RecipeIngredients.Remove(singleRecipeIngredient);
                        return "Delete Success.";
                    }
                    else
                    {
                        return "Cannot delete without specifying an ingredient or when multiple recipes use the same ingredient.";
                    }
                }
            }
            return "";
        }


        public string DeleteRecipesByIdOrName(int? id, string? name)
        {
            Recipe recipe = null;
            
            if (id != null || name != null)
            {
                if (id != null)
                {
                    recipe = _appStorage.Recipes.FirstOrDefault(i => i.Id == id);
                }
                if(recipe == null && name != null)
                {
                    recipe = _appStorage.Recipes.FirstOrDefault(i => i.Name == name);
                }
                if (recipe != null)
                {
                    IEnumerable<RecipeIngredient> recipeIngredientsToDelete = _appStorage.RecipeIngredients.Where(ri => ri.RecipeId == recipe.Id);

                    foreach (var recipeIngredient in recipeIngredientsToDelete)
                    {
                        IEnumerable<IngredientRestriction> ingredientRecipesToDelete = _appStorage.IngredientRestrictions
                            .Where(ir => ir.IngredientId == recipeIngredient.IngredientId);

                        _appStorage.IngredientRestrictions.RemoveWhere(ir => ingredientRecipesToDelete.Contains(ir));
                        return "Delete Success.";
                    }

                }
            }

            return "";
        }


    }
}
