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


using System.Threading;
using System.Windows.Threading;
using ToClient.PicService;
using Client.CustomerService.Framework.ChatService;
using System.ServiceModel;

namespace ToClient
{
    public class ToClientVM : INotifyPropertyChanged, IChatServiceCallback
    {
        public ToClientVM(string user)
        {
            CurrentUser = user;
            Initialize();
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

        private bool timeToReflashCustomerServiceList;
        private bool timeToReflash2CustomerServiceList;
        #endregion

        #region 网络连接
        private ChatServiceClient chatClient { get; set; }
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
                chatClient.ChangeStatusAsync(CurrentUser, value);
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

        public bool ChatRecordsIsLoaded { get; set; }
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
        /// <summary>
        /// 添加截图命令
        /// </summary>
        public ICommand AddScreenShotCommand { get; set; }

        public ICommand AddChatRecordsCommand { get; set; }

        public bool TimeToReflashCustomerServiceList
        {
            get
            { return timeToReflashCustomerServiceList; }
            set
            {
                timeToReflashCustomerServiceList = value;
                OnPropertyChanged(this, "TimeToReflashCustomerServiceList");
            }
        }
        public bool TimeToReflash2CustomerServiceList
        {
            get
            {
                return timeToReflash2CustomerServiceList;
            }
            set
            {
                timeToReflash2CustomerServiceList = value;
                OnPropertyChanged(this, "TimeToReflash2CustomerServiceList");
            }
        }

        public ICommand Command { get; set; }
        public ICommand SwitchChatingWithCommand { get; set; }
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

            CustomerServiceList = new ObservableCollection<UserInfo>();
            CustomerServiceList.CollectionChanged += (d, e) => { OnPropertyChanged(this, "CustomerServiceList"); };
            SuperiorList = new ObservableCollection<UserInfo>();
            SuperiorList.CollectionChanged += (d, e) => { OnPropertyChanged(this, "SuperiorList"); };
            LowerList = new ObservableCollection<UserInfo>();
            LowerList.CollectionChanged += (d, e) => { OnPropertyChanged(this, "LowerList"); };

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
            AddScreenShotCommand = new BaseCommand(AddScreenShot);

            Command = new BaseCommand(BeginChatToSomeone);
            SwitchChatingWithCommand = new BaseCommand(SwitchChatingWith);

            TimeToReflashCustomerServiceList = false;
            TimeToReflash2CustomerServiceList = false;



            chatClient = new ChatServiceClient(new InstanceContext(this));
            chatClient.RegisterAndGetFriendListCompleted += ChatClientRegisterAndGetFriendListCompleted;
            chatClient.RegisterAndGetFriendListAsync(CurrentUser, false);
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
            chatClient.SendMessageAsync(import);
            WaitSendContent = "";
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
                CurrentMessages.Clear();
                ChatRecordsIsLoaded = false;
                chatClient.ChangeTargetUserCompleted += (d, e) =>
                {
                    foreach (MessageResult i in e.Result)
                    {
                        CurrentMessages.Add(i);
                    }
                };
                chatClient.ChangeTargetUserAsync(ChatingWith, CurrentUser);
                NewMessageCount = NewMessageCount - tempUserInfo.NewMessageCount;
                tempUserInfo.NewMessageCount = 0;

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
            CurrentMessages.Clear();
            chatClient.ChangeTargetUserCompleted += (d, e) =>
            {
                foreach (MessageResult i in e.Result)
                {
                    CurrentMessages.Add(i);
                }
            };
            chatClient.ChangeTargetUserAsync(ChatingWith, CurrentUser);
            List<UserInfo> tempList = new List<UserInfo>();
            UserInfo tempUserInfo;
            tempList.AddRange(CustomerServiceList.ToList());
            tempList.AddRange(SuperiorList.ToList());
            tempList.AddRange(LowerList.ToList());
            tempUserInfo = tempList.Where(x => x.Username == ChatingWith).ToList().First();
            NewMessageCount = NewMessageCount - tempUserInfo.NewMessageCount;
            tempUserInfo.NewMessageCount = 0;
            ChatRecordsIsLoaded = false;
        }
        #endregion
        #region 关闭当前聊天
        public void CloseCurrentChat(object objectChatingWith)
        {
            ChatingWithList.Remove(ChatingWithList.Where(x => x.Username == ChatingWith).First());
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
            ChatRecordsIsLoaded = false;

        }
        #endregion
        #region 添加屏幕截图
        public void AddScreenShot(object objectImage)
        {
            byte[] byteImage = (byte[])objectImage;
            PicServiceClient client = new PicServiceClient();
            client.UploadCompleted += (sender, e) =>
            {
                WaitSendContent = WaitSendContent + "[^pic]" + e.Result + "[$pic]";
            };
            client.UploadAsync(byteImage);
        }
        #endregion
        #region 添加聊天纪录
        public void AddChatRecords()
        {
            chatClient.GetMessagesCompleted += (d, e) => 
            {
                if (ChatRecordsIsLoaded == false)
                {
                    CurrentMessages.Clear();
                    foreach (MessageResult i in e.Result.Content)
                    {
                        CurrentMessages.Add(i);
                    }
                    ChatRecordsIsLoaded = true;
                }
            };
            chatClient.GetMessagesAsync(ChatingWith,currentUser,1,int.MaxValue);
        }
        #endregion
        #endregion

        #region 登陆后行为
        void ChatClientRegisterAndGetFriendListCompleted(object sender, RegisterAndGetFriendListCompletedEventArgs e)
        {
            #region 填充好友列表
            foreach (UserInfoResult i in e.Result.Users)
            {
                if (i.Type == UserInfoType.客服)
                {
                    CustomerServiceList.Add(new UserInfo
                    {
                        Username = i.Username,
                        UserState = i.OnlineStatus,
                        NewMessageCount = 0,
                        Command = Command,
                        SwitchChatingWithCommand = SwitchChatingWithCommand
                    });
                }
                else if (i.Type == UserInfoType.上级)
                {
                    SuperiorList.Add(new UserInfo
                    {
                        Username = i.Username,
                        UserState = i.OnlineStatus,
                        NewMessageCount = 0,
                        Command = Command,
                        SwitchChatingWithCommand = SwitchChatingWithCommand
                    });
                }
                else if (i.Type == UserInfoType.下级)
                {
                    LowerList.Add(new UserInfo
                    {
                        Username = i.Username,
                        UserState = i.OnlineStatus,
                        NewMessageCount = 0,
                        Command = Command,
                        SwitchChatingWithCommand = SwitchChatingWithCommand
                    });

                }
            }
            #endregion
            #region 填充未读信息
            List<UserInfo> tl = new List<UserInfo>();
            tl.AddRange(CustomerServiceList);
            tl.AddRange(SuperiorList);
            tl.AddRange(LowerList);
            foreach (UserInfo j in tl)
            {
                if (e.Result.UnreadMessageCounts.Any(x => x.Username == j.Username))
                {
                    j.NewMessageCount = e.Result.UnreadMessageCounts.Where(x => x.Username == j.Username).First().Count;
                    NewMessageCount = NewMessageCount + j.NewMessageCount;
                }
            }
            #endregion
        }
        #endregion

        #region 实现接口
        public void AddTheCountOfNewMessageForSomeone(string username)
        {
            newMessageCount++;
            List<UserInfo> tl = new List<UserInfo>();
            tl.AddRange(CustomerServiceList);
            tl.AddRange(SuperiorList);
            tl.AddRange(LowerList);
            tl.Where(x => x.Username == username).First().NewMessageCount++;
        }

        public void WriteMessage(MessageResult message)
        {
            CurrentMessages.Add(message);
        }

        public void ChangeOnlineStatus(string username, UserOnlineStatus onlineStatus, bool isOfficial)
        {
            if (isOfficial)
            {
                if (onlineStatus == UserOnlineStatus.离线)
                {
                    if (CustomerServiceList.Any(x => x.Username == username))
                    {
                        UserInfo y = CustomerServiceList.First(x => x.Username == username);
                        CustomerServiceList.Remove(y);
                        TimeToReflash2CustomerServiceList = true;
                    }
                }
                else
                {
                    if (!CustomerServiceList.Any(x => x.Username == username))
                    {
                        CustomerServiceList.Add(new UserInfo
                        {
                            Username = username,
                            UserState = UserOnlineStatus.在线,
                            NewMessageCount = 0,
                            Command = Command,
                            SwitchChatingWithCommand = SwitchChatingWithCommand
                        });
                        TimeToReflashCustomerServiceList = true;
                    }
                }
                OnPropertyChanged(this, "CustomerServiceList");
            }
            else
            {
                List<ObservableCollection<UserInfo>> l = new List<ObservableCollection<UserInfo>> { SuperiorList, LowerList };
                ObservableCollection<UserInfo> o = l.FirstOrDefault(x => x.Any(y => y.Username == username));
                if (o == null)
                {
                    return;
                }
                o.First(x => x.Username == username).UserState = onlineStatus;
                OnPropertyChanged(this, "SuperiorList");
                OnPropertyChanged(this, "LowerList");

            }
        }
        #endregion
    }
}
