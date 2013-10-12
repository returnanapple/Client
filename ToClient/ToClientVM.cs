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
            CustomerServiceList = new ObservableCollection<UserInfo> { new UserInfo() };
            SuperiorList = new ObservableCollection<UserInfo> { new UserInfo { Username = "a" }, new UserInfo { Username = "b" } };
            LowerList = new ObservableCollection<UserInfo> { new UserInfo(), new UserInfo(), new UserInfo(), new UserInfo() };
            ChatingWithList = new ObservableCollection<UserInfo> { new UserInfo(), new UserInfo(), new UserInfo(), new UserInfo(), new UserInfo(), new UserInfo(), new UserInfo() };
            AddCommand();
        }
        #region 私有字段
        private string currentUser;//？
        private States currentUserOnlineState;
        private bool chatWindowIsOpen;
        private bool friendListWindowIsOpen;
        private int newMessageCount = 8;//？
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
        public States CurrentUserOnlineState
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


        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            messageClient = new MessageServiceClient();
            messageClient.SendCompleted += MessageClientSendCompleted;
            userClient = new UserServiceClient();
            userClient.GetFriendsCompleted += UserClientGetFriendsCompleted;


            currentUserOnlineState = States.在线;
            chatWindowIsOpen = false;
            friendListWindowIsOpen = false;
            customerServiceListIsOpen = false;
            superiorListIsOpen = false;
            lowerListIsOpen = false;
            waitSendContent = "";
            chatingWith = "";
            AddExpressionCommand = new BaseCommand(AddExpression);
        }




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
        }

        /// <summary>
        /// 添加表情函数
        /// </summary>
        /// <param name="objectExpressionName"></param>
        public void AddExpression(object objectExpressionName)
        {
            WaitSendContent = WaitSendContent + (objectExpressionName.ToString());
        }


        /// <summary>
        /// 添加命令函数
        /// </summary>
        public void AddCommand()
        {
            BaseCommand command = new BaseCommand(BeginChatToSomeone);
            BaseCommand switchChatingWithCommand = new BaseCommand(SwitchChatingWith);
            foreach (UserInfo i in CustomerServiceList)
            {
                i.Command = command;
                i.SwitchChatingWithCommand = switchChatingWithCommand;
            }
            foreach (UserInfo i in SuperiorList)
            {
                i.Command = command;
                i.SwitchChatingWithCommand = switchChatingWithCommand;
            }
            foreach (UserInfo i in LowerList)
            {
                i.Command = command;
                i.SwitchChatingWithCommand = switchChatingWithCommand;
            }
        }

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
        /// <summary>
        /// 切换聊天好友函数
        /// </summary>
        /// <param name="objectUsername"></param>
        public void SwitchChatingWith(object objectUsername)
        {
            ChatingWith = objectUsername.ToString();
        }

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
            e.Result.Content.Where(x => x.Type == UserInfoType.上级).OrderByDescending(x=>x.OnlineStatus)
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
        #endregion
    }
}
