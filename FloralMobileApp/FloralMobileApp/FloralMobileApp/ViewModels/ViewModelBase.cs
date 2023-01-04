using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Sextant;

namespace FloralMobileApp.ViewModels
{
    public abstract class ViewModelBase : ReactiveObject, IDisposable, INavigable
    {
        protected ViewModelBase(IParameterViewStackService viewStackService) => NavigationService = viewStackService;

        public abstract string Id { get; }

        public virtual IObservable<Unit> WhenNavigatedFrom(INavigationParameter parameter) => Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatedTo(INavigationParameter parameter) => Observable.Return(Unit.Default);

        public virtual IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter) => Observable.Return(Unit.Default);

        protected IParameterViewStackService NavigationService { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Subscriptions?.Dispose();
            }
        }

        protected readonly CompositeDisposable Subscriptions = new CompositeDisposable();
    }
}