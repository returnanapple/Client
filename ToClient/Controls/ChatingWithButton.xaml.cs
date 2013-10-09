using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ToClient.ValueConverters;

namespace ToClient.Controls
{
    public partial class ChatingWithButton : UserControl
    {
        public ChatingWithButton()
        {
            InitializeComponent();
        }

        private void TextMouseEnterAction(object sender, MouseEventArgs e)
        {
            TextGrid.Background = new SolidColorBrush(Color.FromArgb(255, 113, 87, 86));
            CloseGrid.Background = new SolidColorBrush(Color.FromArgb(255, 113, 87, 86));
        }
        public void TextMouseLeaveAction(object sender, MouseEventArgs e)
        {
            TextGrid.Background = new SolidColorBrush(Color.FromArgb(255, 69, 47, 48));
            CloseGrid.Background = new SolidColorBrush(Color.FromArgb(255, 69, 47, 48));
        }
        public void TextMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        public void CloseMouseEnterAction(object sender, MouseEventArgs e)
        {
            CloseGrid.Background = new SolidColorBrush(Color.FromArgb(255,234,0,41));
        }
        public void CloseMouseLeaveAction(object sender, MouseEventArgs e)
        {
            CloseGrid.Background = new SolidColorBrush(Color.FromArgb(255, 69, 47, 48));
        }
    }
}
