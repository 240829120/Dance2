﻿using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance
{
    /// <summary>
    /// 对象
    /// </summary>
    public class DanceObject : IDisposable
    {
        /// <summary>
        /// 调度检测是否可用
        /// </summary>
        public static Func<bool>? DispatcherCheckAccess { get; set; }

        /// <summary>
        /// 调度执行
        /// </summary>
        public static Action<Action>? DispatcherInvoke { get; set; }

        /// <summary>
        /// 日志执行
        /// </summary>
        public static Action<string>? RecordInvoke { get; set; }

        /// <summary>
        /// 日志
        /// </summary>
        protected readonly static ILog log = LogManager.GetLogger(typeof(DanceObject));

        /// <summary>
        /// 是否已经释放
        /// </summary>
        private bool IsDisposed = false;

        /// <summary>
        /// 析构
        /// </summary>
        ~DanceObject()
        {
            this.Destroy(false);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.Destroy(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="disposing">是否执行销毁</param>
        protected void Destroy(bool disposing)
        {
            if (this.IsDisposed)
                return;

            if (disposing)
            {
                try
                {
                    this.Destroy();
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }

            this.IsDisposed = true;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        protected virtual void Destroy()
        {

        }
    }
}
