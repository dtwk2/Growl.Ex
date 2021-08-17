using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Growl.Ex.View.Infrastructure;
using HandyControl.Data;
using ReactiveUI;


namespace Growl.Ex.View {
   /// <summary>
   /// Interaction logic for GrowlCreatePeriodicView.xaml
   /// </summary>
   public partial class GrowlCreatePeriodicView {

      public GrowlCreatePeriodicView() {
         InitializeComponent();


         this.WhenActivated(disposable => {
            this.PeriodicGrowlsToggle
               .ToThrottledObservable()
               .InvokeCommand(ViewModel.HasPeriodicGrowlsCommand)
               .DisposeWith(disposable);

            this.PeriodicGrowlsToggle
               .ToThrottledObservable()
               .Select(a => a ? BadgeStatus.Processing: BadgeStatus.Dot)
               .ObserveOnDispatcher()
               .Subscribe(a => MainBadge.Status = a)
               .DisposeWith(disposable);
         });
      }
   }
}
