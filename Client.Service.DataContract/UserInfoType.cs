using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Client.Service.DataContract
{
    /// <summary>
    /// 用户类型
    /// </summary>
    [DataContract]
    public enum UserInfoType
    {
        [EnumMember]
        上级 = 101,
        [EnumMember]
        下级 = 102,
        [EnumMember]
        用户 = 201,
        [EnumMember]
        客服 = 301,
    }
}
