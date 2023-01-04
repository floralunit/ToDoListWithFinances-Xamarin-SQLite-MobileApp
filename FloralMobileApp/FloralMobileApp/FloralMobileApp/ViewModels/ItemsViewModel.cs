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

namespace FloralMobileApp.ViewModels
{
    public class ItemsViewModel : ViewModelBase
    {

        public ItemsViewModel(IParameterViewStackService navigationService, IItemManager itemManager) : base(navigationService)
        {
            //Title = "Browse";
            //Items = new ObservableCollection<Item>();
            //LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            //ItemTapped = new Command<Item>(OnItemSelected);

            //AddItemCommand = new Command(OnAddItem);

            DeleteCommand = ReactiveCommand.Create<Item>(itemManager.Remove);

            itemManager
                 .ItemChanges
                 .Bind(out _items)
                 .DisposeMany()
                 .Subscribe()
                 .DisposeWith(Subscriptions);

            itemManager.AddOrUpdate(new Item(Guid.NewGuid().ToString(), "Family vacation planning"));
            itemManager.AddOrUpdate(new Item(Guid.NewGuid().ToString(), "Buy Christmas Gifts"));
            itemManager.AddOrUpdate(new Item(Guid.NewGuid().ToString(), "Go to the Bank"));
            itemManager.AddOrUpdate(new Item(Guid.NewGuid().ToString(), "Buy Milk"));

            AddCommand = ReactiveCommand.CreateFromObservable(() => NavigationService.PushModal<ItemDetailViewModel>());

            ViewCommand = ReactiveCommand.CreateFromObservable<Item, Unit>((item) =>
            {
                SelectedItem = null;
                return NavigationService.PushModal<ItemDetailViewModel>(new NavigationParameter()
                                {
                                    { NavigationParameterConstants.ItemId , item.Id }
                                });
            });

            this.WhenAnyValue(x => x.SelectedItem)
                .Where(x => x != null)
                .InvokeCommand(ViewCommand)
                .DisposeWith(Subscriptions);
        }

        public ReactiveCommand<Unit, Unit> AddCommand { get; }

        public ReactiveCommand<Item, Unit> ViewCommand { get; }

        public ReactiveCommand<Item, Unit> DeleteCommand { get; }

        public Item SelectedItem
        {
            get => _selectedItem;
            set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }


        public ReadOnlyObservableCollection<Item> Items => _items;

        public override string Id => "Reactive ToDo";

        private readonly ReadOnlyObservableCollection<Item> _items;
        private Item _selectedItem;
    }
}