using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Client.Model;

namespace Client.Service.DataContract
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [DataContract]
    public class UserInfoResult
    {
        #region 属性

        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string Username { get; set; }

        /// <summary>
        /// 用户在线状态
        /// </summary>
        [DataMember]
        public UserOnlineStatus OnlineStatus { get; set; }

        /// <summary>
        /// 用户信息的类别
        /// </summary>
        [DataMember]
        public UserInfoType Type { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个新的用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="type">用户的类型</param>
        public UserInfoResult(string username, UserInfoType type)
        {
            this.Username = username;
            this.OnlineStatus = UserOnlineStatus.离线;
            this.Type = type;
        }

        #endregion
    }
}
