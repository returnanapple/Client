using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace Client.Service
{
    /// <summary>
    /// 站点信息的帮助着对象
    /// </summary>
    public class WebHepler
    {
        /// <summary>
        /// 获取客户端通信信息
        /// </summary>
        /// <returns>返回客户端通信信息的封装</returns>
        public static RemoteEndpointMessageProperty GetEndpoint()
        {
            //提供方法执行的上下文环境
            OperationContext context = OperationContext.Current;
            //获取传进的消息属性
            MessageProperties properties = context.IncomingMessageProperties;
            //获取消息发送的远程终结点IP和端口
            RemoteEndpointMessageProperty endpoint = properties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

            return endpoint;
        }
    }
}
