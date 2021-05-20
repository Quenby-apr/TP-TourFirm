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
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using Unity;

namespace TourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowExcursionStatistic.xaml
    /// </summary>
    public partial class WindowExcursionStatistic : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly OperatorStatisticLogic logic;
        public WindowExcursionStatistic(OperatorStatisticLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonMake_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            ((PieSeries)mcChart.Series[0]).ItemsSource = logic.GetExcursionCostStatistic(new ReportBindingModel
            {
                DateFrom = DatePikerFrom.SelectedDate,
                DateTo = DatePikerTo.SelectedDate
            });
            ((PieSeries)mcChart2.Series[0]).ItemsSource = logic.GetExcursionDurationStatistic(new ReportBindingModel
            {
                DateFrom = DatePikerFrom.SelectedDate,
                DateTo = DatePikerTo.SelectedDate
            });
        }
    }
}
