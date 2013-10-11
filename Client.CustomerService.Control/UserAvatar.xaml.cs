using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Client.CustomerService.Framework;

namespace Client.CustomerService.Control
{
    public partial class UserAvatar : UserControl
    {
        public UserAvatar()
        {
            InitializeComponent();
        }

        #region 依赖属性

        public int CountOfNewMessage
        {
            get { return (int)GetValue(CountOfNewMessageProperty); }
            set { SetValue(CountOfNewMessageProperty, value); }
        }

        public static readonly DependencyProperty CountOfNewMessageProperty =
            DependencyProperty.Register("CountOfNewMessage", typeof(int), typeof(UserAvatar)
            , new PropertyMetadata(0, (d, e) =>
            {
                UserAvatar tool = (UserAvatar)d;
                Storyboard s = (Storyboard)tool.Resources["s"];
                if ((int)e.NewValue != 0)
                {
                    s.Stop();
                    s.Begin();
                }
                else
                {
                    s.Stop();
                }
            }));




        public UserInfoModel.UserModelType UserType
        {
            get { return (UserInfoModel.UserModelType)GetValue(UserTypeProperty); }
            set { SetValue(UserTypeProperty, value); }
        }

        public static readonly DependencyProperty UserTypeProperty =
            DependencyProperty.Register("UserType", typeof(UserInfoModel.UserModelType), typeof(UserAvatar)
            , new PropertyMetadata(UserInfoModel.UserModelType.客服, (d, e) =>
            {
                UserAvatar tool = (UserAvatar)d;
                UserInfoModel.UserModelType userType = (UserInfoModel.UserModelType)e.NewValue;
                switch (userType)
                {
                    case UserInfoModel.UserModelType.客服:
                        tool.root.Style = (Style)tool.Resources["cs"];
                        break;
                    case UserInfoModel.UserModelType.在线:
                        tool.root.Style = (Style)tool.Resources["online"];
                        break;
                    case UserInfoModel.UserModelType.离线:
                        tool.root.Style = (Style)tool.Resources["offline"];
                        break;
                }
            }));





        #endregion
    }
}
