using System.Collections.Generic;

namespace RecipeBookApp
{
    public interface IRecipeFilterStrategy
    {
        IEnumerable<Recipe> Filter(IEnumerable<Recipe> recipes, string? parameter = null);
    }
}
