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
    public class AddEditItemViewModel : BaseViewModel
    {
        #region Properties
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }
        public int ItemId
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
        public int Id { get; set; }
        private int itemId;
        private string content;

        #endregion

        #region Commands 

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        #endregion

        #region Constructors
        public AddEditItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
    (_, __) => SaveCommand.ChangeCanExecute();
        }

        #endregion

        #region Command Handlers

        private bool ValidateSave()
        {
            return !string.IsNullOrEmpty(Content);
            //return true;
        }
        private async void OnSave()
        {
            Item itemNew;
            if (ItemId != 0)
            {
                itemNew = await App.Db.GetItemAsync(ItemId);
                itemNew.Content = Content;
            }
            else
            {
                var item = new Item()
                {
                    Content = Content,
                    IsCompleted = false,
                };
                itemNew = item;
            }

            await App.Db.AddOrUpdateItemAsync(itemNew);
            await MessageService.ShowAsync(itemNew.IsCompleted.ToString());

            await Shell.Current.GoToAsync("..");
        }
        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                var item = await App.Db.GetItemAsync(itemId);
                Id = item.Id;
                Content = item.Content;
                IsCompleted = item.IsCompleted;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        #endregion

    }
}