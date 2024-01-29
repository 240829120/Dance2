using CommunityToolkit.Mvvm.Messaging;
using Dance.Plugin.Project;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 资源管理器管理器
    /// </summary>
    [DanceSingleton(typeof(IExplorerManager))]
    public class ExplorerManager : DanceObject, IExplorerManager
    {

        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 延时管理器
        /// </summary>
        private readonly IDanceDelayManager DelayManager = DanceDomain.Current.LifeScope.Resolve<IDanceDelayManager>();

        /// <summary>
        /// 文件系统监视器
        /// </summary>
        private FileSystemWatcher? FileSystemWatcher;

        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        /// <summary>
        /// 资源管理器信息
        /// </summary>
        public List<ExplorerInfo> ExplorerInfos { get; } = [];

        /// <summary>
        /// 扩展名过滤器
        /// </summary>
        public List<string> ExtensionFilters { get; } = [ProjectOptions.ProjectExtension];

        /// <summary>
        /// 文件名过滤器
        /// </summary>
        public List<string> FileNameFilters { get; } = [ProjectOptions.ProjectCacheFileName, ProjectOptions.ProjectCacheLogFileName];

        /// <summary>
        /// 节点集合
        /// </summary>
        public DanceObservableCollection<ExplorerNodeModel> Nodes { get; } = [];

        // ===================================================================================================
        // **** Public Function ****
        // ===================================================================================================

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="project">项目领域</param>
        public void Initialize(ProjectDomain project)
        {
            this.FileSystemWatcher?.Dispose();
            this.FileSystemWatcher = null;

            // 构建文件树
            ExplorerNodeModel root = new(ExplorerNodeType.Project, project.WorkPath, null);
            root.Expand();

            this.Nodes.Clear();
            this.Nodes.Add(root);

            // 监视文件
            if (Directory.Exists(project.WorkPath) && File.Exists(project.ProjectPath))
            {
                this.FileSystemWatcher = new(project.WorkPath)
                {
                    IncludeSubdirectories = true,
                    EnableRaisingEvents = true,
                    NotifyFilter = NotifyFilters.CreationTime
                                 | NotifyFilters.DirectoryName
                                 | NotifyFilters.FileName
                                 | NotifyFilters.LastWrite
                };
                this.FileSystemWatcher.Created += FileSystemWatcher_Created;
                this.FileSystemWatcher.Deleted += FileSystemWatcher_Deleted;
                this.FileSystemWatcher.Renamed += FileSystemWatcher_Renamed;
                this.FileSystemWatcher.Changed += FileSystemWatcher_Changed;
            }
        }

        /// <summary>
        /// 取消初始化
        /// </summary>
        public void UnInitialize()
        {
            this.FileSystemWatcher?.Dispose();
            this.FileSystemWatcher = null;

            this.Nodes.Clear();
        }

        // ===================================================================================================
        // **** Private Function ****
        // ===================================================================================================

        /// <summary>
        /// 文件创建
        /// </summary>
        private void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (this.ExtensionFilters.Any(p => e.FullPath.EndsWith(p, StringComparison.OrdinalIgnoreCase)))
                return;

            string? parent = Path.GetDirectoryName(e.FullPath);
            if (string.IsNullOrWhiteSpace(parent))
                return;

            if (!Directory.Exists(parent))
                return;

            ExplorerNodeModel? parentNode = this.FindNode(parent);
            if (parentNode == null)
                return;

            ExplorerNodeModel? node = this.FindNode(parentNode, e.FullPath);
            if (node != null)
                return;

            if (Directory.Exists(e.FullPath))
            {
                parentNode.Items.Add(new(ExplorerNodeType.Folder, e.FullPath, parentNode));
            }
            else if (File.Exists(e.FullPath))
            {
                parentNode.Items.Add(new(ExplorerNodeType.File, e.FullPath, parentNode));
            }

            DanceDomain.Current.Messenger.Send(new FileCreateMsg(e.FullPath));
        }

        /// <summary>
        /// 文件删除
        /// </summary>
        private void FileSystemWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if (this.ExtensionFilters.Any(p => e.FullPath.EndsWith(p, StringComparison.OrdinalIgnoreCase)))
                return;

            string? parent = Path.GetDirectoryName(e.FullPath);
            if (string.IsNullOrWhiteSpace(parent))
                return;

            if (!Directory.Exists(parent))
                return;

            ExplorerNodeModel? parentNode = this.FindNode(parent);
            if (parentNode == null)
                return;

            ExplorerNodeModel? node = this.FindNode(parentNode, e.FullPath);
            if (node == null)
                return;

            parentNode.Items.Remove(node);

            DanceDomain.Current.Messenger.Send(new FileDeleteMsg(e.FullPath));
        }

        /// <summary>
        /// 文件改名
        /// </summary>
        private void FileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (this.ExtensionFilters.Any(p => e.FullPath.EndsWith(p, StringComparison.OrdinalIgnoreCase)))
                return;

            string? parent = Path.GetDirectoryName(e.OldFullPath);
            if (string.IsNullOrWhiteSpace(parent))
                return;

            if (!Directory.Exists(parent))
                return;

            ExplorerNodeModel? parentNode = this.FindNode(parent);
            if (parentNode == null)
                return;

            ExplorerNodeModel? node = this.FindNode(parentNode, e.OldFullPath);
            if (node == null)
                return;

            node.InitPath(e.FullPath);

            DanceDomain.Current.Messenger.Send(new FileRenameMsg(e.FullPath, e.OldFullPath));
        }

        /// <summary>
        /// 其他改变
        /// </summary>
        private void FileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
                return;

            if (this.ExtensionFilters.Any(p => e.FullPath.EndsWith(p, StringComparison.OrdinalIgnoreCase)))
                return;


        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>节点</returns>
        private ExplorerNodeModel? FindNode(string path)
        {
            ExplorerNodeModel? next = null;
            foreach (var item in this.Nodes)
            {
                next = this.FindNode(item, path);
                if (next != null)
                    break;
            }

            return next;
        }

        /// <summary>
        /// 查找节点
        /// </summary>
        /// <param name="node">查找开始节点</param>
        /// <param name="path">节点路径</param>
        /// <returns>节点</returns>
        private ExplorerNodeModel? FindNode(ExplorerNodeModel node, string path)
        {
            if (string.Equals(node.Path, path, StringComparison.OrdinalIgnoreCase))
                return node;

            if (path.StartsWith(node.Path, StringComparison.OrdinalIgnoreCase))
            {
                foreach (ExplorerNodeModel item in node.Items)
                {
                    ExplorerNodeModel? next = this.FindNode(item, path);
                    if (next != null)
                        return next;
                }

                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
