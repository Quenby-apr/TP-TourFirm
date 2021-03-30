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

namespace TourFirmView
{
    /// <summary>
    /// Логика взаимодействия для WindowSignUp.xaml
    /// </summary>
    public partial class WindowSignUp : Window
    {
        public WindowSignUp()
        {
            InitializeComponent();
        }
        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            WindowSignIn window = new WindowSignIn();
            window.Show();
            this.Close();
        }
    }
}
