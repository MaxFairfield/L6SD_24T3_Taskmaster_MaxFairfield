using TaskPlanner.MVVM.ViewModels;
using TaskPlanner.MVVM.Models;
using TaskPlanner.Services;

namespace TaskPlanner.MVVM.Views
{
    public partial class MainView : ContentPage
    {
        private readonly MainViewModels mainViewModels;

        public MainView(DatabaseService databaseService)
        {
            InitializeComponent();
            mainViewModels = new MainViewModels(databaseService);
            BindingContext = mainViewModels;
        }

        private void checkBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            mainViewModels.UpdateData();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var taskView = new NewTaskView(mainViewModels.DatabaseService, mainViewModels.Categories, mainViewModels.Tasks);
            Navigation.PushAsync(taskView);
        }

        private async void DeleteTaskClicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                var task = button.CommandParameter as MyTask;

                if (task != null)
                {
                    bool confirm = await DisplayAlert("Delete Task", $"Are you sure you want to delete the task '{task.TaskName}'?", "Yes", "No");

                    if (confirm)
                    {
                        await mainViewModels.DeleteTaskAsync(task);
                    }
                }
            }
        }

        private async void EditTaskClicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                var task = button.CommandParameter as MyTask;

                if (task != null)
                {
                    string newName = await DisplayPromptAsync("Editing task", "Enter new task name:", initialValue: task.TaskName);
                    if (!string.IsNullOrEmpty(newName))
                    {
                        task.TaskName = newName;
                        await mainViewModels.DatabaseService.UpdateTaskAsync(task);
                        mainViewModels.UpdateData();
                    }
                }
            }
        }
    }
}
