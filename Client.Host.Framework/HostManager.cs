using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Client.Service;

namespace Client.Host.Framework
{
    /// <summary>
    /// 服务主机的管理者对象
    /// </summary>
    public class HostManager
    {
        static ServiceHost chatHost = new ServiceHost(typeof(ChatService));
        static ServiceHost picHost = new ServiceHost(typeof(PicService));
        static ServiceHost loginHost = new ServiceHost(typeof(OfficialLoginService));
        static ServiceHost domainHost = new ServiceHost(typeof(DomainService));
        static bool running = false;

        /// <summary>
        /// 一个布尔值 标识主机是否在运行
        /// </summary>
        public static bool Running { get { return running; } }

        /// <summary>
        /// 运行
        /// </summary>
        public static void Run()
        {
            if (running) { return; }
            chatHost.Open();
            picHost.Open();
            loginHost.Open();
            domainHost.Open();
            running = true;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            if (!running) { return; }
            chatHost.Close();
            picHost.Close();
            loginHost.Close();
            domainHost.Close();
            Reset();
            running = false;
        }

        /// <summary>
        /// 重置
        /// </summary>
        static void Reset()
        {
            chatHost = new ServiceHost(typeof(ChatService));
            picHost = new ServiceHost(typeof(PicService));
            loginHost = new ServiceHost(typeof(OfficialLoginService));
            domainHost = new ServiceHost(typeof(DomainService));
        }
    }
}
