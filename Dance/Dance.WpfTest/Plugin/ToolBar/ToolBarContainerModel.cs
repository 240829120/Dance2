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
    public class ToolBarContainerModel : DanceToolBarControlModel
    {
        public ToolBarContainerModel()
        {
            this.Content = "工具容器";

            this.Items.Add(new ToolButtonBarItemModel());
            this.Items.Add(new ToolButtonBarItemModel());
            this.Items.Add(new ToolButtonBarItemModel());
            this.Items.Add(new ToolButtonBarItemModel());
        }
    }
}
