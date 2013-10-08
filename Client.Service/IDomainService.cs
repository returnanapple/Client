using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;

namespace Client.Service
{
    /// <summary>
    /// 定义域名服务
    /// </summary>
    [ServiceContract]
    public interface IDomainService
    {
        /// <summary>
        /// 获取跨域文件
        /// </summary>
        /// <returns>返回跨域文件</returns>
        [OperationContract]
        [WebGet(UriTemplate = "ClientAccessPolicy.xml")]
        Message GetPolicyFile();
    }
}
