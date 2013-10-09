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
        static ServiceHost messageHost = new ServiceHost(typeof(MessageService));
        static ServiceHost userHost = new ServiceHost(typeof(UserService));
        static ServiceHost picHost = new ServiceHost(typeof(PicService));
        static ServiceHost officialMessageService = new ServiceHost(typeof(OfficialMessageService));
        static ServiceHost officialUserService = new ServiceHost(typeof(OfficialUserService));
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
            messageHost.Open();
            userHost.Open();
            picHost.Open();
            officialMessageService.Open();
            officialUserService.Open();
            domainHost.Open();
            running = true;
        }

        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            if (!running) { return; }
            messageHost.Close();
            userHost.Close();
            picHost.Close();
            officialMessageService.Close();
            officialUserService.Close();
            domainHost.Close();
            Reset();
            running = false;
        }

        /// <summary>
        /// 重置
        /// </summary>
        static void Reset()
        {
            messageHost = new ServiceHost(typeof(MessageService));
            userHost = new ServiceHost(typeof(UserService));
            picHost = new ServiceHost(typeof(PicService));
            domainHost = new ServiceHost(typeof(DomainService));
            officialMessageService = new ServiceHost(typeof(OfficialMessageService));
            officialUserService = new ServiceHost(typeof(OfficialUserService));
        }
    }
}
