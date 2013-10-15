using Client.Service.Reader;
using System;
using System.ServiceModel;

namespace Client.Service
{
    /// <summary>
    /// 官方用户登陆服务
    /// </summary>
    public class OfficialLoginService : IOfficialLoginService
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回用户名</returns>
        public bool Login(string username, string password)
        {
            return UserReader.Login(username, password);
        }
    }
}
