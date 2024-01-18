using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Dock
{
    /// <summary>
    /// 视图菜单项模型
    /// </summary>
    public class ViewBarCheckBoxItemModel : DanceBarItemModelBase
    {
        #region DockingItem -- Docking项

        private DanceDockItemViewModelBase? dockingItem;

        /// <summary>
        /// Docking项
        /// </summary>
        public DanceDockItemViewModelBase? DockingItem
        {
            get { return dockingItem; }
            set { this.SetProperty(ref dockingItem, value); }
        }

        #endregion
    }
}
