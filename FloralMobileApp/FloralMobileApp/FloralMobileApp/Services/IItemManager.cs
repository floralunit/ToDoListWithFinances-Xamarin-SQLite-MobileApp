using System;
using DynamicData;
using DynamicData.Kernel;
using FloralMobileApp.Models;

namespace FloralMobileApp.Services
{
    public interface IItemManager
    {
        IObservable<IChangeSet<Item, string>> ItemChanges { get; }

        Optional<Item> Get(string id);

        void AddOrUpdate(Item item);

        void Remove(Item item);
    }
}