using Client.Service.DataContract;
using System.Collections.Generic;
using System.ServiceModel;
using Client.Model;

namespace Client.Service
{
    /// <summary>
    /// 定义聊天服务
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IChatServiceCallback))]
    public interface IChatService
    {
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="username">自己的用户名</param>
        /// <param name="isOfficia">一个布尔值 表示是否官方用户</param>
        /// <returns>返回登入结果</returns>
        [OperationContract]
        SetInResult RegisterAndGetFriendList(string username, bool isOfficia = false);

        /// <summary>
        /// 改变所要订阅的消息的发布对象
        /// </summary>
        /// <param name="targetUser">目标用户的用户名</param>
        /// <param name="self">自己的用户名</param>
        /// <returns>返回目标对象相对于自己的唯独消息</returns>
        [OperationContract]
        List<MessageResult> ChangeTargetUser(string targetUser, string self);

        /// <summary>
        /// 改变在线状态
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newOnlineStatus"></param>
        [OperationContract]
        void ChangeStatus(string username, UserOnlineStatus newOnlineStatus);

        /// <summary>
        /// 保持心跳
        /// </summary>
        /// <param name="username">用户名</param>
        [OperationContract]
        void KeepHeartbeat(string username);

        /// <summary>
        /// 发送新消息
        /// </summary>
        /// <param name="import">数据集</param>
        [OperationContract]
        void SendMessage(SendMessageImport import);

        /// <summary>
        /// 获取聊天记录的分页列表
        /// </summary>
        /// <param name="_from">发件人</param>
        /// <param name="to">收件人</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>返回聊天记录的分页列表</returns>
        [OperationContract]
        PaginationList<MessageResult> GetMessages(string _from, string to, int pageIndex, int pageSize);
    }
}
