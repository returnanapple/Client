using System;
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
using ToClient.UserService;

namespace ToClient.ValueConverters
{
    public class CurrentUserOnlineStateConverter:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            UserOnlineStatus source = (UserOnlineStatus)value;
            string tempParameter = (string)parameter;
            if (source.ToString() == tempParameter)
            {
                return 1;
            }
            else
            {
                return 0.3; 
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double source = (double)value;
            string tempParameter = (string)parameter;
            if (source == 1)
            {
                switch (tempParameter)
                {
                    case "在线":
                        return UserOnlineStatus.在线;
                    case "忙碌":
                        return UserOnlineStatus.忙碌;
                    case "隐身":
                        return UserOnlineStatus.隐身;
                    default:
                        throw new NotImplementedException();
                }
            }
            else
                throw new NotImplementedException();
        }
    }
}
