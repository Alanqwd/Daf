using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Daf
{
    public class DateTimeLessDateConverter : IValueConverter
    {

        public object Convert(object value,
        Type targetType, object parameter, CultureInfo culture)
        {
            var red = value as DateTime?;
            if (red != null)
            {
                return DateTime.Now > red.Value;
            }
            return false;
            if (red == null)
            {
                return DateTime.Now > red.Value;
            }
            return false;
        }
     
        public object ConvertBack(object value,
            Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }



    }
}
