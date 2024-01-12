using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dance.WpfTest
{
    public class MenuBarPluginLifescope : DanceObject, IDancePluginLifescope
    {
        public void Initialize()
        {

        }

        public IDancePluginInfo Register()
        {
            DanceMenuBarPluginInfo info = new("MenuBar", "MenuBar");

            DanceBarSubItemModel sub = new() { Content = "Sub" };
            sub.Items.Add(new DanceBarButtonItemModel());
            info.BarItems.Add(sub);
            DanceBarButtonItemModel b = new() { Content = "Button" };
            b.OnClick += (s, e) => { MessageBox.Show("click"); };
            info.BarItems.Add(b);
            info.BarItems.Add(new DanceBarSeparatorItemModel());
            info.BarItems.Add(new DanceBarCheckBoxItemModel() { Content = "Check" });

            return info;
        }
    }
}
