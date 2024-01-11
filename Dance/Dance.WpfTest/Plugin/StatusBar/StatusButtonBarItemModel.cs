using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.WpfTest.Plugin.ToolBar
{
    public class StatusButtonBarItemModel : DanceBarButtonItemModelBase
    {
        public StatusButtonBarItemModel()
        {
            this.Content = "状态按钮";
        }

        protected override void Click()
        {
            MessageBox.Show("StatusButtonBarItemModel click");
        }
    }
}
