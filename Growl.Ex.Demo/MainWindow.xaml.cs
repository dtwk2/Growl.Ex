using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using ReactiveUI;
using Splat;
using Growl.Ex.View;
using Growl.Ex.View.Infrastructure;
using Growl.Ex.ViewModel;

namespace Growl.Ex.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
           
            InitializeComponent();
         
            this.WhenActivated(disposables =>
            {
               MainViewModelViewHost.ViewModel = Locator.Current.GetService<MainViewModel>();
            });
        }
    }
}
