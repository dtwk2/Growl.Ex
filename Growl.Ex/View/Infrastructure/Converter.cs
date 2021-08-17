using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Growl.Ex.View.Infrastructure {

   public abstract class BaseConverter<T, R> : IValueConverter {
      protected BaseConverter(T trueValue, T falseValue) {
         True = trueValue;
         False = falseValue;
      }

      public T True { get; set; }

      public T False { get; set; }

      public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
         return Check(Convert(value)) ? True : False;
      }

      public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
         return value is T value1 && EqualityComparer<T>.Default.Equals(value1, True);
      }

      protected abstract bool Check(R value);

      protected abstract R Convert(object value);
   }

   public abstract class BaseConverter<T> : BaseConverter<T, object> {
      protected BaseConverter(T trueValue, T falseValue) : base(trueValue, falseValue) {
         True = trueValue;
         False = falseValue;
      }

      protected override object Convert(object value) => value;
   }
   public class BooleanConverter<T> : BaseConverter<T> {
      public BooleanConverter(T trueValue, T falseValue) : base(trueValue, falseValue) {
      }

      protected override bool Check(object value) {
         return value is bool b && b;
      }
   }

   public sealed class BooleanToVisibilityConverter : BooleanConverter<Visibility> {
      static BooleanToVisibilityConverter() {
      }

      public BooleanToVisibilityConverter() : base(Visibility.Visible, Visibility.Collapsed) { }

      public static BooleanToVisibilityConverter Instance => new BooleanToVisibilityConverter();
   }

   public sealed class InverseBooleanToVisibilityConverter : BooleanConverter<Visibility> {
      static InverseBooleanToVisibilityConverter() {
      }

      public InverseBooleanToVisibilityConverter() : base(Visibility.Collapsed, Visibility.Visible) { }

      public static BooleanToVisibilityConverter Instance => new BooleanToVisibilityConverter();
   }
}
