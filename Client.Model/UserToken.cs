using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model
{
    /// <summary>
    /// 用户令牌
    /// </summary>
    public class UserToken
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
        /// 一个布尔值 标识该用户是否正在以官方用户身份登入
        /// </summary>
        public bool IsOfficial { get; set; }

        /// <summary>
        /// UID
        /// </summary>
        public int UID { get; set; }

        /// <summary>
        /// 父用户的UID
        /// </summary>
        public string PID { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个新的用户令牌
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="uid">UID</param>
        /// <param name="pid">父用户的UID</param>
        public UserToken(string username, int uid, string pid)
        {
            this.Username = username;
            this.OnlineStatus = UserOnlineStatus.离线;
            this.LoginTime = DateTime.MinValue;
            this.Timeout = DateTime.MinValue;
            this.IsOfficial = false;
            this.UID = uid;
            this.PID = pid;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 声明用户已经接入系统
        /// </summary>
        /// <param name="isOfficial"></param>
        public void SetIn(bool isOfficial)
        {
            if (this.OnlineStatus == UserOnlineStatus.离线)
            {
                this.OnlineStatus = UserOnlineStatus.在线;
            }
            this.LoginTime = DateTime.Now;
            this.Timeout = this.LoginTime.AddMinutes(1);
            this.IsOfficial = isOfficial;
        }

        /// <summary>
        /// 改变在线状态
        /// </summary>
        /// <param name="newOnlineStatus"></param>
        public void ChangeOnlineStatus(UserOnlineStatus newOnlineStatus)
        {
            this.OnlineStatus = newOnlineStatus;
        }

        /// <summary>
        /// 检查令牌是否已经过期
        /// </summary>
        public void CheckIfTimeOut()
        {
            if (this.Timeout <= DateTime.Now
                && this.OnlineStatus != UserOnlineStatus.离线)
            {
                this.OnlineStatus = UserOnlineStatus.离线;
                this.IsOfficial = false;
                if (TimeoutEventHandler != null)
                {
                    TimeoutEventHandler(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// 保持心跳
        /// </summary>
        public void KeepHeartbeat()
        {
            if (this.Timeout <= DateTime.Now)
            {
                throw new Exception("令牌已经过期");
            }
            this.Timeout = DateTime.Now.AddMinutes(1);
        }

        #endregion

        #region 事件

        public event EventHandler TimeoutEventHandler;

        #endregion
    }
}
