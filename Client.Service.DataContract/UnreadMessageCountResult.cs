using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Client.Service.DataContract
{
    /// <summary>
    /// 用户未读信息条数
    /// </summary>
    [DataContract]
    public class UnreadMessageCountResult
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string Username { get; set; }

        /// <summary>
        /// 条数
        /// </summary>
        [DataMember]
        public int Count { get; set; }

        /// <summary>
        /// 实例化一个新的用户未读信息条数
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="count">条数</param>
        public UnreadMessageCountResult(string username, int count)
        {
            this.Username = username;
            this.Count = count;
        }
    }
}
