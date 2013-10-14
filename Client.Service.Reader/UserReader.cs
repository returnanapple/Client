using Client.Service.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Model;

namespace Client.Service.Reader
{
    /// <summary>
    /// 用户令牌的阅读者对象
    /// </summary>
    public class UserReader
    {
        public static string Login(string username, string password)
        {
            using (MainDatadbmlDataContext db = new MainDatadbmlDataContext())
            {
                UserInfo ui = db.UserInfo.FirstOrDefault(x => x.UserID == username && x.UserPwd == password);
                if (ui == null) { throw new Exception("用户名/密码错误"); }
                return ui.UserID;
            }
        }
    }
}
