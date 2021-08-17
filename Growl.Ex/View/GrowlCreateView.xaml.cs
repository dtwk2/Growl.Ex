using ReactiveUI;

namespace Growl.Ex.View {
   /// <summary>
   /// Interaction logic for GrowlCreateView.xaml
   /// </summary>
   public partial class GrowlCreateView  {
      public GrowlCreateView() {
         InitializeComponent();

         this.WhenActivated(disposable =>
         {
            this.GrowlCreateInstanceViewModelViewHost.ViewModel = this.ViewModel.GrowlCreateInstanceViewModel;
            this.GrowlCreatePeriodicViewModelViewHost.ViewModel = this.ViewModel.GrowlCreatePeriodicViewModel;
         });
      }
   }
}
