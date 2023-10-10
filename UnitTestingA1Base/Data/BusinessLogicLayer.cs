using Microsoft.Extensions.Logging.Abstractions;
using UnitTestingA1Base.Models;

namespace UnitTestingA1Base.Data
{
    public class BusinessLogicLayer
    {
        private AppStorage _appStorage;

        public BusinessLogicLayer(AppStorage appStorage) {
            _appStorage = appStorage;
        }
        public HashSet<Recipe> GetRecipesByIngredients(int? id, string? name)
        {
            Ingredient ingredient = null;
            HashSet<Recipe> recipes = new HashSet<Recipe>();
            HashSet<RecipeIngredient> recipeIngredients = new HashSet<RecipeIngredient>();

            if (id == null && name == null)
            {
                throw new ArgumentNullException($"Both {nameof(id)} and {nameof(name)} are null.");
            }
            else
            {
                if (id != null && String.IsNullOrEmpty(name))
                {
                    ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Id == id);

                    if (ingredient == null)
                    {
                        throw new ArgumentOutOfRangeException(nameof(ingredient));
                    }
                }
                else if (id == null && !String.IsNullOrEmpty(name))
                {
                    ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Name == name);

                    if (ingredient == null)
                    {
                        throw new ArgumentOutOfRangeException(nameof(ingredient));
                    }
                }
                else
                {
                    ingredient = _appStorage.Ingredients.FirstOrDefault(i => i.Id == id && i.Name == name);

                    if (ingredient == null)
                    {
                        throw new InvalidOperationException(nameof(ingredient));
                    }
                }

                recipeIngredients = _appStorage.RecipeIngredients
                    .Where(rI => rI.IngredientId == ingredient.Id)
                    .ToHashSet();

                if (recipeIngredients == null || recipeIngredients.Count == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(recipeIngredients));
                }

                recipes = _appStorage.Recipes
                    .Where(r => recipeIngredients.Any(rI => rI.RecipeId == r.Id))
                    .ToHashSet();

                if (recipes == null || recipes.Count == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(recipes));
                }
            }

            return recipes;
        }

        public HashSet<Recipe> GetRecipesByDietary(int? id, string? name)
        {
            DietaryRestriction dietaryRestriction = null;
            HashSet<IngredientRestriction> ingredientRestrictions = new HashSet<IngredientRestriction>();
            HashSet<RecipeIngredient> recipeIngredients = new HashSet<RecipeIngredient>();
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            if (id == null && name == null)
            {
                throw new ArgumentNullException("");
            }
            else
            {
                if (id != null && String.IsNullOrEmpty(name))
                {
                    dietaryRestriction = _appStorage.DietaryRestrictions.FirstOrDefault(dR => dR.Id == id);

                    if (dietaryRestriction == null)
                    {
                        throw new ArgumentOutOfRangeException(nameof(dietaryRestriction));
                    }
                }
                else if (id == null & !String.IsNullOrEmpty(name))
                {
                    dietaryRestriction = _appStorage.DietaryRestrictions.FirstOrDefault(dR => dR.Name == name);

                    if (dietaryRestriction == null)
                    {
                        throw new ArgumentOutOfRangeException(nameof(dietaryRestriction));
                    }
                }
                else
                {
                    dietaryRestriction = _appStorage.DietaryRestrictions.FirstOrDefault(dR => dR.Id == id && dR.Name == name);

                    if (dietaryRestriction == null)
                    {
                        throw new InvalidOperationException(nameof(dietaryRestriction));
                    }
                }

                ingredientRestrictions = _appStorage.IngredientRestrictions
                    .Where(iR => iR.DietaryRestrictionId == dietaryRestriction.Id)
                    .ToHashSet();

                if (ingredientRestrictions == null || ingredientRestrictions.Count == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(ingredientRestrictions));
                }

                recipeIngredients = _appStorage.RecipeIngredients
                    .Where(rI => ingredientRestrictions.Any(iR => iR.IngredientId == rI.IngredientId))
                    .ToHashSet();

                if (recipeIngredients == null || recipeIngredients.Count == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(recipeIngredients));
                }

                recipes = _appStorage.Recipes
                    .Where(r => recipeIngredients.Any(rI => rI.RecipeId == r.Id))
                    .ToHashSet();

                if (recipes == null || recipes.Count == 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(recipes));
                }
            }

            return recipes;
        }

        public HashSet<Recipe> GetRecipes(int? id, string? name)
        {
            HashSet<Recipe> recipes = new HashSet<Recipe>();

            if (id == null && string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Both id and name cannot be null.");
            }

            if (id.HasValue)
            {
                var recipeById = _appStorage.Recipes.FirstOrDefault(r => r.Id == id);
                if (recipeById != null)
                {
                    recipes.Add(recipeById);
                }
            }
            else if (!string.IsNullOrEmpty(name))
            {
                // If name length is 3, we search for recipes starting with those letters
                if (name.Length == 3)
                {
                    recipes = _appStorage.Recipes
                        .Where(r => r.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                        .ToHashSet();
                }
                else
                {
                    recipes = _appStorage.Recipes
                        .Where(r => r.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                        .ToHashSet();
                }
            }

            if (recipes.Count == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(recipes), "No recipes found for the provided criteria.");
            }

            return recipes;
        }

    }
}
