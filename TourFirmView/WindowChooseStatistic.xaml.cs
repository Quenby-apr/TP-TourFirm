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
using System.Windows.Shapes;
using Unity;

namespace TourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowChooseStatistic.xaml
    /// </summary>
    public partial class WindowChooseStatistic : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public WindowChooseStatistic()
        {
            InitializeComponent();
        }
        private void tourStatistic_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowTourStatistic>();
            form.ShowDialog();
        }

        private void guideStatistic_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowGuideStatistic>();
            form.ShowDialog();
        }

        private void excursionStatistic_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowExcursionStatistic>();
            form.ShowDialog();
        }
    }
}
