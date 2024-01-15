using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Dance.Framework
{
    /// <summary>
    /// 消息通知模型
    /// </summary>
    public class DanceMessageNotificationModel : DanceModel
    {
        #region Width -- 宽度

        private double width;
        /// <summary>
        /// 宽度
        /// </summary>
        public double Width
        {
            get { return width; }
            set { this.SetProperty(ref width, value); }
        }

        #endregion

        #region Height -- 高度

        private double height;
        /// <summary>
        /// 高度
        /// </summary>
        public double Height
        {
            get { return height; }
            set { this.SetProperty(ref height, value); }
        }

        #endregion

        #region Image -- 图标

        private ImageSource? image;
        /// <summary>
        /// 图标
        /// </summary>
        public ImageSource? Image
        {
            get { return image; }
            set { this.SetProperty(ref image, value); }
        }

        #endregion

        #region Title -- 标题

        private string? title;
        /// <summary>
        /// 标题
        /// </summary>
        public string? Title
        {
            get { return title; }
            set { this.SetProperty(ref title, value); }
        }

        #endregion

        #region Text -- 内容

        private string? text;
        /// <summary>
        /// 内容
        /// </summary>
        public string? Text
        {
            get { return text; }
            set { this.SetProperty(ref text, value); }
        }

        #endregion
    }
}
