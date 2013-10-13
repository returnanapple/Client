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
        /// 登入
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="onlineStatus">在线状态</param>
        /// <param name="isOfficial">一个布尔值 标识用户是否官方成员</param>
        public static void SetIn(string username, UserOnlineStatus onlineStatus, bool isOfficial = false)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                db.Users.Where(x => x.Username == username
                    && x.IsOfficial == isOfficial && x.Timeout > DateTime.Now).ToList().ForEach(x =>
                    {
                        x.Timeout = DateTime.Now.AddDays(-1);
                    });
                User u = new User(username, onlineStatus, isOfficial);
                db.Users.Add(u);
                db.SaveChanges();
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
            using (Model2DataContext db = new Model2DataContext())
            {
                bool hadUser = db.Users.Any(x => x.Username == username
                    && x.IsOfficial == isOfficial && x.Timeout > DateTime.Now);
                if (!hadUser) { throw new Exception("用户未登录"); }
                User u = db.Users.First(x => x.Username == username
                    && x.IsOfficial == isOfficial && x.Timeout > DateTime.Now);
                u.OnlineStatus = newOnlineStatus;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// 心跳
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="isOfficial">一个布尔值 标识用户是否官方成员</param>
        public static void Heartbeat(string username, bool isOfficial = false)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                bool hadUser = db.Users.Any(x => x.Username == username
                    && x.IsOfficial == isOfficial && x.Timeout > DateTime.Now);
                if (!hadUser) { return; }
                User u = db.Users.First(x => x.Username == username
                    && x.IsOfficial == isOfficial && x.Timeout > DateTime.Now);
                u.Heartbeat();
                db.SaveChanges();
            }
        }
    }
}
