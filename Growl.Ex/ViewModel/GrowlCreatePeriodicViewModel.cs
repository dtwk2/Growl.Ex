using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using Growl.Ex.ViewModel.Infrastructure;
using ReactiveUI;

namespace Growl.Ex.ViewModel {

   public class GrowlCreatePeriodicViewModel : IObservable<GrowlRequest> {

      private readonly ReplaySubject<GrowlRequest> growlRequests = new ReplaySubject<GrowlRequest>();

      public GrowlCreatePeriodicViewModel() {

         PeriodicGrowls(HasPeriodicGrowlsCommand)
            .Subscribe(growlRequests.OnNext);

         static IObservable<GrowlRequest> PeriodicGrowls(IObservable<bool> doGrowls)

               => Observable.Timer(TimeSpan.FromMilliseconds(1000), TimeSpan.FromSeconds(2))
                  .CombineLatest(doGrowls, (a, b) => (a, b))
                  .Where(a => a.b)
                  .Delay(TimeSpan.FromSeconds(2))
                  .Select(da => new GrowlRequest(1));
      }

      public ReactiveCommand<bool, bool> HasPeriodicGrowlsCommand { get; } = ReactiveCommand.Create<bool, bool>(a => a);

      public IDisposable Subscribe(IObserver<GrowlRequest> observer) {
         return growlRequests.Subscribe(observer);
      }
   }
}
