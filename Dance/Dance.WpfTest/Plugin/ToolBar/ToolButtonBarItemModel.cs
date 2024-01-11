using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.WpfTest.Plugin.ToolBar
{
    public class ToolButtonBarItemModel : DanceBarButtonItemModelBase
    {
        public ToolButtonBarItemModel()
        {
            this.Content = "工具按钮";
        }

        protected override void Click()
        {
            MessageBox.Show("MenuButtonBarItemModel click");
        }
    }
}
