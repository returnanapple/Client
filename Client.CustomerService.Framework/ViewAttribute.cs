using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 标记视图对象为可跳转界面
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ViewAttribute : Attribute
    {
        /// <summary>
        /// 界面标识
        /// </summary>
        public Page Page { get; protected set; }

        /// <summary>
        /// 界面名称
        /// </summary>
        public string PageName { get; protected set; }

        /// <summary>
        /// 一个布尔值 表示被标记对象是否为默认界面
        /// </summary>
        public bool IsDefault { get; protected set; }

        /// <summary>
        /// 视图模型
        /// </summary>
        public Type ViewModel { get; protected set; }

        /// <summary>
        /// 标记视图对象为可跳转界面
        /// </summary>
        /// <param name="page">界面标识</param>
        /// <param name="pageName">界面名称</param>
        /// <param name="viewModel">视图模型</param>
        /// <param name="isDefault">一个布尔值 表示被标记对象是否为默认界面</param>
        public ViewAttribute(Page page, string pageName, Type viewModel, bool isDefault)
        {
            this.Page = page;
            this.PageName = pageName;
            this.IsDefault = isDefault;
            this.ViewModel = viewModel;
        }
    }
}
