using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Growl.Ex.View.Infrastructure;
using Growl.Ex.ViewModel.Infrastructure;
using ReactiveUI;

namespace Growl.Ex.View {
   /// <summary>
   /// Interaction logic for GrowlDrawerView.xaml
   /// </summary>
   public partial class GrowlDrawerView {
      public GrowlDrawerView() {
         InitializeComponent();

         this.WhenActivated(disposables => {
            if (!(this.Resources["Blink"] is Storyboard storyboard))
               throw new Exception("Blink storyboard is null");

            this.ViewModel.WhenAnyValue(a => a.State)
                .ObserveOnDispatcher()
                .Subscribe(a => {
                   if (a)
                      storyboard.Begin();
                   else {
                      storyboard.Stop();
                      ToggleButton1.Opacity = 1;
                   }
                });

            this.OneWayBind(ViewModel, vm => vm.GroupedItems, v => v.ItemsControl1.ItemsSource)
                .DisposeWith(disposables);

            this.OneWayBind(ViewModel, vm => vm.GroupedItems.Count, v => v.Grid1.Visibility, a => a > 0 ? Visibility.Visible : Visibility.Hidden)
                .DisposeWith(disposables);

            this.ToggleButton1.ToThrottledObservable().Subscribe(this.ViewModel.OnNext);

            MainObservableGrid
               .ToObservable()
                .Select(StackPanel1_VisualChildrenChanged)
               .Subscribe(this.ViewModel.OnNext);

            (AddRemove, HandyControl.Controls.Growl) StackPanel1_VisualChildrenChanged(VisualChildrenChangedEventArgs e) {
               if (e.VisualAdded is HandyControl.Controls.Growl growl)
                  return (AddRemove.Add, growl);
               if (e.VisualRemoved is HandyControl.Controls.Growl growl2)
                  return (AddRemove.Remove, growl2);

               throw new Exception($"Expected either visual added or removed to be a {nameof(Growl)}.");
            }
         });
      }

      private void ExecutedCustomCommand(object sender, ExecutedRoutedEventArgs e) {
         DrawerRight.IsOpen = true;
      }

      private void CanExecuteCustomCommand(object sender, CanExecuteRoutedEventArgs e) {
         e.CanExecute = true;
      }
   }
}
