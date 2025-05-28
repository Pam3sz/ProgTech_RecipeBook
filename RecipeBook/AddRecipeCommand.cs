using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using NLog;

namespace RecipeBookApp
{
    public class AddRecipeCommand : ICommand
    {
        private readonly ObservableCollection<Recipe> _recipes;
        private readonly Recipe _recipe;

        public AddRecipeCommand(ObservableCollection<Recipe> recipes, Recipe recipe)
        {
            _recipes = recipes;
            _recipe = recipe;
        }

        public void Execute()
        {
            _recipes.Add(_recipe);
            DatabaseHelper.InsertRecipe(_recipe);
            LogManager.GetCurrentClassLogger().Info($"Command: Added recipe '{_recipe.Name}'");
        }
    }
}
