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
    /// Логика взаимодействия для WindowSignIn.xaml
    /// </summary>
    public partial class WindowSignIn : Window
    {
        public WindowSignIn()
        {
            InitializeComponent();
        }
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            WindowMain window = new WindowMain();
            window.Show();
            this.Close();
        }
        private void GoToSignUp_Click(object sender, RoutedEventArgs e)
        {
            WindowSignUp window = new WindowSignUp();
            window.Show();
            this.Close();
        }
    }
}
