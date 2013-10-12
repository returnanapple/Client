using Client.CustomerService.Framework;
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
    [Window(Pop.NormalPrompt)]
    public partial class NormalPrompt : ChildWindow, IPop<string>
    {
        public NormalPrompt()
        {
            InitializeComponent();
        }

        #region 消息

        public string State
        {
            get { return (string)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(string), typeof(NormalPrompt)
            , new PropertyMetadata("", (d, e) =>
            {
                NormalPrompt tool = (NormalPrompt)d;
                tool.text_content.Text = e.NewValue.ToString();
            }));

        #endregion

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Enter(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}

