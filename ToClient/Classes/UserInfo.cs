using Client.CustomerService.Framework.ChatService;
using System;
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

namespace ToClient.Classes
{
    public class UserInfo : INotifyPropertyChanged
    {
        private String username;
        private UserOnlineStatus userState;
        private int newMessageCount;
        private ICommand command;
        private ICommand switchChatingWithCommand;
        #region 属性
        /// <summary>
        /// 用户名
        /// </summary>
        public String Username
        {
            get
            { return username; }
            set
            {
                username = value;
                OnPropertyChanged(this, "Username");
            }
        }
        /// <summary>
        /// 用户在线状态
        /// </summary>
        public UserOnlineStatus UserState
        {
            get
            { return userState; }
            set
            {
                userState = value;
                OnPropertyChanged(this, "UserState");
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
        /// 点击好友按键开始聊天命令
        /// </summary>
        public ICommand Command
        {
            get
            { return command; }
            set
            {
                command = value;
                OnPropertyChanged(this, "Command");
            }
        }
        public ICommand SwitchChatingWithCommand
        {
            get
            { return switchChatingWithCommand; }
            set
            {
                switchChatingWithCommand = value;
                OnPropertyChanged(this, "SwitchChatingWithCommand");
            }
        }
        #endregion 属性

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(object sender, String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs("property"));
            }
        }
    }
}
