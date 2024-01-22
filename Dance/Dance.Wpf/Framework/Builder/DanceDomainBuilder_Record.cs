using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using log4net;

namespace Dance.Wpf
{
    /// <summary>
    /// 领域构建器 -- 操作日志
    /// </summary>
    public class DanceDomainBuilder_Record : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 操作日志
        /// </summary>
        public string Name { get; } = "操作日志";

        /// <summary>
        /// 操作日志管理器
        /// </summary>
        private IDanceRecordManager? RecordManager;

        /// <summary>
        /// 循环管理器
        /// </summary>
        private IDanceLoopManager? LoopManager;

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
            DanceModelBase.RecordInvoke = ExecuteRecordInvoke;

            this.LoopManager = DanceDomain.Current.LifeScope.Resolve<IDanceLoopManager>();
            this.LoopManager.Register("IDanceRecordManager.Flush", 30, () =>
            {
                IDanceRecordManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceRecordManager>();
                manager?.Flush();
            });
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {
            IDanceRecordManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceRecordManager>();
            manager?.Flush();
            manager?.Dispose();
        }

        /// <summary>
        /// 执行日志
        /// </summary>
        /// <param name="content">内容</param>
        private void ExecuteRecordInvoke(string content)
        {
            try
            {
                this.RecordManager ??= DanceDomain.Current.LifeScope.Resolve<IDanceRecordManager>();
                this.RecordManager.Log(content);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }
    }
}
