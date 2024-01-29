using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Project
{
    /// <summary>
    /// 项目缓存上下文
    /// </summary>
    public class ProjectCacheContext : DanceObject
    {
        /// <summary>
        /// 项目缓存上下文
        /// </summary>
        /// <param name="workPath">项目工作路径</param>
        public ProjectCacheContext(string workPath)
        {
            string path = Path.Combine(workPath, ProjectOptions.ProjectCacheFileName);
            this.Database = new(path);

            this.OpendDocuments = this.Database.GetCollection<OpendDocumentEntity>();
        }

        /// <summary>
        /// 缓存数据库
        /// </summary>
        public LiteDatabase Database { get; }

        /// <summary>
        /// 打开的文档集合
        /// </summary>
        public ILiteCollection<OpendDocumentEntity> OpendDocuments { get; }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            this.Database.Dispose();
        }
    }
}
