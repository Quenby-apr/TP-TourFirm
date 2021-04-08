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
    /// Логика взаимодействия для WindowTravel.xaml
    /// </summary>
    public partial class WindowTravel : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly TravelLogic travelLogic;
        private readonly List<TourViewModel> listAllTours;

        public int Id { set { id = value; } }
        private int? id;

        private Dictionary<int, string> travelTours;
        private Dictionary<int, string> travelExcursions;

        private readonly Logger logger;

        public WindowTravel(TravelLogic travelLogic, TourLogic tourLogic, ExcursionLogic excursionLogic)
        {
            InitializeComponent();
            this.travelLogic = travelLogic;
            listAllTours = tourLogic.Read(null);
            logger = LogManager.GetCurrentClassLogger();
        }

        private void WindowTravel_Load(object sender, RoutedEventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = travelLogic.Read(new TravelBindingModel { ID = id })?[0];

                    if (view != null)
                    {
                        TextBoxName.Text = view.Name;
                        DatePickerStart.SelectedDate = view.DateStart;
                        DatePickerEnd.SelectedDate = view.DateEnd;
                        travelTours = view.TravelTours;
                        travelExcursions = view.TravelExcursions;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    logger.Warn("Ошибка при попытке загрузки данных о путешествии");
                }
            }
            else
            {
                travelTours = new Dictionary<int, string>();
                travelExcursions = new Dictionary<int, string>();

                foreach (var tour in listAllTours)
                {
                    ListBoxAvaliableTours.Items.Add(tour);
                }
            }
        }

        private void LoadData()
        {
            try
            {
                //Если к путешествию уже привязаны какие-то туры, в ListBox с доступными турами 
                //оставляем только непривязанные туры
                if (travelTours != null)
                {
                    foreach (var tour in travelTours)
                    {
                        ListBoxSelectedTours.Items.Add(tour);
                    }

                    ListBoxAvaliableTours.Items.Clear();

                    foreach (var tourFromAll in listAllTours)
                    {
                        if (!travelTours.ContainsKey(tourFromAll.ID))
                        {
                            ListBoxAvaliableTours.Items.Add(tourFromAll);
                        }
                    }
                }

                if (travelExcursions != null)
                {
                    ListBoxExcursions.ItemsSource = travelExcursions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка про попытке загрузки данных о привязанных турах/экскурсиях");
            }
        }

        private void ButtonAddTour_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxAvaliableTours.SelectedItems.Count == 0)
            {
                MessageBox.Show("Тур не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            travelTours.Add((int)ListBoxAvaliableTours.SelectedValue, ((TourViewModel)ListBoxAvaliableTours.SelectedItem).Name);
            Listboxes_Reload();
        }


        private void ButtonRemoveTour_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxSelectedTours.SelectedItems.Count == 0)
            {
                MessageBox.Show("Тур не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Удалить запись?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    travelTours.Remove((int)ListBoxSelectedTours.SelectedValue);
                    Listboxes_Reload();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    logger.Warn("Ошибка при попытке удаления привязанного тура");
                }
            }
        }

        private void Listboxes_Reload()
        {
            ListBoxAvaliableTours.Items.Clear();
            ListBoxSelectedTours.Items.Clear();

            foreach (var selectedTour in travelTours)
            {
                ListBoxSelectedTours.Items.Add(selectedTour);
            }

            foreach (var tourFromAll in listAllTours)
            {
                if (!travelTours.ContainsKey(tourFromAll.ID))
                {
                    ListBoxAvaliableTours.Items.Add(tourFromAll);
                }
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название путешествия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePickerStart.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату начала путешествия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePickerEnd.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату окончания путешествия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePickerStart.SelectedDate >= DatePickerEnd.SelectedDate)
            {
                MessageBox.Show("Дата начала путешествия не может быть позднее даты окончания путешествия", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ListBoxSelectedTours.Items.Count == 0)
            {
                MessageBox.Show("Выберите туры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                travelLogic.CreateOrUpdate(new TravelBindingModel
                {
                    ID = id,
                    Name = TextBoxName.Text,
                    DateStart = (DateTime)DatePickerStart.SelectedDate,
                    DateEnd = (DateTime)DatePickerEnd.SelectedDate,
                    TouristID = App.Tourist.ID,
                    TravelTours = travelTours,
                    TravelExcursions = travelExcursions
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка при попытке сохранения данных о путешествии");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}