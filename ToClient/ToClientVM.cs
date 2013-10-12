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

namespace ToClient
{
    public class ToClientVM:INotifyPropertyChanged
    {
        public ToClientVM()
        {
            CustomerServiceList = new ObservableCollection<UserInfo>{ new UserInfo()};
            SuperiorList = new ObservableCollection<UserInfo> { new UserInfo{Username="a"},new UserInfo{Username="b"} };
            LowerList = new ObservableCollection<UserInfo> {new UserInfo(),new UserInfo(),new UserInfo(),new UserInfo() };
            ChatingWithList = new ObservableCollection<UserInfo> { new UserInfo(), new UserInfo(), new UserInfo(), new UserInfo(), new UserInfo(), new UserInfo(), new UserInfo() };
            FriendButtonAddBeginChatToSomeoneCommand();
        }
        #region 私有字段
        private string currentUser;
        private States currentUserOnlineState=States.在线;
        private bool chatWindowIsOpen = false;
        private bool friendListWindowIsOpen=false;
        private int newMessageCount=8;
        private bool customerServiceListIsOpen =false;
        private bool superiorListIsOpen = false;
        private bool lowerListIsOpen=false;
        private string waitSendContent="";
        private string chatingWith="";

        private ICommand sendMessageCommand
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

        public ICommand SendMessageCommand { }
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
        /// 添加命令
        /// </summary>
        public void FriendButtonAddBeginChatToSomeoneCommand()
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

        public void SwitchChatingWith(object objectUsername)
        {
            ChatingWith = objectUsername.ToString();
        }
    }
}
