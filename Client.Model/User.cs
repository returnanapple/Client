using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model
{
    /// <summary>
    /// 用户信息（聊天系统）
    /// </summary>
    public class User
    {
        #region 属性

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 在线状态
        /// </summary>
        public UserOnlineStatus OnlineStatus { get; set; }

        /// <summary>
        /// 登入时间
        /// </summary>
        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 超时时间
        /// </summary>
        public DateTime Timeout { get; set; }

        /// <summary>
        /// 一个布尔值 标识该用户是否官方成员
        /// </summary>
        public bool IsOfficial { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个新的用户信息（聊天系统）
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// 实例化一个新的用户信息（聊天系统）
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="onlineStatus">在线状态</param>
        /// <param name="isOfficial">一个布尔值 标识该用户是否官方成员</param>
        public User(string username, UserOnlineStatus onlineStatus, bool isOfficial = false)
        {
            this.Username = username;
            this.OnlineStatus = onlineStatus;
            this.LoginTime = DateTime.Now;
            this.Timeout = this.LoginTime.AddMinutes(2);
            this.IsOfficial = isOfficial;
        }

        #endregion

        #region 方法

        public void Heartbeat()
        {
            this.Timeout = DateTime.Now.AddMinutes(2);
        }

        #endregion
    }
}
