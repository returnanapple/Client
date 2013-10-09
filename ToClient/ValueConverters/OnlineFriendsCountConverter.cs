using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ToClient.Classes;
using System.Linq;

namespace ToClient.ValueConverters
{
    public class OnlineFriendsCountConverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<UserInfo> source = (ObservableCollection<UserInfo>)value;
            return source.Where(x => x.UserState == States.在线 && x.UserState == States.忙碌).ToList().Count;
            
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
