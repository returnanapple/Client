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
using Client.CustomerService.Framework.PicService;
using System.IO;
using System.Text.RegularExpressions;

namespace Client.CustomerService.Control
{
    public partial class MessageShowTool : UserControl
    {
        public MessageShowTool()
        {
            InitializeComponent();
        }

        #region 依赖属性

        public bool IsOwn
        {
            get { return (bool)GetValue(IsOwnProperty); }
            set { SetValue(IsOwnProperty, value); }
        }

        public static readonly DependencyProperty IsOwnProperty =
            DependencyProperty.Register("IsOwn", typeof(bool), typeof(MessageShowTool)
            , new PropertyMetadata(false, (d, e) =>
            {
                MessageShowTool tool = (MessageShowTool)d;
                string key = (bool)e.NewValue == true ? "self" : "other";
                Style s = (Style)tool.Resources[key];
                tool.text_username.Style = s;
                tool.text_date.Style = s;
                tool.text_time.Style = s;
            }));

        public string MessageText
        {
            get { return (string)GetValue(MessageTextProperty); }
            set { SetValue(MessageTextProperty, value); }
        }

        public static readonly DependencyProperty MessageTextProperty =
            DependencyProperty.Register("MessageText", typeof(string), typeof(MessageShowTool)
            , new PropertyMetadata("", (d, e) =>
            {
                MessageShowTool tool = (MessageShowTool)d;
                tool.WriteMessage(e.NewValue.ToString());
            }));

        public DateTime SendTime
        {
            get { return (DateTime)GetValue(SendTimeProperty); }
            set { SetValue(SendTimeProperty, value); }
        }

        public static readonly DependencyProperty SendTimeProperty =
            DependencyProperty.Register("SendTime", typeof(DateTime), typeof(MessageShowTool)
            , new PropertyMetadata(new DateTime(), (d, e) =>
            {
                MessageShowTool tool = (MessageShowTool)d;
                DateTime time = (DateTime)e.NewValue;
                tool.text_date.Text = time.ToShortDateString();
                tool.text_time.Text = time.ToLongTimeString();
            }));

        #endregion

        #region 私有方法

        void WriteMessage(string message)
        {
            #region 处理字符串

            Regex regOfPic = new Regex(@"\[\^pic\]([a-zA-Z0-9]{0,})\[\$pic\]");
            Regex regOfIcon = new Regex(@"\[\^icon\]([a-zA-Z0-9]{0,})\[\$icon\]");
            Regex regOfLineBreak = new Regex(@"\r|\n|\r\n|\n\r");
            List<string> listOfPic = new List<string>();
            List<string> listOfIcon = new List<string>();
            Match mOfPic = regOfPic.Match(message);
            while (mOfPic.Success)
            {
                string t = mOfPic.Groups[1].Value;
                listOfPic.Add(t);
                mOfPic = mOfPic.NextMatch();
            }
            Match mOfIcon = regOfIcon.Match(message);
            while (mOfIcon.Success)
            {
                string t = mOfIcon.Groups[1].Value;
                listOfIcon.Add(t);
                mOfIcon = mOfIcon.NextMatch();
            }
            message = regOfPic.Replace(message, ",[==>pic<==],");
            message = regOfIcon.Replace(message, ",[==>icon<==],");
            message = regOfLineBreak.Replace(message, ",[==>lineBreak<==],");

            #endregion

            #region 生成数据集

            List<TClass> tClass = new List<TClass>();
            List<string> values = message.Split(new char[] { ',' }).ToList();
            int numOfPic = 0;
            int numOfIcon = 0;
            for (int i = 0; i < values.Count; i++)
            {
                if (values[i] == "[==>pic<==]")
                {
                    tClass.Add(new TClass(TEnum.pic, listOfPic[numOfPic]));
                    numOfPic++;
                }
                else if (values[i] == "[==>icon<==]")
                {
                    tClass.Add(new TClass(TEnum.icon, listOfIcon[numOfIcon]));
                    numOfIcon++;
                }
                else if (values[i] == "[==>lineBreak<==]")
                {
                    tClass.Add(new TClass(TEnum.lineBreak, ""));
                }
                else
                {
                    tClass.Add(new TClass(TEnum.text, values[i]));
                }
            }

            #endregion

            for (int i = 0; i < tClass.Count; i++)
            {
                object obj = tClass[i].GetControl();
                body.Items.Add(obj);
            }
        }

        #endregion

        #region 内嵌类型

        class TClass
        {
            TEnum Type { get; set; }

            string Value { get; set; }

            public TClass(TEnum type, string value)
            {
                this.Type = type;
                this.Value = value;
            }

            public object GetControl()
            {
                if (Type == TEnum.text)
                {
                    return GetText(Value);
                }
                else if (Type == TEnum.icon)
                {
                    return GetIcon(Value);
                }
                else if (Type == TEnum.pic)
                {
                    return GetPic(Value);
                }
                else
                {
                    return GetLineBreak();
                }
            }

            #region 私有方法

            TextBlock GetText(string input)
            {
                TextBlock tb = new TextBlock();
                tb.Text = input;
                return tb;
            }

            Image GetIcon(string input)
            {
                Image img = new Image();
                string path = string.Format("icon/{0}.png", input);
                img.Source = new BitmapImage(new Uri(path, UriKind.Relative));
                img.Width = 60;
                return img;
            }

            Image GetPic(string input)
            {
                Image img = new Image();
                img.Source = new BitmapImage(new Uri("img/icon_files.png", UriKind.Relative));
                img.Width = 20;
                img.MaxWidth = 600;

                PicServiceClient client = new PicServiceClient();
                client.DownloadCompleted += (sender, e) =>
                {
                    Stream s = new MemoryStream(e.Result);
                    BitmapImage bi = new BitmapImage();
                    bi.SetSource(s);
                    img.Source = bi;
                    img.Width = bi.PixelWidth > 480 ? 480 : bi.PixelWidth;
                };
                client.DownloadAsync(input);

                return img;
            }

            Grid GetLineBreak()
            {
                Grid grid = new Grid();
                grid.Width = 2000;
                grid.Height = 0;

                return grid;
            }

            #endregion
        }

        enum TEnum
        {
            text,
            icon,
            pic,
            lineBreak
        }

        #endregion
    }
}
