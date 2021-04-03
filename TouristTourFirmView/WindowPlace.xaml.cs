using System;
using System.Windows;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using Unity;

namespace TouristTourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowPlace.xaml
    /// </summary>
    public partial class WindowPlace : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly PlaceLogic logic;

        public int Id { set { id = value; } }
        private int? id;

        public WindowPlace(PlaceLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Введите название места", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxType.Text))
            {
                MessageBox.Show("Выберите тип места", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                logic.CreateOrUpdate(new PlaceBindingModel
                {
                    ID = id,
                    Name = TextBoxName.Text,
                    Type = TextBoxType.Text,
                    TouristID = App.Tourist.ID
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
