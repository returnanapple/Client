using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model
{
    /// <summary>
    /// 聊天信息
    /// </summary>
    public class Message : RecordingTimeModelBase
    {
        #region 属性

        /// <summary>
        /// 发件人
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 已经阅读
        /// </summary>
        public string Readed { get; set; }

        /// <summary>
        /// 一个布尔值 标识改信息是否官方信息
        /// </summary>
        public bool IsOfficial { get; set; }

        /// <summary>
        /// 来源地址
        /// </summary>
        public string Ip { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个新的聊天信息
        /// </summary>
        public Message()
        {
        }

        /// <summary>
        /// 实例化一个新的聊天信息
        /// </summary>
        /// <param name="_from">发件人</param>
        /// <param name="_to">收件人</param>
        /// <param name="content">正文</param>
        /// <param name="ip">来源地址</param>
        /// <param name="isOfficial">一个布尔值 标识改信息是否官方信息(默认为 false)</param>
        public Message(string _from, string _to, string content, string ip, bool isOfficial = false)
        {
            this.From = _from;
            this.To = _to;
            this.Content = content;
            this.Readed = "";
            this.IsOfficial = isOfficial;
            this.Ip = ip;
        }

        #endregion
    }
}
