using Dance.Framework;
using Dance.Wpf;
using DevExpress.Drawing;
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
            IDanceCacheManager cacheManager = DanceDomain.Current.LifeScope.Resolve<IDanceCacheManager>();

            DanceBarPluginInfo info = new("MenuBar", "MenuBar");

            DanceBarSubItemModel sub = new() { Content = "Sub1(_F)" };
            DanceBarButtonItemModel bt1_1 = new() { Content = "bt1_1" };
            DanceBarButtonItemModel bt1_2 = new() { Content = "bt1_2" };
            DanceBarButtonItemModel bt1_3 = new() { Content = "bt1_3" };
            sub.Items.Add(bt1_1);
            sub.Items.Add(new DanceBarSeparatorItemModel());
            sub.Items.Add(bt1_2);
            DanceBarSubItemModel sub_1 = new()
            {
                Content = "sub2",
                KeyGesture = new System.Windows.Input.KeyGesture(System.Windows.Input.Key.F1),
                Glyph = cacheManager.GetImage("pack://application:,,,/Dance.WpfTest;component/Resource/Image/ImageEdit.svg")
            };
            sub_1.Items.Add(new DanceBarButtonItemModel() { Content = "1111" });
            sub.Items.Add(sub_1);
            sub.Items.Add(bt1_3);

            DanceBarSubItemModel sub2 = new() { Content = "Sub2" };
            DanceBarButtonItemModel bt2 = new() { Content = "test111" };
            sub2.Items.Add(bt2);
            DanceToolBarModel tool2 = new();
            tool2.Items.Add(sub2);

            info.MenuItems.Add(sub);
            info.ToolItems.Add(tool2);
            info.ToolItems.Add(tool2);

            info.StatusItems.Add(new DanceBarButtonItemModel() { Content = "t1", Alignment = DevExpress.Xpf.Bars.BarItemAlignment.Far });
            info.StatusItems.Add(new DanceBarButtonItemModel() { Content = "t2", Alignment = DevExpress.Xpf.Bars.BarItemAlignment.Near });
            info.StatusItems.Add(new DanceBarButtonItemModel() { Content = "t3", Alignment = DevExpress.Xpf.Bars.BarItemAlignment.Near });
            info.StatusItems.Add(new DanceBarButtonItemModel() { Content = "t4", Alignment = DevExpress.Xpf.Bars.BarItemAlignment.Far });

            return info;
        }
    }
}
