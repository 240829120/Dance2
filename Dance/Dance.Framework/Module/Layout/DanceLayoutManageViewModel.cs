using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 布局视图管理模型
    /// </summary>
    public class DanceLayoutManageViewModel : DanceViewModel
    {
        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        #region Layouts -- 布局集合

        private DanceObservableCollection<DanceLayoutModel> layouts = [];

        /// <summary>
        /// 布局集合
        /// </summary>
        public DanceObservableCollection<DanceLayoutModel> Layouts
        {
            get { return layouts; }
            set { this.SetProperty(ref layouts, value); }
        }

        #endregion


    }
}
