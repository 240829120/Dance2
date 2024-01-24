using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 转化类型
    /// </summary>
    public enum ExplorerNodeType2BoolConverterMode
    {
        Project,
        Folder,
        File,
        ProjectOrFolder
    }

    /// <summary>
    /// 是否是文件夹或项目转化器
    /// </summary>
    public class ExplorerNodeType2BoolConverter : DanceConverterBase
    {
        /// <summary>
        /// 模式
        /// </summary>
        public ExplorerNodeType2BoolConverterMode Mode { get; set; }

        /// <summary>
        /// 转化
        /// </summary>
        public override object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not ExplorerNodeModel model)
                return false;

            switch (this.Mode)
            {
                case ExplorerNodeType2BoolConverterMode.Project: return model.NodeType == ExplorerNodeType.Project;
                case ExplorerNodeType2BoolConverterMode.Folder: return model.NodeType == ExplorerNodeType.Folder;
                case ExplorerNodeType2BoolConverterMode.File: return model.NodeType == ExplorerNodeType.File;
                case ExplorerNodeType2BoolConverterMode.ProjectOrFolder: return model.NodeType == ExplorerNodeType.Project || model.NodeType == ExplorerNodeType.Folder;
                default: return false;
            }
        }
    }
}
