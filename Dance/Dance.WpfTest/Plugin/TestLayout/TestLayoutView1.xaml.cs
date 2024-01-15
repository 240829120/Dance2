using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dance.WpfTest
{
    /// <summary>
    /// TestLayoutView1.xaml 的交互逻辑
    /// </summary>
    public partial class TestLayoutView1 : UserControl
    {
        public TestLayoutView1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            IDanceMessageManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceMessageManager>();
            manager.Notify("Lorem ipsum dolor sit amet", "In ornare ante magna, eget volutpat mi bibendum a. Nam ut ullamcorper libero.In ornare ante magna, eget volutpat mi bibendum a. Nam ut ullamcorper libero.", new BitmapImage(new Uri("/Dance.WpfTest;component/Resource/Image/notification-icon.png", UriKind.RelativeOrAbsolute)), 380, 100);

            manager.Notify("In ornare ante magna, eget volutpat mi bibendum a. Nam ut ullamcorper libero.In ornare ante magna, eget volutpat mi bibendum a. Nam ut ullamcorper libero.");
        }
    }
}
