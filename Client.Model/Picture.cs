using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model
{
    /// <summary>
    /// 图片
    /// </summary>
    public class Picture : ModelBase
    {
        #region 属性

        /// <summary>
        /// 标识码
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 实体
        /// </summary>
        public byte[] Content { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个新的图片
        /// </summary>
        public Picture()
        {
        }

        /// <summary>
        /// 实例化一个新的图片
        /// </summary>
        /// <param name="token">标识码</param>
        /// <param name="content">实体</param>
        public Picture(string token, byte[] content)
        {
            this.Token = token;
            this.Content = content;
        }

        #endregion
    }
}
