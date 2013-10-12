using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace Client.CustomerService.Control
{
    /// <summary>
    /// 竖向滚屏触发器（仅限于父节点为 ScrollViewer 的对象）
    /// </summary>
    public class VerticalScrollTrigger : TriggerBase<FrameworkElement>
    {
        #region 保护方法

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseWheel += OnVerticalScroll;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.MouseWheel -= OnVerticalScroll;
        }

        #endregion

        /// <summary>
        /// 竖向滚屏
        /// </summary>
        /// <param name="sender">监视对象</param>
        /// <param name="e">触发对象</param>
        void OnVerticalScroll(object sender, MouseWheelEventArgs e)
        {
            FrameworkElement fe = (FrameworkElement)sender;
            ScrollViewer viewer = fe.Parent as ScrollViewer;
            if (viewer == null)
                return;
            double num = Math.Abs((int)(e.Delta / 2));
            double offset = 0.0;
            if (e.Delta > 0)
            {
                offset = Math.Max((double)0.0, (double)(viewer.VerticalOffset - num));
            }
            else
            {
                offset = Math.Min(viewer.ScrollableHeight, viewer.VerticalOffset + num);
            }
            if (offset != viewer.VerticalOffset)
            {
                viewer.ScrollToVerticalOffset(offset);
                e.Handled = true;
            }
        }
    }
}
