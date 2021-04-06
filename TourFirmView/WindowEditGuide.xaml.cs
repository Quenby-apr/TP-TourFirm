using System;
using System.Windows;
using NLog;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
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
        private readonly Logger logger;

        public int Id { set { id = value; } }
        private int? id;
        public int HaltID { get; set; }

        public WindowEditGuide(GuideLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            logger = LogManager.GetCurrentClassLogger();
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
            try
            {
                logic.CreateOrUpdate(new GuideBindingModel
                {
                    ID = id,
                    Name = NameTextBox.Text,
                    Surname = SurnameTextBox.Text,
                    PhoneNumber = PhoneTextBox.Text,
                    WorkPlace = WorkPlaceTextBox.Text,
                    DateWork = DateTime.Now,
                    MainLanguage = MainLanguageTextBox.Text,
                    AdditionalLanguage = AdditionalLanguageTextBox.Text,
                    OperatorID = App.Operator.ID,
                }); ;
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка в форме редактирования гида при сохранении");
            }
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
