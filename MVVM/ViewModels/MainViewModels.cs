using PropertyChanged;
using System.Collections.ObjectModel;
using TaskPlanner.MVVM.Models;
using TaskPlanner.Services;

namespace TaskPlanner.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModels
    {
        public DatabaseService DatabaseService { get; set; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<MyTask> Tasks { get; set; }

        public MainViewModels(DatabaseService databaseService)
        {
            DatabaseService = databaseService;
            Categories = new ObservableCollection<Category>(); //initialize the collection
            Tasks = new ObservableCollection<MyTask>(); //initialize the collection
            Tasks.CollectionChanged += Tasks_CollectionChanged; //update data when tasks change
            LoadData(); //load initial data from the database
        }

        private void Tasks_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateData(); //update data whenever the task collection changes
        }

        private async void LoadData()
        {
            //load categories from the database
            var categoriesFromDb = await DatabaseService.GetCategoriesAsync();
            foreach (var category in categoriesFromDb)
            {
                Categories.Add(category);
            }

            //load tasks from the database
            var tasksFromDb = await DatabaseService.GetTasksAsync();
            foreach (var task in tasksFromDb)
            {
                Tasks.Add(task);
            }

            UpdateData(); //update data after loading from the database
        }

        public void UpdateData()
        {
            //update each category with the number of pending tasks and completion percentage
            foreach (var c in Categories)
            {
                var tasks = Tasks.Where(t => t.CategoryID == c.Id);
                var completed = tasks.Where(t => t.Completed).Count();
                var totalTasks = tasks.Count();

                c.PendingTasks = totalTasks - completed;
                c.TotalTasks = totalTasks;
                c.CompletedTasks = completed;
                c.BothTasks = $"{completed}/{totalTasks}";
                c.Percentage = totalTasks == 0 ? 0 : (float)completed / totalTasks;
            }

            //assign the category color to each task
            foreach (var t in Tasks)
            {
                var catColor = Categories.FirstOrDefault(c => c.Id == t.CategoryID)?.Color;
                t.TaskColor = catColor;
            }
        }

        public async Task DeleteTaskAsync(MyTask task)
        {
            if (task == null) return; //check if the task is null before proceeding
            await DatabaseService.DeleteTaskAsync(task);
            Tasks.Remove(task); //remove the task from the collection after deletion
            UpdateData(); //update data after task deletion
        }
    }
}
