using NLog;
using System;
using System.Windows;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.ViewModels;
using Unity;

namespace TouristTourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowTravels.xaml
    /// </summary>
    public partial class WindowTravels : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly TravelLogic logic;
        private readonly Logger logger;

        public WindowTravels(TravelLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void WindowTravels_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(new TravelBindingModel
                {
                    TouristID = App.Tourist.ID
                });

                if (list != null)
                {
                    DataGridTravels.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка при попытке загрузки списка пуешествий");
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowTravel>();

            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridTravels.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите путешествие", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            var form = Container.Resolve<WindowTravel>();
            form.Id = ((TravelViewModel)DataGridTravels.SelectedItems[0]).ID;

            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridTravels.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите путешествие", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                int id = ((TravelViewModel)DataGridTravels.SelectedItems[0]).ID;

                try
                {
                    logic.Delete(new TravelBindingModel { ID = id });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    logger.Warn("Ошибка при попытке удаления путешествия");
                }
                LoadData();
            }

        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void ButtonBondExcursions_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridTravels.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите путешествие", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var form = Container.Resolve<WindowBondTravelExcursions>();
            form.Id = ((TravelViewModel)DataGridTravels.SelectedItems[0]).ID;

            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }
    }
}