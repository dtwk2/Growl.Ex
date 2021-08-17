using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using Growl.Ex.ViewModel.Infrastructure;
using ReactiveUI;


namespace Growl.Ex.ViewModel {

   public class GrowlCreateInstanceViewModel : IObservable<GrowlRequest> {

      private readonly ReplaySubject<GrowlRequest> growlRequests = new ReplaySubject<GrowlRequest>();

      public GrowlCreateInstanceViewModel() {

         CreateRandomGrowls.SelectMany(a => a)
            //.Buffer(TimeSpan.FromSeconds(0.4))
            //.Select(a => a.LastOrDefault())
            //.WhereNotDefault()
            .Subscribe(growlRequests.OnNext);
      }


      public ReactiveCommand<int, IObservable<GrowlRequest>> CreateRandomGrowls { get; } = ReactiveCommand.Create<int, IObservable<GrowlRequest>>(a =>
           Observable.Return(new GrowlRequest(a)));


      public IDisposable Subscribe(IObserver<GrowlRequest> observer) {
         return growlRequests.Subscribe(observer);
      }
   }
}
