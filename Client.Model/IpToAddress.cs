using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model
{
    /// <summary>
    /// Ip/实际地址对照
    /// </summary>
    public class IpToAddress : ModelBase
    {
        #region 属性

        /// <summary>
        /// 网络地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 实际地址
        /// </summary>
        public string Address { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个新的Ip/实际地址对照
        /// </summary>
        public IpToAddress()
        {
        }

        /// <summary>
        /// 实例化一个新的Ip/实际地址对照
        /// </summary>
        /// <param name="ip">网络地址</param>
        /// <param name="address">实际地址</param>
        public IpToAddress(string ip, string address)
        {
            this.Ip = ip;
            this.Address = address;
        }

        #endregion
    }
}
