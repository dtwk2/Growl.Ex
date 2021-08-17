using Bogus;
using HandyControl.Data;
using Splat;

namespace Growl.Ex.ViewModel.Infrastructure
{
   static class GrowlFactoryHelper {

      public static GrowlInfo RandomGrowlInfo() {
         var type = Locator.Current.GetService<Faker>().Random.Enum<InfoType>();

         // Avoid fatal types (throws error further down stack)
         while (type == InfoType.Fatal) {
            type = Locator.Current.GetService<Faker>().Random.Enum<InfoType>();
         }

         return new GrowlInfo {
            Message = Locator.Current.GetService<Faker>().Lorem.Sentence(),
            Type = type,
            Token = Locator.Current.GetService<Faker>().Random.Bool() ? MessageToken.GrowlMainPanel : MessageToken.GrowlLowerPanel
         };
      }
   }
}