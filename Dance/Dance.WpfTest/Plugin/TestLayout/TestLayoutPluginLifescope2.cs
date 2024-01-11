using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.WpfTest
{
    public class TestLayoutPluginLifescope2 : DanceObject, IDancePluginLifescope
    {
        public void Initialize()
        {

        }

        public IDancePluginInfo Register()
        {
            return new DanceLayoutViewPluginInfo("TestLayout2", "测试面板2", typeof(TestLayoutView2));
        }
    }
}
