using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using NLog;

namespace RecipeBookApp
{
    public class DeleteRecipeCommand : ICommand
    {
        private readonly ObservableCollection<Recipe> _recipes;
        private readonly Recipe _recipe;

        public DeleteRecipeCommand(ObservableCollection<Recipe> recipes, Recipe recipe)
        {
            _recipes = recipes;
            _recipe = recipe;
        }

        public void Execute()
        {
            _recipes.Remove(_recipe);
            DatabaseHelper.DeleteRecipe(_recipe.Name);
            LogManager.GetCurrentClassLogger().Info($"Command: Deleted recipe '{_recipe.Name}'");
        }
    }
}
