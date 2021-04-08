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
    /// Логика взаимодействия для WindowPlaces.xaml
    /// </summary>
    public partial class WindowPlaces : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly PlaceLogic logic;
        private readonly Logger logger;

        public WindowPlaces(PlaceLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void WindowPlaces_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(new PlaceBindingModel
                {
                    TouristID = App.Tourist.ID
                });

                if (list != null)
                {
                    DataGridPlaces.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка при попытке загрузки списка места");
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowPlace>();

            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPlaces.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите место", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var form = Container.Resolve<WindowPlace>();
            form.Id = ((PlaceViewModel)DataGridPlaces.SelectedItems[0]).ID;

            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPlaces.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите место", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                int id = ((PlaceViewModel)DataGridPlaces.SelectedItems[0]).ID;

                try
                {
                    logic.Delete(new PlaceBindingModel { ID = id });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    logger.Warn("Ошибка при попытке удаления места");
                }
                LoadData();
            }

        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}