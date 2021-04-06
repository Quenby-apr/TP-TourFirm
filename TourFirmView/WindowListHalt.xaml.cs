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
using NLog;
using TourFirmBusinessLogic.BindingModels;
using TourFirmBusinessLogic.BusinessLogic;
using TourFirmBusinessLogic.ViewModels;
using Unity;

namespace TourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowListStop.xaml
    /// </summary>
    public partial class WindowListHalt : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly HaltLogic logic;
        private readonly Logger logger;

        public WindowListHalt(HaltLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
            logger = LogManager.GetCurrentClassLogger();
        }

        private void WindowListHalt_Load(object sender, RoutedEventArgs e)
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
                    gridHalts.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var form = Container.Resolve<WindowEditHalt>();

                if (form.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка в форме списка остановок при создании");
            }
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (gridHalts.SelectedItems.Count == 1)
                {
                    var form = Container.Resolve<WindowEditHalt>();
                    form.Id = ((HaltViewModel)gridHalts.SelectedItems[0]).ID;

                    if (form.ShowDialog() == true)
                    {
                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Warn("Ошибка в форме списка остановок при редактировании");
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (gridHalts.SelectedItems.Count == 1)
            {
                MessageBoxResult result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    int id = ((HaltViewModel)gridHalts.SelectedItems[0]).ID;

                    try
                    {
                        logic.Delete(new HaltBindingModel { ID = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        logger.Warn("Ошибка в форме списка остановок при удалении");
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

