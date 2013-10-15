using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace ToClient.Controls
{
    public partial class ChatWindowControl : UserControl
    {
        public ChatWindowControl()
        {
            InitializeComponent();
            BingSomthing();
        }


        #region 依赖属性
        /// <summary>
        /// 发送信息命令
        /// </summary>
        public ICommand SendMessageConmmand
        {
            get { return (ICommand)GetValue(SendMessageConmmandProperty); }
            set { SetValue(SendMessageConmmandProperty, value); }
        }
        public static readonly DependencyProperty SendMessageConmmandProperty =
            DependencyProperty.Register("SendMessageConmmand", typeof(ICommand), typeof(ChatWindowControl), new PropertyMetadata(null));

        /// <summary>
        /// 关闭当前聊天命令
        /// </summary>
        public ICommand CloseCurrentChatCommand
        {
            get { return (ICommand)GetValue(CloseCurrentChatCommandProperty); }
            set { SetValue(CloseCurrentChatCommandProperty, value); }
        }
        public static readonly DependencyProperty CloseCurrentChatCommandProperty =
            DependencyProperty.Register("CloseCurrentChatCommand", typeof(ICommand), typeof(ChatWindowControl), new PropertyMetadata(null));

        public double ShowHeight
        {
            get { return (double)GetValue(ShowHeightProperty); }
            set { SetValue(ShowHeightProperty, value); }
        }

        public static readonly DependencyProperty ShowHeightProperty =
            DependencyProperty.Register("ShowHeight", typeof(double), typeof(ChatWindowControl)
            , new PropertyMetadata(0.0, (d, e) =>
            {
                ChatWindowControl tool = (ChatWindowControl)d;
                tool.sv.ScrollToVerticalOffset((double)e.NewValue);
            }));
        #endregion

        #region 发送按键
        public void SenderBorderMouseEnterAction(object sender, MouseEventArgs e)
        {
            SenderBorder.BorderThickness = new Thickness(1);
        }
        public void SenderBorderMouseLeaveAction(object sender, MouseEventArgs e)
        {
            SenderBorder.BorderThickness = new Thickness(0);
        }
        public void SenderBorderMouseLeftButtonDownAction(object sender, MouseButtonEventArgs e)
        {
            this.SendMessageConmmand.Execute(null);
        }
        #endregion

        #region 关闭按键
        public void CloseBorderMouseEnterAction(object sender, MouseEventArgs e)
        {
            CloseBorder.BorderThickness = new Thickness(1);
        }
        public void CloseBorderMouseLeaveAction(object sender, MouseEventArgs e)
        {
            CloseBorder.BorderThickness = new Thickness(0);
        }
        public void CloseBorderMouseLeftButtonDownAction(object sender, MouseButtonEventArgs e)
        {
            this.CloseCurrentChatCommand.Execute(null);
        }
        #endregion

        #region 私有方法

        void BingSomthing()
        {
            Binding binding = new Binding("ExtentHeight") { Source = this.sv };
            this.SetBinding(ChatWindowControl.ShowHeightProperty, binding);
        }

        #endregion
    }
}
