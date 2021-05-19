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
    /// Логика взаимодействия для WindowTravelStatistic.xaml
    /// </summary>
    public partial class WindowTravelStatistic : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public WindowTravelStatistic()
        {
            InitializeComponent();
        }
    }
}
