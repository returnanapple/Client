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

namespace ToClient
{
    public partial class ToClientV : UserControl
    {
        public ToClientV()
        {
            InitializeComponent();
            MySelf = this;
        }

        public static ToClientV MySelf { get; set; }

        public bool ChatWindowIsOpen
        {
            get { return (bool)GetValue(ChatWindowIsOpenProperty); }
            set { SetValue(ChatWindowIsOpenProperty, value); }
        }
        public static readonly DependencyProperty ChatWindowIsOpenProperty =
            DependencyProperty.Register("ChatWindowIsOpen", typeof(bool), typeof(ToClientV), new PropertyMetadata(false, (d, e) =>
                {
                    ToClientV tempD = (ToClientV)d;
                    Storyboard tempStoryboard = tempD.Resources["ChatWindowControlStoryboard"] as Storyboard;
                    DoubleAnimation tempDoubleAnimation = tempD.ChatWindowControlDoubleAnimation;
                    bool tempE = (bool)e.NewValue;
                    if (tempD.FriendListWindowIsOpen == true)
                    {
                        tempDoubleAnimation.To = tempE == true ? 655 : 0;
                        tempStoryboard.Begin();
                    }
                    else
                    {
                            Storyboard tempStoryboard2 = tempD.Resources["FriendListControlStoryboard"] as Storyboard;
                            DoubleAnimation tempDoubleAnimation2 = tempD.FriendListControlDoubleAnimation;
                            DoubleAnimation tempDoubleAnimation3 = tempD.OtherDoubleAnimation;
                            tempDoubleAnimation2.To = 0;
                            tempDoubleAnimation3.To = 110;

                            Storyboard xStoryboard = tempD.Resources["OtherChatWindowControlStoryboard"] as Storyboard;
                            xStoryboard.Completed += (_d, _e) => { tempStoryboard2.Begin(); };
                            xStoryboard.Begin();
                    }
                }));



        public bool FriendListWindowIsOpen
        {
            get { return (bool)GetValue(FriendListWindowIsOpenProperty); }
            set { SetValue(FriendListWindowIsOpenProperty, value); }
        }
        public static readonly DependencyProperty FriendListWindowIsOpenProperty =
            DependencyProperty.Register("FriendListWindowIsOpen", typeof(bool), typeof(ToClientV), new PropertyMetadata(false, (d, e) => 
            {
                ToClientV tempD = (ToClientV)d;
                if (tempD.ChatWindowIsOpen == false)
                {
                    bool tempE = (bool)e.NewValue;
                    Storyboard tempStoryboard = tempD.Resources["FriendListControlStoryboard"] as Storyboard;
                    DoubleAnimation tempDoubleAnimation2 = tempD.FriendListControlDoubleAnimation;
                    DoubleAnimation tempDoubleAnimation3 = tempD.OtherDoubleAnimation;
                    tempDoubleAnimation2.To = tempE == true ? 290 : 0;
                    tempDoubleAnimation3.To = tempE == true ? 400 : 0;
                    tempStoryboard.Begin();
                }
                else
                {
                    tempD.ChatWindowIsOpen = false;
                }
            }));

        
        

        
    }
}
