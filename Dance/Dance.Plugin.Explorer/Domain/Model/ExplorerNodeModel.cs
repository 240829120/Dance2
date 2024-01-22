using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 资源管理器节点模型
    /// </summary>
    /// <param name="nodeType">节点类型</param>
    /// <param name="path">路径</param>
    public class ExplorerNodeModel(ExplorerNodeType nodeType, string path) : DanceModel
    {
        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 是否已经展开过
        /// </summary>
        private bool IsExpanded;

        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        /// <summary>
        /// 子项集合
        /// </summary>
        public DanceObservableCollection<ExplorerNodeModel> Items { get; } = [];

        #region HasItems -- 是否拥有子项

        private bool hasItems;
        /// <summary>
        /// 是否拥有子项
        /// </summary>
        public bool HasItems
        {
            get { return hasItems; }
            set { this.SetProperty(ref hasItems, value); }
        }

        #endregion

        #region NodeType -- 节点类型

        private ExplorerNodeType nodeType = nodeType;
        /// <summary>
        /// 节点类型
        /// </summary>
        public ExplorerNodeType NodeType
        {
            get { return nodeType; }
            set { this.SetProperty(ref nodeType, value); }
        }

        #endregion

        #region DirectoryName -- 文件夹名称

        private string? directoryName = System.IO.Path.GetDirectoryName(path);
        /// <summary>
        /// 文件夹名称
        /// </summary>
        public string? DirectoryName
        {
            get { return directoryName; }
            set { this.SetProperty(ref directoryName, value); }
        }

        #endregion

        #region FileName -- 文件名

        private string? fileName = System.IO.Path.GetFileName(path);
        /// <summary>
        /// 文件名
        /// </summary>
        public string? FileName
        {
            get { return fileName; }
            set { this.SetProperty(ref fileName, value); }
        }

        #endregion

        #region Path -- 路径

        private string path = path;

        /// <summary>
        /// 路径
        /// </summary>
        public string Path
        {
            get { return path; }
            set { this.SetProperty(ref path, value); }
        }

        #endregion

        #region Extension -- 扩展名

        private string? extension = System.IO.Path.GetExtension(path);
        /// <summary>
        /// 扩展名
        /// </summary>
        public string? Extension
        {
            get { return extension; }
            set { extension = value; }
        }

        #endregion

        #region Info -- 信息

        private ExplorerInfo? info;
        /// <summary>
        /// 信息
        /// </summary>
        public ExplorerInfo? Info
        {
            get { return info; }
            set { this.SetProperty(ref info, value); }
        }

        #endregion

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 展开
        /// </summary>
        public void Expand()
        {
            if (this.IsExpanded)
                return;

            this.Items.Clear();

            if (this.NodeType == ExplorerNodeType.Project || this.NodeType == ExplorerNodeType.Folder)
            {
                foreach (var folder in System.IO.Directory.GetDirectories(this.Path))
                {
                    this.Items.Add(new(ExplorerNodeType.Folder, folder) { HasItems = true });
                }

                foreach (var file in System.IO.Directory.GetFiles(this.Path))
                {
                    this.Items.Add(new(ExplorerNodeType.File, file) { HasItems = false });
                }
            }

            this.IsExpanded = true;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh()
        {
            this.IsExpanded = false;
            this.Expand();
        }
    }
}
