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
    public class DanceBarButtonItemModel : DanceBarItemModelBase
    {
        /// <summary>
        /// 按钮项模型基类
        /// </summary>
        public DanceBarButtonItemModel()
        {
            this.ClickCommand = new(this.Click);
        }

        /// <summary>
        /// 点击时触发
        /// </summary>
        public event EventHandler<EventArgs>? OnClick;

        // ============================================================================================
        // Command

        #region ClickCommand -- 点击命令

        /// <summary>
        /// 点击命令
        /// </summary>
        public RelayCommand ClickCommand { get; private set; }

        /// <summary>
        /// 点击
        /// </summary>
        private void Click()
        {
            this.OnClick?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
