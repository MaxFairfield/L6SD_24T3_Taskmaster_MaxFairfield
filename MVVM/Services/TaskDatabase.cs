using SQLite;
using TaskPlanner.MVVM.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskPlanner.MVVM.Services
{
    public class TaskDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public TaskDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Category>().Wait();
            _database.CreateTableAsync<MyTask>().Wait();
        }

        //methods for MyTask obj
        public Task<List<MyTask>> GetTasksAsync()
        {
            return _database.Table<MyTask>().ToListAsync();
        }

        public Task<MyTask> GetTaskAsync(int id)
        {
            return _database.Table<MyTask>().Where(t => t.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveTaskAsync(MyTask task)
        {
            if (task.Id != 0)
            {
                return _database.UpdateAsync(task);
            }
            else
            {
                return _database.InsertAsync(task);
            }
        }

        public Task<int> DeleteTaskAsync(MyTask task)
        {
            return _database.DeleteAsync(task);
        }

        //methods for Category obj
        public Task<List<Category>> GetCategoriesAsync()
        {
            return _database.Table<Category>().ToListAsync();
        }

        public Task<Category> GetCategoryAsync(int id)
        {
            return _database.Table<Category>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveCategoryAsync(Category category)
        {
            if (category.Id != 0)
            {
                return _database.UpdateAsync(category);
            }
            else
            {
                return _database.InsertAsync(category);
            }
        }

        public Task<int> DeleteCategoryAsync(Category category)
        {
            return _database.DeleteAsync(category);
        }
    }
}
