using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Client.Service.DataContract
{
    /// <summary>
    /// 用于发送新的聊天信息的数据集
    /// </summary>
    [DataContract]
    public class SendMessageImport
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
        /// 正文
        /// </summary>
        [DataMember]
        public string Content { get; set; }

        /// <summary>
        /// 一个布尔值 标识改信息是否官方信息
        /// </summary>
        [DataMember]
        public bool IsOfficial { get; set; }
    }
}
