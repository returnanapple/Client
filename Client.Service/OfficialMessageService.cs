using Client.Model;
using Client.Model.Manager;
using Client.Service.DataContract;
using Client.Service.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Service
{
    /// <summary>
    /// 官方消息服务
    /// </summary>
    public class OfficialMessageService : IOfficialMessageService
    {
        /// <summary>
        /// 获取未读信息的数量
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>包含未读信息的数量的操作结果</returns>
        public OperatingResult<List<UnreadMessageCountResult>> GetCountOfUnreadMessages(string username)
        {
            try
            {
                List<UnreadMessageCountResult> counts = OfficialMessageReader.ReadCountOfUnreadMessages(username);
                return new OperatingResult<List<UnreadMessageCountResult>>(counts);
            }
            catch (Exception ex)
            {
                return new OperatingResult<List<UnreadMessageCountResult>>(null, ex.Message);
            }
        }

        /// <summary>
        /// 获取未读信息的列表
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns>返回包含未读信息的列表的操作结果</returns>
        public OperatingResult<List<MessageResult>> GetUnreadMessages(string _from, string _to)
        {
            try
            {
                List<Message> t = OfficialMessageReader.ReadUnreadMessages(_from, _to);
                t.ForEach(x =>
                {
                    MessageManager.Read(x.Id, _to);
                });
                List<MessageResult> tResult = t.ConvertAll(x => new MessageResult(x, x.From != _from));
                return new OperatingResult<List<MessageResult>>(tResult);
            }
            catch (Exception ex)
            {
                return new OperatingResult<List<MessageResult>>(null, ex.Message);
            }
        }

        /// <summary>
        /// 获取聊天记录的分页列表
        /// </summary>
        /// <param name="_from">发件人</param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">每个页面包含信息条数</param>
        /// <returns>返回包含聊天纪录的分页列表的操作结果</returns>
        public OperatingResult<PaginationList<MessageResult>> GetMessages(string _from, int page, int pageSize)
        {
            try
            {
                PaginationList<MessageResult> t = OfficialMessageReader.ReadMessages(_from, page, pageSize);
                return new OperatingResult<PaginationList<MessageResult>>(t);
            }
            catch (Exception ex)
            {
                return new OperatingResult<PaginationList<MessageResult>>(null, ex.Message);
            }
        }

        /// <summary>
        /// 发送新消息
        /// </summary>
        /// <param name="import">数据集</param>
        /// <returns>返回操作结果</returns>
        public OperatingResult Send(SendMessageImport import)
        {
            try
            {
                MessageManager.Send(import.From, import.To, import.Content, true);
                return new OperatingResult();
            }
            catch (Exception ex)
            {
                return new OperatingResult(ex.Message);
            }
        }
    }
}
