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

namespace Client.CustomerService.Control
{
    public partial class LoginPrompt : UserControl
    {
        public LoginPrompt()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LoginPrompt)
            , new PropertyMetadata(string.Empty, (d, e) =>
            {
                LoginPrompt tool = (LoginPrompt)d;
                string newValue = e.NewValue.ToString();

                tool.text_content.Text = newValue;
                double myHeight = tool.ActualHeight;
                if (newValue == "")
                {
                    tool.root.Visibility = Visibility.Collapsed;
                }
                else
                {
                    tool.root.Visibility = Visibility.Visible;
                    Storyboard s = (Storyboard)tool.Resources["s_open"];
                    s.Begin();
                }
            }));
    }
}
