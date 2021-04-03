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
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.ViewModels;
using Unity;

namespace TourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowEditTour.xaml
    /// </summary>
    public partial class WindowEditTour : Window
    {

        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly TourLogic logic;
        private readonly HaltLogic Hlogic;
        private readonly GuideLogic Guidelogic;
        TourViewModel tour;
        public int Id { set { id = value; } }
        private int? id;
        public WindowEditTour(TourLogic logic, GuideLogic Guidelogic, HaltLogic Hlogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.Guidelogic = Guidelogic;
            this.Hlogic = Hlogic;
        }
        private void WindowEditTour_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var listAvailableGuides = Guidelogic.Read(null);
                var tours = logic.Read(null);
                foreach (var tour in tours)
                {
                    if (tour.ID == id)
                    {
                        this.tour = tour;
                    }
                }
                var listSelectedGuides = tour.TourGuides.ToList();
                if (listAvailableGuides != null)
                {
                    ListBoxAvailable.ItemsSource = listAvailableGuides;
                    ListBoxSelected.ItemsSource = listSelectedGuides;
                    foreach (var guide in ListBoxSelected.Items)
                    {
                        ListBoxAvailable.Items.Remove(guide);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            ListBoxSelected.Items.Add(ListBoxAvailable.SelectedItem);
            ListBoxAvailable.Items.Remove(ListBoxAvailable.SelectedItem);
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            ListBoxSelected.Items.Remove(ListBoxSelected.SelectedItem);
            ListBoxAvailable.Items.Add(ListBoxSelected.SelectedItem);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Введите название остановки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(CountryTextBox.Text))
            {
                MessageBox.Show("Введите страну", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(PriceTextBox.Text))
            {
                MessageBox.Show("Введите стоимость", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {

                Dictionary<int, string> _TourGuides = new Dictionary<int, string>
                {

                };
                logic.CreateOrUpdate(new TourBindingModel
                {
                    ID = id,
                    Name = NameTextBox.Text,
                    Country = CountryTextBox.Text,
                    Price = decimal.Parse(PriceTextBox.Text),
                    OperatorID = App.Operator.ID,

                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
