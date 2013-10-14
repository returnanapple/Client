using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Client.Service.DataContract
{
    /// <summary>
    /// 登入结果
    /// </summary>
    [DataContract]
    public class SetInResult
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        [DataMember]
        List<UserInfoResult> Users { get; set; }

        /// <summary>
        /// 唯独消息条数列表
        /// </summary>
        [DataMember]
        List<UnreadMessageCountResult> UnreadMessageCounts { get; set; }

        /// <summary>
        /// 实例化一个新的登入结果
        /// </summary>
        /// <param name="users">用户列表</param>
        /// <param name="unreadMessageCounts">唯独消息条数列表</param>
        public SetInResult(List<UserInfoResult> users, List<UnreadMessageCountResult> unreadMessageCounts)
        {
            this.Users = users;
            this.UnreadMessageCounts = unreadMessageCounts;
        }
    }
}
