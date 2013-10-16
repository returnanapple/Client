using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Service.Aid;
using Client.Model;
using Client.Service.DataContract;

namespace Client.Service
{
    /// <summary>
    /// 池
    /// </summary>
    public class MyPond
    {
        #region 默认对象

        static MyPond _default = new MyPond();

        /// <summary>
        /// 默认对象
        /// </summary>
        public static MyPond Default
        {
            get { return _default; }
            set { _default = value; }
        }

        #endregion

        #region 用户池

        /// <summary>
        /// 用户令牌的集合
        /// </summary>
        public List<UserTokenAndCallback> Tokens { get; private set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个新的池
        /// </summary>
        public MyPond()
        {
            Tokens = new List<UserTokenAndCallback>();
            using (MainDatadbmlDataContext db = new MainDatadbmlDataContext())
            {
                db.UserInfo.ToList().ForEach(x =>
                    {
                        UserToken token = new UserToken(x.UserID, x.ID, x.ParentUID);
                        token.TimeoutEventHandler += RemoveCallback;
                        UserTokenAndCallback t = new UserTokenAndCallback
                        {
                            Token = token,
                            TargetUser = "",
                            Callback = null
                        };
                        Tokens.Add(t);
                    });
            }
            StartTimeline();
        }
        #region 移除客户端回调和关注用户

        /// <summary>
        /// 移除客户端回调关注用户
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">监视对象</param>
        void RemoveCallback(object sender, EventArgs e)
        {
            UserToken token = (UserToken)sender;
            var t = GetUser(token.Username);
            t.TargetUser = "";
            t.Callback = null;
            Tokens.Where(x => x.Token.UID.ToString() == t.Token.PID
                || x.Token.PID == t.Token.UID.ToString()
                || x.Token.IsOfficial)
                .ToList()
                .ForEach(x =>
                {
                    CallStatusChanged(x, t.Token.Username, UserOnlineStatus.离线);
                });
        }

        #endregion

        #endregion

        #region 方法

        /// <summary>
        /// 用户登入
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="isOfficial">一个布尔值 标识用户是否以官方身份登入</param>
        /// <param name="callback">客户端回调</param>
        public void SetIn(string username, bool isOfficial, IChatServiceCallback callback)
        {
            UserTokenAndCallback t = GetUser(username);
            t.Token.SetIn(isOfficial);
            t.Callback = callback;

            Tokens.Where(x => x.Token.UID.ToString() == t.Token.PID
                || x.Token.PID == t.Token.UID.ToString()
                || x.Token.IsOfficial)
                .ToList()
                .ForEach(x =>
                    {
                        CallStatusChanged(x, username, UserOnlineStatus.在线);
                    });
        }

        /// <summary>
        /// 改变目标用户的在线状态
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="newOnlineStatus">新状态</param>
        public void ChangeOnlieStatus(string username, UserOnlineStatus newOnlineStatus)
        {
            UserTokenAndCallback t = GetUser(username);
            t.Token.ChangeOnlineStatus(newOnlineStatus);

            Tokens.Where(x => x.Token.UID.ToString() == t.Token.PID
                || x.Token.PID == t.Token.UID.ToString()
                || x.Token.IsOfficial)
                .ToList()
                .ForEach(x =>
                {
                    CallStatusChanged(x, username, newOnlineStatus);
                });
        }
        #region 通知用户有好友改变了在线状态

        void CallStatusChanged(UserTokenAndCallback input, string targetUser, UserOnlineStatus newOnlineStatus)
        {
            try
            {
                var t = GetUser(targetUser);
                input.Callback.ChangeOnlineStatus(targetUser, newOnlineStatus, t.Token.IsOfficial);
            }
            catch (Exception)
            {
                RemoveToken(input.Token.Username);
            }
        }

        #endregion

        /// <summary>
        /// 保持心跳
        /// </summary>
        /// <param name="username">用户名</param>
        public void KeepHeartbeat(string username)
        {
            UserTokenAndCallback t = GetUser(username);
            t.Token.KeepHeartbeat();
        }

        /// <summary>
        /// 改变关注的用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="newTargetUser">所要关注的用户</param>
        public void ChangeTargetUser(string username, string newTargetUser)
        {
            UserTokenAndCallback t = GetUser(username);
            t.TargetUser = newTargetUser;
        }

        /// <summary>
        /// 移除用户令牌
        /// </summary>
        /// <param name="username">用户名</param>
        public void RemoveToken(string username)
        {
            UserTokenAndCallback t = GetUser(username);
            t.Token.Timeout = DateTime.Now.AddDays(-1);
            t.Token.CheckIfTimeOut();

        }

        #region 获取指定用户

        /// <summary>
        /// 获取指定用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>返回指定用户</returns>
        UserTokenAndCallback GetUser(string username)
        {
            if (!Tokens.Any(x => x.Token.Username == username))
            {
                throw new Exception("用户名不存在");
            }
            return Tokens.First(x => x.Token.Username == username);
        }

        #endregion

        #endregion

        #region 时间线任务

        /// <summary>
        /// 开启时间线任务
        /// </summary>
        public void StartTimeline()
        {
            System.Timers.Timer timer = new System.Timers.Timer(2000);
            timer.Elapsed += CheckIfTimeout;
            timer.Start();
        }

        /// <summary>
        /// 检查令牌是否超时
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">监视对象</param>
        void CheckIfTimeout(object sender, System.Timers.ElapsedEventArgs e)
        {
            Tokens.ForEach(x =>
                {
                    x.Token.CheckIfTimeOut();
                });
        }

        #endregion

        #region 内置类型

        /// <summary>
        /// 用户令牌和对应的客户端回调
        /// </summary>
        public class UserTokenAndCallback
        {
            /// <summary>
            /// 用户令牌
            /// </summary>
            public UserToken Token { get; set; }

            /// <summary>
            /// 关注的用户
            /// </summary>
            public string TargetUser { get; set; }

            /// <summary>
            /// 客户端回调
            /// </summary>
            public IChatServiceCallback Callback { get; set; }
        }

        #endregion

        #region 获取用户列表

        /// <summary>
        /// 获取好友列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="isOfficia">一个布尔值 标识该用户是否使用官方身份登入</param>
        /// <returns>返回好友列表</returns>
        public List<UserInfoResult> GetFriendList(string username, bool isOfficia)
        {
            return isOfficia
                ? GetFriendListFoCustomerService(username)
                : GetFriendListForClient(username);
        }
        #region 附属方法

        /// <summary>
        /// 获取普通用户的好友列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>返回普通用户的好友列表</returns>
        List<UserInfoResult> GetFriendListForClient(string username)
        {
            List<UserInfoResult> result = new List<UserInfoResult>();
            var t = GetUser(username);

            #region 客服
            Tokens.Where(x => x.Token.IsOfficial && x.Token.OnlineStatus != UserOnlineStatus.离线).ToList()
                .ForEach(x =>
                {
                    UserInfoResult uir = new UserInfoResult
                    {
                        Username = x.Token.Username,
                        OnlineStatus = UserOnlineStatus.在线,
                        Type = UserInfoType.客服
                    };
                    result.Add(uir);
                });
            #endregion
            #region 上级
            Tokens.Where(x => x.Token.UID.ToString() == t.Token.PID).ToList()
                .ForEach(x =>
                {
                    UserInfoResult uir = new UserInfoResult
                    {
                        Username = x.Token.Username,
                        OnlineStatus = x.Token.OnlineStatus,
                        Type = UserInfoType.上级
                    };
                    result.Add(uir);
                });
            #endregion
            #region 下级
            Tokens.Where(x => x.Token.PID == t.Token.UID.ToString()).ToList()
                .ForEach(x =>
                {
                    UserInfoResult uir = new UserInfoResult
                    {
                        Username = x.Token.Username,
                        OnlineStatus = x.Token.OnlineStatus,
                        Type = UserInfoType.下级
                    };
                    result.Add(uir);
                });
            #endregion

            return result;
        }

        /// <summary>
        /// 获取完整的用户列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>返回完整的用户列表</returns>
        List<UserInfoResult> GetFriendListFoCustomerService(string username)
        {
            var t = GetUser(username);
            if (!t.Token.IsOfficial) { throw new Exception("你不是客服人员"); }
            List<UserInfoResult> result = new List<UserInfoResult>();
            Tokens.Where(x => x.Token.Username != username).ToList().ForEach(x =>
                {
                    UserInfoResult uir = new UserInfoResult
                    {
                        Username = x.Token.Username,
                        OnlineStatus = x.Token.OnlineStatus,
                        Type = x.Token.IsOfficial ? UserInfoType.客服 : UserInfoType.用户
                    };
                    result.Add(uir);
                });
            return result;
        }

        #endregion

        #endregion
    }
}
