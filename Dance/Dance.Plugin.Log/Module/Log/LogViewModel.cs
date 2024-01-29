using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Plugin.Log
{
    /// <summary>
    /// 日志视图模型
    /// </summary>
    [DanceSingleton]
    public class LogViewModel : DanceViewModel
    {
        public LogViewModel()
        {
            DanceLog.Logging -= DanceLog_Logging;
            DanceLog.Logging += DanceLog_Logging;

            this.CopyCommand = new(COMMAND_GROUP, "复制", this.Copy);
            this.ClearCommand = new(COMMAND_GROUP, "清理", this.Clear);
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        private void DanceLog_Logging(object? sender, DanceLogEventArgs e)
        {
            if (this.View is not LogView view || view.edit == null)
                return;

            view.edit.AppendText(e.Content);
        }

        // ===================================================================================================
        // **** Field ****
        // ===================================================================================================

        /// <summary>
        /// 命令分组
        /// </summary>
        private const string COMMAND_GROUP = "日志";

        // ===================================================================================================
        // **** Command ****
        // ===================================================================================================

        #region CopyCommand -- 复制命令

        /// <summary>
        /// 复制命令
        /// </summary>
        public DanceCommand CopyCommand { get; private set; }

        /// <summary>
        /// 复制
        /// </summary>
        private async Task Copy()
        {
            if (this.View is not LogView view)
                return;

            view.edit.Copy();

            await Task.CompletedTask;
        }

        #endregion

        #region ClearCommand -- 清理命令

        /// <summary>
        /// 清理命令
        /// </summary>
        public DanceCommand ClearCommand { get; private set; }

        /// <summary>
        /// 清理
        /// </summary>
        private async Task Clear()
        {
            if (this.View is not LogView view)
                return;

            view.edit.Clear();

            await Task.CompletedTask;
        }

        #endregion

    }
}
