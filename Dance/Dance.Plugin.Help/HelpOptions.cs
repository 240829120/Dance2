using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Help
{
    /// <summary>
    /// 帮助参数
    /// </summary>
    public static class HelpOptions
    {
        /// <summary>
        /// 显示帮助命令
        /// </summary>
        public static DanceCommand? ShowHelpCommand { get; set; }

        /// <summary>
        /// 关于命令
        /// </summary>
        public static DanceCommand? AboutCommand { get; set; }
    }
}
