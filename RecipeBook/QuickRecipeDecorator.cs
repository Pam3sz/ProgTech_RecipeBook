using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBookApp
{
    public class QuickRecipeDecorator : IRecipeDecorator
    {
        private readonly Recipe _recipe;

        public QuickRecipeDecorator(Recipe recipe)
        {
            _recipe = recipe;
        }

        public string GetDisplayName()
        {
            return "⏱️ " + _recipe.Name;
        }
    }
}
