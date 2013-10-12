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
    public partial class ChatWindowControl : UserControl
    {
        public ChatWindowControl()
        {
            InitializeComponent();
        }



        public ICommand SendMessageConmmand
        {
            get { return (ICommand)GetValue(SendMessageConmmandProperty); }
            set { SetValue(SendMessageConmmandProperty, value); }
        }
        public static readonly DependencyProperty SendMessageConmmandProperty =
            DependencyProperty.Register("SendMessageConmmand", typeof(ICommand), typeof(ChatWindowControl), new PropertyMetadata(null));



        public void SenderBorderMouseEnterAction(object sender, MouseEventArgs e)
        {
            SenderBorder.BorderThickness = new Thickness(1);
        }
        public void SenderBorderMouseLeaveAction(object sender, MouseEventArgs e)
        {
            SenderBorder.BorderThickness = new Thickness(0);
        }
        public void SenderBorderMouseLeftButtonDownAction(object sender, MouseButtonEventArgs e)
        {
            this.SendMessageConmmand.Execute(null);
        }
    }
}
