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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ToClient.Controls
{
    public partial class ChooseExpressionButton : UserControl
    {
        public ChooseExpressionButton()
        {
            InitializeComponent();
        }
        #region 依赖属性

        public int RealValue
        {
            get { return (int)GetValue(RealValueProperty); }
            set { SetValue(RealValueProperty, value); }
        }

        public static readonly DependencyProperty RealValueProperty =
            DependencyProperty.Register("RealValue", typeof(int), typeof(ChooseExpressionButton), new PropertyMetadata(0));

        public string ShowValue
        {
            get { return (string)GetValue(ShowValueProperty); }
            set { SetValue(ShowValueProperty, value); }
        }

        public static readonly DependencyProperty ShowValueProperty =
            DependencyProperty.Register("ShowValue", typeof(string), typeof(ChooseExpressionButton)
            , new PropertyMetadata("", (d, e) =>
            {
                ChooseExpressionButton tool = (ChooseExpressionButton)d;
                tool.img.Source = new BitmapImage(new Uri("../"+e.NewValue.ToString(), UriKind.Relative));
            }));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ChooseExpressionButton), new PropertyMetadata(null));

        #endregion

        private void OnHover(object sender, MouseEventArgs e)
        {
            bg.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnUnhover(object sender, MouseEventArgs e)
        {
            bg.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void OnClick(object sender, MouseButtonEventArgs e)
        {
            if (Command == null) { return; }
            if (!Command.CanExecute(RealValue)) { return; }
            Command.Execute(RealValue);
        }
    }
}
