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

namespace ToClient.Controls
{
    public partial class ExpressionButton : UserControl
    {
        public ExpressionButton()
        {
            InitializeComponent();
        }



        public ICommand AddExpressionCommand
        {
            get { return (ICommand)GetValue(AddExpressionCommandProperty); }
            set { SetValue(AddExpressionCommandProperty, value); }
        }
        public static readonly DependencyProperty AddExpressionCommandProperty =
            DependencyProperty.Register("AddExpressionCommand", typeof(ICommand), typeof(ExpressionButton), new PropertyMetadata(null));

        

        public void MouseEnterAction(object sender, MouseEventArgs e)
        {
            RootBorder.Background = new SolidColorBrush(Color.FromArgb(255, 252, 240, 193));//#FFFCF0C1
        }
        public void MouseLeaveAction(object sender, MouseEventArgs e)
        {
            RootBorder.Background = new SolidColorBrush(Color.FromArgb(255, 247, 245, 245));//#FFF7F5F5
        }
        public void MouseLeftButtonDownAction(object sender, MouseButtonEventArgs e)
        {
            ExpressionControl childWindow = new ExpressionControl();
            childWindow.Closed += (_d,_e) => 
            {
                ExpressionControl temp_d = (ExpressionControl)_d;
                if (temp_d.DialogResult == true)
                {
                    AddExpressionCommand.Execute("[^icon]"+temp_d.State+"[$icon]");
                }
                RootBorder.Background = new SolidColorBrush(Color.FromArgb(255, 247, 245, 245));
            };
            childWindow.Show();
        }
    }
}
