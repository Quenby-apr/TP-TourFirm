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
    /// Логика взаимодействия для WindowBindingExcursions.xaml
    /// </summary>
    public partial class WindowBindingExcursions : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly GuideLogic guidelogic;
        private readonly ExcursionLogic excursionlogic;
        public WindowBindingExcursions(GuideLogic guideLogic, ExcursionLogic excursionLogic)
        {
            InitializeComponent();
            guidelogic = guideLogic;
            excursionlogic = excursionLogic;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            ComboBoxChoosenGuide.ItemsSource = guidelogic.Read(null);
            ComboBoxChoosenGuide.SelectedItem = null;
            var listbindmodels = excursionlogic.Read(null);
            foreach (var excursion in listbindmodels)
            {
                ListBoxExcursion.Items.Add(excursion);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<int, string> excursionGuides = new Dictionary<int, string>();
            foreach (var exc in ListBoxExcursion.SelectedItems)
            {
                var excursion = (ExcursionViewModel)exc;
                excursionGuides.Add(excursion.ID, excursion.Name);
            }
            GuideViewModel guide = (GuideViewModel)ComboBoxChoosenGuide.SelectedItem;
            guidelogic.CreateOrUpdate(new GuideBindingModel
            {
                ID = guide.ID,
                Name = guide.Name,
                Surname = guide.Surname,
                WorkPlace = guide.WorkPlace,
                PhoneNumber = guide.PhoneNumber,
                MainLanguage = guide.MainLanguage,
                AdditionalLanguage = guide.AdditionalLanguage,
                OperatorID = App.Operator.ID,
                ExcursionGuides = excursionGuides
            });
            MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }
    }
}
