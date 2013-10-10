using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Client.CustomerService.Framework.UserService;

namespace Client.CustomerService.Framework
{
    public class CountOfUserConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return 0; }
            ObservableCollection<UserInfoModel> users = (ObservableCollection<UserInfoModel>)value;
            return users.Count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
