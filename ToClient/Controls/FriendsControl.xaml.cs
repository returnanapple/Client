﻿using System;
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



        public bool TimeToReflash
        {
            get { return (bool)GetValue(TimeToReflashProperty); }
            set { SetValue(TimeToReflashProperty, value); }
        }
        public static readonly DependencyProperty TimeToReflashProperty =
            DependencyProperty.Register("TimeToReflash", typeof(bool), typeof(FriendsControl), new PropertyMetadata(false, (d, e) => 
            {
                FriendsControl tempD = (FriendsControl)d;
                bool tempE = (bool)(e.NewValue);
                if (tempE == true && tempD.IsOpen == true)
                {
                    Storyboard tempStoryboard = tempD.Resources["OpenStoryboard"] as Storyboard;
                    tempD.OpenAnimation.To = tempD.MyItemsControl.Height+20;
                    tempStoryboard.Begin();
                    tempD.TimeToReflash = false;
                }
            }));



        public bool TimeToReflash2
        {
            get { return (bool)GetValue(TimeToReflash2Property); }
            set { SetValue(TimeToReflash2Property, value); }
        }
        public static readonly DependencyProperty TimeToReflash2Property =
            DependencyProperty.Register("TimeToReflash2", typeof(bool), typeof(FriendsControl), new PropertyMetadata(false, (d, e) => 
            {
                FriendsControl tempD = (FriendsControl)d;
                bool tempE = (bool)(e.NewValue);
                if (tempE == true && tempD.IsOpen == true)
                {
                    Storyboard tempStoryboard = tempD.Resources["OpenStoryboard"] as Storyboard;
                    tempD.OpenAnimation.To = tempD.MyItemsControl.Height-20;
                    tempStoryboard.Begin();
                    tempD.TimeToReflash2 = false;
                }
            }));



        




    }
}
