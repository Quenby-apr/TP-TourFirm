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

        private readonly TravelLogic travelLogic;
        private readonly List<ExcursionViewModel> listAllExcursions;

        public int Id { set { id = value; } }
        private int? id;

        private Dictionary<int, string> travelExcursions;

        public WindowBondTravelExcursions(TravelLogic travelLogic, ExcursionLogic excursionLogic)
        {
            InitializeComponent();
            this.travelLogic = travelLogic;
            listAllExcursions = excursionLogic.Read(null);
        }

        private void WindowBondTravelExcursions_Load(object sender, RoutedEventArgs e)
        {
                try
                {
                    var view = travelLogic.Read(new TravelBindingModel { ID = id })?[0];

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
                    foreach (var excursion in travelExcursions)
                    {
                        ListBoxSelectedExcursions.Items.Add(excursion);
                    }

                    ListBoxAvaliableExcursions.Items.Clear();

                    foreach (var excursionFromAll in listAllExcursions)
                    {
                        if (!travelExcursions.ContainsKey(excursionFromAll.ID))
                        {
                            ListBoxAvaliableExcursions.Items.Add(excursionFromAll);
                        }
                    }
                }
                else
                {
                    foreach (var excursionFromAll in listAllExcursions)
                    {
                        ListBoxAvaliableExcursions.Items.Add(excursionFromAll);
                    }
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
                MessageBox.Show("Экскурсия не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            travelExcursions.Add((int)ListBoxAvaliableExcursions.SelectedValue, ((ExcursionViewModel)ListBoxAvaliableExcursions.SelectedItem).Name);
            Listboxes_Reload();
        }


        private void ButtonRemoveExcursion_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxSelectedExcursions.SelectedItems.Count == 0)
            {
                MessageBox.Show("Экскурсия не выбрана", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Убрать экскурсию?", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    travelExcursions.Remove((int)ListBoxSelectedExcursions.SelectedValue);
                    Listboxes_Reload();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Listboxes_Reload()
        {
            ListBoxAvaliableExcursions.Items.Clear();
            ListBoxSelectedExcursions.Items.Clear();

            foreach (var selectedExcursion in travelExcursions)
            {
                ListBoxSelectedExcursions.Items.Add(selectedExcursion);
            }

            foreach (var tourFromAll in listAllExcursions)
            {
                if (!travelExcursions.ContainsKey(tourFromAll.ID))
                {
                    ListBoxAvaliableExcursions.Items.Add(tourFromAll);
                }
            }
        }

        private void ButtonBond_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var travel = travelLogic.Read(new TravelBindingModel { ID = id })?[0];

                travelLogic.CreateOrUpdate(new TravelBindingModel
                {
                    ID = id,
                    Name = travel.Name,
                    DateStart = travel.DateStart,
                    DateEnd = travel.DateEnd,
                    TouristID = App.Tourist.ID,
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
