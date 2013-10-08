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

namespace ToClient
{
    public class ToClientVM:INotifyPropertyChanged
    {
        #region 私有字段
        private bool chatWindowIsOpen;
        private bool friendListWindowIsOpen;
        private int newMessageCount;

        private bool customerServiceListIsOpen;
        private bool superiorListIsOpen;
        private bool lowerListIsOpen;
        #endregion
        #region 属性
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
        #endregion


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(object sender, string property)
        {
            PropertyChanged(sender, new PropertyChangedEventArgs(property));
        }
    }
}
