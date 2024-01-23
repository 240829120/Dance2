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
            DanceLog.Logging += DanceLog_Logging;
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        private void DanceLog_Logging(object? sender, DanceLogEventArgs e)
        {
            if (this.View is not LogView view)
                return;

            view.edit.AppendText(e.Content);
        }
    }
}
