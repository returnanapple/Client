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

namespace ToClient.Controls
{
    public partial class FriendListTittleButton : UserControl
    {
        public FriendListTittleButton()
        {
            InitializeComponent();
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(FriendListTittleButton), new PropertyMetadata(false,(d,e)=>
            {
                FriendListTittleButton tempD = (FriendListTittleButton)d;
                bool tempE = (bool)(e.NewValue);
                Storyboard tempStoryboard = tempD.Resources["IsOpenSymbolStoryboard"] as Storyboard;
                tempD.FirstAnimation.To=tempE == true ? 180 : 90;
                tempStoryboard.Begin();
                
            }));



        public string TittleText
        {
            get { return (string)GetValue(TittleTextProperty); }
            set { SetValue(TittleTextProperty, value); }
        }
        public static readonly DependencyProperty TittleTextProperty =
            DependencyProperty.Register("TittleText", typeof(string), typeof(FriendListTittleButton), new PropertyMetadata(""));



        public string OnlineFriendsCount
        {
            get { return (string)GetValue(OnLineFriendsCountProperty); }
            set { SetValue(OnLineFriendsCountProperty, value); }
        }
        public static readonly DependencyProperty OnLineFriendsCountProperty =
            DependencyProperty.Register("OnlineFriendsCount", typeof(string), typeof(FriendListTittleButton), new PropertyMetadata(""));

        public string AllFriendsCount
        {
            get { return (string)GetValue(AllFriendsCountProperty); }
            set { SetValue(AllFriendsCountProperty, value); }
        }
        public static readonly DependencyProperty AllFriendsCountProperty =
            DependencyProperty.Register("AllFriendsCount", typeof(string), typeof(FriendListTittleButton), new PropertyMetadata(""));

        public void MouseEnterAction(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = new SolidColorBrush(Color.FromArgb(255, 229, 229, 227));
        }

        public void MouseLeaveAction(object sender, MouseEventArgs e)
        {
            (sender as Grid).Background = new SolidColorBrush(Color.FromArgb(255, 247, 245, 245));
            
        }

        public void MouseLeftButtonDownAction(object sender, MouseButtonEventArgs e)
        {
            IsOpen = IsOpen == false ? true : false;
        }
    }
}
