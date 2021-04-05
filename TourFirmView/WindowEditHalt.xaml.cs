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
    /// Логика взаимодействия для WindowEditHalt.xaml
    /// </summary>
    public partial class WindowEditHalt : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly HaltLogic logic;

        public int Id { set { id = value; } }
        private int? id;

        public WindowEditHalt(HaltLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Введите название остановки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(AddressTextBox.Text))
            {
                MessageBox.Show("Введите адрес остановки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new HaltBindingModel
                {
                    ID = id,
                    Name = NameTextBox.Text,
                    Address = AddressTextBox.Text,
                    OperatorID = App.Operator.ID,
                    
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            if (id != null)
            {
                var halt = logic.Read(new HaltBindingModel
                {
                    ID = id
                })[0];
                NameTextBox.Text = halt.Name;
                AddressTextBox.Text = halt.Address;
            }
        }
    }
}
