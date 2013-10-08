using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Client.Service.DataContract;
using Client.Model;

namespace Client.Service
{
    /// <summary>
    /// 定义用户令牌服务
    /// </summary>
    [ServiceContract]
    public interface IUserService
    {
        /// <summary>
        /// 用户登入
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="onlineStatus">在线状态</param>
        /// <returns>返回操作结果</returns>
        [OperationContract]
        OperatingResult SetIn(string username, UserOnlineStatus onlineStatus);

        /// <summary>
        /// 改变在线状态
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="newOnlineStatus">新的在线状态</param>
        /// <returns>返回操作结果</returns>
        [OperationContract]
        OperatingResult ChangeOnlineStatus(string username, UserOnlineStatus newOnlineStatus);

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>返回包含用户列表的操作结果（如果操作失败列表为空）</returns>
        [OperationContract]
        OperatingResult<List<UserInfoResult>> GetFriends(string username);
    }
}
