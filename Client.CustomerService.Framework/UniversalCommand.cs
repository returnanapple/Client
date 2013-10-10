using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 通用的通知者对象
    /// </summary>
    public class UniversalCommand : ICommand
    {
        #region 私有变量

        Action<object> action;
        bool canExecute;

        #endregion

        /// <summary>
        /// 实例化一个新的通用的通知者对象
        /// </summary>
        /// <param name="action">所要封装的方法</param>
        /// <param name="canExecute">一个布尔值 标识是否允许被触发</param>
        public UniversalCommand(Action<object> action, bool canExecute = true)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        #region 实例方法

        /// <summary>
        /// 获取当前的是否允许被触发的状态
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>返回一个布尔值 标识是否允许被触发</returns>
        public bool CanExecute(object parameter)
        {
            if (this.action == null)
            {
                return false;
            }
            return this.canExecute;
        }

        /// <summary>
        /// 触发
        /// </summary>
        /// <param name="parameter">参数</param>
        public void Execute(object parameter)
        {
            if (!canExecute) { return; }
            if (action == null) { return; }
            action(parameter);
        }

        /// <summary>
        /// 设置是否允许被触发的状态
        /// </summary>
        /// <param name="canExecute">一个布尔值 标识是否允许被触发</param>
        public void SetCanExecute(bool canExecute)
        {
            this.canExecute = canExecute;
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        #endregion

        #region 事件

        /// <summary>
        /// 获取当前的是否允许被触发的状态时将触发的事件
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion
    }
}
