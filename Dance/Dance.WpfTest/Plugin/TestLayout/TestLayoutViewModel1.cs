using Dance.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Dance.WpfTest
{
    public class TestLayoutViewModel1 : DanceViewModel
    {
        public TestLayoutViewModel1()
        {
            this.ClickCommand = new("Click", this.Click, false);
            this.SelectedChangedCommand = new("选择改变", this.SelectedChanged, false);
        }

        public DanceCommand ClickCommand { get; private set; }

        private async Task Click()
        {
            Debug.WriteLine("1");

            await Task.Delay(5000);
        }

        public DanceCommand<SelectionChangedEventArgs> SelectedChangedCommand { get; private set; }

        private async Task SelectedChanged(SelectionChangedEventArgs? e)
        {
            Debug.WriteLine("1");

            IDanceMessageManager manager = DanceDomain.Current.LifeScope.Resolve<IDanceMessageManager>();
            manager.Notify("Lorem ipsum dolor sit amet", "In ornare ante magna, eget volutpat mi bibendum a. Nam ut ullamcorper libero.In ornare ante magna, eget volutpat mi bibendum a. Nam ut ullamcorper libero.", new BitmapImage(new Uri("/Dance.WpfTest;component/Resource/Image/notification-icon.png", UriKind.RelativeOrAbsolute)), 380, 100);
            manager.Notify("In ornare ante magna, eget volutpat mi bibendum a. Nam ut ullamcorper libero.In ornare ante magna, eget volutpat mi bibendum a. Nam ut ullamcorper libero.");

            await Task.Delay(5000);
        }
    }
}
