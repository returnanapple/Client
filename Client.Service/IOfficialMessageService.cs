using Client.Service.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Client.Service
{
    /// <summary>
    /// 定义官方消息服务
    /// </summary>
    [ServiceContract]
    public interface IOfficialMessageService
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
        /// <param name="username">用户名</param>
        /// <returns>返回包含未读信息的列表的操作结果</returns>
        [OperationContract]
        OperatingResult<List<MessageResult>> GetUnreadMessages(string username);

        /// <summary>
        /// 获取聊天记录的分页列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">每个页面包含信息条数</param>
        /// <returns>返回包含聊天纪录的分页列表的操作结果</returns>
        [OperationContract]
        OperatingResult<PaginationList<MessageResult>> GetMessages(string username, int page, int pageSize);

        /// <summary>
        /// 发送新消息
        /// </summary>
        /// <param name="import">数据集</param>
        /// <returns>返回操作结果</returns>
        [OperationContract]
        OperatingResult Send(SendMessageImport import);
    }
}
