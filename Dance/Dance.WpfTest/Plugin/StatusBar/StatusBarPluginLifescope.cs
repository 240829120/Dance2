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
            DanceStatusBarPluginInfo info = new("StatusBar", "StatusBar")
            {
                Location = DanceStatusBarLocation.LeftBottom
            };


            return info;
        }
    }

    public class StatusBarPluginLifescope1 : DanceObject, IDancePluginLifescope
    {
        public void Initialize()
        {

        }

        public IDancePluginInfo Register()
        {
            DanceStatusBarPluginInfo info = new("StatusBar", "StatusBar")
            {
                Location = DanceStatusBarLocation.RightTop
            };


            return info;
        }
    }

    public class StatusBarPluginLifescope2 : DanceObject, IDancePluginLifescope
    {
        public void Initialize()
        {

        }

        public IDancePluginInfo Register()
        {
            DanceStatusBarPluginInfo info = new("StatusBar", "StatusBar")
            {
                Location = DanceStatusBarLocation.RightBottom
            };


            return info;
        }
    }
}
