using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using FloralMobileApp.Services;
using FloralMobileApp.Models;
using ReactiveUI;
using Sextant;
using Xamarin.Forms;
using FloralMobileApp.Views;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FloralMobileApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly Services.IMessageService _messageService;
        private Item _selectedItem;
        private bool isCompleted;
        public Command LoadItemsCommand { get; }
        public Command AddCommand { get; }
        public Command<Item> ViewCommand { get; }
        public Command<Item> DeleteCommand { get; }
        public ObservableCollection<Item> Items { get; }

        public ItemsViewModel()
        {
            this._messageService = DependencyService.Get<Services.IMessageService>();
            _messageService.ShowAsync("Hi!!!пидрила");
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            DeleteCommand = new Command<Item>(OnDeleteItem);

            AddCommand = new Command(OnAddItem);

            ViewCommand = new Command<Item>(OnItemSelected);

        }
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public bool IsCompleted
        {
            get => isCompleted;
            set => SetProperty(ref isCompleted, value);
        }
        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(ItemDetailPage));
        }
        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }
        private async void OnDeleteItem(Item item)
        {
            await DataStore.DeleteItemAsync(item.Id);
        }
    }
}