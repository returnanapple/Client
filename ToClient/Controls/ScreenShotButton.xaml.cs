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
            UserControl tUC = Application.Current.RootVisual as UserControl;
            Panel tUCContent = tUC.Content as Panel;
            WriteableBitmap tWB = new WriteableBitmap(tUC,null);
            Canvas cover = new Canvas() { Background = new ImageBrush { ImageSource = tWB } };
            tUCContent.Children.Add(cover);
        }
    }
}
