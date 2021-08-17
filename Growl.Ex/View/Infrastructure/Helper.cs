using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using ReactiveUI;

namespace Growl.Ex.View.Infrastructure {
   public static class Helper {

      public static IObservable<bool> ToThrottledObservable(this ToggleButton toggleButton) {
         return Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
               s => toggleButton.Checked += s,
               s => toggleButton.Checked -= s)
            .Select(evt => true)
            .Merge(Observable.FromEventPattern<RoutedEventHandler, RoutedEventArgs>(
                  s => toggleButton.Unchecked += s,
                  s => toggleButton.Unchecked -= s)
               .Select(evt => false))
            // better to select on the UI thread
            .Throttle(TimeSpan.FromMilliseconds(500))
            .StartWith(toggleButton.IsChecked ?? false)
            .DistinctUntilChanged();
      }

      public static IObservable<NotifyCollectionChangedEventArgs> SelectChanges(this INotifyCollectionChanged oc) {
         return Observable
            .FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
               h => oc.CollectionChanged += h,
               h => oc.CollectionChanged -= h)
            .Select(_ => _.EventArgs);
      }

      public static IObservable<T> SelectAddedItems<T>(this INotifyCollectionChanged notifyCollectionChanged) {
         return notifyCollectionChanged
            .SelectChanges()
            .SelectMany(x => x.NewItems?.Cast<T>() ?? new T[] { })
            .WhereNotNull();
      }

      /// <summary>
      /// In order to be notified of all added items the case where a collection is reset needs to be considered.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <typeparam name="TR"></typeparam>
      /// <param name="notifyCollectionChanged"></param>
      /// <returns></returns>
      public static IObservable<T> SelectResetItems<T, TR>(this TR notifyCollectionChanged) where TR : ICollection<T>, INotifyCollectionChanged {
         return notifyCollectionChanged
            .SelectChanges()
            .Where(a => a.Action == NotifyCollectionChangedAction.Reset)
            .Select(a => notifyCollectionChanged.SingleOrDefault())
            .WhereNotNull();
      }


      public static IObservable<T> SelectAddedAndResetItems<T, TR>(this TR observableCollection) where TR : ICollection<T>, INotifyCollectionChanged {
         return
            observableCollection
               .SelectResetItems<T, TR>()
               .Merge(observableCollection.SelectAddedItems<T>());
      }

      public static IObservable<T> SelectAddedAndResetItems<T>(this ReadOnlyObservableCollection<T> observableCollection) {
         return observableCollection.SelectAddedAndResetItems<T, ReadOnlyObservableCollection<T>>();
      }

      public static IObservable<T> SelectAddedAndResetItems<T>(this ObservableCollection<T> observableCollection) {
         return observableCollection.SelectAddedAndResetItems<T, ObservableCollection<T>>();
      }
   }
}
