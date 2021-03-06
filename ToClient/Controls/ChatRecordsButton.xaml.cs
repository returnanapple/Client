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
    public partial class ChatRecordsButton : UserControl
    {
        public ChatRecordsButton()
        {
            InitializeComponent();
        }

        public void MouseEnterAction(object sender, MouseEventArgs e)
        {
            RootBorder.Background = new SolidColorBrush(Color.FromArgb(255, 252, 240, 193));//#FFFCF0C1
        }
        public void MouseLeaveAction(object sender, MouseEventArgs e)
        {
            RootBorder.Background = new SolidColorBrush(Color.FromArgb(255, 247, 245, 245));//#FFF7F5F5
        }
    }
}
