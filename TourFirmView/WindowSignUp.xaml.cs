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

        public int Id { set { id = value; } }
        private int? id;

        public WindowSignUp(OperatorLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
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
            logic.CreateOrUpdate(new OperatorBindingModel
            {
                ID = id,
                Name = FirstNameTextBox.Text,
                Surname = SecondNameTextBox.Text,
                PhoneNumber = PhoneTextBox.Text,
                Login = LoginTextBox.Text,
                Password = PasswordTextBox.Text,
                Mail = MailTextBox.Text,
            });
            DialogResult = true;
            Close();
        }
    }
}
