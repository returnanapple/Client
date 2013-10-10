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

namespace Client.CustomerService.Control
{
    /// <summary>
    /// 遮蔽层控件（显示 Loading...）
    /// </summary>
    public partial class CoverFloorTool : UserControl
    {
        /// <summary>
        /// 实例化一个新的遮蔽层控件（显示 Loading...）
        /// </summary>
        public CoverFloorTool()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 启动故事版
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">监视对象</param>
        private void StartStoryboard(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = (Storyboard)this.Resources["s_loading"];
            storyboard.Begin();
        }

        /// <summary>
        /// 可见
        /// </summary>
        public bool CanSee
        {
            get { return (bool)GetValue(CanSeeProperty); }
            set { SetValue(CanSeeProperty, value); }
        }

        public static readonly DependencyProperty CanSeeProperty =
            DependencyProperty.Register("CanSee", typeof(bool), typeof(CoverFloorTool)
            , new PropertyMetadata(false, (d, e) =>
            {
                CoverFloorTool tool = (CoverFloorTool)d;
                tool.Visibility = (bool)e.NewValue == true ? Visibility.Visible : Visibility.Collapsed;
            }));
    }
}
