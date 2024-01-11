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
    public abstract class DanceBarButtonItemModelBase : DanceBarItemModelBase
    {
        /// <summary>
        /// 按钮项模型基类
        /// </summary>
        public DanceBarButtonItemModelBase()
        {
            this.ClickCommand = new(this.Click);
        }

        // ============================================================================================
        // Command

        #region ClickCommand -- 点击命令

        /// <summary>
        /// 点击命令
        /// </summary>
        public RelayCommand? ClickCommand { get; set; }

        /// <summary>
        /// 点击
        /// </summary>
        protected abstract void Click();

        #endregion
    }
}
