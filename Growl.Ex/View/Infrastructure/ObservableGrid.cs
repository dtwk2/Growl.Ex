//using System;

using System;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Growl.Ex.View.Infrastructure
{
    /// <summary>
    /// Notifies when child added or removed from Children collection.
    /// </summary>
    public class ObservableGrid : StackPanel
    {
       protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
       {
          this.Dispatcher.InvokeAsync(() =>
          {
             base.OnVisualChildrenChanged(visualAdded, visualRemoved);

             VisualChildrenChanged?.Invoke(this, new VisualChildrenChangedEventArgs(visualAdded, visualRemoved));
          });
       }

       public event EventHandler<VisualChildrenChangedEventArgs> VisualChildrenChanged;
    }

    public class VisualChildrenChangedEventArgs : EventArgs
    {
        public VisualChildrenChangedEventArgs(DependencyObject visualAdded, DependencyObject visualRemoved)
        {
            VisualAdded = visualAdded;
            VisualRemoved = visualRemoved;
        }

        public DependencyObject VisualAdded { get; }
        public DependencyObject VisualRemoved { get; }
    }

    public static class ObservableHelper
    {
        public static IObservable<VisualChildrenChangedEventArgs> ToObservable(this ObservableGrid textBox)
        {
            return Observable
                .FromEventPattern<EventHandler<VisualChildrenChangedEventArgs>, VisualChildrenChangedEventArgs>(
                    s => textBox.VisualChildrenChanged += s,
                    s => textBox.VisualChildrenChanged -= s)
                .Select(evt => evt.EventArgs);

        }
    }
}
