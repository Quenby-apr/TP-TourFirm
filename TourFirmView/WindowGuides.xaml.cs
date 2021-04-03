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
    /// Логика взаимодействия для WindowGuides.xaml
    /// </summary>
    public partial class WindowGuides : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly GuideLogic logic;

        public WindowGuides(GuideLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void WindowGuides_Load(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);

                if (list != null)
                {
                    GridGuides.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            var form = Container.Resolve<WindowEditGuide>();
            if (form.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (GridGuides.SelectedItems.Count == 1)
            {
                var form = Container.Resolve<WindowEditGuide>();
                form.Id = ((GuideViewModel)GridGuides.SelectedItems[0]).ID;

                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (GridGuides.SelectedItems.Count == 1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    int id = ((TourViewModel)GridGuides.SelectedItems[0]).ID;

                    try
                    {
                        logic.Delete(new GuideBindingModel { ID = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
