using System.Collections.Generic;
using System.Linq;

namespace RecipeBookApp
{
    public class LengthBasedFilter : IRecipeFilterStrategy
    {
        public IEnumerable<Recipe> Filter(IEnumerable<Recipe> recipes, string? parameter = null)
        {
            return recipes.Where(r => r.Instructions.Length >= 100); // hosszú recept
        }
    }
}
