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
    public partial class FriendListControl : UserControl
    {
        public FriendListControl()
        {
            InitializeComponent();
        }


        public bool FriendListWindowIsOpen
        {
            get { return (bool)GetValue(FriendListWindowIsOpenProperty); }
            set { SetValue(FriendListWindowIsOpenProperty, value); }
        }
        public static readonly DependencyProperty FriendListWindowIsOpenProperty =
            DependencyProperty.Register("FriendListWindowIsOpen", typeof(bool), typeof(FriendListControl), new PropertyMetadata(false, (d, e) => 
            {
                FriendListControl tempD = (FriendListControl)d;
                bool tempE = (bool)e.NewValue;
                Storyboard tempStoryboard = tempD.Resources["RootScrollViewerStoryboard"] as Storyboard;
                tempD.RootScrollViewerDoubleAnimation.To = tempE == true ? 290 : 0;
                tempStoryboard.Begin();
            }));
    }
}
