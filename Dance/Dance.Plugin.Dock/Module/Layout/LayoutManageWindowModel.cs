using Dance.Framework;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.Plugin.Dock
{
    /// <summary>
    /// 布局视图管理模型
    /// </summary>
    public class LayoutManageWindowModel : DanceViewModel
    {
        public LayoutManageWindowModel()
        {
            this.LoadedCommand = new("布局管理", "布局管理加载", this.Loaded);
            this.MoveUpCommand = new("布局管理", "上移", this.MoveUp);
            this.MoveDownCommand = new("布局管理", "下移", this.MoveDown);
            this.DeleteCommand = new("布局管理", "删除", this.Delete);
            this.EnterCommand = new("布局管理", "保存布局", this.Enter);
            this.CancelCommand = new("布局管理", "取消", this.Cancel);
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

        #region SlectedLayout -- 当前选中布局

        private DanceLayoutModel? selectedLayout;
        /// <summary>
        /// 当前选中布局
        /// </summary>
        public DanceLayoutModel? SelectedLayout
        {
            get { return selectedLayout; }
            set { this.SetProperty(ref selectedLayout, value); }
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
                var layouts = this.ConfigManager.Context.Layouts.Find(p => !p.IsMainLayout && !p.IsDefaultLayout);
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

        #region MoveUpCommand -- 上移命令

        /// <summary>
        /// 上移命令
        /// </summary>
        public DanceCommand MoveUpCommand { get; private set; }

        /// <summary>
        /// 上移
        /// </summary>
        private async Task MoveUp()
        {
            if (this.SelectedLayout == null)
                return;

            int index = this.Layouts.IndexOf(this.SelectedLayout);
            if (index <= 0)
                return;

            this.Layouts.Move(index, index - 1);

            await Task.CompletedTask;
        }

        #endregion

        #region MoveDownCommand -- 下移命令

        /// <summary>
        /// 下移命令
        /// </summary>
        public DanceCommand MoveDownCommand { get; private set; }

        /// <summary>
        /// 下移
        /// </summary>
        private async Task MoveDown()
        {
            if (this.SelectedLayout == null)
                return;

            int index = this.Layouts.IndexOf(this.SelectedLayout);
            if (index >= this.Layouts.Count - 1)
                return;

            this.Layouts.Move(index, index + 1);

            await Task.CompletedTask;
        }

        #endregion

        #region DeleteCommand -- 删除命令

        /// <summary>
        /// 删除命令
        /// </summary>
        public DanceCommand DeleteCommand { get; private set; }

        /// <summary>
        /// 删除
        /// </summary>
        private async Task Delete()
        {
            if (this.SelectedLayout == null)
                return;

            this.Layouts.Remove(this.SelectedLayout);
            this.SelectedLayout = null;

            await Task.CompletedTask;
        }

        #endregion

        #region EnterCommand -- 确定命令

        /// <summary>
        /// 确定命令
        /// </summary>
        public DanceCommand EnterCommand { get; private set; }

        /// <summary>
        /// 确定
        /// </summary>
        private async Task Enter()
        {
            if (this.View is not Window window)
                return;

            this.ConfigManager.Context.Layouts.DeleteMany(p => !p.IsMainLayout && !p.IsDefaultLayout);
            for (int i = 0; i < this.Layouts.Count; i++)
            {
                DanceLayoutModel model = this.Layouts[i];
                model.Entity.Order = i;
                model.Entity.Name = model.Name;

                this.ConfigManager.Context.Layouts.Upsert(model.Entity);
            }

            window.DialogResult = true;
            window.Close();

            await Task.CompletedTask;
        }

        #endregion

        #region CancelCommand -- 取消命令

        /// <summary>
        /// 取消命令
        /// </summary>
        public DanceCommand CancelCommand { get; private set; }

        /// <summary>
        /// 取消
        /// </summary>
        private async Task Cancel()
        {
            if (this.View is not Window window)
                return;

            window.DialogResult = false;
            window.Close();

            await Task.CompletedTask;
        }

        #endregion

    }
}
