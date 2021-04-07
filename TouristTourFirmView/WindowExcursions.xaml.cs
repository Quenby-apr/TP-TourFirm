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
    /// Логика взаимодействия для WindowExcursions.xaml
    /// </summary>
    public partial class WindowExcursions : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly ExcursionLogic logic;
        private readonly Logger logger;

        public WindowExcursions(ExcursionLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void WindowExcursions_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(new ExcursionBindingModel
                {
                    TouristID = App.Tourist.ID
                });

                if (list != null)
                {
                    DataGridExcursions.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка при попытке загрузки списка экскурсий");
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowExcursion>();

            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridExcursions.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите экскурсию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var form = Container.Resolve<WindowExcursion>();
            form.Id = ((ExcursionViewModel)DataGridExcursions.SelectedItems[0]).ID;

            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridExcursions.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите экскурсию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                int id = ((ExcursionViewModel)DataGridExcursions.SelectedItems[0]).ID;

                try
                {
                    logic.Delete(new ExcursionBindingModel { ID = id });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    logger.Warn("Ошибка при попытке удаления экскурсии");
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
