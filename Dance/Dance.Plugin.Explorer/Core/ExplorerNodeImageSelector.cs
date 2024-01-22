using Dance.Framework;
using Dance.Wpf;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 资源管理器节点图标选择器
    /// </summary>
    public class ExplorerNodeImageSelector : TreeListNodeImageSelector
    {
        public ExplorerNodeImageSelector()
        {
            this.FolderIcon = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Explorer;component/Themes/Icons/folder.svg");
            this.UnknowIcon = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Explorer;component/Themes/Icons/unknow.svg");
            this.ProjectIcon = this.CacheManager.GetImage("pack://application:,,,/Dance.Plugin.Explorer;component/Themes/Icons/project.svg");
        }

        /// <summary>
        /// 资源管理器管理器
        /// </summary>
        private readonly IExplorerManager ExplorerManager = DanceDomain.Current.LifeScope.Resolve<IExplorerManager>();

        /// <summary>
        /// 缓存管理器
        /// </summary>
        private readonly IDanceCacheManager CacheManager = DanceDomain.Current.LifeScope.Resolve<IDanceCacheManager>();

        /// <summary>
        /// 文件夹图标
        /// </summary>
        private readonly ImageSource? FolderIcon;

        /// <summary>
        /// 未知图标
        /// </summary>
        private readonly ImageSource? UnknowIcon;

        /// <summary>
        /// 项目图标
        /// </summary>
        private readonly ImageSource? ProjectIcon;

        /// <summary>
        /// 选择图标
        /// </summary>
        /// <param name="rowData">行数据</param>
        /// <returns>图标</returns>
        public override ImageSource? Select(DevExpress.Xpf.Grid.TreeList.TreeListRowData rowData)
        {
            if (rowData.Row is not ExplorerNodeModel model)
                return this.UnknowIcon;

            if (model.NodeType == ExplorerNodeType.Project)
                return this.ProjectIcon;

            if (model.NodeType == ExplorerNodeType.Folder)
                return this.FolderIcon;

            ExplorerInfo? info = this.ExplorerManager.ExplorerInfos.FirstOrDefault(p => p.Extension == model.Extension);
            if (info == null)
                return this.UnknowIcon;

            return info.Icon;
        }
    }
}
