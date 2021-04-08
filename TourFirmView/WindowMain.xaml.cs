using System.Windows;
using Unity;

namespace TourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowMain.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public WindowMain()
        {
            InitializeComponent();
        }

        private void MenuItemGuides_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowGuides>();
            form.ShowDialog();
        }

        private void MenuItemTours_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowTours>();
            form.ShowDialog();
        }

        private void MenuItemStop_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowListHalt>();
            form.ShowDialog();
        }

        private void MenuItemExcursions_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowListExcursions>();
            form.ShowDialog();
        }

        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowReport>();
            form.ShowDialog();
        }

        private void MenuItemBindingGuide_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowBindingExcursions>();
            form.ShowDialog();
        }
    }
}
