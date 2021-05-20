using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TourFirmBusinessLogic.BusinessLogic;
using Unity;

namespace TouristTourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowStatistics.xaml
    /// </summary>
    public partial class WindowStatistics : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly TouristStatisticsLogic logic;

        public WindowStatistics(TouristStatisticsLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void LoadData()
        {
            ((ColumnSeries)mcChartTravels.Series[0]).ItemsSource = logic.GetCountByMonths(0);
            ((ColumnSeries)mcChartTravels.Series[1]).ItemsSource = logic.GetCountByMonths(App.Tourist.ID);
            ((ColumnSeries)mcChartExcursions.Series[0]).ItemsSource = logic.GetExcursionsInfo();
        }

        private void WindowStatistics_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}