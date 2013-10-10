using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 弹窗标识
    /// </summary>
    public enum Pop
    {
        /// <summary>
        /// 普通提示弹窗
        /// </summary>
        NormalPrompt = 101,
        /// <summary>
        /// 错误提示弹窗
        /// </summary>
        ErrorPrompt = 102,
    }
}
