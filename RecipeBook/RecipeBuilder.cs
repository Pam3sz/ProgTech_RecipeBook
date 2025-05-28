namespace RecipeBookApp
{
    public class RecipeBuilder
    {
        private Recipe _recipe = new Recipe();

        public RecipeBuilder SetName(string name)
        {
            _recipe.Name = name;
            return this;
        }

        public RecipeBuilder SetInstructions(string instructions)
        {
            _recipe.Instructions = instructions;
            return this;
        }

        public Recipe Build()
        {
            return _recipe;
        }
    }
}
