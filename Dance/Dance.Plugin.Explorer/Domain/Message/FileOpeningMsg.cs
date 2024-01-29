﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 文件打开消息
    /// </summary>
    /// <param name="path">文件路径</param>
    public class FileOpeningMsg(string path)
    {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; } = path;

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancel { get; set; }
    }
}
