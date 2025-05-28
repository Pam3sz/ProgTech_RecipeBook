using System.Collections.Generic;
using System.Linq;

namespace RecipeBookApp
{
    public class KeywordFilter : IRecipeFilterStrategy
    {
        public IEnumerable<Recipe> Filter(IEnumerable<Recipe> recipes, string? parameter = null)
        {
            if (string.IsNullOrWhiteSpace(parameter))
                return recipes;

            return recipes.Where(r =>
                r.Name.Contains(parameter, System.StringComparison.OrdinalIgnoreCase) ||
                r.Instructions.Contains(parameter, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
