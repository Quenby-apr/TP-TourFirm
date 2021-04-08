using NLog;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using Unity;

namespace TourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowSignUp.xaml
    /// </summary>
    public partial class WindowSignUp : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly OperatorLogic logic;
        private readonly Logger logger;

        public WindowSignUp(OperatorLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTextBox.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(PhoneTextBox.Text))
            {
                MessageBox.Show("Введите номер телефона ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(SecondNameTextBox.Text))
            {
                MessageBox.Show("Введите фамилию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(MailTextBox.Text))
            {
                MessageBox.Show("Введите почту", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!Regex.IsMatch(MailTextBox.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                MessageBox.Show("Почта введена некорректно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                logic.CreateOrUpdate(new OperatorBindingModel
                {
                    Login = LoginTextBox.Text,
                    Password = PasswordTextBox.Text,
                    Name = FirstNameTextBox.Text,
                    Surname = SecondNameTextBox.Text,
                    Mail = MailTextBox.Text,
                    PhoneNumber = PhoneTextBox.Text
                });
                MessageBox.Show("Регистрация прошла успешно!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка при попытке регистрации нового пользователя");
            }
        }
    }
}