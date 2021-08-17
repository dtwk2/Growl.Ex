using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ReactiveUI;

namespace Growl.Ex.View {

   /// <summary>
   /// Interaction logic for GrowlCreateInstanceView.xaml
   /// </summary>
   public partial class GrowlCreateInstanceView {

      public GrowlCreateInstanceView() {
         InitializeComponent();

         this.WhenActivated(disposable => {
            AddRandomGrowls.Events().Click.Select(a => 1)
               .Where(a => AddRandomGrowls.IsDropDownOpen == false)
               .Merge(One.Events().Click.Select(a => int.Parse(One.Header.ToString())))
               .Merge(Three.Events().Click.Select(a => int.Parse(Three.Header.ToString())))
               .Merge(Thirty.Events().Click.Select(a => int.Parse(Thirty.Header.ToString())))
               .InvokeCommand(ViewModel.CreateRandomGrowls)
               .DisposeWith(disposable);
         });
      }
   }
}
