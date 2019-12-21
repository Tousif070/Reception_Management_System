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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Reception_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private void Button_ExitApp(object sender, RoutedEventArgs ex)
        {
            Application.Current.Shutdown();
        }

        private void Testing_Receptionist_Dashboard(object sender, RoutedEventArgs ex)
        {
            Dashboard_Receptionist receptionist = new Dashboard_Receptionist();
            receptionist.Show();
            receptionist.setMainWindow(this);
            this.Hide();
        }

    }
}
