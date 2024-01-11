using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 勾选项模型基类
    /// </summary>
    public abstract class DanceBarCheckBoxItemModelBase : DanceBarItemModelBase
    {
        // ============================================================================================
        // Property

        #region IsChecked -- 是否勾选

        private bool isChecked;
        /// <summary>
        /// 是否勾选
        /// </summary>
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                this.SetProperty(ref isChecked, value);
                this.OnIsCheckedChnaged(value);
            }
        }

        #endregion

        // ============================================================================================
        // Private Function

        /// <summary>
        /// 当勾选值发生改变时触发
        /// </summary>
        /// <param name="value">是否勾选</param>
        protected abstract void OnIsCheckedChnaged(bool value);
    }
}
