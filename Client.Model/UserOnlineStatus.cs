using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Client.Model
{
    /// <summary>
    /// 用户在线状态
    /// </summary>
    [DataContract]
    public enum UserOnlineStatus
    {
        [EnumMember]
        离线 = -2,
        [EnumMember]
        隐身 = -1,
        [EnumMember]
        在线 = 1,
        [EnumMember]
        忙碌 = 2
    }
}
