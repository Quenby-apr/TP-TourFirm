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
    /// Логика взаимодействия для WindowBondExcursionTravel.xaml
    /// </summary>
    public partial class WindowBondExcursionTravel : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly TravelLogic travelLogic;
        private readonly ExcursionLogic excursionLogic;
        private Dictionary<int, string> excursionTravels;
        private readonly List<TravelViewModel> listAllTravels;


        public int Id { set { id = value; } }
        private int id;

        public WindowBondExcursionTravel(TravelLogic travelLogic, ExcursionLogic excursionLogic)
        {
            InitializeComponent();
            this.travelLogic = travelLogic;
            this.excursionLogic = excursionLogic;
            listAllTravels = travelLogic.Read(null);
        }

        private void WindowBondExcursionTravel_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                ExcursionViewModel view = excursionLogic.Read(new ExcursionBindingModel { ID = id })?[0];

                if (view != null)
                {
                    excursionTravels = view.ExcursionTravels;

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
                foreach (var travel in listAllTravels)
                {
                    bool isChecked = false;

                    if (excursionTravels.ContainsKey(travel.ID))
                    {
                        isChecked = true;
                    }
                    
                    DataGridTravels.Items.Add(new object[] { isChecked, travel.Name });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonBond_Click(object sender, RoutedEventArgs e)
        {
            /*DataGridTravels.Items.

            foreach (var travel in DataGridTravels.)
            {
                if (travel)
            }*/
        }

    }
}
