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
    public class DanceBarCheckBoxItemModel : DanceBarDefaultItemModelBase
    {
        /// <summary>
        /// 勾选时触发
        /// </summary>
        public event EventHandler<EditValueChangedEventArgs>? OnChecked;

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
                bool oldValue = isChecked;

                this.SetProperty(ref isChecked, value);
                this.OnChecked?.Invoke(this, new EditValueChangedEventArgs(oldValue, value));
            }
        }

        #endregion
    }
}
