using System;

namespace Growl.Ex.ViewModel.Infrastructure
{
   public readonly struct GrowlRequest {
      public GrowlRequest(int quantity, TimeSpan delay = default) {
         Quantity = quantity;
         Delay = delay;
      }

      public int Quantity { get; }

      public TimeSpan Delay { get; }
   }
}