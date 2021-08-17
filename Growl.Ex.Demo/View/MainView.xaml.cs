using ReactiveUI;

namespace Growl.Ex.Demo.View {
   /// <summary>
   /// Interaction logic for MainView.xaml
   /// </summary>
   public partial class MainView  {
      public MainView() {
         InitializeComponent();

         this.WhenActivated(disposable =>
         {
            GrowlCreateViewModelViewHost.ViewModel = ViewModel.GrowlCreateViewModel;
            GrowlDrawerViewModelViewHost.ViewModel = ViewModel.GrowlDrawerViewModel;
            GrowlLowerViewModelViewHost.ViewModel = ViewModel.GrowlLowerViewModel;

         });
      }
   }
}
