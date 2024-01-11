using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.WpfTest
{
    public class MenuButtonBarItemModel : DanceBarButtonItemModelBase
    {
        public MenuButtonBarItemModel()
        {
            this.Content = "菜单按钮(F1)";
            this.KeyGesture = new System.Windows.Input.KeyGesture(System.Windows.Input.Key.F1);
        }

        protected override void Click()
        {
            MessageBox.Show("MenuButtonBarItemModel click");
        }
    }
}
