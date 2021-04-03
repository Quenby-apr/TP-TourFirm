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
    /// Логика взаимодействия для WindowBondTravelExcursions.xaml
    /// </summary>
    public partial class WindowBondTravelExcursions : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly TravelLogic logic;
        private readonly List<ExcursionViewModel> listAllExcursions;

        public int Id { set { id = value; } }
        private int? id;

        private Dictionary<int, string> travelExcursions;

        public WindowBondTravelExcursions(TravelLogic travelLogic, ExcursionLogic excursionLogic)
        {
            InitializeComponent();
            logic = travelLogic;
            listAllExcursions = excursionLogic.Read(null);
        }

        private void WindowBondTravelExcursions_Load(object sender, RoutedEventArgs e)
        {
                try
                {
                    var view = logic.Read(new TravelBindingModel { ID = id })?[0];

                    if (view != null)
                    {
                        travelExcursions = view.TravelExcursions;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }

        private void LoadData()
        {
            try
            {
                //Если к путешествию уже привязаны какие-то экскурсии, в ListBox с доступными экскурсиями 
                //оставляем только непривязанные экскурсии
                if (travelExcursions != null)
                {
                    ListBoxSelectedExcursions.ItemsSource = travelExcursions;
                    ListBoxAvaliableExcursions.Items.Clear();

                    foreach (var excursionFromAll in listAllExcursions)
                    {
                        foreach (var excursionFromSelected in travelExcursions)
                        {
                            if (excursionFromAll.ID != excursionFromSelected.Key)
                            {
                                ListBoxAvaliableExcursions.Items.Add(excursionFromSelected);
                            }
                        }
                    }
                }
                else
                {
                    ListBoxAvaliableExcursions.ItemsSource = listAllExcursions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonAddExcursion_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxAvaliableExcursions.SelectedItems.Count == 0)
            {
                MessageBox.Show("Эскурсия не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //TODO: перепроверить - не уверена, что это будет работать
            var selectedExcursion = ListBoxAvaliableExcursions.SelectedItem;
            travelExcursions.Add((int)ListBoxAvaliableExcursions.SelectedValue, ((TourViewModel)selectedExcursion).Name);

            ListBoxSelectedExcursions.Items.Add(selectedExcursion);
            ListBoxAvaliableExcursions.Items.Remove(selectedExcursion);
        }

        private void ButtonRemoveExcursion_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxSelectedExcursions.SelectedItems.Count == 0)
            {
                MessageBox.Show("Эскурсия не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    travelExcursions.Remove((int)ListBoxSelectedExcursions.SelectedValue);

                    //TODO: перепроверить - не уверена, что это будет работать
                    var selectedExcursion = ListBoxSelectedExcursions.SelectedItem;
                    ListBoxAvaliableExcursions.Items.Add(selectedExcursion);
                    ListBoxSelectedExcursions.Items.Remove(selectedExcursion);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonBond_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxSelectedExcursions.Items.Count == 0)
            {
                MessageBox.Show("Выберите экскурсии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var travel = logic.Read(new TravelBindingModel { ID = id })?[0];

                logic.CreateOrUpdate(new TravelBindingModel
                {
                    ID = id,
                    Name = travel.Name,
                    DateStart = travel.DateStart,
                    DateEnd = travel.DateEnd,
                    TouristID = travel.TouristID,
                    TravelTours = travel.TravelTours,
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
