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

            DanceBarSubItemModel sub = new() { Content = "Sub", Content2 = "Content2", ToolTip = "sub123123123123" };
            DanceBarButtonItemModel bt = new() { Content = "test111", KeyGesture = new System.Windows.Input.KeyGesture(System.Windows.Input.Key.F3) };
            bt.OnClick += (s, e) => { MessageBox.Show("1223"); };
            sub.Items.Add(bt);
            DanceToolBarControlModel tool = new();
            tool.Items.Add(sub);

            info.MenuBarItems.Add(tool);
            info.ToolBarItems.Add(tool);

            return info;
        }
    }
}
