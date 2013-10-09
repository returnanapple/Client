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
    public partial class FriendsControl : ItemsControl
    {
        public FriendsControl()
        {
            InitializeComponent();
        }


        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(FriendsControl), new PropertyMetadata(false,(d,e)=>
            {
                FriendsControl tempD = (FriendsControl)d;
                Storyboard tempStoryboard = tempD.Resources["OpenStoryboard"] as Storyboard;
                bool TempE = (bool)(e.NewValue);
                tempD.OpenAnimation.To = TempE == true ? (20 * (tempD.Items.ToList().Count)) : 0;
                tempStoryboard.Begin();
            }));


    }
}
