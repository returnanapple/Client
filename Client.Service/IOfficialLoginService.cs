using System.ServiceModel;

namespace Client.Service
{
    /// <summary>
    /// 定义官方用户登陆服务
    /// </summary>
    [ServiceContract]
    public interface IOfficialLoginService
    {
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>返回用户名</returns>
        [OperationContract]
        bool Login(string username, string password);
    }
}
