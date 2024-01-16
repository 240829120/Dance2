using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace Dance.Framework
{
    /// <summary>
    /// 按钮项模型基类
    /// </summary>
    public class DanceBarButtonItemModel : DanceBarDefaultItemModelBase
    {
        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        #region ClickCommand -- 点击命令

        private DanceCommand? clickCommand;

        /// <summary>
        /// 点击命令
        /// </summary>
        public DanceCommand? ClickCommand
        {
            get { return clickCommand; }
            set { this.SetProperty(ref clickCommand, value); }
        }

        #endregion
    }
}
