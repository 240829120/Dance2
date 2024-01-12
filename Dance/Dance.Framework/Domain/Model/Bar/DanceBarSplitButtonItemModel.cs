using DevExpress.Xpf.Bars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 分隔项模型
    /// </summary>
    public class DanceBarSplitButtonItemModel : DanceBarButtonItemModel
    {
        #region PopupControl -- 弹出控件

        private IPopupControl? popupControl;
        /// <summary>
        /// 弹出控件
        /// </summary>
        public IPopupControl? PopupControl
        {
            get { return popupControl; }
            set { this.SetProperty(ref popupControl, value); }
        }

        #endregion
    }
}
