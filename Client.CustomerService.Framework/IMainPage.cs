using System.Reflection;
using System.Windows.Controls;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 定义主界面
    /// </summary>
    public interface IMainPage
    {
        /// <summary>
        /// 显示目标子界面
        /// </summary>
        /// <param name="userControl">所要显示的子界面</param>
        void Show(UserControl userControl);

        /// <summary>
        /// 注册可跳转界面
        /// </summary>
        void RegisterViews();

        /// <summary>
        /// 源数据
        /// </summary>
        object DataContext { get; set; }
    }
}
