using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 定义信息
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// 发布消息的源对象
        /// </summary>
        object Sender { get; }

        /// <summary>
        /// 操作方法
        /// </summary>
        object ActionName { get; }

        /// <summary>
        /// 状态的值
        /// </summary>
        int StatusValue { get; }

        /// <summary>
        /// 信息主体
        /// </summary>
        object Content { get; }

        /// <summary>
        /// 检查该消息是否符合指定的监视条件
        /// </summary>
        /// <param name="condition">监视条件</param>
        /// <returns>返回一个布尔值 标识该消息是否符合指定的监视条件</returns>
        bool Licit(MonitorCondition condition);

        /// <summary>
        /// 处理
        /// </summary>
        /// <param name="newMessage">所要附加于消息的主体部分的新内容</param>
        void Handle(object newContent = null);

        /// <summary>
        /// 设置消息的命令状态
        /// </summary>
        /// <param name="newStatusValue">新状态的值</param>
        /// <param name="newMessage">所要附加于消息的主体部分的新内容</param>
        void SetStatus(int newStatusValue, object newnewContent = null);
    }

    //定义信息的泛型实现
    public interface IMessage<T> : IMessage
    {
        /// <summary>
        /// 状态
        /// </summary>
        T Status { get; }

        /// <summary>
        /// 设置消息的命令状态
        /// </summary>
        /// <param name="newStatus">新状态</param>
        /// <param name="newMessage">所要附加于消息的主体部分的新内容</param>
        void SetStatus(T newStatus, object newContent = null);
    }
}
