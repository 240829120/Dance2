using DevExpress.Xpf.Editors;
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
    public class DanceBarCheckBoxItemModel : DanceBarItemModelBase
    {
        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        #region IsChecked -- 是否勾选

        private bool isChecked;
        /// <summary>
        /// 是否勾选
        /// </summary>
        public bool IsChecked
        {
            get { return isChecked; }
            set { this.SetProperty(ref isChecked, value); }
        }

        #endregion
    }
}
