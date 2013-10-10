using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 标记控件对象为弹窗
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class WindowAttribute : Attribute
    {
        /// <summary>
        /// 弹窗标记
        /// </summary>
        public Pop Pop { get; protected set; }

        /// <summary>
        /// 标记控件对象为弹窗
        /// </summary>
        /// <param name="pop">弹窗标记</param>
        public WindowAttribute(Pop pop = Framework.Pop.NormalPrompt)
        {
            this.Pop = pop;
        }
    }
}
