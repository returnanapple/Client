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
    /// 清空信息在获得焦点的时候（仅限于 PasswordBox 控件）
    /// </summary>
    public class ClearPasswordOnFocus : TriggerBase<PasswordBox>
    {
        #region 保护方法

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.GotFocus += Clear;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.GotFocus -= Clear;
        }

        #endregion

        /// <summary>
        /// 清空信息
        /// </summary>
        /// <param name="sender">触发对象</param>
        /// <param name="e">监视对象</param>
        void Clear(object sender, EventArgs e)
        {
            ((PasswordBox)sender).Password = "";
        }
    }
}
