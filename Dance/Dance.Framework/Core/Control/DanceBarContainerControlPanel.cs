using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Bars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Dance.Framework
{
    /// <summary>
    /// Bar容器面板
    /// </summary>
    public class DanceBarContainerControlPanel : BarContainerControlPanel
    {
        /// <summary>
        /// 布局计算器
        /// </summary>
        /// <param name="container">容器</param>
        internal class DanceBarLayoutCalculator2(BarContainerControlPanel container) : BarLayoutCalculator2(container)
        {
            protected override bool CheckMakeBarFloating(BarLayoutTableCell currentCell)
            {
                return false;
            }

            public override void OnBarControlDrag(IBarLayoutTableInfo layoutInfo, DragDeltaEventArgs e, bool? move = null)
            {
                if (Mouse.LeftButton == MouseButtonState.Released && e.Source is DragWidget dragWidget)
                {
                    dragWidget.CancelDrag();
                }
                base.OnBarControlDrag(layoutInfo, e, move);
            }
        }

        protected override BaseBarLayoutCalculator CreateLayoutCalculator()
        {
            if (this.Owner.Return(x => x.IsFloating, () => false))
                return new FloatingBarLayoutCalculator(this);
            return new DanceBarLayoutCalculator2(this);
        }
    }
}