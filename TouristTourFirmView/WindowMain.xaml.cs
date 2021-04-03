using System.Windows;
using Unity;

namespace TouristTourFirmView
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

        private void MenuItemTravels_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowTravels>();
            form.ShowDialog();
        }

        private void MenuItemExcursions_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowExcursions>();
            form.ShowDialog();
        }

        private void MenuItemPlaces_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowPlaces>();
            form.ShowDialog();
        }

        private void MenuItemGuidesList_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowGetTravelGuides>();
            form.ShowDialog();
        }

        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowReportTravels>();
            form.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Tourist = null;
            var windowSignIn = Container.Resolve<WindowSignIn>();
         //   Close();
            windowSignIn.ShowDialog();
        }
    }
}
