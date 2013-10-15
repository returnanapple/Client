using Client.CustomerService.Framework.ChatService;
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

namespace ToClient.ValueConverters
{
    public class OnlineStateConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            UserOnlineStatus source = (UserOnlineStatus)value;
            if (source == UserOnlineStatus.离线 || source == UserOnlineStatus.隐身)
            {
                return "/ToClient;component/Images/离线.png";
            }
            else
            {
                return (source == UserOnlineStatus.在线 ? "/ToClient;component/Images/在线.png" : "/ToClient;component/Images/忙碌.png");
                
            }
                
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string source = (string)value;
            switch (source)
            {
                case "/ToClient;component/Images/离线.png":
                    return UserOnlineStatus.离线;
                case "/ToClient;component/Images/忙碌.png":
                    return UserOnlineStatus.忙碌;
                case "/ToClient;component/Images/隐身.png":
                    return UserOnlineStatus.隐身;
                case "/ToClient;component/Images/在线.png":
                    return UserOnlineStatus.在线;
                default:
                    return null;
            }
        }
    }
}
