using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.WpfTest
{
    public class ToolBarPluginLifescope : DanceObject, IDancePluginLifescope
    {
        public void Initialize()
        {

        }

        public IDancePluginInfo Register()
        {
            DanceToolBarPluginInfo info = new("ToolBar", "ToolBar");

            info.BarItems.Add(new ToolBarContainerModel());
            info.BarItems.Add(new ToolBarContainerModel());


            return info;
        }
    }
}
