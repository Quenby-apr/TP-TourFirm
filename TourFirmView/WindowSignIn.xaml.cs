using System;
using System.Windows;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.ViewModels;
using Unity;

namespace TourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowSignIn.xaml
    /// </summary>
    public partial class WindowSignIn : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly OperatorLogic logic;
        public WindowSignIn(OperatorLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
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

            var operators = logic.Read(null);
            Console.WriteLine(LoginTextBox.Text);
            Console.WriteLine(PasswordTextBox.Text);
            OperatorViewModel _operator = null;
            foreach (var oper in operators)
            {
                if (oper.Login == LoginTextBox.Text && oper.Password == PasswordTextBox.Text)
                {
                    _operator = oper;
                }
            }
            if (_operator != null)
            {
                App.Operator = _operator;
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
        private void GoToSignUp_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowSignUp>();
            form.ShowDialog();
        }
    }
}