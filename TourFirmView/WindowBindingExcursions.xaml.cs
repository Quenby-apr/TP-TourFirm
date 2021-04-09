using System;
using System.Collections.Generic;
using System.Windows;
using NLog;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.ViewModels;
using Unity;

namespace TourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowBindingExcursions.xaml
    /// </summary>
    public partial class WindowBindingExcursions : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly GuideLogic guidelogic;
        private readonly ExcursionLogic excursionlogic;
        private readonly Logger logger;

        public WindowBindingExcursions(GuideLogic guideLogic, ExcursionLogic excursionLogic)
        {
            InitializeComponent();
            guidelogic = guideLogic;
            excursionlogic = excursionLogic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            ComboBoxChoosenGuide.ItemsSource = guidelogic.Read(new GuideBindingModel { 
                OperatorID = App.Operator.ID
            });
            ComboBoxChoosenGuide.SelectedItem = null;
            var listbindmodels = excursionlogic.Read(null);
            foreach (var excursion in listbindmodels)
            {
                ListBoxExcursion.Items.Add(excursion);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dictionary<int, string> guideExcursions = new Dictionary<int, string>();
                foreach (var exc in ListBoxExcursion.SelectedItems)
                {
                    var excursion = (ExcursionViewModel)exc;
                    guideExcursions.Add(excursion.ID, excursion.Name);
                }
                GuideViewModel guide = (GuideViewModel)ComboBoxChoosenGuide.SelectedItem;
                guidelogic.CreateOrUpdate(new GuideBindingModel
                {
                    ID = guide.ID,
                    Name = guide.Name,
                    Surname = guide.Surname,
                    WorkPlace = guide.WorkPlace,
                    PhoneNumber = guide.PhoneNumber,
                    MainLanguage = guide.MainLanguage,
                    AdditionalLanguage = guide.AdditionalLanguage,
                    OperatorID = App.Operator.ID,
                    GuideExcursions = guideExcursions
                });
                MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка при попытке привязки гидов к экскурсии");
            }
        }
    }
}