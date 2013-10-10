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
        private States userState;
        private int newMessageCount;

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

        public States UserState
        {
            get
            { return userState; }
            set
            {
                userState = value;
                OnPropertyChanged(this, "UserState");
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(object sender, String property)
        {
            PropertyChanged(sender, new PropertyChangedEventArgs("property"));
        }
    }
}
