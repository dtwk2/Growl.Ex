using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Bogus;
using Growl.Ex.View;
using Growl.Ex.ViewModel;
using Growl.Ex.ViewModel.Infrastructure;
using ReactiveUI;
using Splat;

namespace Growl.Ex.Meta {
   public class BootStrapper {

      public static void Register(ContainerBuilder builder)
      {
         // View
         builder.RegisterType<GrowlCreateView>().As<IViewFor<GrowlCreateViewModel>>();
         builder.RegisterType<GrowlCreateInstanceView>().As<IViewFor<GrowlCreateInstanceViewModel>>();
         builder.RegisterType<GrowlCreatePeriodicView>().As<IViewFor<GrowlCreatePeriodicViewModel>>();
         builder.RegisterType<GrowlLowerView>().As<IViewFor<GrowlLowerViewModel>>();
         builder.RegisterType<GrowlDrawerView>().As<IViewFor<GrowlDrawerViewModel>>();


         // ViewModel
         builder.RegisterType<GrowlCreateInstanceViewModel>().SingleInstance().AsImplementedInterfaces().AsSelf();
         builder.RegisterType<GrowlCreatePeriodicViewModel>().SingleInstance().AsImplementedInterfaces().AsSelf();
         builder.RegisterType<GrowlCreateViewModel>().SingleInstance().AsImplementedInterfaces().AsSelf();

         builder.RegisterType<GrowlLowerViewModel>().SingleInstance().AsImplementedInterfaces().AsSelf();
         builder.RegisterType<GrowlDrawerViewModel>().SingleInstance().AsImplementedInterfaces().AsSelf();

         // Service
         builder.RegisterType<GrowlInfoFactory>().SingleInstance().AsImplementedInterfaces().AsSelf().AutoActivate();

         builder.RegisterType<Faker>().SingleInstance().AsImplementedInterfaces().AsSelf().AutoActivate();
      }
   }
}
