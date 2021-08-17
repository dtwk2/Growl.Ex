using Growl.Ex.ViewModel;


namespace Growl.Ex.Demo {

   public class MainViewModel {
      
      public MainViewModel(GrowlCreateViewModel growlCreateViewModel, GrowlDrawerViewModel growlDrawerViewModel, GrowlLowerViewModel growlLowerViewModel)
      {
         GrowlCreateViewModel = growlCreateViewModel;
         GrowlDrawerViewModel = growlDrawerViewModel;
         GrowlLowerViewModel = growlLowerViewModel;
      }

      public GrowlCreateViewModel GrowlCreateViewModel { get; }

      public GrowlDrawerViewModel GrowlDrawerViewModel { get; }

      public GrowlLowerViewModel GrowlLowerViewModel { get; }
   }
}
