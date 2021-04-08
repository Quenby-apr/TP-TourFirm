using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NLog;
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
        private readonly Logger logger;
        public int Id { set { id = value; } }
        private int? id;
        public WindowEditTour(TourLogic logic, GuideLogic Guidelogic, HaltLogic Hlogic)
        {
            InitializeComponent();
            this.logic = logic;
            this.Guidelogic = Guidelogic;
            this.Hlogic = Hlogic;
            logger = LogManager.GetCurrentClassLogger();
        }
        private void WindowEditTour_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            ComboBoxHalts.ItemsSource = Hlogic.Read(null);
            ComboBoxHalts.SelectedItem = null;
            var tours = logic.Read(null);
            foreach (var tour in tours)
            {
                if (tour.ID == id)
                {
                    this.tour = tour;
                }
            }
            var listbindmodels = Guidelogic.Read(null);
            foreach (var guide in listbindmodels)
            {
                ListBoxAvailable.Items.Add(guide.Surname);
            }
            if (tour != null)
            {
                var listSelectedGuides = tour.TourGuides.ToList();
                foreach (var guide in listSelectedGuides)
                {
                    ListBoxSelected.Items.Add(Guidelogic.Read(new GuideBindingModel
                    {
                        ID = guide.Key
                    })[0].Surname);
                }
                foreach (var guide in ListBoxSelected.Items)
                {
                    ListBoxAvailable.Items.Remove(guide);
                }
                NameTextBox.Text = tour.Name;
                CountryTextBox.Text = tour.Country;
                PriceTextBox.Text = tour.Price.ToString();
            }    
        }
        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxAvailable.SelectedItem != null)
            {
                ListBoxSelected.Items.Add(ListBoxAvailable.SelectedItem);
                ListBoxAvailable.Items.Remove(ListBoxAvailable.SelectedItem);
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxSelected.SelectedItem != null)
            {
                ListBoxAvailable.Items.Add(ListBoxSelected.SelectedItem);
                ListBoxSelected.Items.Remove(ListBoxSelected.SelectedItem);
            }
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
            HaltViewModel halt = (HaltViewModel)ComboBoxHalts.SelectedItem;
            Dictionary<int, string> _TourGuides = new Dictionary<int, string>();
            foreach (var guide in ListBoxSelected.Items)
            {
                _TourGuides.Add(Guidelogic.Read(new GuideBindingModel
                {
                    Surname = guide.ToString()
                })[0].ID, guide.ToString());
            }
            foreach (var zap in _TourGuides)
            {
                Console.WriteLine(zap.Key);
            }
            try
            {
                logic.CreateOrUpdate(new TourBindingModel
                {
                    ID = id,
                    Name = NameTextBox.Text,
                    Country = CountryTextBox.Text,
                    Price = decimal.Parse(PriceTextBox.Text),
                    TourGuides = _TourGuides,
                    HaltID = halt.ID,
                    OperatorID = App.Operator.ID,

                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка в форме редактирования тура");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ComboBoxHalts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HaltViewModel halt = (HaltViewModel)ComboBoxHalts.SelectedItem;
            AddressTextBox.Text = halt.Address;
        }
    }

}
