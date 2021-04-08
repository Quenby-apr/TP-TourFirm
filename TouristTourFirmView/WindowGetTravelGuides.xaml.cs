using Microsoft.Win32;
using NLog;
using System;
using System.Collections.Generic;
using System.Windows;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.ViewModels;
using Unity;

namespace TouristTourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowGetTravelGuides.xaml
    /// </summary>
    public partial class WindowGetTravelGuides : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly TravelLogic travelLogic;
        private readonly TouristReportLogic reportLogic;
        private readonly Logger logger;

        public WindowGetTravelGuides(TravelLogic travelLogic, TouristReportLogic reportLogic)
        {
            InitializeComponent();
            this.travelLogic = travelLogic;
            this.reportLogic = reportLogic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void WindowGetTravelGuides_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = travelLogic.Read(new TravelBindingModel
                {
                    TouristID = App.Tourist.ID
                });

                if (list != null)
                {
                    ListBoxTravels.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Question);
                logger.Warn("Ошибка при попытке загрузки списка путешествий");
            }
        }

        private void ButtonSaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxTravels.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите путешествия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var list = new List<TravelViewModel>();

                    foreach (var travel in ListBoxTravels.SelectedItems)
                    {
                        list.Add((TravelViewModel)travel);
                    }

                    reportLogic.SaveTravelGuidesToExcel(new ReportTravelBindingModel
                    {
                        FileName = dialog.FileName,
                        Travels = list
                    });

                    MessageBox.Show("Список успешно сохранён в Excel-файл!", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    logger.Warn("Ошибка при попытке формирования отчёта Excel");
                }
            }
        }

        private void ButtonSaveToWord_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxTravels.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите путешествия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            try
            {
                if (dialog.ShowDialog() == true)
                {
                    var list = new List<TravelViewModel>();

                    foreach (var travel in ListBoxTravels.SelectedItems)
                    {
                        list.Add((TravelViewModel)travel);
                    }

                    reportLogic.SaveTravelGuidesToWord(new ReportTravelBindingModel
                    {
                        FileName = dialog.FileName,
                        Travels = list
                    });

                    MessageBox.Show("Список успешно сохранён в Word-файл!", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка при попытке формирования отчёта Word");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}