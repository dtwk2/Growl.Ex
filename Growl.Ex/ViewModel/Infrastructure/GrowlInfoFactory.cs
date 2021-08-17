using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using HandyControl.Data;

namespace Growl.Ex.ViewModel.Infrastructure {
   public class GrowlInfoFactory : IObserver<GrowlRequest>, IObservable<GrowlInfo> {

      private readonly ReplaySubject<GrowlRequest> growlRequests = new ReplaySubject<GrowlRequest>();
      private readonly ReplaySubject<GrowlInfo> growlInfos = new ReplaySubject<GrowlInfo>();

      public GrowlInfoFactory(IEnumerable<IObservable<GrowlRequest>> requests) {
         requests.ToObservable().SelectMany(a => a).Subscribe(growlRequests);

         growlRequests
            .SelectMany(a => Observable.Range(0, a.Quantity).Select(a=> GrowlFactoryHelper.RandomGrowlInfo()).Delay(a.Delay))
            .ObserveOnDispatcher()
            .Subscribe(growlInfos.OnNext);
      }

      public void OnCompleted() {
         throw new NotImplementedException();
      }

      public void OnError(Exception error) {
         throw new NotImplementedException();
      }

      public void OnNext(GrowlRequest value) {
         growlRequests.OnNext(value);
      }

      public IDisposable Subscribe(IObserver<GrowlInfo> observer) {
         return growlInfos.Subscribe(observer);
      }
   }
}