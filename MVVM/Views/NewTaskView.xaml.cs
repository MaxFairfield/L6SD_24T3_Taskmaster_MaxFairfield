using System.Collections.ObjectModel;
using TaskPlanner.MVVM.Models;
using TaskPlanner.MVVM.ViewModels;
using TaskPlanner.Services;

namespace TaskPlanner.MVVM.Views
{
    public partial class NewTaskView : ContentPage
    {
        private NewTaskViewModel viewModel;

        public NewTaskView(DatabaseService databaseService, ObservableCollection<Category> categories, ObservableCollection<MyTask> tasks)
        {
            InitializeComponent();
            viewModel = new NewTaskViewModel(databaseService, categories, tasks);
            BindingContext = viewModel;
        }

        private async void AddTaskClicked(object sender, EventArgs e)
        {
            var selectedCategory = viewModel.Categories.FirstOrDefault(x => x.IsSelected);

            if (selectedCategory != null)
            {
                var task = new MyTask
                {
                    TaskName = viewModel.Task,
                    CategoryID = selectedCategory.Id,
                };
                await viewModel.AddTaskAsync(task);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Invalid Selection", "You must select a category", "OK");
            }
        }

        private async void AddCategoryClicked(object sender, EventArgs e)
        {
            string category = await DisplayPromptAsync("New Category", "Write the new Category Name", maxLength: 25, keyboard: Keyboard.Text);
            var random = new Random();

            if (!string.IsNullOrEmpty(category))
            {
                int newId = viewModel.Categories.Any() ? viewModel.Categories.Max(x => x.Id) + 1 : 1;
                var newCategory = new Category
                {
                    Id = newId,
                    Color = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)).ToHex(),
                    CategoryName = category,
                };
                await viewModel.AddCategoryAsync(newCategory);
            }
        }

        private async void DeleteCategoryClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                int categoryId = (int)button.CommandParameter;
                var category = viewModel.Categories.FirstOrDefault(c => c.Id == categoryId);

                if (category != null)
                {
                    bool confirm = await DisplayAlert("Delete Category", $"Are you sure you want to delete the category '{category.CategoryName}'?", "Yes", "No");

                    if (confirm)
                    {
                        await viewModel.DeleteCategoryAsync(category);
                    }
                }
            }
        }
    }
}
