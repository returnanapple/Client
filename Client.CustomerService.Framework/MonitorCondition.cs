using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 监视条件
    /// </summary>
    public class MonitorCondition
    {
        /// <summary>
        /// 目标对象
        /// </summary>
        public Type Sender { get; protected set; }

        /// <summary>
        /// 所要监听的动作的名称
        /// </summary>
        public object ActionName { get; private set; }

        /// <summary>
        /// 所要监听的状态
        /// </summary>
        public object Status { get; protected set; }

        /// <summary>
        /// 监视条件
        /// </summary>
        /// <param name="sender">目标对象</param>
        /// <param name="actionName">所要监听的动作的名称</param>
        /// <param name="status">所要监听的状态</param>
        public MonitorCondition(Type sender, object actionName, object status)
        {
            this.Sender = sender;
            this.ActionName = actionName;
            this.Status = status;
        }
    }
}
