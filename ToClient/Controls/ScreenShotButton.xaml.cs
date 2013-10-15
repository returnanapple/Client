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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToClient.Controls
{
    public partial class ScreenShotButton : UserControl
    {
        public ScreenShotButton()
        {
            InitializeComponent();
        }



        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ScreenShotButton), new PropertyMetadata(null));



        public void MouseEnterAction(object sender, MouseEventArgs e)
        {
            RootBorder.Background = new SolidColorBrush(Color.FromArgb(255, 252, 240, 193));//#FFFCF0C1
        }
        public void MouseLeaveAction(object sender, MouseEventArgs e)
        {
            RootBorder.Background = new SolidColorBrush(Color.FromArgb(255, 247, 245, 245));//#FFF7F5F5
        }

        public void MouseLeftButtonDownAction(object sender, MouseButtonEventArgs e)
        {
            new ScreenShotControl(App.Current.RootVisual,this.Command);
        }
    }
}
