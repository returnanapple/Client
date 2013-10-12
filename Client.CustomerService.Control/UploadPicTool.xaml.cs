using Client.CustomerService.Framework;
using Client.CustomerService.Framework.PicService;
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
using System.IO;
using System.Windows.Media.Imaging;

namespace Client.CustomerService.Control
{
    [Window(Pop.UploadPicWindow)]
    public partial class UploadPicTool : ChildWindow, IPop<string>
    {
        bool hadOpenFile = false;
        string path = "";

        public UploadPicTool()
        {
            InitializeComponent();
        }

        #region 消息

        string state = "";

        public string State
        {
            get { return state; }
            set
            {
                state = value;
            }
        }

        #endregion

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "图片 (*.jpg)|*.jpg",
                Multiselect = false  //不允许多选 
            };
            bool chooseFile = dialog.ShowDialog() == true;
            if (!chooseFile) { return; }
            cover.CanSee = true;

            Stream tStream = dialog.File.OpenRead();
            byte[] t = new byte[tStream.Length];
            tStream.Read(t, 0, (int)tStream.Length);
            PicServiceClient client = new PicServiceClient();
            client.UploadCompleted += (_sender, _e) =>
                {
                    if (!_e.Result.Success)
                    {
                        this.DialogResult = false;
                    }
                    State = _e.Result.Content;
                    cover.CanSee = false;
                    hadOpenFile = true;
                };
            client.UploadAsync(t);

            Stream ts = new MemoryStream(t);
            BitmapImage bi = new BitmapImage();
            bi.SetSource(ts);
            img.ImageSource = bi;
        }

        private void Upload(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

