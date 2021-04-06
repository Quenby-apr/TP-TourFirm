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
using Microsoft.Win32;
using NLog;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.ViewModels;
using Unity;

namespace TourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowListExcursions.xaml
    /// </summary>
    public partial class WindowListExcursions : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly GuideLogic guidelogic;
        private readonly ExcursionLogic excursionlogic;
        private readonly TourLogic tourlogic;
        private ReportLogic reportlogic;
        private readonly Logger logger;
        public WindowListExcursions(GuideLogic guidelogic, ExcursionLogic excursionlogic, TourLogic tourlogic, ReportLogic reportlogic)
        {
            InitializeComponent();
            this.guidelogic = guidelogic;
            this.excursionlogic = excursionlogic;
            this.tourlogic = tourlogic;
            this.reportlogic = reportlogic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            var listbindmodels = tourlogic.Read(null);
            foreach (var tour in listbindmodels)
            {
                ListBoxTours.Items.Add(tour);
            }
        }

        private void ButtonWord_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxTours.SelectedItems.Count==0)
            {
                MessageBox.Show("Выберите туры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            try
            {
                if (dialog.ShowDialog() == true)
                {
                    var list = new List<TourViewModel>();
                    foreach (var tour in ListBoxTours.SelectedItems)
                    {
                        list.Add((TourViewModel)tour);
                    }
                    reportlogic.SaveTourExcurionToWordFile(new ReportTourBindingModel
                    {
                        FileName = dialog.FileName,
                        Tours = list
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка в форме списка экскурсий при отчёте в ворд");
            }
        }

        private void ButtonExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxTours.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите туры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SaveFileDialog dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var list = new List<TourViewModel>();
                    foreach (var tour in ListBoxTours.SelectedItems)
                    {
                        list.Add((TourViewModel)tour);
                    }
                    reportlogic.SaveTourExcurionToExcelFile(new ReportTourBindingModel
                    {
                        FileName = dialog.FileName,
                        Tours = list
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                   MessageBoxImage.Error);
                    logger.Warn("Ошибка в форме списка экскурсий при отчёте в эксель");
                }
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
