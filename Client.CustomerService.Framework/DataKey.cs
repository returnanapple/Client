using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 数据存储的键值
    /// </summary>
    public enum DataKey
    {
        /// <summary>
        /// 记住密码
        /// </summary>
        Client_RememberMe = 101,
        /// <summary>
        /// 当前登入的用户名
        /// </summary>
        Client_Username = 102
    }
}
