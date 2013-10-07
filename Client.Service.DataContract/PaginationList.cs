using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Client.Service.DataContract
{
    [DataContract]
    public class PaginationList<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        [DataMember]
        public int PageIndex { get; set; }

        /// <summary>
        /// 页面大小
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// 总信息条数
        /// </summary>
        [DataMember]
        public int Total { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [DataMember]
        public int TotalOfPage { get; set; }

        /// <summary>
        /// 主体
        /// </summary>
        [DataMember]
        public List<T> Content { get; set; }

        /// <summary>
        /// 实例化一个新的分页列表（成功）
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="total">页面大小</param>
        /// <param name="content">主体</param>
        public PaginationList(int pageIndex, int pageSize, int total, List<T> content)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
            this.Total = total;
            this.TotalOfPage = total % pageSize == 0 ? total / pageSize : total / pageSize + 1;
            this.Content = new List<T>();

            if (this.TotalOfPage == 0)
            {
                this.TotalOfPage = 1;
            }
            this.Content.AddRange(content);
        }
    }
}
