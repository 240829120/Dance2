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
            project.Messenger.Register<FileClosingMsg>(this, this.OnFileClosing);
            project.Messenger.Register<FileClosedMsg>(this, this.OnFileClosed);

            DanceMainWindowModel vm = DanceDomain.Current.LifeScope.Resolve<DanceMainWindowModel>();
            var list = project.CacheContext.OpendDocuments.FindAll();
            list.ForEach(p =>
            {
                DancePluginDomain? pluginDomain = DanceDomain.Current.PluginBuilder.PluginDomains.FirstOrDefault(i => i.PluginInfo.Key.NameSpace == p.PluginNameSpace &&
                                                                                                                      i.PluginInfo.Key.Group == p.PluginGroup &&
                                                                                                                      i.PluginInfo.Key.ID == p.PluginID);

                if (pluginDomain == null || pluginDomain.PluginInfo is not DanceDocumentViewPluginInfo pluginInfo)
                    return;

                if (string.IsNullOrWhiteSpace(pluginInfo.ViewModelType.FullName))
                    return;

                DanceDocumentViewModel? document = pluginInfo.ViewModelType.Assembly.CreateInstance(pluginInfo.ViewModelType.FullName) as DanceDocumentViewModel;
                if (document == null)
                    return;

                document.BindableName = p.BindableName;
                document.Caption = System.IO.Path.GetFileName(p.Path);
                document.ToolTip = document.Caption;
                document.PluginInfo = pluginInfo;
                document.ViewType = pluginInfo.ViewType;
                document.Path = p.Path;
                document.AllowClose = p.AllowClose;

                vm.Documents.Add(document);
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
            DanceDocumentViewModel? old = vm.Documents.FirstOrDefault(p => p.Path == msg.Path);
            if (old != null)
            {
                old.IsActive = true;
                return;
            }

            string extension = System.IO.Path.GetExtension(msg.Path);
            DancePluginDomain? pluginDomain = DanceDomain.Current.PluginBuilder.PluginDomains.FirstOrDefault(p => p.PluginInfo is DanceDocumentViewPluginInfo info && info.Extensions.Any(p => string.Equals(p, extension, StringComparison.OrdinalIgnoreCase)));
            if (pluginDomain == null || pluginDomain.PluginInfo is not DanceDocumentViewPluginInfo pluginInfo || string.IsNullOrWhiteSpace(pluginInfo.ViewModelType.FullName))
                return;

            DanceDocumentViewModel? document = pluginInfo.ViewModelType.Assembly.CreateInstance(pluginInfo.ViewModelType.FullName) as DanceDocumentViewModel;
            if (document == null)
                return;

            document.BindableName = $"Document_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
            document.Caption = System.IO.Path.GetFileName(msg.Path);
            document.ToolTip = document.Caption;
            document.PluginInfo = pluginInfo;
            document.ViewType = pluginInfo.ViewType;
            document.Path = msg.Path;
            document.AllowClose = true;

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

        #region FileClosingMsg -- 文件关闭之前消息

        /// <summary>
        /// 文件关闭之前消息
        /// </summary>
        private void OnFileClosing(object sender, FileClosingMsg msg)
        {
            // nothing todo.
        }

        #endregion

        #region FileClosedMsg -- 文件关闭之后消息

        /// <summary>
        /// 文件关闭之后消息
        /// </summary>
        private void OnFileClosed(object sender, FileClosedMsg msg)
        {
            ProjectDomain? project = this.ProjectManager.Current;
            if (project == null)
                return;

            OpendDocumentEntity? entity = project.CacheContext.OpendDocuments.FindOne(p => p.Path == msg.Path);
            if (entity == null)
                return;

            project.CacheContext.OpendDocuments.Delete(entity.ID);
        }

        #endregion
    }
}
