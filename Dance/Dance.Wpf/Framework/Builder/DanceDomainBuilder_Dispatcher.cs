using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 领域构建器 -- 调度
    /// </summary>
    public class DanceDomainBuilder_Dispatcher : DanceObject, IDanceDomainBuilder
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; } = "调度";

        /// <summary>
        /// 构建
        /// </summary>
        public void Build()
        {
            DispatcherCheckAccess = ExecuteDispatcherCheckAccess;
            DispatcherInvoke = ExecuteDispatcherInvoke;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected override void Destroy()
        {

        }

        /// <summary>
        /// 调度检测
        /// </summary>
        /// <returns>是否可以执行</returns>
        private bool ExecuteDispatcherCheckAccess()
        {
            return System.Windows.Application.Current.Dispatcher.CheckAccess();
        }

        /// <summary>
        /// 调度执行
        /// </summary>
        /// <param name="action">行为</param>
        private void ExecuteDispatcherInvoke(Action action)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                action();
            });
        }
    }
}
