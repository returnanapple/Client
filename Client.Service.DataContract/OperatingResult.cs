using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Client.Service.DataContract
{
    /// <summary>
    /// 操作结果
    /// </summary>
    [DataContract]
    public class OperatingResult
    {
        #region 属性

        /// <summary>
        /// 一个布尔值 标识操作是否成功
        /// </summary>
       [DataMember]
        public bool Success { get; set; }

        /// <summary>
        /// 错误信息 如果操作成功 则为空
       /// </summary>
       [DataMember]
        public string Error { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个新的操作结果（成功）
        /// </summary>
        public OperatingResult()
        {
            this.Success = true;
            this.Error = "";
        }

        /// <summary>
        /// 实例化一个新的操作结果（失败）
        /// </summary>
        /// <param name="error">错误信息</param>
        public OperatingResult(string error)
        {
            this.Success = false;
            this.Error = error;
        }
        #endregion
    }

    /// <summary>
    /// 操作结果的泛型实现
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    [DataContract]
    public class OperatingResult<T> : OperatingResult
    {
        #region 属性

        /// <summary>
        /// 主体
        /// </summary>
        [DataMember]
        public T Content { get; set; }

        #endregion

        #region 构造方法

        /// <summary>
        /// 实例化一个新的操作结果（成功）
        /// </summary>
        /// <param name="content">主体</param>
        public OperatingResult(T content)
        {
            this.Content = content;
        }

        /// <summary>
        /// 实例化一个新的操作结果（失败）
        /// </summary>
        /// <param name="content">主体</param>
        /// <param name="error">错误信息</param>
        public OperatingResult(T content, string error)
            : base(error)
        {
            this.Content = content;
        }

        #endregion
    }
}
