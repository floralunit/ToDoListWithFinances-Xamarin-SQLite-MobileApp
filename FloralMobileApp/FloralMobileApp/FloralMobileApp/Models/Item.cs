using ReactiveUI;

namespace FloralMobileApp.Models
{
    public class Item : ReactiveObject
    {
        public string Id { get; set; }

        public string Title { get; set; }
        public bool IsCompleted
        {
            get => _isCompleted;
            set => this.RaiseAndSetIfChanged(ref _isCompleted, value);
        }

        private bool _isCompleted;
    }
}