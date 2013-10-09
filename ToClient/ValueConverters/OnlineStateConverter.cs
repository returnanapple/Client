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
            States source = (States)value;
            if (source == States.离线 || source == States.隐身)
            {
                return "/ToClient;component/Images/离线.png";
            }
            else
            {
                if (source != 0)
                {
                    return (source == States.在线 ? "/ToClient;component/Images/在线.png" : "/ToClient;component/Images/忙碌.png");
                }
                else
                    return "/ToClient;component/Images/离线.png";
            }
                
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string source = (string)value;
            switch (source)
            {
                case "/ToClient;component/Images/离线.png":
                    return States.离线;
                case "/ToClient;component/Images/忙碌.png":
                    return States.忙碌;
                case "/ToClient;component/Images/隐身.png":
                    return States.隐身;
                case "/ToClient;component/Images/在线.png":
                    return States.在线;
                default:
                    return null;
            }
        }
    }
}
