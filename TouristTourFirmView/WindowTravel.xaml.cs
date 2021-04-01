using System;
using System.Collections.Generic;
using System.Windows;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.Interfaces;
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
        private readonly ITourStorage tourStorage;

        public int Id { set { id = value; } }
        private int? id;

        private Dictionary<int, string> travelTours;

        public WindowTravel(TravelLogic travelLogic, ITourStorage tourStorage)
        {
            InitializeComponent();
            this.travelLogic = travelLogic;
            this.tourStorage = tourStorage;
        }

        private void WindowTravel_Load(object sender, RoutedEventArgs e)
        {
            var avaliableTours = tourStorage.GetFullList();
            ListBoxAvaliableTours.ItemsSource = avaliableTours;

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

                        //TODO: перепроверить
                        ListBoxSelectedTours.ItemsSource = travelTours;

                        foreach (var tourFromAll in avaliableTours)
                        {
                            foreach (var tourFromSelected in travelTours)
                            {
                                if (tourFromAll.ID == tourFromSelected.Key)
                                {
                                    ListBoxAvaliableTours.Items.Remove(tourFromSelected);
                                }
                            }
                        }
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
            }
        }

        private void ButtonAddTour_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxAvaliableTours.SelectedItems.Count == 0)
            {
                MessageBox.Show("Тур не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //TODO: перепроверить - не уверена, что это будет работать,
            //      продумать, как добавлять эти туры в словарь travelTours. где брать ключ?
            var tour = ListBoxAvaliableTours.SelectedItem;
            
           // travelTours.Add(ListBoxAvaliableTours.SelectedItem)
            ListBoxSelectedTours.Items.Add(ListBoxAvaliableTours.SelectedItem);
            ListBoxAvaliableTours.Items.Remove(ListBoxAvaliableTours.SelectedItem);
        }

        private void ButtonRemoveTour_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxSelectedTours.SelectedItems.Count == 0)
            {
                MessageBox.Show("Тур не выбран", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //TODO: перепроверить - не уверена, что это будет работать
            ListBoxAvaliableTours.Items.Add(ListBoxSelectedTours.SelectedItem);
            ListBoxSelectedTours.Items.Remove(ListBoxSelectedTours.SelectedItem);
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

          
            try
            {
                travelLogic.CreateOrUpdate(new TravelBindingModel
                {
                    ID = id,
                    Name = TextBoxName.Text,
                    DateStart = (DateTime)DatePickerStart.SelectedDate,
                    DateEnd = (DateTime)DatePickerEnd.SelectedDate,
                    TouristID = App.Tourist.ID,
                    //TODO: продумать. + что делать с travelExcursions?
                  //  TravelTours = new Dictionary<int, string> 
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
