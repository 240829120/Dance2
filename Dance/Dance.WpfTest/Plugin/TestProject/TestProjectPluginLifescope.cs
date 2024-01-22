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
            return new ProjectPluginInfo(new("TEST", "测试项目", "TestProject"), "测试项目1", "测试")
            {
                Icon = this.CacheManager.GetImage("pack://application:,,,/Dance.WpfTest;component/Resource/Image/test.svg"),
                Detail = "描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述" +
                " 描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描" +
                " 述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述描述",
                Tags = ["面板", "虚幻", "JavaScript"]
            };
        }
    }
}
