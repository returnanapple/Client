using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.Windows.Controls;
using Client.CustomerService.Framework.UserService;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 首页的视图模型
    /// </summary>
    public class IndexViewModel : ViewModelBase
    {
        #region 私有字段

        string username = "";
        string doingNow = "发送消息";
        string targetUser = "";

        #endregion

        #region 属性

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username
        {
            get
            {
                if (username == "")
                {
                    string dataKeyOfUsername = DataKey.Client_Username.ToString();
                    username = IsolatedStorageSettings.ApplicationSettings[dataKeyOfUsername].ToString();
                }
                return username;
            }
        }

        /// <summary>
        /// 当前正在进行的动作
        /// </summary>
        public string DoingNow
        {
            get { return doingNow; }
            set
            {
                if (doingNow != value)
                {
                    doingNow = value;
                    OnPropertyChanged("DoingNow");
                }
            }
        }

        /// <summary>
        /// 目标用户
        /// </summary>
        public string TargetUser
        {
            get { return targetUser; }
            set
            {
                if (targetUser != value)
                {
                    targetUser = value;
                    OnPropertyChanged("TargetUser");
                }
            }
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        public ObservableCollection<UserInfoModel> Users { get; set; }

        /// <summary>
        /// 安全退出命令
        /// </summary>
        public UniversalCommand LogoutCommand { get; set; }

        /// <summary>
        /// 打开聊天窗口的命令
        /// </summary>
        public UniversalCommand OpenTalkingWindowCommand { get; set; }

        /// <summary>
        /// 选择进行动作的命令
        /// </summary>
        public UniversalCommand ChooseWhatDoingCommand { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 首页的视图模型
        /// </summary>
        public IndexViewModel()
        {
            LogoutCommand = new UniversalCommand(new Action<object>(Logout));
            OpenTalkingWindowCommand = new UniversalCommand(new Action<object>(OpenTalkingWindow));
            ChooseWhatDoingCommand = new UniversalCommand(new Action<object>(ChooseWhatDoing));
            RefreshUserList();
        }

        #endregion

        #region 私有方法

        #region 登出

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="parameter">可选参数</param>
        void Logout(object parameter)
        {
            string p = "该操作将退出客户管理，你是要退出吗？";
            IMessage m = Messager.Default.CreateMessage<LogoutStatus>(this, ActionName.Logout, p);
            Messager.Default.Send(m);
        }

        #endregion

        #region 打开聊天窗口

        public void OpenTalkingWindow(object parameter)
        {
            TargetUser = parameter.ToString();
        }

        #endregion

        #region 选择当前动作

        public void ChooseWhatDoing(object parameter)
        {
            DoingNow = parameter.ToString();
        }

        #endregion

        #region 刷新用户列表

        /// <summary>
        /// 刷新用户列表
        /// </summary>
        void RefreshUserList()
        {
            OfficialUserServiceClient client = new OfficialUserServiceClient();
            client.GetUsersCompleted += WriteUserList;
            client.GetUsersAsync(Username);
        }
        #region 写用户列表

        /// <summary>
        /// 重写用户列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WriteUserList(object sender, GetUsersCompletedEventArgs e)
        {
            if (e.Result.Success)
            {
                if (Users == null) { Users = new ObservableCollection<UserInfoModel>(); }
                lock (Users)
                {
                    List<UserInfoModel> ums = new List<UserInfoModel>();
                    e.Result.Content.ForEach(x =>
                        {
                            UserInfoModel um = new UserInfoModel();
                            um.Username = x.Username;
                            if (x.OnlineStatus == UserOnlineStatus.离线)
                            {
                                um.UserType = UserInfoModel.UserModelType.离线;
                            }
                            else if (x.Type == UserInfoType.客服)
                            {
                                um.UserType = UserInfoModel.UserModelType.客服;
                            }
                            else
                            {
                                um.UserType = UserInfoModel.UserModelType.在线;
                            }
                            if (Users.Any(u => u.Username == x.Username))
                            {
                                um.CountOfNewMessage = Users.Where(u => u.Username == x.Username)
                                    .Select(u => u.CountOfNewMessage).First();
                            }
                            um.OpenTalkingWindowCommand = this.OpenTalkingWindowCommand;
                            ums.Add(um);
                        });
                    Users.Clear();
                    ums.OrderBy(x => x.UserType).ToList().ForEach(x =>
                        {
                            Users.Add(x);
                        });
                }
                OnPropertyChanged("Users");
            }
            else
            {
                IMessage<LogoutStatus> m = Messager.Default
                    .CreateMessage<LogoutStatus>(this, ActionName.Logout) as IMessage<LogoutStatus>;
                m.SetStatus(LogoutStatus.Do);
                Messager.Default.Send(m);
            }
        }

        #endregion

        #endregion

        #endregion

        #region 静态方法

        #region 登出

        /// <summary>
        /// 确认登出
        /// </summary>
        /// <param name="message">系统消息</param>
        [RegisterToMessager(typeof(IndexViewModel), ActionName.Logout, LogoutStatus.WaitForEnter)]
        public static void ConfirmLogout(IMessage message)
        {
            IPop pop = ViewModelService.Current.GetPop(Pop.NormalPrompt);
            pop.SystemMessage = message;
            pop.Show();
        }

        /// <summary>
        /// 执行登出动作
        /// </summary>
        /// <param name="message">系统消息</param>
        [RegisterToMessager(typeof(IndexViewModel), ActionName.Logout, LogoutStatus.Do)]
        public static void DoLogout(IMessage message)
        {
            bool confirm = (bool)message.Content;
            if (!confirm) { return; }
            message.Handle();
            Messager.Default.Send(message);
            string dataKeyOfUsername = DataKey.Client_Username.ToString();
            IsolatedStorageSettings.ApplicationSettings.Remove(dataKeyOfUsername);
            ViewModelService.Current.JumpToDefaultPage();
        }

        #endregion

        #endregion

        #region 内嵌类型

        /// <summary>
        /// 操作名
        /// </summary>
        public enum ActionName
        {
            /// <summary>
            /// 登出
            /// </summary>
            Logout = 0,
        }

        /// <summary>
        /// 登出动作的状态
        /// </summary>
        public enum LogoutStatus
        {
            /// <summary>
            /// 等待确认
            /// </summary>
            WaitForEnter = 0,
            /// <summary>
            /// 执行
            /// </summary>
            Do = 1,
            /// <summary>
            /// 执行完毕
            /// </summary>
            Done = 2
        }

        #endregion
    }
}
