using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Client.CustomerService.Framework
{
    /// <summary>
    /// 视图模型的基类
    /// </summary>
    public abstract class ViewModelBase : ModelBase
    {
        #region 私有参数

        bool isBusy = false;
        string viewTitle = "";

        #endregion

        #region 公开属性

        /// <summary>
        /// 一个布尔值 标识线程正忙
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
            }
        }

        /// <summary>
        /// 界面标题
        /// </summary>
        public string ViewTitle
        {
            get
            {
                return viewTitle;
            }
            set
            {
                if (viewTitle != value)
                {
                    viewTitle = value;
                    OnPropertyChanged("ViewTitle");
                }
            }
        }

        #endregion
    }
}
