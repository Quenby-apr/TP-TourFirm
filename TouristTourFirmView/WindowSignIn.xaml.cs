using System.Windows;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.ViewModels;
using Unity;

namespace TouristTourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowSignIn.xaml
    /// </summary>
    public partial class WindowSignIn : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly TouristLogic logic;
        public WindowSignIn(TouristLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(PasswordBox.Password))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var tourists = logic.Read(null);

            TouristViewModel touristView = null;

            foreach (var tourist in tourists)
            {
                if (tourist.Login == TextBoxLogin.Text && tourist.Password == PasswordBox.Password)
                {
                    touristView = tourist;
                }
            }

            if (touristView != null)
            {
                App.Tourist = touristView;
                var form = Container.Resolve<WindowMain>();
                form.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Неверно введён логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void ButtonGoToSignUp_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowSignUp>();
            form.ShowDialog();
        }
    }
}