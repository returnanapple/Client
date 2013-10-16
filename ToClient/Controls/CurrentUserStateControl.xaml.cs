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



        public int NewMessageCount
        {
            get { return (int)GetValue(NewMessageCountProperty); }
            set { SetValue(NewMessageCountProperty, value); }
        }
        public static readonly DependencyProperty NewMessageCountProperty =
            DependencyProperty.Register("NewMessageCount", typeof(int), typeof(CurrentUserStateControl), new PropertyMetadata(0, (d, e) => 
            {
                CurrentUserStateControl tempD = (CurrentUserStateControl)d;
                int tempE = (int)e.NewValue;
                Storyboard tStoryboard = tempD.Resources["NewMessageStoryboard"] as Storyboard;
                if (tempE != 0)
                {
                    tStoryboard.Begin();
                }
                else
                {
                    tStoryboard.Stop();
                }
            }));

        
    }
}
