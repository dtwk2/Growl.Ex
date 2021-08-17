using System;
using System.Collections.Generic;
using System.Text;

namespace Growl.Ex.ViewModel {
   public class GrowlCreateViewModel {
 
      public GrowlCreateViewModel(GrowlCreatePeriodicViewModel growlCreatePeriodicViewModel, GrowlCreateInstanceViewModel growlCreateInstanceViewModel)
      {
         GrowlCreatePeriodicViewModel = growlCreatePeriodicViewModel;
         GrowlCreateInstanceViewModel = growlCreateInstanceViewModel;
      }

      public GrowlCreatePeriodicViewModel GrowlCreatePeriodicViewModel { get; }

      public GrowlCreateInstanceViewModel GrowlCreateInstanceViewModel { get; }
   }
}
