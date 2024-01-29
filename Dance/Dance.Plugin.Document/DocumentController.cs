using Dance.Plugin.Explorer;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Messaging;
using Dance.Framework;
using Dance.Plugin.Project;
using System.Windows;

namespace Dance.Plugin.Document
{
    /// <summary>
    /// 文档控制器
    /// </summary>
    public class DocumentController
    {
        public DocumentController()
        {
            DanceDomain.Current.Messenger.Register<ProjectOpendMsg>(this, this.OnProjectOpend);
            DanceDomain.Current.Messenger.Register<ProjectClosedMsg>(this, this.OnProjectClosed);
        }

        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 项目管理器
        /// </summary>
        private readonly IProjectManager ProjectManager = DanceDomain.Current.LifeScope.Resolve<IProjectManager>();

        /// <summary>
        /// 消息管理器
        /// </summary>
        private readonly IDanceMessageManager MessageManager = DanceDomain.Current.LifeScope.Resolve<IDanceMessageManager>();

        // ===================================================================================================
        // **** Message ****
        // ===================================================================================================

        #region ProjectOpendMsg -- 项目打开消息

        /// <summary>
        /// 项目打开消息
        /// </summary>
        private void OnProjectOpend(object sender, ProjectOpendMsg msg)
        {
            ProjectDomain? project = this.ProjectManager.Current;
            if (project == null)
                return;

            project.Messenger.Register<FileCreateMsg>(this, this.OnFileCreate);
            project.Messenger.Register<FileDeleteMsg>(this, this.OnFileDelete);
            project.Messenger.Register<FileOpeningMsg>(this, this.OnFileOpening);
            project.Messenger.Register<FileOpendMsg>(this, this.OnFileOpend);

            DanceMainWindowModel vm = DanceDomain.Current.LifeScope.Resolve<DanceMainWindowModel>();
            var list = project.CacheContext.OpendDocuments.FindAll();
            list.ForEach(p =>
            {
                DancePluginDomain? pluginDomain = DanceDomain.Current.PluginBuilder.PluginDomains.FirstOrDefault(i => i.PluginInfo.Key.NameSpace == p.PluginNameSpace &&
                                                                                                                      i.PluginInfo.Key.Group == p.PluginGroup &&
                                                                                                                      i.PluginInfo.Key.ID == p.PluginID);

                if (pluginDomain == null || pluginDomain.PluginInfo is not DanceDocumentViewPluginInfo pluginInfo)
                    return;

                vm.Documents.Add(new DanceDocumentViewModel()
                {
                    BindableName = p.BindableName,
                    Caption = System.IO.Path.GetFileName(p.Path),
                    PluginInfo = pluginInfo,
                    ViewType = pluginInfo.ViewType,
                    ToolTip = System.IO.Path.GetFileName(p.Path),
                    Path = p.Path,
                    AllowClose = p.AllowClose,
                });
            });
        }

        #endregion

        #region ProjectClosedMsg -- 项目关闭消息

        /// <summary>
        /// 项目关闭消息
        /// </summary>
        private void OnProjectClosed(object sender, ProjectClosedMsg msg)
        {
            ProjectDomain? project = this.ProjectManager.Current;
            if (project == null)
                return;

            project.Messenger.Unregister<FileCreateMsg>(this);
            project.Messenger.Unregister<FileDeleteMsg>(this);
            project.Messenger.Unregister<FileOpeningMsg>(this);
            project.Messenger.Unregister<FileOpendMsg>(this);

            DanceMainWindowModel vm = DanceDomain.Current.LifeScope.Resolve<DanceMainWindowModel>();
            vm.Documents.Clear();
        }

        #endregion

        #region FileCreateMsg -- 文件创建消息

        /// <summary>
        /// 文件创建消息
        /// </summary>
        private void OnFileCreate(object sender, FileCreateMsg msg)
        {
            // nothing todo.
        }

        #endregion

        #region FileDeleteMsg -- 文件删除消息

        /// <summary>
        /// 文件删除消息
        /// </summary>
        private void OnFileDelete(object sender, FileDeleteMsg msg)
        {
            //DanceMainWindowModel vm = DanceDomain.Current.LifeScope.Resolve<DanceMainWindowModel>();



        }

        #endregion

        #region FileOpeningMsg -- 文件打开前消息

        /// <summary>
        /// 文件打开前消息
        /// </summary>
        private void OnFileOpening(object sender, FileOpeningMsg msg)
        {
            // nothing todo.
        }

        #endregion

        #region FileOpendMsg -- 文件打开后消息

        /// <summary>
        /// 文件打开消息
        /// </summary>
        private void OnFileOpend(object sender, FileOpendMsg msg)
        {
            ProjectDomain? project = this.ProjectManager.Current;
            if (project == null)
                return;

            DanceMainWindowModel vm = DanceDomain.Current.LifeScope.Resolve<DanceMainWindowModel>();
            if (vm.Documents.Any(p => p.Path == msg.Path))
                return;

            string extension = System.IO.Path.GetExtension(msg.Path);
            DancePluginDomain? pluginDomain = DanceDomain.Current.PluginBuilder.PluginDomains.FirstOrDefault(p => p.PluginInfo is DocumentPluginInfo info && info.Infos.Any(p => string.Equals(p.Extension, extension, StringComparison.OrdinalIgnoreCase)));
            if (pluginDomain == null || pluginDomain.PluginInfo is not DanceDocumentViewPluginInfo pluginInfo)
                return;

            DanceDocumentViewModel document = new()
            {
                BindableName = $"",
                Caption = System.IO.Path.GetFileName(msg.Path),
                PluginInfo = pluginInfo,
                ViewType = pluginInfo.ViewType,
                ToolTip = System.IO.Path.GetFileName(msg.Path),
                Path = msg.Path,
                AllowClose = true,
            };

            vm.Documents.Add(document);
            project.CacheContext.OpendDocuments.Insert(new OpendDocumentEntity()
            {
                BindableName = document.BindableName,
                AllowClose = document.AllowClose,
                Path = document.Path,
                PluginNameSpace = pluginInfo.Key.NameSpace,
                PluginGroup = pluginInfo.Key.Group,
                PluginID = pluginInfo.Key.ID,
            });
        }

        #endregion
    }
}
