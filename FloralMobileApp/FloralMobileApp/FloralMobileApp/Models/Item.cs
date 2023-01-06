using ReactiveUI;
using SQLite;

namespace FloralMobileApp.Models
{
    [Table("Items")]
    public class Item : ReactiveObject
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsCompleted
        {
            get => _isCompleted;
            set => this.RaiseAndSetIfChanged(ref _isCompleted, value);
        }
        private bool _isCompleted;
        public string Category { get; set; }
    }
}