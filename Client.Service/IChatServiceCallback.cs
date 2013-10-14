using Client.Model;
using Client.Service.DataContract;
using System.ServiceModel;

namespace Client.Service
{
    /// <summary>
    /// 定义聊天服务的回调
    /// </summary>
    public interface IChatServiceCallback
    {
        /// <summary>
        /// 向某用户的未读信息数量加1
        /// </summary>
        /// <param name="username"></param>
        [OperationContract(IsOneWay = true)]
        void AddTheCountOfNewMessageForSomeone(string username);

        /// <summary>
        /// 写信息
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(IsOneWay = true)]
        void WriteMessage(MessageResult message);

        /// <summary>
        /// 改用某用户的在线状态
        /// </summary>
        /// <param name="username"></param>
        /// <param name="onlineStatus"></param>
        [OperationContract(IsOneWay = true)]
        void ChangeOnlineStatus(string username, UserOnlineStatus onlineStatus);
    }
}
