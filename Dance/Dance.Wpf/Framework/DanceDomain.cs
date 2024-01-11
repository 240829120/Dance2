﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Wpf
{
    /// <summary>
    /// 领域
    /// </summary>
    public class DanceDomain : DanceDomainBase
    {
        public DanceDomain()
        {
            // IOC构建
            this.IocBuilder.AddAssemblys(Assembly.Load("Dance"), Assembly.Load("Dance.Wpf"));

            // 捕获未处理异常
            this.Builders.Add(new DanceDomainBuilder_CatchUnhandledException());
            // 单例启动           
            this.Builders.Add(new DanceDomainBuilder_SingletonProcess());
            // 调度
            this.Builders.Add(new DanceDomainBuilder_Dispatcher());
            // 日志
            this.Builders.Add(new DanceDomainBuilder_Log());
            // 释放
            this.Builders.Add(new DanceDomainBuilder_Dispose());
            // 操作日志
            this.Builders.Add(new DanceDomainBuilder_Record());
            // 延时
            this.Builders.Add(new DanceDomainBuilder_Delay());
            // 循环
            this.Builders.Add(new DanceDomainBuilder_Loop());
            // 阻塞
            this.Builders.Add(new DanceDomainBuilder_Blocking());
        }

        /// <summary>
        /// 当前领域
        /// </summary>
        [NotNull]
        public static DanceDomain? Current { get; set; }
    }
}