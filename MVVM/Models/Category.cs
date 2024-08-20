using PropertyChanged;
using SQLite;

namespace TaskPlanner.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; } //primary key
        public string CategoryName { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        [Ignore]
        public int PendingTasks { get; set; }
        [Ignore]
        public int TotalTasks { get; set; }
        [Ignore]
        public int CompletedTasks { get; set; }
        [Ignore]
        public string BothTasks { get; set; } = string.Empty;
        [Ignore]
        public float Percentage { get; set; }
        [Ignore]
        public bool IsSelected { get; set; }
    }
}
