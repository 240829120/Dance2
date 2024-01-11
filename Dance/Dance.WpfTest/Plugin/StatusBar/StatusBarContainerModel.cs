using Dance.Framework;
using Dance.Framework.Domain.Model.Bar;
using Dance.WpfTest.Plugin.ToolBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.WpfTest
{
    public class StatusBarContainerModel : DanceToolBarControlModel
    {
        public StatusBarContainerModel()
        {
            this.Content = "状态容器";

            this.Items.Add(new StatusButtonBarItemModel());
            this.Items.Add(new StatusButtonBarItemModel());
            this.Items.Add(new StatusButtonBarItemModel());
            this.Items.Add(new StatusButtonBarItemModel());
        }
    }
}
