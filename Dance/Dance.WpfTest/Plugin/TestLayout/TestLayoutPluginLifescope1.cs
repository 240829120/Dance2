using Dance.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.WpfTest
{
    public class TestLayoutPluginLifescope1 : DanceObject, IDancePluginLifescope
    {
        public void Initialize()
        {

        }

        public IDancePluginInfo Register()
        {
            return new DanceLayoutViewPluginInfo("TestLayout1", "测试面板1", typeof(TestLayoutView1))
            {
                AllowClose = false
            };
        }
    }
}
