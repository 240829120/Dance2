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

            DanceBarSubItemModel sub = new() { Content = "Sub1", ToolTip = "sub123123123123" };
            DanceBarButtonItemModel bt = new() { Content = "test111", KeyGesture = new System.Windows.Input.KeyGesture(System.Windows.Input.Key.F3) };
            bt.OnClick += (s, e) => { MessageBox.Show("1223"); };
            sub.Items.Add(bt);
            DanceToolBarControlModel tool = new();
            tool.Items.Add(sub);

            DanceBarSubItemModel sub2 = new() { Content = "Sub2", ToolTip = "sub123123123123" };
            DanceBarButtonItemModel bt2 = new() { Content = "test111" };
            bt2.OnClick += (s, e) => { MessageBox.Show("1223"); };
            sub2.Items.Add(bt2);
            DanceToolBarControlModel tool2 = new();
            tool2.Items.Add(sub2);

            info.MenuBarItems.Add(tool);
            info.ToolBarItems.Add(tool2);
            info.StatusBarLeftBottomItems.Add(tool2);
            info.StatusBarRightTopItems.Add(tool2);
            info.StatusBarRightBottomItems.Add(tool2);

            return info;
        }
    }
}
