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
    public partial class FriendButton : UserControl
    {
        public FriendButton()
        {
            InitializeComponent();
        }

        public int NewMessageCount
        {
            get { return (int)GetValue(NewMessageCountProperty); }
            set { SetValue(NewMessageCountProperty, value); }
        }
        public static readonly DependencyProperty NewMessageCountProperty =
            DependencyProperty.Register("NewMessageCount", typeof(int), typeof(FriendButton), new PropertyMetadata(0, (d, e) => 
            {
                FriendButton tempX = (FriendButton)d;
                Storyboard tempY = (Storyboard)(tempX.Resources["NewMessageStoryboard"]);
                if ((int)e.NewValue != 0)
                { tempY.Begin(); }
                else
                { tempY.Stop(); }
            }));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(FriendButton), new PropertyMetadata(null));



        public void MouseEnterAction(object sender,MouseEventArgs e)
        {
            MyGrid.Background = new SolidColorBrush(Color.FromArgb(255,252,240,193));//#FFFCF0C1
        }
        public void MouseLeaveAction(object sender, MouseEventArgs e)
        {
            MyGrid.Background = new SolidColorBrush(Color.FromArgb(255, 247, 245, 245));//#FFF7F5F5
        }
        public void MouseLeftButtonDownAction(object sender,MouseButtonEventArgs e)
        {
            Command.Execute(null);
        }
    }
}
