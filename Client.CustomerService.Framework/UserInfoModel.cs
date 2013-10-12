using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfoModel : ModelBase
    {
        #region 私有字段

        string username = "";
        UserModelType userType = UserModelType.客服;
        int countOfNewMessage = 0;

        #endregion

        #region 属性

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username
        {
            get { return username; }
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserModelType UserType
        {
            get { return userType; }
            set
            {
                if (userType != value)
                {
                    userType = value;
                    OnPropertyChanged("UserType");
                }
            }
        }

        /// <summary>
        /// 新消息条数
        /// </summary>
        public int CountOfNewMessage
        {
            get { return countOfNewMessage; }
            set
            {
                if (countOfNewMessage != value)
                {
                    countOfNewMessage = value;
                    OnPropertyChanged("CountOfNewMessage");
                }
            }
        }

        /// <summary>
        /// 打开聊天窗口的命令
        /// </summary>
        public UniversalCommand OpenTalkingWindowCommand { get; set; }

        #endregion

        #region 内嵌枚举

        public enum UserModelType
        {
            客服 = 0,
            在线 = 1,
            离线 = 2
        }

        #endregion
    }
}
