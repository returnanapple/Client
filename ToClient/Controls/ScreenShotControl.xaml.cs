using System;
using System.Collections.Generic;
using System.IO;
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
        public ScreenShotControl(UIElement rootVisual,ICommand saveCommand)
        {
            InitializeComponent();
            p = (rootVisual as UserControl).Content as Panel;
            WriteableBitmap twb = new WriteableBitmap(rootVisual, null);

            Cover.Background = new SolidColorBrush(Colors.Black) { Opacity = 0.3 };
            Cover.Width = twb.PixelWidth;
            Cover.Height = twb.PixelHeight;

            ShotImage.Width = twb.PixelWidth;
            ShotImage.Height = twb.PixelHeight;
            ShotImage.Source = twb;
            p.Children.Add(this);
            save = saveCommand;
        }
        Panel p;
        bool isPressed = false;
        double x;
        double y;
        ICommand save;
        bool ended = false;
        

        public void MouseLeftButtonDownAction(object sender, MouseButtonEventArgs e)
        {
            double tx;
            double ty;
            tx = e.GetPosition(Cover).X;
            ty = e.GetPosition(Cover).Y;
            x = e.GetPosition(Cover).X;
            y = e.GetPosition(Cover).Y;
            RectClip.Rect = new Rect(x, y, 0, 0);
            isPressed = true;
            ended = true;

        }
        public void MouseMoveAction(object sender, MouseEventArgs e)
        {
            if (!isPressed)
            {
                return;
            }
            double tx;
            double ty;
            tx = e.GetPosition(Cover).X;
            ty = e.GetPosition(Cover).Y;
            if (tx >= x && ty >= y)
            {
                RectClip.Rect = new Rect(x, y, tx - x, ty - y);

            }
            else
            {
                if (tx < x && ty < y)
                {
                    RectClip.Rect = new Rect(tx, ty, x - tx, y - ty);
                }
                else
                {
                    if (tx < x)
                    {
                        RectClip.Rect = new Rect(tx, y, x - tx, ty - y);
                    }
                    else
                    {
                        RectClip.Rect = new Rect(x, ty, tx - x, y - ty);
                    }
                }
            }
        }
        public void MouseLeftButtonUpAction(object sender, MouseButtonEventArgs e)
        {
            if (!ended)
            { return; }
            isPressed = false;
            WriteableBitmap twb = new WriteableBitmap(ShotGrid, null);
            int width = twb.PixelWidth;
            int height = twb.PixelHeight;
            int bands = 3;
            byte[][,] raster = new byte[bands][,];
            for (int i = 0; i < bands; i++)
            {
                raster[i] = new byte[width, height];
            }
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    int pixel = twb.Pixels[width*row+column];
                    raster[0][column, row] = (byte)(pixel >> 16);
                    raster[1][column, row] = (byte)(pixel >> 8);
                    raster[2][column, row] = (byte)pixel;
                }
            }

            FluxJpeg.Core.ColorModel model = new FluxJpeg.Core.ColorModel { colorspace = FluxJpeg.Core.ColorSpace.RGB };
            FluxJpeg.Core.Image img = new FluxJpeg.Core.Image(model, raster);
            MemoryStream stream = new MemoryStream();
            FluxJpeg.Core.Encoder.JpegEncoder encoder = new FluxJpeg.Core.Encoder.JpegEncoder(img, 100, stream);
            encoder.Encode();
            stream.Seek(0, SeekOrigin.Begin);
            byte[] binaryData = stream.ToArray();
            save.Execute(binaryData);
            p.Children.Remove(this);
            ended = false;
        }
        public void MouseLeaveAction(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }
    }
}
