using SQLite;
using System.IO;
using TaskPlanner.MVVM.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TaskPlanner.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService()
        {
            string databasePath = Path.Combine(FileSystem.AppDataDirectory, "TaskPlanner.db");
            _database = new SQLiteAsyncConnection(databasePath);
            InitializeTablesAsync().ConfigureAwait(false); //returns an object used to await this task (blocking func)
        }

        private async Task InitializeTablesAsync()
        {
            await _database.CreateTableAsync<Category>();
            await _database.CreateTableAsync<MyTask>();
        }

        //category CRUD operations
        public Task<List<Category>> GetCategoriesAsync() => _database.Table<Category>().ToListAsync();

        public Task<int> SaveCategoryAsync(Category category) => _database.InsertAsync(category);

        public Task<int> UpdateCategoryAsync(Category category) => _database.UpdateAsync(category);

        public Task<int> DeleteCategoryAsync(Category category) => _database.DeleteAsync(category);

        //task CRUD operations
        public Task<List<MyTask>> GetTasksAsync() => _database.Table<MyTask>().ToListAsync();

        public Task<int> SaveTaskAsync(MyTask task) => _database.InsertAsync(task);

        public Task<int> UpdateTaskAsync(MyTask task) => _database.UpdateAsync(task);

        public Task<int> DeleteTaskAsync(MyTask task) => _database.DeleteAsync(task);
    }
}
