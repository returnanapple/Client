using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.Model;

namespace Client.Model.Manager
{
    /// <summary>
    /// Ip/实际地址对照的管理者对象
    /// </summary>
    public class IpToAddressManager
    {
        /// <summary>
        /// 创建一个新的Ip/实际地址对照
        /// </summary>
        /// <param name="ip">网络地址</param>
        /// <param name="address">实际地址</param>
        public static void Create(string ip, string address)
        {
            using (Model2DataContext db = new Model2DataContext())
            {
                db.PondOfIpToAddress.Where(x => x.Ip == ip)
                    .ToList()
                    .ForEach(x => db.PondOfIpToAddress.Remove(x));
                IpToAddress t = new IpToAddress(ip, address);
                db.PondOfIpToAddress.Add(t);
                db.SaveChanges();
            }
        }
    }
}
