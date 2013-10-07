using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Model;
using System.Timers;

namespace Client.Model.Manager
{
    /// <summary>
    /// 用户令牌的管理者对象
    /// </summary>
    public class UserManager
    {
        /// <summary>
        /// 令牌池（只读）
        /// </summary>
        public static List<User> Pond
        {
            get { return pond; }
        }
        #region 令牌池
        static List<User> pond = new List<User>();
        #endregion

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="onlineStatus">在线状态</param>
        /// <param name="isOfficial">一个布尔值 标识用户是否官方成员</param>
        public static void SetIn(string username, UserOnlineStatus onlineStatus, bool isOfficial = false)
        {
            lock (pond)
            {
                pond.RemoveAll(x => x.Username == username && x.IsOfficial == isOfficial);
                User userInfo = new User(username, onlineStatus, isOfficial);
                pond.Add(userInfo);
            }
        }

        /// <summary>
        /// 改变在线状态
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="newOnlineStatus">新的在线状态</param>
        /// <param name="isOfficial">一个布尔值 标识用户是否官方成员</param>
        public static void ChangeOnlineStatus(string username, UserOnlineStatus newOnlineStatus, bool isOfficial = false)
        {
            lock (pond)
            {
                User userInfo = pond.Single(x => x.Username == username && x.IsOfficial == isOfficial);
                userInfo.OnlineStatus = newOnlineStatus;
            }
        }

        /// <summary>
        /// 初始化令牌池
        /// </summary>
        public static void Initialize()
        {
            const int cleanupInterval = 2000;

            Timer timer = new Timer(cleanupInterval);
            timer.Elapsed += PurgeExpired;
            timer.Start();
        }
        #region 清理过期令牌
        static void PurgeExpired(object sender, ElapsedEventArgs e)
        {
            lock (pond)
            {
                pond.RemoveAll(x => x.Timeout <= DateTime.Now);
            }
        }
        #endregion
    }
}
