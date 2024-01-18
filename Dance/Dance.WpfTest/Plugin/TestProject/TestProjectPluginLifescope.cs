using Dance.Framework;
using Dance.Plugin.Project;
using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dance.WpfTest
{
    public class TestProjectPluginLifescope : DanceObject, IDancePluginLifescope
    {
        private readonly IDanceCacheManager CacheManager = DanceDomain.Current.LifeScope.Resolve<IDanceCacheManager>();

        public void Initialize()
        {

        }

        public IDancePluginInfo Register()
        {
            return new ProjectPluginInfo("TestProject", "测试项目1", "测试")
            {
                Icon = this.CacheManager.GetImage("pack://application:,,,/Dance.WpfTest;component/Resource/Image/test.svg")
            };
        }
    }
}
