using System.Windows;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.Interfaces;
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
        private readonly ITouristStorage touristStorage;
        public WindowSignIn(TouristLogic logic, ITouristStorage touristStorage)
        {
            InitializeComponent();
            this.logic = logic;
            this.touristStorage = touristStorage;
        }

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPassword.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //TODO: Прописать проверку логина и пароля
        }

        private void ButtonGoToSignUp_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowSignUp>();
            form.ShowDialog();
        }
    }
}
