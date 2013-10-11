using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO.IsolatedStorage;
using System.Windows.Controls;
using Client.CustomerService.Framework.UserService;
using Client.CustomerService.Framework.MessageService;
using System.Windows.Threading;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 首页的视图模型
    /// </summary>
    public class IndexViewModel : ViewModelBase
    {
        #region 网络链接

        OfficialUserServiceClient UserClient { get; set; }
        OfficialMessageServiceClient MessageClient { get; set; }

        #endregion

        #region 私有字段

        string username = "";
        string doingNow = "发送消息";
        string targetUser = "";
        string messageValue = "";
        int pageInde = 0;
        int allPage = 0;
        GridLength talkToolHeight = new GridLength(220);

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
        /// 将要发送的消息主体
        /// </summary>
        public string MessageValue
        {
            get { return messageValue; }
            set
            {
                if (messageValue != value)
                {
                    messageValue = value;
                    OnPropertyChanged("MessageValue");
                }
            }
        }

        /// <summary>
        /// 页码（聊天纪录）
        /// </summary>
        public int PageIndex
        {
            get { return pageInde; }
            set
            {
                if (pageInde != value)
                {
                    pageInde = value;
                    OnPropertyChanged("PageIndex");
                }
            }
        }

        /// <summary>
        /// 所有页数（聊天纪录）
        /// </summary>
        public int AllPage
        {
            get { return allPage; }
            set
            {
                if (allPage != value)
                {
                    allPage = value;
                    OnPropertyChanged("AllPage");
                }
            }
        }

        /// <summary>
        /// 聊天工具高度
        /// </summary>
        public GridLength TalkToolHeight
        {
            get { return talkToolHeight; }
            set
            {
                if (talkToolHeight != value)
                {
                    talkToolHeight = value;
                    OnPropertyChanged("TalkToolHeight");
                }
            }
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        public ObservableCollection<UserInfoModel> Users { get; set; }

        /// <summary>
        /// 消息列表
        /// </summary>
        public ObservableCollection<MessageResult> Messages { get; set; }

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

        /// <summary>
        /// 发送新消息的命令
        /// </summary>
        public UniversalCommand SendMessageCommand { get; set; }

        /// <summary>
        /// 显示表情选择窗口的命令
        /// </summary>
        public UniversalCommand ShowChooseIconWindowCommand { get; set; }

        /// <summary>
        /// 显示上传图片窗口的命令
        /// </summary>
        public UniversalCommand ShowUploadPicWindowCommand { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 首页的视图模型
        /// </summary>
        public IndexViewModel()
        {
            UserClient = new OfficialUserServiceClient();
            UserClient.GetUsersCompleted += WriteUserList;
            MessageClient = new OfficialMessageServiceClient();
            MessageClient.SendCompleted += ShowSendMessageResult;
            MessageClient.GetCountOfUnreadMessagesCompleted += UpdateCountOfNewMessage;
            MessageClient.GetUnreadMessagesCompleted += WriteMessageList;
            MessageClient.GetMessagesCompleted += WriteOldMessages;

            Users = new ObservableCollection<UserInfoModel>();
            Messages = new ObservableCollection<MessageResult>();
            LogoutCommand = new UniversalCommand(new Action<object>(Logout));
            OpenTalkingWindowCommand = new UniversalCommand(new Action<object>(OpenTalkingWindow));
            ChooseWhatDoingCommand = new UniversalCommand(new Action<object>(ChooseWhatDoing));
            SendMessageCommand = new UniversalCommand(new Action<object>(SendNewMessage));
            ShowChooseIconWindowCommand = new UniversalCommand(new Action<object>(ShowChooseIconWindow));
            ShowUploadPicWindowCommand = new UniversalCommand(new Action<object>(ShowUploadPicWindow));
            RefreshUserList();
            ReadMessageListOnTimeLine();
            KeepHeartbeat();
        }

        #endregion

        #region 私有方法

        #region 命令

        #region 登出

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="parameter">可选参数</param>
        void Logout(object parameter)
        {
            IPop<string> pop = (IPop<string>)ViewModelService.Current.GetPop(Pop.NormalPrompt);
            pop.State = "该操作将推出客服聊天系统，你确定要退出吗？";
            pop.Closed += (sender, e) =>
                {
                    IPop cw = (IPop)sender;
                    if (cw.DialogResult == false) { return; }
                    Logout_do();
                };
            pop.Show();
        }
        #region 登出

        void Logout_do()
        {
            string dataKeyOfUsername = DataKey.Client_Username.ToString();
            IsolatedStorageSettings.ApplicationSettings.Remove(dataKeyOfUsername);
            IsolatedStorageSettings.ApplicationSettings.Save();
            ViewModelService.Current.JumpToDefaultPage();
        }

        #endregion

        #endregion

        #region 打开聊天窗口

        public void OpenTalkingWindow(object parameter)
        {
            TargetUser = parameter.ToString();
            ResetPage();
        }

        #endregion

        #region 选择当前动作

        public void ChooseWhatDoing(object parameter)
        {
            DoingNow = parameter.ToString();
            ResetPage();
        }

        #endregion

        #region 发送新消息

        void SendNewMessage(object parameter)
        {
            if (TargetUser == "" || !Users.Any(x => x.Username == TargetUser))
            {
                IPop<string> pop = ViewModelService.Current.GetPop(Pop.ErrorPrompt) as IPop<string>;
                pop.State = "请先指定你要对话的用户";
                pop.Show();
                return;
            }
            if (MessageValue == "") { return; }
            SendMessageImport import = new SendMessageImport
            {
                From = Username,
                To = TargetUser,
                Content = MessageValue
            };
            MessageClient.SendAsync(import);
        }
        #region 显示信息发送结果

        void ShowSendMessageResult(object sender, SendCompletedEventArgs e)
        {
            if (!e.Result.Success)
            {
                IPop<string> pop = ViewModelService.Current.GetPop(Pop.ErrorPrompt) as IPop<string>;
                pop.State = "消息发送失败，请重试";
                pop.Show();
                return;
            }
            MessageValue = "";
            RefreshMessageList();
        }

        #endregion

        #endregion

        #region 显示表情选择窗口

        void ShowChooseIconWindow(object parameter)
        {
            IPop<string> cw = ViewModelService.Current.GetPop(Pop.ChooseIconWindow) as IPop<string>;
            cw.Closed += (sender, e) =>
                {
                    IPop<string> _cw = (IPop<string>)sender;
                    if (_cw.DialogResult != true) { return; }
                    string value = string.Format("[^icon]{0}[$icon]", _cw.State);
                    MessageValue += value;
                };
            cw.Show();
        }

        #endregion

        #region 显示图片上传窗口

        void ShowUploadPicWindow(object parameter)
        {
            IPop<string> cw = ViewModelService.Current.GetPop(Pop.UploadPicWindow) as IPop<string>;
            cw.Closed += (sender, e) =>
                {
                    IPop<string> _cw = (IPop<string>)sender;
                    if (_cw.DialogResult != true) { return; }
                    string value = string.Format("[^pic]{0}[$pic]", _cw.State);
                    MessageValue += value;
                };
            cw.Show();
        }

        #endregion

        #endregion

        #region 后台操作

        #region 刷新用户列表

        /// <summary>
        /// 刷新用户列表
        /// </summary>
        void RefreshUserList()
        {
            UserClient.GetUsersAsync(Username);
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
                Logout_do();
            }
        }

        #endregion
        #region 读取唯独信息条数

        void ReadCountOfNewMessage()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(300);
            timer.Tick += (sender, e) =>
                {
                    MessageClient.GetCountOfUnreadMessagesAsync(Username);
                };
            timer.Start();
        }

        void UpdateCountOfNewMessage(object sender, GetCountOfUnreadMessagesCompletedEventArgs e)
        {
            if (!e.Result.Success) { Logout_do(); }
            e.Result.Content.ForEach(x =>
                {
                    Users.First(u => u.Username == x.Username).CountOfNewMessage = x.Count;
                });
        }

        #endregion

        #endregion

        #region 刷新消息列表

        void RefreshMessageList()
        {
            if (TargetUser == "" || Username == "") { return; }
            MessageClient.GetUnreadMessagesAsync(TargetUser, Username);
        }
        #region 写消息列表

        void WriteMessageList(object sender, GetUnreadMessagesCompletedEventArgs e)
        {
            if (!e.Result.Success) { Logout_do(); }
            e.Result.Content.OrderBy(x => x.SendTime).ToList().ForEach(x =>
                {
                    Messages.Add(x);
                });
        }

        #endregion
        #region 轮询唯独信息

        void ReadMessageListOnTimeLine()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(30);
            timer.Tick += (sender, e) =>
                {
                    RefreshMessageList();
                };
            timer.Start();
        }

        #endregion

        #endregion

        #region 显示聊天纪录

        void ShowOldMessages(int page = 1)
        {
            MessageClient.GetMessagesAsync(TargetUser, page, 20000);
        }

        void WriteOldMessages(object sender, GetMessagesCompletedEventArgs e)
        {
            if (!e.Result.Success) { Logout_do(); }
            Messages.Clear();
            e.Result.Content.Content.OrderBy(x => x.SendTime).ToList().ForEach(x =>
            {
                Messages.Add(x);
            });
            PageIndex = e.Result.Content.PageIndex;
            AllPage = e.Result.Content.TotalOfPage;
        }

        #endregion

        #region 重置界面

        void ResetPage()
        {
            Messages.Clear();
            MessageValue = "";
            if (DoingNow == "聊天记录")
            {
                TalkToolHeight = new GridLength(60);
                ShowOldMessages();
            }
            else
            {
                TalkToolHeight = new GridLength(220);
            }
        }

        #endregion

        #region 心跳协议

        void KeepHeartbeat()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(300);
            timer.Tick += (sender, e) =>
                {
                    UserClient.HeartbeatAsync(Username);
                };
            timer.Start();
        }

        #endregion

        #endregion

        #endregion
    }
}
