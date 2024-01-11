using DevExpress.Mvvm.POCO;
using DevExpress.Office.Model;
using DevExpress.Xpf.LayoutControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.Framework
{
    /// <summary>
    /// 主视图模型
    /// </summary>
    [DanceSingleton]
    public class DanceMainViewModel : DanceViewModel
    {
        // =======================================================================================
        // Property

        #region Layouts -- 布局容器视图

        private ObservableCollection<DanceLayoutViewModel>? layouts;
        /// <summary>
        /// 布局容器视图
        /// </summary>
        public ObservableCollection<DanceLayoutViewModel>? Layouts
        {
            get { return layouts; }
            set { this.SetProperty(ref layouts, value); }
        }

        #endregion

        #region Documents -- 文档集合

        private ObservableCollection<DanceDocumentViewModel>? documents;
        /// <summary>
        /// 文档集合
        /// </summary>
        public ObservableCollection<DanceDocumentViewModel>? Documents
        {
            get { return documents; }
            set { this.SetProperty(ref documents, value); }
        }

        #endregion

    }
}
