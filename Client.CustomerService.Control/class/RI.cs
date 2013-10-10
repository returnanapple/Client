using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Client.CustomerService.Framework;

namespace Client.CustomerService.Control
{
    /// <summary>
    /// 注册机器人
    /// </summary>
    public class RI
    {
        /// <summary>
        /// 注册
        /// </summary>
        public void Register()
        {
            //获取目标程序集
            Assembly assembly = Assembly.GetExecutingAssembly();
            //注册弹窗
            assembly.GetTypes()
                .Where(x => (x.GetInterfaces().Any(t => t == typeof(IPop)))
                    && (x.GetCustomAttributes(true).Any(t => t is WindowAttribute)))
                .ToList().ForEach(_type =>
                {
                    WindowAttribute attribute = _type.GetCustomAttributes(true)
                        .First(x => x is WindowAttribute) as WindowAttribute;
                    ViewModelService.Current.RegisterPop(attribute.Pop
                        , new ViewModelService.WindowCreater(() =>
                        {
                            return assembly.CreateInstance(_type.FullName) as ChildWindow;
                        }));
                });
        }
    }
}
