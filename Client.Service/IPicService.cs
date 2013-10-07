using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Client.Service.DataContract;
using System.IO;

namespace Client.Service
{
    /// <summary>
    /// 定义图片服务
    /// </summary>
    [ServiceContract]
    public interface IPicService
    {
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="picStream">图片的流文件</param>
        /// <returns>返回带图片的存储令牌的操作结果</returns>
        [OperationContract]
        OperatingResult<string> Upload(Stream picStream);

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="token">图片的存储令牌</param>
        /// <returns>返回图片的流文件</returns>
        [OperationContract]
        Stream Download(string token);
    }
}
