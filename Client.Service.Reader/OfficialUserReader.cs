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
                var tList = db.UserInfo.ToList().ConvertAll(x => new UserInfoResult(x.UserID, UserInfoType.用户));
                using (Model2DataContext _db = new Model2DataContext())
                {
                    _db.Users.Where(x => x.Timeout > DateTime.Now).ToList().ForEach(x =>
                        {
                            if (!tList.Any(u => u.Username == x.Username)) { return; }
                            var ur = tList.First(u => u.Username == x.Username);
                            if (x.IsOfficial)
                            {
                                ur.OnlineStatus = UserOnlineStatus.在线;
                                ur.Type = UserInfoType.客服;
                            }
                            else if (ur.Type != UserInfoType.客服)
                            {
                                ur.OnlineStatus = x.OnlineStatus;
                            }
                        });
                }
                return tList;
            }
        }
    }
}
