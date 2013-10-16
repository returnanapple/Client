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
            ToClientV.MySelf.Visibility = System.Windows.Visibility.Collapsed;
            UIElement rootUI = App.Current.RootVisual;
            WriteableBitmap wb = new WriteableBitmap(rootUI, null);
            int width = wb.PixelWidth;
            int height = wb.PixelHeight;
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
                    int pixel = wb.Pixels[width * row + column];
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

            Command.Execute(binaryData);
            ToClientV.MySelf.Visibility = System.Windows.Visibility.Visible;

        }
    }
}
