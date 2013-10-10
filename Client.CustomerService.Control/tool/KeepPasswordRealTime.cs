using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace Client.CustomerService.Control
{
    /// <summary>
    /// 用于保证信息实时性的触发器（仅限于 PasswordBox 控件）
    /// </summary>
    public class KeepPasswordRealTime : TriggerBase<PasswordBox>
    {
        #region 保护方法

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PasswordChanged += Update;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PasswordChanged -= Update;
        }

        #endregion

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">监视对象</param>
        void Update(object sender, EventArgs e)
        {
            BindingExpression expression = ((PasswordBox)sender).GetBindingExpression(PasswordBox.PasswordProperty);
            expression.UpdateSource();
        }
    }
}
