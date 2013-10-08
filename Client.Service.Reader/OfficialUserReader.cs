using Client.Model;
using Client.Service.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Service.Reader
{
    public class OfficialUserReader
    {
        /// <summary>
        /// 读取用户列表
        /// </summary>
        /// <returns>返回用户列表</returns>
        public static List<UserInfoResult> ReadUsers()
        {
            using (MainDatadbmlDataContext db = new MainDatadbmlDataContext())
            {
                return db.UserInfo.ToList().ConvertAll(x => new UserInfoResult(x.UserID, UserInfoType.用户));
            }
        }
    }
}
