using Dance.Framework;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.LayoutManage
{
    /// <summary>
    /// 布局视图管理模型
    /// </summary>
    public class DanceLayoutManageWindowModel : DanceViewModel
    {
        public DanceLayoutManageWindowModel()
        {
            this.LoadedCommand = new("布局管理加载", this.Loaded);
        }

        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 配置管理器
        /// </summary>
        private readonly IDanceConfigManager ConfigManager = DanceDomain.Current.LifeScope.Resolve<IDanceConfigManager>();

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

        // ===================================================================================================
        // **** Command ****
        // ===================================================================================================

        #region LoadedCommand -- 加载命令

        /// <summary>
        /// 加载命令
        /// </summary>
        public DanceCommand LoadedCommand { get; private set; }

        /// <summary>
        /// 加载
        /// </summary>
        private async Task Loaded()
        {
            await Task.Run(() =>
            {
                var layouts = this.ConfigManager.Context.Layouts.FindAll();
                foreach (var layout in layouts)
                {
                    this.Layouts.Add(new DanceLayoutModel(layout)
                    {
                        Name = layout.Name,
                        Content = layout.Content
                    });
                }
            });
        }

        #endregion
    }
}
