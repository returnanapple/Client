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
        客服,
        [EnumMember]
        上级,
        [EnumMember]
        下级
    }
}
