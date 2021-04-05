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
    /// Логика взаимодействия для WindowEditGuide.xaml
    /// </summary>
    public partial class WindowEditGuide : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly GuideLogic logic;

        public int Id { set { id = value; } }
        private int? id;
        public int HaltID { get; set; }

        public WindowEditGuide(GuideLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Введите имя гида", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(SurnameTextBox.Text))
            {
                MessageBox.Show("Введите фамилию гида", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(PhoneTextBox.Text))
            {
                MessageBox.Show("Введите номер телефона гида", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(WorkPlaceTextBox.Text))
            {
                MessageBox.Show("Введите место работы гида (город)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(MainLanguageTextBox.Text))
            {
                MessageBox.Show("Введите основной язык гида", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(AdditionalLanguageTextBox.Text))
            {
                MessageBox.Show("Введите дополнительный язык гида", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
                logic.CreateOrUpdate(new GuideBindingModel
                {
                    ID = id,
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    PhoneNumber = PhoneTextBox.Text,
                    WorkPlace = WorkPlaceTextBox.Text,
                    DateWork=DateTime.Now,
                    MainLanguage =MainLanguageTextBox.Text,
                    AdditionalLanguage = AdditionalLanguageTextBox.Text,
                    OperatorID = App.Operator.ID,
                });;
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            if (id != null)
            {
                var guide = logic.Read(new GuideBindingModel
                {
                    ID = id
                })[0];
                NameTextBox.Text = guide.Name;
                SurnameTextBox.Text = guide.Surname;
                WorkPlaceTextBox.Text = guide.WorkPlace;
                PhoneTextBox.Text = guide.PhoneNumber;
                MainLanguageTextBox.Text = guide.MainLanguage;
                AdditionalLanguageTextBox.Text = guide.AdditionalLanguage;
            }
        }
    }
}
