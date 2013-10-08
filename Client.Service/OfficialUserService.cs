﻿using Client.Model;
using Client.Model.Manager;
using Client.Service.DataContract;
using Client.Service.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Service
{
    public class OfficialUserService : IOfficialUserService
    {
        /// <summary>
        /// 用户登入
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="onlineStatus">在线状态</param>
        /// <returns>返回操作结果</returns>
        public OperatingResult SetIn(string username, UserOnlineStatus onlineStatus)
        {
            try
            {
                UserManager.SetIn(username, onlineStatus, true);
                return new OperatingResult();
            }
            catch (Exception ex)
            {
                return new OperatingResult(ex.Message);
            }
        }

        /// <summary>
        /// 改变在线状态
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="newOnlineStatus">新的在线状态</param>
        /// <returns>返回操作结果</returns>
        public OperatingResult ChangeOnlineStatus(string username, UserOnlineStatus newOnlineStatus)
        {
            try
            {
                UserManager.ChangeOnlineStatus(username, newOnlineStatus, true);
                return new OperatingResult();
            }
            catch (Exception ex)
            {
                return new OperatingResult(ex.Message);
            }
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns>返回包含用户列表的操作结果（如果操作失败列表为空）</returns>
        public OperatingResult<List<UserInfoResult>> GetUsers()
        {
            try
            {
                List<UserInfoResult> result = OfficialUserReader.ReadUsers();
                UserManager.Pond.Where(x => result.Any(r => r.Username == x.Username))
                    .ToList().ForEach(x =>
                    {
                        result.First(user => user.Username == x.Username).OnlineStatus = x.OnlineStatus;
                    });
                List<UserInfoResult> t = UserManager.Pond.Where(x => x.IsOfficial == true)
                    .ToList().ConvertAll(x =>
                        new UserInfoResult(x.Username, UserInfoType.客服) { OnlineStatus = x.OnlineStatus }
                        );
                result.AddRange(t);

                return new OperatingResult<List<UserInfoResult>>(result);
            }
            catch (Exception ex)
            {
                return new OperatingResult<List<UserInfoResult>>(null, ex.Message);
            }
        }
    }
}
