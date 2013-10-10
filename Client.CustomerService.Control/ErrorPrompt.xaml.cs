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
    [Window(Pop.ErrorPrompt)]
    public partial class ErrorPrompt : ChildWindow, IPop
    {
        public ErrorPrompt()
        {
            InitializeComponent();
        }

        #region 信息

        IMessage systemMessage = null;

        /// <summary>
        /// 信息
        /// </summary>
        public IMessage SystemMessage
        {
            get
            {
                return systemMessage;
            }
            set
            {
                systemMessage = value;
                text_content.Text = systemMessage.Content.ToString();
            }
        }

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
        /// 反馈弹窗操作结果
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">监视对象</param>
        private void ShowResult(object sender, EventArgs e)
        {
            NormalPrompt tool = (NormalPrompt)sender;
            if (tool.DialogResult != true)
            {
                return;
            }
            if (tool.SystemMessage == null)
            {
                tool.DialogResult = false;
                return;
            }
            tool.SystemMessage.Handle(tool.DialogResult);
            Messager.Default.Send(tool.SystemMessage);
        }
    }
}

