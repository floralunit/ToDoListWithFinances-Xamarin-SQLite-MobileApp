using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using FloralMobileApp.Services;
using FloralMobileApp.Models;
using ReactiveUI;
using Sextant;
using Xamarin.Forms;
using System.Diagnostics;

namespace FloralMobileApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private readonly Services.IMessageService _messageService;
        public ItemDetailViewModel()
        {
            this._messageService = DependencyService.Get<Services.IMessageService>();

            SaveCommand = new Command(OnSave, ValidateSave);

            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
    (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !string.IsNullOrEmpty(Title);
        }
        private async void OnSave()
        {
            if (!string.IsNullOrEmpty(ItemId))
            {
                var oldItem = await DataStore.GetItemAsync(ItemId);
                oldItem.Title = Title;
                _messageService.ShowAsync(oldItem.Title);
                await DataStore.UpdateItemAsync(oldItem);
            }
            else
            {
                Item newItem = new Item()
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = Title,
                    IsCompleted = false,
                };

                await DataStore.AddItemAsync(newItem);
            }

            await Shell.Current.GoToAsync("..");
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        public Command SaveCommand { get; }

        public Command CancelCommand { get; }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
        public bool IsCompleted
        {
            get => isCompleted;
            set => SetProperty(ref isCompleted, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }
        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Title = item.Title;
                IsCompleted = item.IsCompleted;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }
        public string Id { get; set; }
        private string itemId;
        private string title;
        private bool isCompleted;
    }
}