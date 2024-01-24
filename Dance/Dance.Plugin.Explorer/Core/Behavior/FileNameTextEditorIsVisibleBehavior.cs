using Dance.Wpf;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Plugin.Explorer
{
    /// <summary>
    /// 文件名文本编辑器可见性改变行为
    /// </summary>
    public class FileNameTextEditorIsVisibleBehavior : Behavior<TextEdit>
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

            Application.Current.Dispatcher.BeginInvoke(() =>
            {
                edit.Focus();
                int index = edit.EditValue?.ToString()?.IndexOf('.') ?? -1;
                if (index <= 0)
                {
                    edit.SelectAll();
                }
                else
                {
                    edit.Select(0, index);
                }

            }, System.Windows.Threading.DispatcherPriority.Background);
        }
    }
}
