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

        public WindowTravel(TravelLogic travelLogic, TourLogic tourLogic, ExcursionLogic excursionLogic)
        {
            InitializeComponent();
            this.travelLogic = travelLogic;
            listAllTours = tourLogic.Read(null);
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
                }
            }
            else
            {
                travelTours = new Dictionary<int, string>();
                travelExcursions = new Dictionary<int, string>();
                ListBoxAvaliableTours.ItemsSource = listAllTours;

                //Нужно ли?
                ListBoxSelectedTours.ItemsSource = travelTours;
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
                    ListBoxSelectedTours.ItemsSource = travelTours;
                    ListBoxAvaliableTours.Items.Clear();


                    foreach (var tourFromAll in listAllTours)
                    {
                        foreach (var tourFromSelected in travelTours)
                        {
                            if (tourFromAll.ID != tourFromSelected.Key)
                            {
                                ListBoxAvaliableTours.Items.Add(tourFromSelected);
                            }
                        }
                    }
                }
                else
                {
                    ListBoxAvaliableTours.ItemsSource = listAllTours;

                    //Нужно ли?
                    ListBoxSelectedTours.ItemsSource = travelTours;
                }
                if (travelExcursions != null)
                {
                    ListBoxExcursions.ItemsSource = travelExcursions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAddTour_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxAvaliableTours.SelectedItems.Count == 0)
            {
                MessageBox.Show("Тур не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //TODO: перепроверить - не уверена, что это будет работать
            var selectedTour = ListBoxAvaliableTours.SelectedItem;
            travelTours.Add((int)ListBoxAvaliableTours.SelectedValue, ((TourViewModel)selectedTour).Name);

            ListBoxSelectedTours.Items.Add(selectedTour);
            ListBoxAvaliableTours.Items.Remove(selectedTour);
        }

        private void ButtonRemoveTour_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxSelectedTours.SelectedItems.Count == 0)
            {
                MessageBox.Show("Тур не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    travelTours.Remove((int)ListBoxSelectedTours.SelectedValue);

                    //TODO: перепроверить - не уверена, что это будет работать
                    var selectedTour = ListBoxSelectedTours.SelectedItem;
                    ListBoxAvaliableTours.Items.Add(selectedTour);
                    ListBoxSelectedTours.Items.Remove(selectedTour);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
