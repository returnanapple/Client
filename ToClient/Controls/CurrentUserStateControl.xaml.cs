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
    public partial class CurrentUserStateControl : UserControl
    {
        public CurrentUserStateControl()
        {
            InitializeComponent();
        }



        public bool FriendListWindowIsOpen
        {
            get { return (bool)GetValue(FriendListWindowIsOpenProperty); }
            set { SetValue(FriendListWindowIsOpenProperty, value); }
        }
        public static readonly DependencyProperty FriendListWindowIsOpenProperty =
            DependencyProperty.Register("FriendListWindowIsOpen", typeof(bool), typeof(CurrentUserStateControl), new PropertyMetadata(false, (d, e) => 
            {
                CurrentUserStateControl tempD = (CurrentUserStateControl)d;
                Storyboard tempStoryboard = tempD.Resources["DirectionStoryboard"] as Storyboard;
                bool tempE = (bool)e.NewValue;
                tempD.DirectionDoubleAnimation.To = tempE == true ? -180 : 0;
                tempStoryboard.Begin();
            }));

        public void StateImageMouseLeftButtonDownAction(object sender, MouseButtonEventArgs e)
        {
            Image tempSender = (Image)sender;
            tempSender.Opacity = 1;
        }

        public void FriendListWindowOpenOrCloseMouseLeftButtonDownAction(object sender, MouseButtonEventArgs e)
        {
            this.FriendListWindowIsOpen = this.FriendListWindowIsOpen == true ? false : true;
        }
    }
}
