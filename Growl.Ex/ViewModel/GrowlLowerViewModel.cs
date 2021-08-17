using System;
using System.Reactive.Linq;
using Growl.Ex.ViewModel.Infrastructure;
using HandyControl.Data;
using ReactiveUI;

namespace Growl.Ex.ViewModel
{
   public class GrowlLowerViewModel
   {
      public GrowlLowerViewModel(IObservable<GrowlInfo> growlInfos) {

         var mainEvents = growlInfos.Where(a => a.Token == MessageToken.GrowlLowerPanel);

            mainEvents
            .Select(GrowlHelper.ToGrowlAction)
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(a => a.Invoke());
      }
   }
}
