using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model
{
    /// <summary>
    /// 记录创建时间和最新修改时间的数据模型的基类
    /// </summary>
    public class RecordingTimeModelBase : ModelBase
    {
        #region 属性

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 最后一次修改的时间
        /// </summary>
        public DateTime ModifiedTime { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个新的记录创建时间和最新修改时间的数据模型
        /// </summary>
        public RecordingTimeModelBase()
        {
            this.CreatedTime = DateTime.Now;
            this.ModifiedTime = DateTime.Now;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 声明数据模型已经被修改
        /// </summary>
        public void OnModify()
        {
            this.ModifiedTime = DateTime.Now;
        }

        #endregion
    }
}
