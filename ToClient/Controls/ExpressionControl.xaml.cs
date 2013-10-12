using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Client.CustomerService.Framework;

namespace ToClient.Controls
{
    public partial class ExpressionControl : ChildWindow
    {
        public ExpressionControl()
        {
            InitializeComponent();
            List<TClass> ts = new List<TClass>();
            UniversalCommand command = new UniversalCommand(new Action<object>(ChooseIcon));
            for (int i = 41; i <= 70; i++)
            {
                ts.Add(new TClass(i, command));
            }
            ShowContent.ItemsSource = ts;
        }

        #region 消息

        string state = "";

        public string State
        {
            get { return state; }
            set
            {
                state = value;
            }
        }

        #endregion

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        void ChooseIcon(object parameter)
        {
            State = parameter.ToString();
            this.DialogResult = true;
        }

        #region 内置类型

        public class TClass
        {
            public int RealValue { get; set; }

            public string Value
            {
                get
                {
                    return string.Format("icon/{0}.png", RealValue);
                }
            }

            public UniversalCommand ChooseIconCommand { get; set; }

            public TClass(int realValue, UniversalCommand chooseIconCommand)
            {
                RealValue = realValue;
                this.ChooseIconCommand = chooseIconCommand;
            }
        }

        #endregion
    }
}

