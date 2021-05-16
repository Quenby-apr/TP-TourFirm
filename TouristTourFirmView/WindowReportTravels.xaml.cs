using Microsoft.Win32;
using NLog;
using System;
using System.Windows;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.HelperModels;
using TourFirmBusinessLogic.Interfaces;
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
        private readonly ITouristStorage touristStorage;
        private readonly Logger logger;

        public WindowReportTravels(TouristReportLogic logic, ITouristStorage touristStorage)
        {
            InitializeComponent();
            this.logic = logic;
            this.touristStorage = touristStorage;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void ButtonFormReport_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var dataSource = logic.GetTravelExcursionsGuides(new ReportTravelBindingModel
                {
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate,
                    TouristID = App.Tourist.ID
                });
                DataGridReport.ItemsSource = dataSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка при попытке вывода отчёта на форму");
            }
        }

        private void ButtonToPdf_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
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
                            DateFrom = DatePickerFrom.SelectedDate,
                            DateTo = DatePickerTo.SelectedDate,
                            TouristID = App.Tourist.ID
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        logger.Warn("Ошибка при попытке формирования отчёта Pdf");
                    }
                }
            }
        }

        private void ButtonSendToMail_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var fileName = "Отчет.pdf";

                logic.SaveTravelsExcursionsGuidesToPdf(new ReportTravelBindingModel
                {
                    FileName = fileName,
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate,
                    TouristID = App.Tourist.ID
                });

                MailLogic.MailSend(new MailSendInfo
                {
                    MailAddress = App.Tourist.Mail,
                    Subject = "Отчет по путешествиям",
                    Text = "Отчет по путешествиям от " + DatePickerFrom.SelectedDate.Value.ToShortDateString() + " по " + DatePickerTo.SelectedDate.Value.ToShortDateString(),
                    FileName = fileName
                });
                MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                logger.Error("Ошибка отправки отчета: " + ex.Message);
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
        }
    }
}