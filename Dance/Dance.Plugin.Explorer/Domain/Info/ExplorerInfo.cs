using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 资源管理器扩展信息
    /// </summary>
    /// <param name="extension">扩展名</param>
    /// <param name="icon">图标</param>
    public class ExplorerInfo(string extension, ImageSource? icon)
    {
        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension { get; } = extension;

        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource? Icon { get; } = icon;
    }
}
