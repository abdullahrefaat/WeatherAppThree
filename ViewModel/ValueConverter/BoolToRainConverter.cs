using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WeatherAppThree.ViewModel.ValueConverter
{
    public class BoolToRainConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isRaining = (bool)value;
            if (isRaining)
                return "Currently raining";
            return "Currently not raining";
        }
        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string isRaining = (string)value;
            if (isRaining == "Currently raining")
                return true;
            return false;
        }
    }
}
