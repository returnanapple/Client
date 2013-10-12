using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Client.Model;

namespace Client.Service.DataContract
{
    /// <summary>
    /// 聊天信息
    /// </summary>
    [DataContract]
    public class MessageResult
    {
        /// <summary>
        /// 发件人
        /// </summary>
        [DataMember]
        public string From { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        [DataMember]
        public string To { get; set; }

        /// <summary>
        /// 一个布尔值 表示是否自己的发言
        /// </summary>
        [DataMember]
        public bool IsSelf { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        [DataMember]
        public string Content { get; set; }

        /// <summary>
        /// 一个布尔值 标识改信息是否官方信息
        /// </summary>
        [DataMember]
        public bool IsOfficial { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [DataMember]
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 来源地址
        /// </summary>
        [DataMember]
        public string Ip { get; set; }

        /// <summary>
        /// 实例化一个新的聊天信息
        /// </summary>
        /// <param name="message">聊天信息的数据封装</param>
        /// <param name="isSelf">一个布尔值 表示是否自己的发言</param>
        public MessageResult(Message message, bool isSelf)
        {
            this.From = message.From;
            this.To = message.To;
            this.Content = message.Content;
            this.IsOfficial = message.IsOfficial;
            this.SendTime = message.CreatedTime;
            this.IsSelf = isSelf;
            this.Ip = message.Ip;
        }
    }
}
