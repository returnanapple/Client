using Client.Service.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Client.Service.Reader;
using Client.Service.Aid;
using Client.Model.Manager;
using Client.Model;

namespace Client.Service
{
    /// <summary>
    /// 聊天服务
    /// </summary>
    public class ChatService : IChatService
    {
        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="self">自己的用户名</param>
        /// <param name="isOfficia">一个布尔值 表示是否官方用户</param>
        /// <returns>返回登入结果</returns>
        public SetInResult RegisterAndGetFriendList(string self, bool isOfficia = false)
        {
            IChatServiceCallback callback = OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();
            MyPond.Default.SetIn(self, isOfficia, callback);
            List<UserInfoResult> users = MyPond.Default.GetFriendList(self, isOfficia);
            List<UnreadMessageCountResult> counts = MessageReader.ReadCountOfUnreadMessages(self);
            return new SetInResult(users, counts);
        }

        /// <summary>
        /// 改变所要订阅的消息的发布对象
        /// </summary>
        /// <param name="targetUser">目标用户的用户名</param>
        /// <param name="self">自己的用户名</param>
        /// <returns>返回目标对象相对于自己的唯独消息</returns>
        public List<MessageResult> ChangeTargetUser(string targetUser, string self)
        {
            MyPond.Default.ChangeTargetUser(self, targetUser);
            return MessageReader.ReadUnreadMessages(targetUser, self);
        }

        /// <summary>
        /// 改变在线状态
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newOnlineStatus"></param>
        public void ChangeStatus(string username, UserOnlineStatus newOnlineStatus)
        {
            MyPond.Default.ChangeOnlieStatus(username, newOnlineStatus);
        }

        /// <summary>
        /// 保持心跳
        /// </summary>
        /// <param name="username">用户名</param>
        public void KeepHeartbeat(string username)
        {
            MyPond.Default.KeepHeartbeat(username);
        }

        /// <summary>
        /// 发送新消息
        /// </summary>
        /// <param name="import">数据集</param>
        public void SendMessage(SendMessageImport import)
        {
            #region 判断发件人是否在线
            if (!MyPond.Default.Tokens.Any(x => x.Token.Username == import.From))
            {
                throw new Exception("您可以离线 请重新登陆");
            }

            #endregion
            #region 判断收件人是否在线并且正在关注发件人
            bool targetUserIsOnlineAndLookAtMe = MyPond.Default.Tokens.Any(x => x.Token.Username == import.To
                && x.TargetUser == import.From);

            #endregion
            string ip = WebHelper.GetEndpoint().Address;
            string address = WebHelper.GetAddress(ip);
            MessageResult mr = new MessageResult
            {
                From = import.From,
                To = import.To,
                IsSelf = true,
                Content = import.Content,
                IsOfficial = import.IsOfficial,
                SendTime = DateTime.Now,
                Ip = ip,
                Address = address
            };
            #region 向发件人的聊天面板写入聊天信息
            try
            {
                MyPond.Default.Tokens.First(x => x.Token.Username == import.From).Callback.WriteMessage(mr);
            }
            catch (Exception)
            {
                MyPond.Default.RemoveToken(import.From);
            }
            #endregion
            #region 想收件人通报新信息
            if (targetUserIsOnlineAndLookAtMe)
            {
                try
                {
                    mr.IsSelf = false;
                    MyPond.Default.Tokens.First(x => x.Token.Username == import.To).Callback.WriteMessage(mr);
                }
                catch (Exception)
                {
                    targetUserIsOnlineAndLookAtMe = false;
                    MyPond.Default.RemoveToken(import.To);
                }
            }
            else if (MyPond.Default.Tokens.Any(x => x.Token.Username == import.To))
            {
                try
                {
                    MyPond.Default.Tokens.First(x => x.Token.Username == import.To).Callback.AddTheCountOfNewMessageForSomeone(import.From);
                }
                catch (Exception)
                {
                    MyPond.Default.RemoveToken(import.To);
                }
            }
            #endregion
            MessageManager.Send(import.From, import.To, import.Content, ip, address
                , targetUserIsOnlineAndLookAtMe, import.IsOfficial);
        }

        /// <summary>
        /// 获取聊天记录的分页列表
        /// </summary>
        /// <param name="targetUser">对方的用户名</param>
        /// <param name="self">自己的用户名</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns>返回聊天记录的分页列表</returns>
        public PaginationList<MessageResult> GetMessages(string targetUser, string self, int pageIndex, int pageSize)
        {
            return MessageReader.ReadMessages(targetUser, self, pageIndex, pageSize);
        }
    }
}
