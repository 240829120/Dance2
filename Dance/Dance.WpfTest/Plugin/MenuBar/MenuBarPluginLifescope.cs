using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.WpfTest
{
    public class MenuBarPluginLifescope : DanceObject, IDancePluginLifescope
    {
        public void Initialize()
        {

        }

        public IDancePluginInfo Register()
        {
            DanceMenuBarItemPluginInfo info = new("MenuBar", "MenuBar");

            info.BarItems.Add(new MenuButtonBarItemModel());


            return info;
        }
    }
}
