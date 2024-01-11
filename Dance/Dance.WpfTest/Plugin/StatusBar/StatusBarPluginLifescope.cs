using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.WpfTest
{
    public class StatusBarPluginLifescope : DanceObject, IDancePluginLifescope
    {
        public void Initialize()
        {

        }

        public IDancePluginInfo Register()
        {
            DanceStatusBarItemPluginInfo info = new("StatusBar", "StatusBar");

            info.BarItems.Add(new StatusBarContainerModel());
            info.BarItems.Add(new StatusBarContainerModel());


            return info;
        }
    }
}
