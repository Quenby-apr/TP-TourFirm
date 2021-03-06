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
            ComboBoxHalts.ItemsSource = Hlogic.Read(new HaltBindingModel { 
                OperatorID = App.Operator.ID
            });
            ComboBoxHalts.SelectedItem = null;
            var tours = logic.Read(new TourBindingModel {
                OperatorID = App.Operator.ID
            });
            foreach (var tour in tours)
            {
                if (tour.ID == id)
                {
                    this.tour = tour;
                }
            }
            var listbindmodels = Guidelogic.Read(new GuideBindingModel {
                OperatorID = App.Operator.ID
            });
            foreach (var guide in listbindmodels)
            {
                ListBoxAvailable.Items.Add(guide);
            }
            if (tour != null)
            {
                ListBoxAvailable.Items.Clear();
                List<int> array = new List<int>();
                var listSelectedGuides = tour.TourGuides.ToList();
                foreach (var guide in listSelectedGuides)
                {
                    GuideViewModel current = Guidelogic.Read(new GuideBindingModel
                    {
                        ID = guide.Key
                    })[0];
                    ListBoxSelected.Items.Add(current);
                    array.Add(current.ID);
                }
                foreach (var guide in listbindmodels)
                {
                    if (!array.Contains(guide.ID))
                    ListBoxAvailable.Items.Add(guide);
                }
                NameTextBox.Text = tour.Name;
                CountryTextBox.Text = tour.Country;
                PriceTextBox.Text = tour.Price.ToString();
                ComboBoxHalts.SelectedValue = tour.HaltID;

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
            foreach (GuideViewModel guide in ListBoxSelected.Items)
            {
                _TourGuides.Add(guide.ID, guide.Surname);
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
                logger.Warn("Ошибка при попытке сохранения данных о туре");
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