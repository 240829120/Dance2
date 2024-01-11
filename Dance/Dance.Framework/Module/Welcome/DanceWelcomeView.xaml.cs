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

namespace Dance.Framework
{
    /// <summary>
    /// DanceWelcomeView.xaml 的交互逻辑
    /// </summary>
    public partial class DanceWelcomeView : UserControl
    {
        public DanceWelcomeView()
        {
            InitializeComponent();

            if (XamlHelper.IsInDesignMode)
                return;

            DanceWelcomeViewModel vm = DanceDomain.Current.LifeScope.Resolve<DanceWelcomeViewModel>();
            vm.View = this;
            this.DataContext = vm;
        }
    }
}
