using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Model;
using Client.Service.DataContract;
using System.ServiceModel;

namespace Client.Service
{
    /// <summary>
    /// 定义聊天信息服务
    /// </summary>
    [ServiceContract]
    public interface IMessageService
    {
        /// <summary>
        /// 获取未读信息的数量
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>包含未读信息的数量的操作结果</returns>
        [OperationContract]
        OperatingResult<List<UnreadMessageCountResult>> GetCountOfUnreadMessages(string username);

        /// <summary>
        /// 获取未读信息的列表
        /// </summary>
        /// <param name="_from">发件人</param>
        /// <param name="_to">收件人</param>
        /// <returns>返回包含未读信息的列表的操作结果</returns>
        [OperationContract]
        OperatingResult<List<MessageResult>> GetUnreadMessages(string _from, string _to);

        /// <summary>
        /// 获取聊天记录的分页列表
        /// </summary>
        /// <param name="_from">发件人</param>
        /// <param name="_to">收件人</param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">每个页面包含信息条数</param>
        /// <returns>返回包含聊天纪录的分页列表的操作结果</returns>
        [OperationContract]
        OperatingResult<PaginationList<MessageResult>> GetMessages(string _from, string _to, int page, int pageSize);

        /// <summary>
        /// 发送新消息
        /// </summary>
        /// <param name="import">数据集</param>
        /// <returns>返回操作结果</returns>
        [OperationContract]
        OperatingResult Send(SendMessageImport import);
    }
}
