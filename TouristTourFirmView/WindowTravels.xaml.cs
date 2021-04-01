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

        public WindowTravels(TravelLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void WindowTravels_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);

                if (list != null)
                {
                    DataGridTravels.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            if (DataGridTravels.SelectedItems.Count == 1)
            {
                var form = Container.Resolve<WindowTravel>();
                form.Id = ((TravelViewModel)DataGridTravels.SelectedItems[0]).ID;

                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridTravels.SelectedItems.Count == 1)
            {
                MessageBoxResult result = (MessageBoxResult)MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

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
                    }
                    LoadData();
                }
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
