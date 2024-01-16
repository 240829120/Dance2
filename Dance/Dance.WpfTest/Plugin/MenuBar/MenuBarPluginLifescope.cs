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
            DanceBarButtonItemModel bt = new() { Content = "test111", KeyGesture = new System.Windows.Input.KeyGesture(System.Windows.Input.Key.F3) };
            sub.Items.Add(bt);

            DanceBarSubItemModel sub2 = new() { Content = "Sub2" };
            DanceBarButtonItemModel bt2 = new() { Content = "test111" };
            sub2.Items.Add(bt2);
            DanceToolBarControlModel tool2 = new();
            tool2.Items.Add(sub2);

            info.MenuBarItems.Add(sub);
            info.ToolBarItems.Add(tool2);
            info.StatusBarLeftItems.Add(bt);
            info.StatusBarRightItems.Add(bt);

            return info;
        }
    }
}
