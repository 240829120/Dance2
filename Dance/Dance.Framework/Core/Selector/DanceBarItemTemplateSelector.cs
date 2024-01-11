using Dance.Framework.Domain.Model.Bar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Dance.Framework
{
    /// <summary>
    /// Bar项模板选择器
    /// </summary>
    public class DanceBarItemTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// 按钮项
        /// </summary>
        public DataTemplate? BarButtonItem { get; set; }

        /// <summary>
        /// 勾选项
        /// </summary>
        public DataTemplate? BarCheckBoxItem { get; set; }

        /// <summary>
        /// 容器项
        /// </summary>
        public DataTemplate? BarContainerItem { get; set; }

        /// <summary>
        /// 编辑项
        /// </summary>
        public DataTemplate? BarEditItem { get; set; }

        /// <summary>
        /// 分隔线
        /// </summary>
        public DataTemplate? BarSeparatorItem { get; set; }

        /// <summary>
        /// 分隔项
        /// </summary>
        public DataTemplate? BarSplitButtonItem { get; set; }

        /// <summary>
        /// 静态项
        /// </summary>
        public DataTemplate? BarStaticItem { get; set; }

        /// <summary>
        /// 子项
        /// </summary>
        public DataTemplate? BarSubItem { get; set; }

        /// <summary>
        /// 工具项
        /// </summary>
        public DataTemplate? ToolBarControl { get; set; }

        /// <summary>
        /// 选择模板
        /// </summary>
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            if (item is DanceBarButtonItemModelBase)
                return this.BarButtonItem;

            if (item is DanceBarCheckBoxItemModelBase)
                return this.BarCheckBoxItem;

            if (item is DanceBarContainerModel)
                return this.BarContainerItem;

            if (item is DanceBarEditItemModelBase)
                return this.BarEditItem;

            if (item is DanceBarSeparatorItemModel)
                return this.BarSeparatorItem;

            if (item is DanceBarSplitButtonItemModel)
                return this.BarSplitButtonItem;

            if (item is DanceBarStaticItemModel)
                return this.BarStaticItem;

            if (item is DanceBarSubItemModel)
                return this.BarSubItem;

            if (item is DanceToolBarControlModel)
                return this.ToolBarControl;

            return null;
        }
    }
}