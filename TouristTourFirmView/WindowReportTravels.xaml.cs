using Microsoft.Win32;
using System;
using System.Windows;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using Unity;

namespace TouristTourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowReportTravels.xaml
    /// </summary>
    public partial class WindowReportTravels : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly TouristReportLogic logic;
        public WindowReportTravels(TouristReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void ButtonMake_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void ButtonToPdf_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerStart.SelectedDate >= DatePickerEnd.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            {
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        logic.SaveTravelsExcursionsGuidesToPdf(new ReportTravelBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = DatePickerStart.SelectedDate,
                            DateTo = DatePickerEnd.SelectedDate
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                       MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}

