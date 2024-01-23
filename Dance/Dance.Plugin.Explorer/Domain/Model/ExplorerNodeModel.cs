using Dance.Wpf;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 资源管理器节点模型
    /// </summary>
    public class ExplorerNodeModel : DanceModel
    {
        /// <summary>
        /// 资源管理器节点模型
        /// </summary>
        /// <param name="nodeType">节点类型</param>
        /// <param name="path">路径</param>
        /// <param name="parent">父级节点</param>
        public ExplorerNodeModel(ExplorerNodeType nodeType, string path, ExplorerNodeModel? parent)
        {
            this.nodeType = nodeType;
            this.path = path;
            this.directoryName = System.IO.Path.GetDirectoryName(path);
            this.fileName = System.IO.Path.GetFileName(path);
            this.extension = System.IO.Path.GetExtension(path);
            this.parent = parent;

            this.EnterCommand = new(COMMAND_GROUP, "确定", this.Enter);
        }

        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 命令分组
        /// </summary>
        private const string COMMAND_GROUP = "资源管理器节点";

        /// <summary>
        /// 是否加载了子项
        /// </summary>
        private bool IsLoadedItems;

        /// <summary>
        /// 消息管理器
        /// </summary>
        private readonly static IDanceMessageManager MessageManager = DanceDomain.Current.LifeScope.Resolve<IDanceMessageManager>();

        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        /// <summary>
        /// 子项集合
        /// </summary>
        public DanceObservableCollection<ExplorerNodeModel> Items { get; } = [];

        #region Parent -- 父级节点

        private ExplorerNodeModel? parent;
        /// <summary>
        /// 父级节点
        /// </summary>
        public ExplorerNodeModel? Parent
        {
            get { return parent; }
            set { this.SetProperty(ref parent, value); }
        }

        #endregion

        #region IsExpanded -- 是否展开

        private bool isExpanded;
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                this.SetProperty(ref isExpanded, value);
                if (value)
                {
                    this.Expand();
                }
            }
        }

        #endregion

        #region NodeType -- 节点类型

        private ExplorerNodeType nodeType;
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

        private string? directoryName;
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

        private string? fileName;
        /// <summary>
        /// 文件名
        /// </summary>
        public string? FileName
        {
            get { return fileName; }
            set
            {
                if (!this.TryRename(fileName, value))
                {
                    MessageManager.ShowError($"路径: \"{value}\" 已经存在");
                    return;
                }
            }
        }

        #endregion

        #region Path -- 路径

        private string path;

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

        private string? extension;
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

        #region IsEditing -- 是否正在编辑

        private bool isEditing;
        /// <summary>
        /// 是否正在编辑
        /// </summary>
        public bool IsEditing
        {
            get { return isEditing; }
            set { this.SetProperty(ref isEditing, value); }
        }

        #endregion

        // ===================================================================================================
        // **** Command ****
        // ===================================================================================================

        #region EnterCommand -- 确定命令

        /// <summary>
        /// 确定命令
        /// </summary>
        public DanceCommand EnterCommand { get; private set; }

        /// <summary>
        /// 确定
        /// </summary>
        /// <returns></returns>
        private async Task Enter()
        {
            this.IsEditing = false;
            await Task.CompletedTask;
        }

        #endregion

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 初始化路径
        /// </summary>
        /// <param name="path">路径</param>
        public void InitPath(string path)
        {
            this.path = path;
            this.directoryName = System.IO.Path.GetDirectoryName(path);
            this.fileName = System.IO.Path.GetFileName(path);
            this.extension = System.IO.Path.GetExtension(path);

            this.NotifyPropertyChanged(nameof(this.DirectoryName));
            this.NotifyPropertyChanged(nameof(this.FileName));
            this.NotifyPropertyChanged(nameof(this.DirectoryName));
            this.NotifyPropertyChanged(nameof(this.Path));
        }

        /// <summary>
        /// 展开
        /// </summary>
        public void Expand()
        {
            if (this.IsLoadedItems)
                return;

            this.Items.Clear();

            if (this.NodeType == ExplorerNodeType.Project || this.NodeType == ExplorerNodeType.Folder)
            {
                foreach (var folder in System.IO.Directory.GetDirectories(this.Path))
                {
                    this.Items.Add(new(ExplorerNodeType.Folder, folder, this));
                }

                foreach (var file in System.IO.Directory.GetFiles(this.Path))
                {
                    this.Items.Add(new(ExplorerNodeType.File, file, this));
                }
            }

            this.IsLoadedItems = true;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh()
        {
            this.IsLoadedItems = false;
            this.Expand();
        }

        /// <summary>
        /// 尝试重命名
        /// </summary>
        /// <param name="oldName">原始名字</param>
        /// <param name="newName">新名字</param>
        /// <returns>是否成功重命名</returns>
        public bool TryRename(string? oldName, string? newName)
        {
            if (string.IsNullOrWhiteSpace(oldName) || string.IsNullOrWhiteSpace(newName))
                return false;

            string newPath = System.IO.Path.Combine(this.DirectoryName ?? string.Empty, newName);
            if (System.IO.Path.Exists(newPath))
                return false;

            if (this.NodeType == ExplorerNodeType.File)
            {
                File.Move(this.Path, newPath);
            }
            else
            {
                Directory.Move(this.Path, newPath);
            }

            return true;
        }
    }
}
