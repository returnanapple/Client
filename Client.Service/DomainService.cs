using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.ServiceModel;

namespace Client.Service
{
    /// <summary>
    /// 域名服务
    /// </summary>
    public class DomainService : IDomainService
    {
        /// <summary>
        /// 获取跨域文件
        /// </summary>
        /// <returns>返回跨域文件</returns>
        public Message GetPolicyFile()
        {
            try
            {
                XElement doc = XElement.Load(@"clientaccesspolicy.xml");
                return Message.CreateMessage(MessageVersion.None, "", doc);
            }
            catch (Exception ex)
            {
                throw new FaultException<string>(ex.Message);
            }
        }
    }
}
