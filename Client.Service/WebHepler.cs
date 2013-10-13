using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

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

        public static string GetAddress(string ip)
        {
            string path = "http://www.ip138.com/ips138.asp?ip=" + ip;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(path);
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0)";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.GetEncoding("gb2312"));
            string html = reader.ReadToEnd();
            stream.Close();

            Regex reg = new Regex("<ul class=\"ul1\"><li>本站主数据：([\\w\\W]*)</li><li>");
            string result = reg.Match(html).Groups[1].Value;

            return result;
        }
    }
}
