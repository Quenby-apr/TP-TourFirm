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
            string[] arraymonths = new string[] {
                "Январь","Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
            };
            ComboBoxMonths.ItemsSource = arraymonths;
            ComboBoxMonths.SelectedItem = null;
        }

        private void ButtonMake_Click(object sender, RoutedEventArgs e)
        {
            int monthid = (int)ComboBoxMonths.SelectedItem;
            ((ColumnSeries)mcChart.Series[0]).ItemsSource = logic.GetTourByMonthStatistic(new StatisticBindingModel
            {
                 month = monthid+1
            }, App.Operator.ID);
        }
    }
}
