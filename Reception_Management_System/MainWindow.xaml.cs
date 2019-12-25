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
        private BL_Login BLL = new BL_Login();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_ExitApp(object sender, RoutedEventArgs ex)
        {
            Application.Current.Shutdown();
        }

        private void Button_Reset(object sender, RoutedEventArgs ex)
        {
            TbxUsername.Text = "";
            PbxPassword.Password = "";
        }

        private void Button_Login(object sender, RoutedEventArgs ex)
        {
            BLL.Username = TbxUsername.Text;
            BLL.Password = PbxPassword.Password;
            LoginView loginView = new LoginView();

            if(!BLL.emptyFields())
            {
                loginView = BLL.getLoginInfo();

                if(loginView != null)
                {
                    if(!loginView.Username.Equals("emptystring"))
                    {
                        if (loginView.Password.Equals(BLL.Password))
                        {
                            goToNextWindow(loginView);

                            TbxUsername.Text = "";
                            PbxPassword.Password = "";
                            AlertLogin.Visibility = Visibility.Hidden;
                        }
                        else
                        {
                            AlertLogin.Text = "Invalid Password";
                            AlertLogin.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        AlertLogin.Text = "Invalid Username";
                        AlertLogin.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    AlertLogin.Text = "Connection Error! Please Try Again Later";
                    AlertLogin.Visibility = Visibility.Visible;
                }
            }
            else
            {
                AlertLogin.Text = "Empty Username/Password";
                AlertLogin.Visibility = Visibility.Visible;
            }
        }

        private void goToNextWindow(LoginView loginView)
        {
            if(loginView.Designation.Equals("Receptionist"))
            {
                Dashboard_Receptionist receptionist = new Dashboard_Receptionist();
                receptionist.setMainWindow(this);
                receptionist.setUserSpecificInformation(loginView.LoginID, loginView.EmployeeID, loginView.Username, loginView.Password, loginView.Designation);

                receptionist.Show();
                this.Hide();
            }
            else if(loginView.Designation.Equals("Owner/Boss"))
            {
                Dashboard_Owner owner = new Dashboard_Owner();
                owner.setMainWindow(this);
                owner.setUserSpecificInformation(loginView.LoginID, loginView.EmployeeID, loginView.Username, loginView.Password, loginView.Designation);

                owner.Show();
                this.Hide();
            }
        }

    }
}
