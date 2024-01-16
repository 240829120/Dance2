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
            DanceBarPluginInfo info = new("MenuBar", "MenuBar");

            DanceBarSubItemModel sub = new() { Content = "Sub1(_F)" };
            DanceBarButtonItemModel bt1_1 = new() { Content = "bt1_1" };
            DanceBarButtonItemModel bt1_2 = new() { Content = "bt1_2" };
            sub.Items.Add(bt1_1);
            sub.Items.Add(new DanceBarSeparatorItemModel());
            sub.Items.Add(bt1_2);

            DanceBarSubItemModel sub2 = new() { Content = "Sub2" };
            DanceBarButtonItemModel bt2 = new() { Content = "test111" };
            sub2.Items.Add(bt2);
            DanceToolBarControlModel tool2 = new();
            tool2.Items.Add(sub2);

            info.MenuBarItems.Add(sub);
            info.ToolBarItems.Add(tool2);
            info.StatusBarLeftItems.Add(bt1_1);
            info.StatusBarLeftItems.Add(new DanceBarSeparatorItemModel());
            info.StatusBarLeftItems.Add(bt1_2);

            info.StatusBarRightItems.Add(new DanceBarButtonItemModel() { Content = "test" });

            return info;
        }
    }
}
