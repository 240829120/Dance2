using Dance.Framework;
using Dance.Plugin.Project;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Xpf.Grid.TreeList;
using System.Windows.Input;
using DevExpress.Xpf.Grid;
using System.Text.RegularExpressions;
using System.Windows;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using DevExpress.Xpf.Core;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 资源管理器视图模型
    /// </summary>
    [DanceSingleton]
    public class ExplorerViewModel : DanceLayoutViewModel
    {
        public ExplorerViewModel()
        {
            this.NodeDoubleClickCommand = new(COMMAND_GROUP, "节点双击", this.NodeDoubleClick);
            this.NodeEditingCommand = new(COMMAND_GROUP, "节点编辑", this.NodeEditing);
            this.AddFolderCommand = new(COMMAND_GROUP, "新建文件夹", this.AddFolder);

            this.CutCommand = new(COMMAND_GROUP, "剪切", this.Cut);
            this.CopyCommand = new(COMMAND_GROUP, "复制", this.Copy);
            this.PasteCommand = new(COMMAND_GROUP, "粘贴", this.Paste);
            this.DeleteCommand = new(COMMAND_GROUP, "删除", this.Delete);

            this.CopyFullPathCommand = new(COMMAND_GROUP, "复制完整路径", this.CopyFullPath);
            this.OpenInWindowsCommand = new(COMMAND_GROUP, "在资源管理器中打开", this.OpenInWindows);

            this.DropCommand = new(COMMAND_GROUP, "拖拽完成", this.Drop);

            DanceDomain.Current.Messenger.Register<ProjectClosedMsg>(this, this.OnProjectClosed);
            DanceDomain.Current.Messenger.Register<ProjectOpendMsg>(this, this.OnProjectOpend);
        }

        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 命令分组
        /// </summary>
        private const string COMMAND_GROUP = "资源管理器";

        /// <summary>
        /// 资源管理器管理器
        /// </summary>
        private readonly IExplorerManager ExplorerManager = DanceDomain.Current.LifeScope.Resolve<IExplorerManager>();

        /// <summary>
        /// 项目管理器
        /// </summary>
        private readonly IProjectManager ProjectManager = DanceDomain.Current.LifeScope.Resolve<IProjectManager>();

        /// <summary>
        /// 消息管理器
        /// </summary>
        private readonly IDanceMessageManager MessageManager = DanceDomain.Current.LifeScope.Resolve<IDanceMessageManager>();

        /// <summary>
        /// 剪切节点
        /// </summary>
        private readonly List<ExplorerNodeModel> CutNodes = [];

        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        #region Nodes -- 节点集合

        private DanceObservableCollection<ExplorerNodeModel>? nodes;
        /// <summary>
        /// 节点集合
        /// </summary>
        public DanceObservableCollection<ExplorerNodeModel>? Nodes
        {
            get { return nodes; }
            set { this.SetProperty(ref nodes, value); }
        }

        #endregion

        #region SelectedNode -- 当前选中节点

        private ExplorerNodeModel? selectedNode;
        /// <summary>
        /// 当前选中节点
        /// </summary>
        public ExplorerNodeModel? SelectedNode
        {
            get { return selectedNode; }
            set
            {
                if (selectedNode != null)
                {
                    selectedNode.IsEditing = false;
                }
                this.SetProperty(ref selectedNode, value);
            }
        }

        #endregion

        // ===================================================================================================
        // **** Command ****
        // ===================================================================================================

        #region NodeDoubleClickCommand -- 节点双击命令

        /// <summary>
        /// 节点双击命令
        /// </summary>
        public DanceCommand<NodeDoubleClickEventArgs> NodeDoubleClickCommand { get; private set; }

        /// <summary>
        /// 节点双击
        /// </summary>
        private async Task NodeDoubleClick(NodeDoubleClickEventArgs? e)
        {
            if (e == null || this.SelectedNode == null || this.SelectedNode.IsEditing || e.ChangedButton != MouseButton.Left)
                return;

            if (this.SelectedNode.NodeType == ExplorerNodeType.File)
            {
                FileOpeningMsg openingMsg = new(this.SelectedNode.Path);
                DanceDomain.Current.Messenger.Send(openingMsg);
                if (openingMsg.IsCancel)
                    return;

                FileOpendMsg opendMsg = new(this.SelectedNode.Path);
                DanceDomain.Current.Messenger.Send(opendMsg);
            }
            else
            {
                this.SelectedNode.IsExpanded = !this.SelectedNode.IsExpanded;
            }

            await Task.CompletedTask;
        }

        #endregion

        #region NodeEditingCommand -- 节点编辑命令

        /// <summary>
        /// 节点编辑命令
        /// </summary>
        public DanceCommand NodeEditingCommand { get; private set; }

        /// <summary>
        /// 节点编辑
        /// </summary>
        private async Task NodeEditing()
        {
            if (this.SelectedNode == null)
                return;

            this.SelectedNode.IsEditing = true;

            await Task.CompletedTask;
        }

        #endregion

        #region AddFolderCommand -- 新建文件夹命令

        /// <summary>
        /// 新建文件夹命令
        /// </summary>
        public DanceCommand<ExplorerNodeModel> AddFolderCommand { get; private set; }

        /// <summary>
        /// 新建文件夹
        /// </summary>
        /// <param name="args">参数</param>
        private async Task AddFolder(ExplorerNodeModel? args)
        {
            if (args == null)
                return;

            List<int> indexs = [];

            foreach (var item in args.Items)
            {
                if (string.IsNullOrWhiteSpace(item.FileName))
                    continue;

                var matches = Regex.Matches(item.FileName, "^新建文件夹([0-9]*)$");
                var match = matches.FirstOrDefault();
                if (match == null || match.Groups.Count != 2)
                    continue;

                if (!int.TryParse(match.Groups[1].Value, out int value))
                    continue;

                indexs.Add(value);
            }

            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(args.Path, $"新建文件夹{indexs.MaxOrDefault() + 1}"));

            await Task.CompletedTask;
        }

        #endregion

        #region CutCommand -- 剪切命令

        /// <summary>
        /// 剪切命令
        /// </summary>
        public DanceCommand<IList> CutCommand { get; private set; }

        /// <summary>
        /// 剪切
        /// </summary>
        /// <param name="args">参数</param>
        private async Task Cut(IList? args)
        {
            if (args == null || args.Count == 0)
                return;

            this.CutNodes.ForEach(p => p.IsCuting = false);
            this.CutNodes.Clear();

            StringCollection files = [];
            foreach (var item in args)
            {
                if (item is not ExplorerNodeModel model)
                    continue;

                model.IsCuting = true;
                this.CutNodes.Add(model);
                files.Add(model.Path);
            }

            Clipboard.SetFileDropList(files);

            await Task.CompletedTask;
        }

        #endregion

        #region CopyCommand -- 复制命令

        /// <summary>
        /// 复制命令
        /// </summary>
        public DanceCommand<IList> CopyCommand { get; private set; }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="args">参数</param>
        private async Task Copy(IList? args)
        {
            if (args == null || args.Count == 0)
                return;
            this.CutNodes.ForEach(p => p.IsCuting = false);
            this.CutNodes.Clear();

            StringCollection files = [];
            foreach (var item in args)
            {
                if (item is not ExplorerNodeModel model)
                    continue;

                files.Add(model.Path);
            }

            Clipboard.SetFileDropList(files);

            await Task.CompletedTask;
        }

        #endregion

        #region PasteCommand -- 粘贴命令

        /// <summary>
        /// 粘贴命令
        /// </summary>
        public DanceCommand PasteCommand { get; private set; }

        /// <summary>
        /// 粘贴
        /// </summary>
        private async Task Paste()
        {
            if (this.SelectedNode == null)
                return;

            if (this.CutNodes.Count > 0)
            {
                if (this.SelectedNode.NodeType == ExplorerNodeType.File)
                {
                    if (this.SelectedNode.Parent == null || this.SelectedNode.Parent.NodeType == ExplorerNodeType.File)
                        return;

                    Win32FileHelper.Move(this.CutNodes.Select(p => p.Path).ToList(), this.SelectedNode.Parent.Path);
                }
                else
                {
                    Win32FileHelper.Move(this.CutNodes.Select(p => p.Path).ToList(), this.SelectedNode.Path);
                }

                this.CutNodes.Clear();
            }
            else
            {
                List<string> files = [.. Clipboard.GetFileDropList()];

                if (this.SelectedNode.NodeType == ExplorerNodeType.File)
                {
                    if (this.SelectedNode.Parent == null || this.SelectedNode.Parent.NodeType == ExplorerNodeType.File)
                        return;

                    Win32FileHelper.Copy(files, this.SelectedNode.Parent.Path, true);
                }
                else
                {
                    Win32FileHelper.Copy(files, this.SelectedNode.Path, true);
                }
            }

            await Task.CompletedTask;
        }

        #endregion

        #region DeleteCommand -- 删除命令

        /// <summary>
        /// 删除命令
        /// </summary>
        public DanceCommand<IList> DeleteCommand { get; private set; }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="args">参数</param>
        private async Task Delete(IList? args)
        {
            if (args == null || args.Count == 0)
                return;

            if (this.MessageManager.Show("警告", "确定删除选择项?", MessageBoxButton.YesNo, MessageBoxImage.Warning, null) != MessageBoxResult.Yes)
                return;

            List<string> files = [];
            foreach (var item in args)
            {
                if (item is not ExplorerNodeModel model)
                    continue;

                files.Add(model.Path);
            }

            Win32FileHelper.Delete(files);

            await Task.CompletedTask;
        }

        #endregion

        #region CopyFullPathCommand -- 复制完整路径命令

        /// <summary>
        /// 复制完整路径命令
        /// </summary>
        public DanceCommand<ExplorerNodeModel> CopyFullPathCommand { get; private set; }

        /// <summary>
        /// 复制完整路径
        /// </summary>
        /// <param name="args">参数</param>
        private async Task CopyFullPath(ExplorerNodeModel? args)
        {
            if (args == null)
                return;

            Clipboard.SetText(args.Path);

            await Task.CompletedTask;
        }

        #endregion

        #region OpenInWindowsCommand -- 在资源管理器中打开命令

        /// <summary>
        /// 在资源管理器中打开命令
        /// </summary>
        public DanceCommand<ExplorerNodeModel> OpenInWindowsCommand { get; private set; }

        /// <summary>
        /// 在资源管理器中打开
        /// </summary>
        /// <param name="args">参数</param>
        private async Task OpenInWindows(ExplorerNodeModel? args)
        {
            if (args == null)
                return;

            Process.Start("explorer.exe", $"/select,{args.Path}");

            await Task.CompletedTask;
        }

        #endregion

        #region DropCommand -- 拖拽完成命令

        /// <summary>
        /// 拖拽完成命令
        /// </summary>
        public DanceCommand<DropRecordEventArgs> DropCommand { get; private set; }

        /// <summary>
        /// 拖拽完成
        /// </summary>
        /// <param name="args">参数</param>
        private async Task Drop(DropRecordEventArgs? args)
        {
            if (args == null || args.TargetRecord is not ExplorerNodeModel target)
                return;

            args.Effects = DragDropEffects.None;

            RecordDragDropData? dragData = args.Data.GetData(typeof(RecordDragDropData)) as RecordDragDropData;
            if (dragData == null || dragData.Records.Length == 0)
                return;

            List<ExplorerNodeModel> nodes = dragData.Records.Select(p => (ExplorerNodeModel)p).ToList();

            switch (args.DropPosition)
            {
                case DropPosition.Inside:
                case DropPosition.Append:
                    if (target.NodeType == ExplorerNodeType.File)
                    {
                        if (target.Parent != null && target.Parent.NodeType != ExplorerNodeType.File)
                        {
                            Win32FileHelper.Move(nodes.Select(p => p.Path).ToList(), target.Parent.Path);
                        }
                    }
                    else
                    {
                        Win32FileHelper.Move(nodes.Select(p => p.Path).ToList(), target.Path);
                    }
                    break;
                case DropPosition.Before:
                case DropPosition.After:
                    if (target.Parent != null && target.Parent.NodeType != ExplorerNodeType.File)
                    {
                        Win32FileHelper.Move(nodes.Select(p => p.Path).ToList(), target.Parent.Path);
                    }
                    break;
                default:
                    break;
            }


            await Task.CompletedTask;
        }

        #endregion


        // ===================================================================================================
        // **** Message ****
        // ===================================================================================================

        #region ProjectClosedMsg -- 项目关闭消息

        /// <summary>
        /// 项目关闭消息
        /// </summary>
        private void OnProjectClosed(object sender, ProjectClosedMsg msg)
        {
            this.ExplorerManager.UnInitialize();
        }

        #endregion

        #region ProjectOpendMsg -- 项目打开消息

        /// <summary>
        /// 项目打开
        /// </summary>
        private void OnProjectOpend(object sender, ProjectOpendMsg msg)
        {
            ProjectDomain? project = this.ProjectManager.Current;
            if (project == null)
                return;

            this.ExplorerManager.Initialize(project);
            this.Nodes = this.ExplorerManager.Nodes;
            ExplorerNodeModel? root = this.ExplorerManager.Nodes.FirstOrDefault();
            if (root == null)
                return;

            root.IsExpanded = true;
        }

        #endregion
    }
}
