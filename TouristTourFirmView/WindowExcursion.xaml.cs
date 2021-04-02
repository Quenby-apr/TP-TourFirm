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
using TourFirmBusinessLogic.Interfaces;
using Unity;

namespace TouristTourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowExcursion.xaml
    /// </summary>
    public partial class WindowExcursion : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly ExcursionLogic logic;

        public int Id { set { id = value; } }
        private int? id;

        public WindowExcursion(ExcursionLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название экскурсии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPrice.Text))
            {
                MessageBox.Show("Выберите стоимость экскурсии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxDuration.Text))
            {
                MessageBox.Show("Введите продолжительность экскурсии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxType.Text))
            {
                MessageBox.Show("Введите тип экскурсии", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                logic.CreateOrUpdate(new ExcursionBindingModel
                {
                    ID = id,
                    Name = TextBoxName.Text,
                    Price = Text
                    TouristID = App.Tourist.ID,
                    
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
    }
}
