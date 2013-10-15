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
    }
}
