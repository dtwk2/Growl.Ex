using System.Windows.Input;

namespace Growl.Ex.View.Infrastructure
{
    public static class AppCommands
    {
        /// <summary>
        /// Shows the right panel containing the Growl notifications
        /// </summary>
        public static RoutedCommand ShowGrowlPanel { get; } = new RoutedCommand(nameof(ShowGrowlPanel), typeof(AppCommands));
    }
}