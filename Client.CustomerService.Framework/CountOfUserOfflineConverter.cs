using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace Client.CustomerService.Framework
{
    public class CountOfUserOfflineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) { return 0; }
            ObservableCollection<UserInfoModel> users = (ObservableCollection<UserInfoModel>)value;
            return users.Count(x => x.UserType == UserInfoModel.UserModelType.离线);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
