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
using Client.CustomerService.Framework;
using System.Windows.Data;

namespace Client.CustomerService
{
    [View(Page.IndexPage, "首页", typeof(IndexViewModel), false)]
    public partial class Index : UserControl
    {
        public Index()
        {
            InitializeComponent();
            BingSomthing();
        }

        #region 依赖属性

        public double ShowHeight
        {
            get { return (double)GetValue(ShowHeightProperty); }
            set { SetValue(ShowHeightProperty, value); }
        }

        public static readonly DependencyProperty ShowHeightProperty =
            DependencyProperty.Register("ShowHeight", typeof(double), typeof(Index)
            , new PropertyMetadata(0.0, (d, e) =>
                {
                    Index tool = (Index)d;
                    tool.sv.ScrollToVerticalOffset((double)e.NewValue);
                }));

        #endregion

        #region 私有方法

        void BingSomthing()
        {
            Binding binding = new Binding("ExtentHeight") { Source = this.sv };
            this.SetBinding(Index.ShowHeightProperty, binding);
        }

        #endregion
    }
}
