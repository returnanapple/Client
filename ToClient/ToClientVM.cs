using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using ToClient.Classes;
using System.Linq;
using ToClient.MessageService;
using ToClient.UserService;
using System.Threading;
using System.Windows.Threading;

namespace ToClient
{
    public class ToClientVM : INotifyPropertyChanged
    {
        public ToClientVM()
        {
            Initialize();
            //SuperiorList.Add(new UserInfo { Username = "a", UserState = UserOnlineStatus.在线 });
           // SuperiorList.Add(new UserInfo { Username = "b", UserState = UserOnlineStatus.忙碌 });
            //SuperiorList.Add(new UserInfo { Username = "b", UserState = UserOnlineStatus.隐身 });
           // TempFun();
        }
        #region 私有字段
        private string currentUser;//？
        private UserOnlineStatus currentUserOnlineState;
        private bool chatWindowIsOpen;
        private bool friendListWindowIsOpen;
        private int newMessageCount;
        private bool customerServiceListIsOpen;
        private bool superiorListIsOpen;
        private bool lowerListIsOpen;
        private string waitSendContent;
        private string chatingWith;
        #endregion

        #region 网络连接
        private MessageServiceClient messageClient { get; set; }
        private UserServiceClient userClient { get; set; }
        #endregion

        #region 属性
        /// <summary>
        /// 当前用户
        /// </summary>
        public string CurrentUser
        {
            get
            { return currentUser; }
            set
            {
                currentUser = value;
                OnPropertyChanged(this, "currentUser");
            }
        }
        /// <summary>
        /// 当前用户在线状态
        /// </summary>
        public UserOnlineStatus CurrentUserOnlineState
        {
            get
            { return currentUserOnlineState; }
            set
            {
                currentUserOnlineState = value;
                OnPropertyChanged(this, "CurrentUserOnlineState");
            }
        }
        /// <summary>
        /// 聊天窗口是否打开
        /// </summary>
        public bool ChatWindowIsOpen
        {
            get
            { return chatWindowIsOpen; }
            set
            {
                chatWindowIsOpen = value;
                if (value == false)
                {
                    ChatingWithList.Clear();
                    WaitSendContent = "";

                }
                OnPropertyChanged(this, "ChatWindowIsOpen");
            }
        }
        /// <summary>
        /// 好友总列表是否打开
        /// </summary>
        public bool FriendListWindowIsOpen
        {
            get
            { return friendListWindowIsOpen; }
            set
            {
                friendListWindowIsOpen = value;
                OnPropertyChanged(this, "FriendListWindowIsOpen");
            }
        }
        /// <summary>
        /// 新信息条数
        /// </summary>
        public int NewMessageCount
        {
            get
            { return newMessageCount; }
            set
            {
                newMessageCount = value;
                OnPropertyChanged(this, "NewMessageCount");
            }
        }
        /// <summary>
        /// 客服分组是否打开
        /// </summary>
        public bool CustomerServiceListIsOpen
        {
            get
            { return customerServiceListIsOpen; }
            set
            {
                customerServiceListIsOpen = value;
                OnPropertyChanged(this, "CustomerServiceListIsOpen");
            }
        }
        /// <summary>
        /// 上级分组是否打开
        /// </summary>
        public bool SuperiorListIsOpen
        {
            get
            { return superiorListIsOpen; }
            set
            {
                superiorListIsOpen = value;
                OnPropertyChanged(this, "SuperiorListIsOpen");
            }
        }
        /// <summary>
        /// 下级分组是否打开
        /// </summary>
        public bool LowerListIsOpen
        {
            get
            { return lowerListIsOpen; }
            set
            {
                lowerListIsOpen = value;
                OnPropertyChanged(this, "LowerListIsOpen");
            }
        }
        /// <summary>
        /// 编辑框内容
        /// </summary>
        public string WaitSendContent
        {
            get
            { return waitSendContent; }
            set
            {
                waitSendContent = value;
                OnPropertyChanged(this, "WaitSendContent");
            }
        }
        /// <summary>
        /// 正在聊天的好友
        /// </summary>
        public string ChatingWith
        {
            get
            { return chatingWith; }
            set
            {
                chatingWith = value;
                OnPropertyChanged(this, "ChatingWith");
            }
        }
        /// <summary>
        /// 客服分组列表
        /// </summary>
        public ObservableCollection<UserInfo> CustomerServiceList { get; set; }
        /// <summary>
        /// 上级分组列表
        /// </summary>
        public ObservableCollection<UserInfo> SuperiorList { get; set; }
        /// <summary>
        /// 下级分组列表
        /// </summary>
        public ObservableCollection<UserInfo> LowerList { get; set; }
        /// <summary>
        /// 正在聊天的好友列表
        /// </summary>
        public ObservableCollection<UserInfo> ChatingWithList { get; set; }
        /// <summary>
        /// 当前聊天信息
        /// </summary>
        public ObservableCollection<MessageResult> CurrentMessages { get; set; }

        /// <summary>
        /// 发送信息命令
        /// </summary>
        public ICommand SendMessageCommand { get; set; }
        /// <summary>
        /// 添加表情命令
        /// </summary>
        public ICommand AddExpressionCommand { get; set; }
        /// <summary>
        /// 关闭当前聊天命令
        /// </summary>
        public ICommand CloseCurrentChatCommand { get; set; }
        #endregion

        #region 属性改变事件
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(object sender, string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(property));
            }
        }
        #endregion 属性改变事件

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            messageClient = new MessageServiceClient();
            messageClient.SendCompleted += MessageClientSendCompleted;

            userClient = new UserServiceClient();
            userClient.GetFriendsCompleted += UserClientGetFriendsCompleted;

            messageClient.GetCountOfUnreadMessagesCompleted += MessageClientGetCountOfUnreadMessagesCompleted;
            messageClient.GetUnreadMessagesCompleted += MessageClientGetUnreadMessagesCompleted;

            CustomerServiceList = new ObservableCollection<UserInfo>();
            SuperiorList = new ObservableCollection<UserInfo>();
            LowerList = new ObservableCollection<UserInfo>();
            ChatingWithList = new ObservableCollection<UserInfo>();
            CurrentMessages = new ObservableCollection<MessageResult>();


            currentUserOnlineState = UserOnlineStatus.在线;
            chatWindowIsOpen = false;
            friendListWindowIsOpen = false;
            newMessageCount = 0;
            customerServiceListIsOpen = false;
            superiorListIsOpen = false;
            lowerListIsOpen = false;
            waitSendContent = "";
            chatingWith = "";

            SendMessageCommand = new BaseCommand(SendMessage);
            AddExpressionCommand = new BaseCommand(AddExpression);
            CloseCurrentChatCommand = new BaseCommand(CloseCurrentChat);

            ReflashFriendList();
            ReflashMessageAndMessageCount();
            ReflashFriendListTimeLine();
            ReflashMessageAndMessageCountTimeLine();
            
        }
        #endregion

        #region 前台方法
        #region 发送信息
        /// <summary>
        /// 发送信息函数
        /// </summary>
        /// <param name="objectMessage"></param>
        public void SendMessage(object objectMessage)
        {
            if (WaitSendContent == "" || CurrentUser == "" || ChatingWith == "")
            { return; }
            SendMessageImport import = new SendMessageImport
            {
                Content = WaitSendContent,
                From = CurrentUser,
                To = ChatingWith
            };
            messageClient.SendAsync(import);
            ChatingWith = "";
        }
        /// <summary>
        /// 判断信息是否发送成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MessageClientSendCompleted(object sender, SendCompletedEventArgs e)
        {
            if (!e.Result.Success)
            { return; }
            ReflashMessageAndMessageCount();
        }
        #endregion
        #region 添加表情
        /// <summary>
        /// 添加表情函数
        /// </summary>
        /// <param name="objectExpressionName"></param>
        public void AddExpression(object objectExpressionName)
        {
            WaitSendContent = WaitSendContent + (objectExpressionName.ToString());
        }
        #endregion
        #region 开始聊天
        /// <summary>
        /// 点击好友按键开始聊天函数
        /// </summary>
        /// <param name="objectUsername"></param>
        public void BeginChatToSomeone(object objectUsername)
        {
            string username = objectUsername.ToString();

            if (ChatWindowIsOpen == false)
            {
                ChatWindowIsOpen = true;
            }
            ChatingWith = username;

            if (ChatingWithList.Where(x => x.Username == username).ToList().Count == 0)
            {
                List<UserInfo> tempList = new List<UserInfo>();
                UserInfo tempUserInfo;
                tempList.AddRange(CustomerServiceList.ToList());
                tempList.AddRange(SuperiorList.ToList());
                tempList.AddRange(LowerList.ToList());
                tempUserInfo = tempList.Where(x => x.Username == username).ToList().First();
                ChatingWithList.Insert(0, tempUserInfo);
            }
        }
        #endregion
        #region 切换聊天好友
        /// <summary>
        /// 切换聊天好友函数
        /// </summary>
        /// <param name="objectUsername"></param>
        public void SwitchChatingWith(object objectUsername)
        {
            ChatingWith = objectUsername.ToString();
        }
        #endregion
        #region 关闭当前聊天
        public void CloseCurrentChat(object objectChatingWith)
        {
            ChatingWithList.Remove(ChatingWithList.Where(x=>x.Username==ChatingWith).First());
            CurrentMessages.Clear();
            if (ChatingWithList.Count != 0)
            {
                ChatingWith = ChatingWithList.First().Username;
            }
            else
            {
                ChatingWith = "";
                ChatWindowIsOpen = false;
            }

        }
        #endregion
        #endregion

        #region 后台方法

        #region 刷新用户列表
        public void ReflashFriendList()
        {
            userClient.GetFriendsAsync(CurrentUser);
        }
        void UserClientGetFriendsCompleted(object sender, GetFriendsCompletedEventArgs e)
        {
            if (!e.Result.Success)
            { return; }
            List<UserInfo> tList = new List<UserInfo>();
            #region 刷新客服列表
            tList.AddRange(CustomerServiceList);
            CustomerServiceList.Clear();
            e.Result.Content.Where(x => x.Type == UserInfoType.客服 && x.OnlineStatus != UserOnlineStatus.离线)
                .ToList().ForEach(x =>
                {
                    UserInfo user = new UserInfo
                    {
                        Username = x.Username,
                        UserState = x.OnlineStatus,
                        Command = new BaseCommand(BeginChatToSomeone),
                        SwitchChatingWithCommand = new BaseCommand(SwitchChatingWith),
                        NewMessageCount = tList.Any(y => y.Username == x.Username)
                            ? tList.First(y => y.Username == x.Username).NewMessageCount
                            : 0
                    };
                    CustomerServiceList.Add(user);
                });
            #endregion
            #region 刷新上级列表
            tList.Clear();
            tList.AddRange(SuperiorList);
            SuperiorList.Clear();
            e.Result.Content.Where(x => x.Type == UserInfoType.上级).OrderByDescending(x => x.OnlineStatus)
                .ToList().ForEach(x =>
                {
                    UserInfo user = new UserInfo
                    {
                        Username = x.Username,
                        UserState = x.OnlineStatus,
                        Command = new BaseCommand(BeginChatToSomeone),
                        SwitchChatingWithCommand = new BaseCommand(SwitchChatingWith),
                        NewMessageCount = tList.Any(y => y.Username == x.Username)
                            ? tList.First(y => y.Username == x.Username).NewMessageCount
                            : 0
                    };
                    SuperiorList.Add(user);
                });
            #endregion
            #region 刷新下级列表
            tList.Clear();
            tList.AddRange(LowerList);
            LowerList.Clear();
            e.Result.Content.Where(x => x.Type == UserInfoType.下级).OrderByDescending(x => x.OnlineStatus)
                .ToList().ForEach(x =>
                {
                    UserInfo user = new UserInfo
                    {
                        Username = x.Username,
                        UserState = x.OnlineStatus,
                        Command = new BaseCommand(BeginChatToSomeone),
                        SwitchChatingWithCommand = new BaseCommand(SwitchChatingWith),
                        NewMessageCount = tList.Any(y => y.Username == x.Username)
                            ? tList.First(y => y.Username == x.Username).NewMessageCount
                            : 0
                    };
                    LowerList.Add(user);
                });
            #endregion
        }
        public void ReflashFriendListTimeLine()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.Parse("0:0:3");
            timer.Tick += (sender, e) => { ReflashFriendList(); };
            timer.Start();
        }
        #endregion

        #region 刷新信息和未读信息条目列表
        public void ReflashMessageAndMessageCount()
        {
            messageClient.GetCountOfUnreadMessagesAsync(CurrentUser);
            if (ChatingWith != "")
            {
                messageClient.GetUnreadMessagesAsync(ChatingWith, CurrentUser);
            }
        }
        public void MessageClientGetCountOfUnreadMessagesCompleted(object sender, GetCountOfUnreadMessagesCompletedEventArgs e)
        {
            if (e.Result.Success)
            {
                if (e.Result.Content.Count != 0)
                {
                    NewMessageCount = e.Result.Content.Sum(x => x.Count);
                    foreach (UnreadMessageCountResult i in e.Result.Content)
                    {
                        if (CustomerServiceList.Any(x => x.Username == i.Username))
                        {
                            CustomerServiceList.Where(x => x.Username == i.Username).ToList()
                                .First().NewMessageCount = i.Count;
                        }
                        if (SuperiorList.Any(x => x.Username == i.Username))
                        {
                            SuperiorList.Where(x => x.Username == i.Username).ToList()
                                .First().NewMessageCount = i.Count;
                        }
                        if (LowerList.Any(x => x.Username == i.Username))
                        {
                            LowerList.Where(x => x.Username == i.Username).ToList()
                                    .First().NewMessageCount = i.Count;
                        }
                    }
                }
            }
        }
        public void MessageClientGetUnreadMessagesCompleted(object sender, GetUnreadMessagesCompletedEventArgs e)
        {
            if (e.Result.Success)
            {
                if (e.Result.Content.Count!=0)
                { 
                    foreach(MessageResult i in e.Result.Content)
                    {
                        CurrentMessages.Add(i);
                    }
                }
            }
        }
        public void ReflashMessageAndMessageCountTimeLine()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.Parse("0:0:1.5");
            timer.Tick += (sender, e) => { ReflashMessageAndMessageCount(); };
            timer.Start();
        }
        #endregion
        #endregion

        public void TempFun()
        {
            foreach (var i in SuperiorList)
            {
                i.Command = new BaseCommand(BeginChatToSomeone);
                i.SwitchChatingWithCommand = new BaseCommand(SwitchChatingWith);
            }
        }
    }
}
