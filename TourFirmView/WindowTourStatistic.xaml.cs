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
    /// Логика взаимодействия для WindowTourStatistic.xaml
    /// </summary>
    public partial class WindowTourStatistic : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly OperatorStatisticLogic logic;
        List<string> arraymonths = new List<String> {
                "Январь","Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
            };

        public WindowTourStatistic(OperatorStatisticLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            ComboBoxMonths.ItemsSource = arraymonths;
            ComboBoxMonths.SelectedItem = null;
        }

        private void ButtonMake_Click(object sender, RoutedEventArgs e)
        {
            string montname = (string)ComboBoxMonths.SelectedItem;
            int monthid = arraymonths.IndexOf(montname);
            var listtopuser = logic.GetTourByMonthStatistic(new StatisticBindingModel
            {
                month = monthid + 1
            }, App.Operator.ID);
            var listtopall = logic.GetTourByMonthStatistic(new StatisticBindingModel
            {
                month = monthid + 1
            }, 0);
            var listbenefits = logic.GetTourByMonthBenefitStatistic(new StatisticBindingModel
            {
                month = monthid + 1
            }, 0);
            ((PieSeries)mcChart.Series[0]).ItemsSource = listtopuser;
            ((PieSeries)mcChart2.Series[0]).ItemsSource = listtopall;
            ((PieSeries)mcChart3.Series[0]).ItemsSource = listbenefits;
        }
    }
}
