using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 信使
    /// </summary>
    public class Messager
    {
        #region 默认对象

        static Messager _Default = new Messager();

        /// <summary>
        /// 默认对象
        /// </summary>
        public static Messager Default
        {
            get
            {
                return _Default;
            }
        }

        #endregion

        #region 私有变量

        /// <summary>
        /// 监听者
        /// </summary>
        Dictionary<MonitorCondition, RecipientDelegate> _registers
            = new Dictionary<MonitorCondition, RecipientDelegate>();

        #endregion

        #region 实例方法

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            assembly.GetTypes().Where(_type => _type.GetMethods()
                .Any(_method => _method.GetCustomAttributes(true)
                    .Any(_attribute => _attribute is RegisterToMessagerAttribute)
                    )
                ).ToList().ForEach(_type =>
                    {
                        RegisterMethod(_type);
                    });
        }
        #region 注册监听方法

        /// <summary>
        /// 将目标方法注册到监听者列表
        /// </summary>
        /// <param name="type">目标类型</param>
        void RegisterMethod(Type type)
        {
            type.GetMethods().Where(_method => _method.GetCustomAttributes(true)
                .Any(_attribute => _attribute is RegisterToMessagerAttribute)
                ).ToList().ForEach(_method =>
                    {
                        RegisterToMessagerAttribute attribute = _method.GetCustomAttributes(true)
                            .Where(_attribute => _attribute is RegisterToMessagerAttribute)
                            .Single() as RegisterToMessagerAttribute;
                        Messager.Default.RegisterRecipients(attribute.Sender
                            , attribute.ActionName
                            , attribute.Status
                            , (message) =>
                                {
                                    object[] objs = new object[1] { message };
                                    _method.Invoke(null, objs);
                                }
                            , true);

                    });
        }

        #endregion

        /// <summary>
        /// 注册监听信息
        /// </summary>
        /// <param name="sender">所要监听的对象</param>
        /// <param name="actionName">所要监听的动作的名称</param>
        /// <param name="status">所要监听的状态</param>
        /// <param name="action">所要执行的动作</param>
        /// <param name="temporarily">一个布尔值 标识是否临时监听（默认为true）</param>
        public void RegisterRecipients(Type sender, object actionName, object status, RecipientDelegate action
            , bool temporarily = true)
        {
            MonitorCondition condition = new MonitorCondition(sender, actionName, status);
            _registers.Add(condition, action);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public void Send(IMessage message)
        {
            var t = _registers.Where(x => message.Licit(x.Key))
                 .Select(x => x.Value)
                 .ToList();
            t.ForEach(hander => { hander(message); });
        }

        /// 创建一个新的默认格式的消息
        /// </summary>
        /// <typeparam name="T">泛型（应该为枚举）</typeparam>
        /// <param name="sender">发布消息的源对象</param>
        /// <param name="actionName">操作方法</param>
        /// <param name="content">主体</param>
        /// <returns>返回一个新的默认格式的消息</returns>
        public IMessage CreateMessage<T>(object sender, object actionName, object content = null)
        {
            return new Message<T>(sender, actionName, content);
        }

        #endregion

        #region 内嵌类型

        /// <summary>
        /// 默认信息封装
        /// </summary>
        class Message<T> : IMessage<T>, IMessage
        {
            #region 属性

            /// <summary>
            /// 发布消息的源对象
            /// </summary>
            public object Sender { get; protected set; }

            /// <summary>
            /// 操作方法
            /// </summary>
            public object ActionName { get; protected set; }

            /// <summary>
            /// 状态的值
            /// </summary>
            public int StatusValue { get; protected set; }

            /// <summary>
            /// 状态
            /// </summary>
            public T Status
            {
                get { return (T)Enum.ToObject(typeof(T), StatusValue); }
            }

            /// <summary>
            /// 主体
            /// </summary>
            public object Content { get; protected set; }

            #endregion

            #region 构造方法

            /// <summary>
            /// 实例化一个新的默认信息封装
            /// </summary>
            /// <param name="sender">发布消息的源对象</param>
            /// <param name="actionName">操作方法</param>
            /// <param name="content">主体</param>
            public Message(object sender, object actionName, object content)
            {
                this.Sender = sender;
                this.ActionName = actionName;
                this.StatusValue = 0;
                this.Content = content;
            }

            #endregion

            #region 方法

            /// <summary>
            /// 检查该消息是否符合指定的监视条件
            /// </summary>
            /// <param name="condition">监视条件</param>
            /// <returns>返回一个布尔值 标识该消息是否符合指定的监视条件</returns>
            public bool Licit(MonitorCondition condition)
            {
                bool r1 = this.Sender.GetType() == condition.Sender;
                bool r2 = this.ActionName.Equals(condition.ActionName);
                bool r3 = this.StatusValue == (int)condition.Status;
                return r1 && r2 && r3;
            }

            /// <summary>
            /// 处理
            /// </summary>
            /// <param name="newMessage">所要附加于消息的主体部分的新内容</param>
            public void Handle(object newContent = null)
            {
                this.Content = newContent;
                this.StatusValue += 1;
            }

            /// <summary>
            /// 设置消息的命令状态
            /// </summary>
            /// <param name="newStatusValue">新状态的值</param>
            /// <param name="newMessage">所要附加于消息的主体部分的新内容</param>
            public void SetStatus(int newStatusValue, object newContent = null)
            {
                this.StatusValue = newStatusValue;
                this.Content = newContent;
            }

            /// <summary>
            /// 设置消息的命令状态
            /// </summary>
            /// <param name="newStatus">新状态</param>
            /// <param name="newMessage">所要附加于消息的主体部分的新内容</param>
            public void SetStatus(T newStatus, object newContent = null)
            {
                this.StatusValue = (int)(object)newStatus;
                this.Content = newContent;
            }

            #endregion
        }

        #endregion
    }
}
