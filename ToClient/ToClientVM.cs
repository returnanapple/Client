using System;
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

namespace ToClient
{
    public class ToClientVM:INotifyPropertyChanged
    {
        #region 私有字段
        private bool chatWindowIsOpen;
        private bool friendListWindowIsOpen;
        private string newMessageCount;
        private bool customerServiceListIsOpen;
        private bool superiorListIsOpen;
        private bool lowerListIsOpen;
        private string waitSendContent;
        #endregion
        #region 属性
        public bool ChatWindowIsOpen
        {
            get
            { return chatWindowIsOpen; }
            set
            {
                chatWindowIsOpen = value;
                OnPropertyChanged(this, "ChatWindowIsOpen");
            }
        }

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

        public string NewMessageCount
        {
            get
            { return newMessageCount; }
            set
            {
                newMessageCount = value;
                OnPropertyChanged(this, "NewMessageCount");
            }
        }

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
        public ObservableCollection<UserInfo> CustomerServiceList { get; set; }
        public ObservableCollection<UserInfo> SuperiorList { get; set; }
        public ObservableCollection<UserInfo> LowerList { get; set; }
        public ObservableCollection<UserInfo> ChatingWithList { get; set; }
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(object sender, string property)
        {
            PropertyChanged(sender, new PropertyChangedEventArgs(property));
        }
    }
}
