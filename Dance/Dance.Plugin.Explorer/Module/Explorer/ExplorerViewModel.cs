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
            if (e == null || this.SelectedNode == null || this.SelectedNode.IsEditing)
                return;

            this.SelectedNode.IsExpanded = !this.SelectedNode.IsExpanded;

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
        }

        #endregion
    }
}
