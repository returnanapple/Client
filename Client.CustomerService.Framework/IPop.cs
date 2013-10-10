using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 定义弹窗
    /// </summary>
    public interface IPop
    {
        /// <summary>
        /// 系统消息
        /// </summary>
        IMessage SystemMessage { get; set; }

        /// <summary>
        /// 显示弹窗
        /// </summary>
        void Show();
    }
}
