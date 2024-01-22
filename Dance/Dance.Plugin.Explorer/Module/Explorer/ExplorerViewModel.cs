using Dance.Framework;
using Dance.Plugin.Project;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;

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
        /// 项目管理器
        /// </summary>
        private readonly IProjectManager ProjectManager = DanceDomain.Current.LifeScope.Resolve<IProjectManager>();

        // ===================================================================================================
        // **** Property ****
        // ===================================================================================================

        #region Nodes -- 节点集合

        private DanceObservableCollection<ExplorerNodeModel> nodes = [];
        /// <summary>
        /// 节点集合
        /// </summary>
        public DanceObservableCollection<ExplorerNodeModel> Nodes
        {
            get { return nodes; }
            set { this.SetProperty(ref nodes, value); }
        }

        #endregion

        // ===================================================================================================
        // **** Command ****
        // ===================================================================================================

        // ===================================================================================================
        // **** Message ****
        // ===================================================================================================

        #region ProjectClosedMsg -- 项目关闭消息

        /// <summary>
        /// 项目关闭消息
        /// </summary>
        private void OnProjectClosed(object sender, ProjectClosedMsg msg)
        {
            this.Nodes.Clear();
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

            ExplorerNodeModel root = new(ExplorerNodeType.Project, project.WorkPath) { HasItems = true };
            root.Expand();

            this.Nodes.Add(root);
        }

        #endregion
    }
}
