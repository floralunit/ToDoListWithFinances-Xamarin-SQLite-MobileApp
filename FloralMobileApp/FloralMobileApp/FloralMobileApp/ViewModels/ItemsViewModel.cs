using System;
using System.Collections.ObjectModel;
using FloralMobileApp.Models;
using Xamarin.Forms;
using FloralMobileApp.Views;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;

namespace FloralMobileApp.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<Item> Items { get; }
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }
        public string MenuTitle
        {
            get
            {
                return menuTitle;
            }
            set
            {
                menuTitle = value;
            }
        }
        private string menuTitle;
        private Item _selectedItem;

        #endregion

        #region Commands 

        public Command LoadItemsCommand { get; }
        public Command AddCommand { get; }
        public Command<Item> ViewCommand { get; }
        public Command<Item> DeleteCommand { get; }
        public Command<Item> IsCheckedChangedCommand { get; }

        #endregion

        #region Constructors

        public ItemsViewModel(string menuTitle)
        {
            Title = menuTitle;
            MenuTitle = menuTitle;
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            DeleteCommand = new Command<Item>(OnDeleteItem);
            AddCommand = new Command(OnAddItem);
            ViewCommand = new Command<Item>(OnItemSelected);
            IsCheckedChangedCommand = new Command<Item>(IsCheckedChanged);
        }

        #endregion

        #region Command Handlers

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;
            Title = MenuTitle;

            try
            {
                Items.Clear();
                var items = await App.Db.GetItemsAsync();
                foreach (var item in items.Where(x => x.Category == MenuTitle))
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
        public async Task OnAppearingAsync()
        {
            await App.Db.CreateTableItems();
            IsBusy = true;
            SelectedItem = null;
            await ExecuteLoadItemsCommand();
        }
        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(AddEditItemPage)}?{nameof(AddEditItemViewModel.MenuTitle)}={MenuTitle}");
        }
        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(AddEditItemPage)}?{nameof(AddEditItemViewModel.ItemId)}={item.Id}");
        }
        private async void OnDeleteItem(Item item)
        {
            await App.Db.DeleteItemAsync(item);
            Items.Remove(item);
        }
        private async void IsCheckedChanged(Item item)
        {
            await App.Db.AddOrUpdateItemAsync(item);
        }

        #endregion

    }
}