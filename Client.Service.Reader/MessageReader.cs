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
        /// <param name="username">用户名</param>
        /// <returns>返回未读信息的数量</returns>
        public static List<UnreadMessageCountResult> ReadCountOfUnreadMessages(string username)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                List<UnreadMessageCountResult> result = new List<UnreadMessageCountResult>();
                string token = string.Format("[{0}]", username);
                List<string> tos = db.Messages.Where(x => x.To == username && !x.Readed.Contains(token))
                    .Select(x => x.From)
                    .Distinct()
                    .ToList();
                tos.ForEach(sender =>
                    {
                        int c = db.Messages.Count(x => x.To == username && x.From == sender && !x.Readed.Contains(token));
                        result.Add(new UnreadMessageCountResult(sender, c));
                    });
                return result;
            }
        }

        /// <summary>
        /// 读取未读信息的列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>返回未读信息的列表</returns>
        public static List<Message> ReadUnreadMessages(string username)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                string token = string.Format("[{0}]", username);
                return db.Messages.Where(x => x.To == username && !x.Readed.Contains(token))
                    .OrderByDescending(x => x.CreatedTime)
                    .ToList();
            }
        }

        /// <summary>
        /// 读取聊天记录的分页列表
        /// </summary>
        /// <param name="_from">发件人</param>
        /// <param name="_to">收件人</param>
        /// <param name="pageSize">每个页面包含信息条数</param>
        /// <returns>返回聊天记录的分页列表</returns>
        public static PaginationList<MessageResult> ReadMessages(string _from, string _to, int page, int pageSize)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                int statrRow = (page - 1) * pageSize;
                Expression<Func<Message, bool>> pre = x => (x.To == _to && x.From == _from)
                    || (x.From == _to && x.To == _from);
                int c = db.Messages.Count(pre);
                List<MessageResult> t = db.Messages.Where(pre)
                    .OrderByDescending(x => x.CreatedTime)
                    .Skip(statrRow)
                    .Take(pageSize)
                    .ToList()
                    .ConvertAll(x => new MessageResult(x));
                return new PaginationList<MessageResult>(page, pageSize, c, t);
            }
        }

        /// <summary>
        /// 读取聊天记录的分页列表
        /// </summary>
        /// <param name="_from">发件人</param>
        /// <param name="_to">收件人</param>
        /// <param name="pageSize">每个页面包含信息条数</param>
        /// <returns>返回聊天记录的分页列表</returns>
        public static PaginationList<MessageResult> ReadMessages(string username, int page, int pageSize)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                int statrRow = (page - 1) * pageSize;
                Expression<Func<Message, bool>> pre = x => (x.To == username || x.From == username)
                    && x.IsOfficial == true;
                int c = db.Messages.Count(pre);
                List<MessageResult> t = db.Messages.Where(pre)
                    .OrderByDescending(x => x.CreatedTime)
                    .Skip(statrRow)
                    .Take(pageSize)
                    .ToList()
                    .ConvertAll(x => new MessageResult(x));
                return new PaginationList<MessageResult>(page, pageSize, c, t);
            }
        }
    }
}
