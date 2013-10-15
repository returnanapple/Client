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
    public partial class ScreenShotControl : UserControl
    {
        public ScreenShotControl(UIElement rootVisual)
        {
            InitializeComponent();
            p = (rootVisual as UserControl).Content as Panel;
            WriteableBitmap twb = new WriteableBitmap(rootVisual,null);

            Cover.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
            Cover.Width = twb.PixelWidth;
            Cover.Height = twb.PixelHeight;

            ShotImage.Width = twb.PixelWidth;
            ShotImage.Height = twb.PixelHeight;
            ShotImage.Source = twb;
            p.Children.Add(this);
        }
        Panel p;

        public void MouseLeftButtonDownAction(object sender, MouseButtonEventArgs e)
        {
            double tx;
            double ty;
            tx = e.GetPosition(Cover).X;
            ty = e.GetPosition(Cover).Y;
            ShotCanvas.SetValue(Canvas.LeftProperty, tx);
            ShotCanvas.SetValue(Canvas.TopProperty, ty);
        }

    }
}
