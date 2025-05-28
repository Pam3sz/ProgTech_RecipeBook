using System;
using System.Collections.ObjectModel;
using System.Windows;
using NLog;

namespace RecipeBookApp
{
    public partial class MainWindow : Window
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();
        private IRecipeFilterStrategy? currentFilterStrategy;

        public MainWindow()
        {
            InitializeComponent();

            DatabaseHelper.InitializeDatabase();
            recipes = DatabaseHelper.LoadRecipes();
            RecipeList.ItemsSource = recipes;

            logger.Info("MainWindow loaded.");
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(RecipeNameBox.Text) && !string.IsNullOrWhiteSpace(RecipeInstructionsBox.Text))
                {
                    var newRecipe = new RecipeBuilder()
                        .SetName(RecipeNameBox.Text)
                        .SetInstructions(RecipeInstructionsBox.Text)
                        .Build();

                    ICommand addCommand = new AddRecipeCommand(recipes, newRecipe);
                    addCommand.Execute();

                    RecipeNameBox.Text = "";
                    RecipeInstructionsBox.Text = "";
                }
                else
                {
                    logger.Warn("Attempted to add recipe with empty fields.");
                    MessageBox.Show("Please enter both name and instructions.");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while adding recipe (Command).");
                MessageBox.Show("An error occurred while adding the recipe.");
            }
        }

        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RecipeList.SelectedItem is Recipe selected)
                {
                    ICommand deleteCommand = new DeleteRecipeCommand(recipes, selected);
                    deleteCommand.Execute();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error while deleting recipe (Command).");
                MessageBox.Show("An error occurred while deleting the recipe.");
            }
        }

        private void RecipeList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RecipeList.SelectedItem is Recipe selected)
            {
                RecipeNameBox.Text = selected.Name;
                RecipeInstructionsBox.Text = selected.Instructions;
            }
        }

        private void FilterByKeyword_Click(object sender, RoutedEventArgs e)
        {
            currentFilterStrategy = new KeywordFilter();
            var filtered = currentFilterStrategy.Filter(recipes, FilterBox.Text);
            RecipeList.ItemsSource = new ObservableCollection<Recipe>(filtered);
            logger.Info("Filtered by keyword.");
        }

        private void FilterByLength_Click(object sender, RoutedEventArgs e)
        {
            currentFilterStrategy = new LengthBasedFilter();
            var filtered = currentFilterStrategy.Filter(recipes);
            RecipeList.ItemsSource = new ObservableCollection<Recipe>(filtered);
            logger.Info("Filtered by length.");
        }

        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            RecipeList.ItemsSource = recipes;
            logger.Info("Filter cleared.");
        }

        private void MarkAsSpicy_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeList.SelectedItem is Recipe selected)
            {
                var decorated = new SpicyRecipeDecorator(selected);
                selected.Name = decorated.GetDisplayName();
                RecipeList.Items.Refresh();
                logger.Info($"Marked recipe as spicy: {selected.Name}");
            }
        }

        private void MarkAsQuick_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeList.SelectedItem is Recipe selected)
            {
                var decorated = new QuickRecipeDecorator(selected);
                selected.Name = decorated.GetDisplayName();
                RecipeList.Items.Refresh();
                logger.Info($"Marked recipe as quick: {selected.Name}");
            }
        }

    }
}
