using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Client.Model;
using Client.Service.DataContract;

namespace Client.Service.Reader
{
    /// <summary>
    /// 聊天信息的阅读着对象
    /// </summary>
    public class MessageReader
    {
        /// <summary>
        /// 读取未读信息的数量
        /// </summary>
        /// <param name="self">自己的用户名</param>
        /// <returns>返回未读信息的数量</returns>
        public static List<UnreadMessageCountResult> ReadCountOfUnreadMessages(string self)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                List<UnreadMessageCountResult> result = new List<UnreadMessageCountResult>();
                List<string> tos = db.PondOfMessage.Where(x => x.To == self && !x.Readed)
                    .Select(x => x.From)
                    .Distinct()
                    .ToList();
                tos.ForEach(sender =>
                    {
                        int c = db.PondOfMessage.Count(x => x.To == self && x.From == sender && !x.Readed);
                        result.Add(new UnreadMessageCountResult(sender, c));
                    });
                return result;
            }
        }

        /// <summary>
        /// 读取未读信息列表
        /// </summary>
        /// <param name="targetUser">对方的用户名</param>
        /// <param name="self">自己的用户名</param>
        /// <returns>返回未读信息的列表</returns>
        public static List<MessageResult> ReadUnreadMessages(string targetUser, string self)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                List<MessageResult> result = new List<MessageResult>();
                List<Message> tList = db.PondOfMessage.Where(x => x.From == targetUser && x.To == self && !x.Readed)
                    .ToList();
                tList.ForEach(x =>
                    {
                        x.Readed = true;
                        MessageResult mr = new MessageResult(x, x.From == self);
                        result.Add(mr);
                    });
                db.SaveChanges();
                return result;
            }
        }

        /// <summary>
        /// 读取聊天记录的分页列表
        /// </summary>
        /// <param name="targetUser">对方的用户名</param>
        /// <param name="self">自己的用户名</param>
        /// <param name="pageSize">每个页面包含信息条数</param>
        /// <returns>返回聊天记录的分页列表</returns>
        public static PaginationList<MessageResult> ReadMessages(string targetUser, string self, int page, int pageSize)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                int statrRow = (page - 1) * pageSize;
                Expression<Func<Message, bool>> pre = x => (x.To == self && x.From == targetUser)
                    || (x.From == self && x.To == targetUser);
                int c = db.PondOfMessage.Count(pre);
                List<MessageResult> t = db.PondOfMessage.Where(pre)
                    .OrderByDescending(x => x.CreatedTime)
                    .Skip(statrRow)
                    .Take(pageSize)
                    .ToList()
                    .ConvertAll(x => new MessageResult(x, x.From == self));
                return new PaginationList<MessageResult>(page, pageSize, c, t);
            }
        }
    }
}
