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

            if (DanceXamlHelper.IsInDesignMode)
                return;

            DanceWelcomeViewModel vm = DanceDomain.Current.LifeScope.Resolve<DanceWelcomeViewModel>();
            vm.View = this;
            this.DataContext = vm;
        }

        #region ProgressForeground -- 进度条前景色

        /// <summary>
        /// <inheritdoc cref="ProgressForegroundProperty"/>
        /// </summary>
        public Brush ProgressForeground
        {
            get { return (Brush)GetValue(ProgressForegroundProperty); }
            set { SetValue(ProgressForegroundProperty, value); }
        }

        /// <summary>
        /// 进度条前景色
        /// </summary>
        public static readonly DependencyProperty ProgressForegroundProperty =
            DependencyProperty.Register("ProgressForeground", typeof(Brush), typeof(DanceWelcomeView), new PropertyMetadata(Brushes.Blue));

        #endregion
    }
}
