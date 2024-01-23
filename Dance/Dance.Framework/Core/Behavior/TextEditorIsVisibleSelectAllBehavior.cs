using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dance.Framework
{
    /// <summary>
    /// 文本编辑框可见时选中全部行为
    /// </summary>
    public class TextEditorIsVisibleSelectAllBehavior : Behavior<TextEdit>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            if (this.AssociatedObject is not TextEdit edit)
                return;

            edit.IsVisibleChanged -= Edit_IsVisibleChanged;
            edit.IsVisibleChanged += Edit_IsVisibleChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (this.AssociatedObject is not TextEdit edit)
                return;

            edit.IsVisibleChanged -= Edit_IsVisibleChanged;
        }

        /// <summary>
        /// 编辑器可见性改变时触发
        /// </summary>
        private void Edit_IsVisibleChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (sender is not TextEdit edit || e.NewValue is not bool value || !value)
                return;

            edit.Focus();
            edit.SelectAll();
        }
    }
}
