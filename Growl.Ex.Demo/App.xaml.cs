using System.Windows;
using Autofac;
using ReactiveUI;
using Splat;
using Splat.Autofac;
using Growl.Ex.Demo.View;

namespace Growl.Ex.Demo
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {

      public App()
      {
         var builder = new ContainerBuilder();

         builder.RegisterType<MainView>().As<IViewFor<MainViewModel>>();
         builder.RegisterType<MainViewModel>().SingleInstance().AsImplementedInterfaces().AsSelf();

         Growl.Ex.Meta.BootStrapper.Register(builder);
         builder.UseAutofacDependencyResolver();

   
      }
   }
}
