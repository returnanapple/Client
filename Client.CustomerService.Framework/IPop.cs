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
        /// 获取或设置一个值，该值指示是接受还是取消
        /// </summary>
        bool? DialogResult { get; set; }

        /// <summary>
        /// 显示弹窗
        /// </summary>
        void Show();

        /// <summary>
        /// 窗口关闭之后将触发的事件
        /// </summary>
        event EventHandler Closed;
    }

    public interface IPop<T> : IPop
    {
        /// <summary>
        /// 内容
        /// </summary>
        T State { get; set; }
    }
}
