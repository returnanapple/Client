using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model
{
    /// <summary>
    /// 数据模型的基类
    /// </summary>
    public class ModelBase
    {
        #region 属性

        /// <summary>
        /// 存储指针
        /// </summary>
        public int Id { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个新的数据模型
        /// </summary>
        public ModelBase()
        {
        }

        #endregion
    }
}
