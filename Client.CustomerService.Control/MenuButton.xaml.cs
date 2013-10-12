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

namespace Client.CustomerService.Control
{
    public partial class MenuButton : UserControl
    {
        public MenuButton()
        {
            InitializeComponent();
        }

        #region 鼠标事件

        private void ShowHoverStyle(object sender, EventArgs e)
        {
            bg.Style = (Style)this.Resources["bg_hover"];
            text_content.Style = (Style)this.Resources["t_hover"];
        }

        private void ShowNormalStyle(object sender, EventArgs e)
        {
            bg.Style = (Style)this.Resources["bg_normal"];
            text_content.Style = (Style)this.Resources["t_normal"];
        }

        private void OnClick(object sender, EventArgs e)
        {
            if (Click != null)
            {
                Click(this, new EventArgs());
            }
            if (Command != null)
            {
                if (!Command.CanExecute(Text))
                {
                    return;
                }
                Command.Execute(Text);
            }
        }

        #endregion

        #region 事件和依赖属性

        /// <summary>
        /// 按键被点击的时候将触发的事件
        /// </summary>
        public event EventHandler Click;

        #region 触发命令

        /// <summary>
        /// 命令
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MenuButton), new PropertyMetadata(null));

        #endregion

        #region 显示图标

        /// <summary>
        /// 显示图标
        /// </summary>
        public Icons Icon
        {
            get { return (Icons)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(Icons), typeof(MenuButton)
            , new PropertyMetadata(Icons.List, (d, e) =>
            {
                MenuButton tool = (MenuButton)d;
                string url = string.Format("img/icon_{0}.png", e.NewValue.ToString().ToLower());
                tool.img.Source = new BitmapImage(new Uri(url, UriKind.Relative));
            }));

        #endregion

        #region 显示文本

        /// <summary>
        /// 所要显示的文本
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(MenuButton)
            , new PropertyMetadata("Menu Button", (d, e) =>
            {
                MenuButton tool = (MenuButton)d;
                tool.text_content.Text = e.NewValue.ToString();
            }));

        #endregion

        #endregion

        #region 内嵌枚举

        /// <summary>
        /// 显示图标
        /// </summary>
        public enum Icons
        {
            /// <summary>
            /// 列表
            /// </summary>
            List,
            /// <summary>
            /// 笔
            /// </summary>
            Pen,
            /// <summary>
            /// 女人
            /// </summary>
            Women,
            /// <summary>
            /// 文件
            /// </summary>
            Files
        }

        #endregion
    }
}
