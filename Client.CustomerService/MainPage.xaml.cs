using Client.CustomerService.Control;
using Client.CustomerService.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Client.CustomerService
{
    public partial class MainPage : UserControl, IMainPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        #region 实现IMainPage接口

        public void Show(UserControl userControl)
        {
            body.Children.Clear();
            body.Children.Add(userControl);
        }

        /// <summary>
        /// 注册可跳转界面
        /// </summary>
        public void RegisterViews()
        {
            //获取目标程序集
            Assembly assembly = Assembly.GetExecutingAssembly();

            #region 注册可跳转界面

            assembly.GetTypes()
                .Where(x => x.GetCustomAttributes(true).Any(t => t is ViewAttribute))
                .ToList().ForEach(x =>
                {
                    var attribute = x.GetCustomAttributes(true).First(t => t is ViewAttribute) as ViewAttribute;
                    ViewModelService.Current.RegisterPage(attribute.Page
                        , attribute.PageName
                        , attribute.ViewModel
                        , attribute.IsDefault
                        , new ViewModelService.ControlCreater(() =>
                        {
                            return assembly.CreateInstance(x.FullName) as UserControl;
                        }));
                });

            #endregion

            //注册弹窗
            new RI().Register();
        }

        #endregion
    }
}
