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

namespace Reception_Management_System
{
    /// <summary>
    /// Interaction logic for Dashboard_Owner.xaml
    /// </summary>
    public partial class Dashboard_Owner : Window
    {
        private BL_Tables BLT = new BL_Tables();
        private BL_Profile BLP = new BL_Profile();

        private DateTime DT = DateTime.Now;
        private MainWindow MainWindow = null;

        // VARIABLES NEEDED TO STORE USER SPECIFIC INFORMATION DURING LOGIN
        private int loginID = 0;
        private int employeeID = 0;
        private string username = "";
        private string password = "";
        private string designation = "";

        // VARIABLES USED WHILE MODIFYING VISITOR INFORMATION
        private string purpose = "";
        private string occupation = "";
        private string officeName = "";

        public Dashboard_Owner()
        {
            InitializeComponent();

            CBYear.Text = DT.Year.ToString();
            CBMonth.Text = DT.Month.ToString();
            CBDate.Text = DT.Day.ToString();
        }

        public void setMainWindow(MainWindow inputMainWindow)
        {
            MainWindow = inputMainWindow;
        }

        public void setUserSpecificInformation(int inputLoginID, int inputEmployeeID, string inputUsername, string inputPassword, string inputDesignation)
        {
            loginID = inputLoginID;
            employeeID = inputEmployeeID;
            username = inputUsername;
            password = inputPassword;
            designation = inputDesignation;

            TbxProfileName_ISR.Text = BLT.getEmployeeName(employeeID);
            TbxProfileDesignation_ISR.Text = designation;
            TbxProfileUsername_ISR.Text = username;
            TbxProfilePassword_ISR.Text = password;
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Show();
            this.Close();
        }

        private void SEARCHVISITOR_Click(object sender, RoutedEventArgs e)
        {
            // PANELS FOR MENU
            SEARCHVISITORPANEL.Visibility = Visibility.Visible;
            PROFILEPANEL.Visibility = Visibility.Collapsed;

            // PANELS FOR RESULT OR OUTPUT
            OwnerTablePanel.Visibility = Visibility.Collapsed;
            OwnerVisitorInfoPanel.Visibility = Visibility.Collapsed;

            // ALERT MESSAGES
            AlertVisitorSearch.Visibility = Visibility.Collapsed;
            AlertProfilePanel.Visibility = Visibility.Collapsed;
        }

        private void PROFILE_Click(object sender, RoutedEventArgs e)
        {
            // PANELS FOR MENU
            PROFILEPANEL.Visibility = Visibility.Visible;
            SEARCHVISITORPANEL.Visibility = Visibility.Collapsed;

            // PANELS FOR RESULT OR OUTPUT
            OwnerTablePanel.Visibility = Visibility.Collapsed;
            OwnerVisitorInfoPanel.Visibility = Visibility.Collapsed;

            // ALERT MESSAGES
            AlertVisitorSearch.Visibility = Visibility.Collapsed;
            AlertProfilePanel.Visibility = Visibility.Collapsed;
        }



        private void Button_SearchByName(object sender, RoutedEventArgs ex)
        {
            List<VisitorEmployeeView> visitorEmployeeViewList = new List<VisitorEmployeeView>();
            VisitorEmployeeView visitorEmployeeView = new VisitorEmployeeView();
            visitorEmployeeView.VisitorName = TbxVisitorSearch.Text;

            if (!visitorEmployeeView.VisitorName.Equals(""))
            {
                visitorEmployeeViewList = BLT.searchVisitorByName(visitorEmployeeView.VisitorName);

                if (visitorEmployeeViewList != null)
                {
                    if (visitorEmployeeViewList.Count != 0)
                    {
                        AlertVisitorSearch.Visibility = Visibility.Collapsed;
                        TbxVisitorSearch.Text = "";

                        OwnerVisitorDG.ItemsSource = visitorEmployeeViewList;
                        OwnerTablePanel.Visibility = Visibility.Visible;
                        OwnerVisitorDG.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        AlertVisitorSearch.Text = "No Results Found";
                        AlertVisitorSearch.Visibility = Visibility.Visible;
                        OwnerTablePanel.Visibility = Visibility.Collapsed;
                        OwnerVisitorDG.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    AlertVisitorSearch.Text = "Connection Error! Please Try Again Later";
                    AlertVisitorSearch.Visibility = Visibility.Visible;
                    OwnerTablePanel.Visibility = Visibility.Collapsed;
                    OwnerVisitorDG.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                AlertVisitorSearch.Text = "The Search Field Is Empty";
                AlertVisitorSearch.Visibility = Visibility.Visible;
                OwnerTablePanel.Visibility = Visibility.Collapsed;
                OwnerVisitorDG.Visibility = Visibility.Collapsed;
            }
        }



        private void Button_SearchByDate(object sender, RoutedEventArgs ex)
        {
            string date = CBYear.Text + "-" + CBMonth.Text + "-" + CBDate.Text;
            string time1 = date + " " + CBTime1.Text;
            string time2 = date + " " + CBTime2.Text;

            List<VisitorEmployeeView> visitorEmployeeViewList = new List<VisitorEmployeeView>();
            visitorEmployeeViewList = BLT.searchVisitorByDate(time1, time2);

            if (visitorEmployeeViewList != null)
            {
                if (visitorEmployeeViewList.Count != 0)
                {
                    AlertVisitorSearch.Visibility = Visibility.Collapsed;

                    OwnerVisitorDG.ItemsSource = visitorEmployeeViewList;
                    OwnerTablePanel.Visibility = Visibility.Visible;
                    OwnerVisitorDG.Visibility = Visibility.Visible;
                }
                else
                {
                    AlertVisitorSearch.Text = "No Results Found";
                    AlertVisitorSearch.Visibility = Visibility.Visible;
                    OwnerTablePanel.Visibility = Visibility.Collapsed;
                    OwnerVisitorDG.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                AlertVisitorSearch.Text = "Connection Error! Please Try Again Later";
                AlertVisitorSearch.Visibility = Visibility.Visible;
                OwnerTablePanel.Visibility = Visibility.Collapsed;
                OwnerVisitorDG.Visibility = Visibility.Collapsed;
            }
        }



        private void OwnerSeeMoreInfo(object sender, RoutedEventArgs ex)
        {
            string objString = ex.Source.ToString();
            string[] portion = objString.Split(' ');
            Visitor visitor = new Visitor();
            visitor.ID = Int32.Parse(portion[1]);
            visitor = BLT.getAllInfo(visitor.ID);

            if (visitor != null)
            {
                string employeeName = BLT.getEmployeeName(visitor.EmployeeID);

                if (!employeeName.Equals("null"))
                {
                    RFITbx1.Text = visitor.ID.ToString();
                    RFITbx2.Text = visitor.Name;
                    RFITbx3.Text = visitor.Gender;
                    RFITbx4.Text = employeeName;
                    RFITbx5.Text = visitor.Relationship;

                    RFITbx6.Text = visitor.Purpose;
                    purpose = visitor.Purpose;

                    RFITbx7.Text = visitor.Occupation;
                    occupation = visitor.Occupation;

                    RFITbx8.Text = visitor.OfficeName;
                    officeName = visitor.OfficeName;

                    RFITbx9.Text = visitor.EmailID;
                    RFITbx10.Text = visitor.ContactNumber.ToString();
                    RFITbx11.Text = visitor.VisitingDate;
                    RFITbx12.Text = visitor.VisitingTime;
                    OwnerVisitorInfoPanel.Visibility = Visibility.Visible;
                    AlertOwnerTablePanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    AlertOwnerTablePanel.Text = "Connection Error! Please Try Again Later";
                    AlertOwnerTablePanel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                AlertOwnerTablePanel.Text = "Connection Error! Please Try Again Later";
                AlertOwnerTablePanel.Visibility = Visibility.Visible;
            }
        }



        // BACK BUTTON FOR TABLE PANEL
        private void Button_Back_OwnerTablePanel(object sender, RoutedEventArgs ex)
        {
            OwnerTablePanel.Visibility = Visibility.Collapsed;
        }

        // BACK BUTTON FOR VISITOR'S ALL INFO PANEL
        private void Button_Back_OwnerVisitorInfoPanel(object sender, RoutedEventArgs ex)
        {
            OwnerVisitorInfoPanel.Visibility = Visibility.Collapsed;
        }



        // LIGHTLY DONE
        private void Button_DeleteVisitorInformation(object sender, RoutedEventArgs ex)
        {
            int visitorID = Int32.Parse(RFITbx1.Text);
            int value = BLT.deleteAllInfo(visitorID);

            if(value == 1)
            {
                OwnerVisitorInfoPanel.Visibility = Visibility.Collapsed;
                OwnerTablePanel.Visibility = Visibility.Collapsed;
                AlertVisitorSearch.Text = "Visitor Record Deleted!";
                AlertVisitorSearch.Visibility = Visibility.Visible;
            }
        }



        // LIGHTLY DONE
        private void Button_ModifyVisitorInformation(object sender, RoutedEventArgs ex)
        {
            if(!RFITbx6.Text.Equals(purpose) || !RFITbx7.Text.Equals(occupation) || !RFITbx8.Text.Equals(officeName))
            {
                int visitorID = Int32.Parse(RFITbx1.Text);
                int value = BLT.modifyVisitorInfo(visitorID, RFITbx6.Text, RFITbx7.Text, RFITbx8.Text);

                if(value == 1)
                {
                    OwnerVisitorInfoPanel.Visibility = Visibility.Collapsed;
                    OwnerTablePanel.Visibility = Visibility.Collapsed;
                    AlertVisitorSearch.Text = "Visitor Record Modified!";
                    AlertVisitorSearch.Visibility = Visibility.Visible;
                }
            }
        }



        private void Button_ProfileUpdate(object sender, RoutedEventArgs ex)
        {
            BLP.Username = TbxUsername_Change.Text;
            BLP.Password = TbxPassword_Change.Text;
            BLP.RepeatPassword = TbxRepeatPassword_Change.Text;

            if (BLP.CheckEmpty())
            {
                AlertProfilePanel.Text = "Fields Are Empty";
                AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                AlertProfilePanel.Visibility = Visibility.Visible;
            }
            else if (!BLP.CheckPassword())
            {
                AlertProfilePanel.Text = "Passwords Do Not Match";
                AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                AlertProfilePanel.Visibility = Visibility.Visible;
            }
            else if (BLP.Password.Length < 4)
            {
                AlertProfilePanel.Text = "Password Too Small! At Least 4 Characters Needed";
                AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                AlertProfilePanel.Visibility = Visibility.Visible;
            }
            else if (BLP.Username.Equals(username) && !BLP.Password.Equals(password))
            {
                string msg = BLP.UpdateProfile(loginID);

                if (msg.Equals("okay"))
                {
                    AlertProfilePanel.Text = "Updated";
                    AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(20, 90, 50));
                    AlertProfilePanel.Visibility = Visibility.Visible;

                    password = BLP.Password;
                    TbxProfilePassword_ISR.Text = password;

                    TbxUsername_Change.Text = "";
                    TbxPassword_Change.Text = "";
                    TbxRepeatPassword_Change.Text = "";
                }
                else if (msg.Equals("null"))
                {
                    AlertProfilePanel.Text = "Connection Error! Please Try Again Later";
                    AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                    AlertProfilePanel.Visibility = Visibility.Visible;
                }
            }
            else if (!(BLP.Username.Equals(username) && BLP.Password.Equals(password)))
            {
                string msg1 = BLP.CheckUsername(loginID);

                if (msg1.Equals("all clear"))
                {
                    string msg2 = BLP.UpdateProfile(loginID);

                    if (msg2.Equals("okay"))
                    {
                        AlertProfilePanel.Text = "Updated";
                        AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(20, 90, 50));
                        AlertProfilePanel.Visibility = Visibility.Visible;

                        username = BLP.Username;
                        TbxProfileUsername_ISR.Text = username;

                        password = BLP.Password;
                        TbxProfilePassword_ISR.Text = password;

                        TbxUsername_Change.Text = "";
                        TbxPassword_Change.Text = "";
                        TbxRepeatPassword_Change.Text = "";
                    }
                    else if (msg2.Equals("null"))
                    {
                        AlertProfilePanel.Text = "Connection Error! Please Try Again Later";
                        AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                        AlertProfilePanel.Visibility = Visibility.Visible;
                    }
                }
                else if (msg1.Equals("username exists"))
                {
                    AlertProfilePanel.Text = "Username Already Exists!";
                    AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                    AlertProfilePanel.Visibility = Visibility.Visible;
                }
                else if (msg1.Equals("null"))
                {
                    AlertProfilePanel.Text = "Connection Error! Please Try Again Later";
                    AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                    AlertProfilePanel.Visibility = Visibility.Visible;
                }
            }
            else if (BLP.Username.Equals(username) && BLP.Password.Equals(password))
            {
                AlertProfilePanel.Text = "No Changes Made";
                AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(20, 90, 50));
                AlertProfilePanel.Visibility = Visibility.Visible;
            }
        }


    }
}
