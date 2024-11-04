using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FlexComms.Converters
{
    public class ActiveToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Giả sử rằng giá trị 1 có nghĩa là Active và giá trị 2 có nghĩa là không Active
            if (value is int intValue)
            {
                return intValue == 1; // Chuyển đổi giá trị 1 thành true, giá trị khác thành false
            }
            return false;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? 1 : 2; // Chuyển đổi giá trị boolean thành 1 hoặc 2
            }
            return 2; // Giá trị mặc định
        }
    }
}
