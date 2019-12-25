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
    /// Interaction logic for Dashboard_Receptionist.xaml
    /// </summary>
    public partial class Dashboard_Receptionist : Window
    {
        private BL_VisitorForm fm = new BL_VisitorForm();
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

        public Dashboard_Receptionist()
        {
            InitializeComponent();

            CBYear.Text = DT.Year.ToString();
            CBMonth.Text = DT.Month.ToString();
            CBDate.Text = DT.Day.ToString();
        }

        // #########################################################################################

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

        private void Button_SearchByName(object sender, RoutedEventArgs ex)
        {
            List<VisitorEmployeeView> visitorEmployeeViewList = new List<VisitorEmployeeView>();
            VisitorEmployeeView visitorEmployeeView = new VisitorEmployeeView();
            visitorEmployeeView.VisitorName = TbxVisitorSearch.Text;

            if(!visitorEmployeeView.VisitorName.Equals(""))
            {
                visitorEmployeeViewList = BLT.searchVisitorByName(visitorEmployeeView.VisitorName);

                if(visitorEmployeeViewList != null)
                {
                    if(visitorEmployeeViewList.Count != 0)
                    {
                        AlertVisitorSearch.Visibility = Visibility.Collapsed;
                        TbxVisitorSearch.Text = "";

                        ReceptionistVisitorDG.ItemsSource = visitorEmployeeViewList;
                        ReceptionistTablePanel.Visibility = Visibility.Visible;
                        ReceptionistVisitorDG.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        AlertVisitorSearch.Text = "No Results Found";
                        AlertVisitorSearch.Visibility = Visibility.Visible;
                        ReceptionistTablePanel.Visibility = Visibility.Collapsed;
                        ReceptionistVisitorDG.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    AlertVisitorSearch.Text = "Connection Error! Please Try Again Later";
                    AlertVisitorSearch.Visibility = Visibility.Visible;
                    ReceptionistTablePanel.Visibility = Visibility.Collapsed;
                    ReceptionistVisitorDG.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                AlertVisitorSearch.Text = "The Search Field Is Empty";
                AlertVisitorSearch.Visibility = Visibility.Visible;
                ReceptionistTablePanel.Visibility = Visibility.Collapsed;
                ReceptionistVisitorDG.Visibility = Visibility.Collapsed;
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

                    ReceptionistVisitorDG.ItemsSource = visitorEmployeeViewList;
                    ReceptionistTablePanel.Visibility = Visibility.Visible;
                    ReceptionistVisitorDG.Visibility = Visibility.Visible;
                }
                else
                {
                    AlertVisitorSearch.Text = "No Results Found";
                    AlertVisitorSearch.Visibility = Visibility.Visible;
                    ReceptionistTablePanel.Visibility = Visibility.Collapsed;
                    ReceptionistVisitorDG.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                AlertVisitorSearch.Text = "Connection Error! Please Try Again Later";
                AlertVisitorSearch.Visibility = Visibility.Visible;
                ReceptionistTablePanel.Visibility = Visibility.Collapsed;
                ReceptionistVisitorDG.Visibility = Visibility.Collapsed;
            }
        }

        private void ReceptionistSeeMoreInfo(object sender, RoutedEventArgs ex)
        {
            string objString = ex.Source.ToString();
            string[] portion = objString.Split(' ');
            Visitor visitor = new Visitor();
            visitor.ID = Int32.Parse(portion[1]);
            visitor = BLT.getAllInfo(visitor.ID);

            if(visitor != null)
            {
                string employeeName = BLT.getEmployeeName(visitor.EmployeeID);

                if(!employeeName.Equals("null"))
                {
                    RFITbx1.Text = visitor.ID.ToString();
                    RFITbx2.Text = visitor.Name;
                    RFITbx3.Text = visitor.Gender;
                    RFITbx4.Text = employeeName;
                    RFITbx5.Text = visitor.Relationship;
                    RFITbx6.Text = visitor.Purpose;
                    RFITbx7.Text = visitor.Occupation;
                    RFITbx8.Text = visitor.OfficeName;
                    RFITbx9.Text = visitor.EmailID;
                    RFITbx10.Text = visitor.ContactNumber.ToString();
                    RFITbx11.Text = visitor.VisitingDate;
                    RFITbx12.Text = visitor.VisitingTime;
                    ReceptionistVisitorInfoPanel.Visibility = Visibility.Visible;
                    AlertReceptionistTablePanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    AlertReceptionistTablePanel.Text = "Connection Error! Please Try Again Later";
                    AlertReceptionistTablePanel.Visibility = Visibility.Visible;
                }
            }
            else
            {
                AlertReceptionistTablePanel.Text = "Connection Error! Please Try Again Later";
                AlertReceptionistTablePanel.Visibility = Visibility.Visible;
            }
        }

        // BACK BUTTON FOR TABLE PANEL
        private void Button_Back_ReceptionistTablePanel(object sender, RoutedEventArgs ex)
        {
            ReceptionistTablePanel.Visibility = Visibility.Collapsed;
        }

        // BACK BUTTON FOR VISITOR'S ALL INFO PANEL
        private void Button_Back_ReceptionistVisitorInfoPanel(object sender, RoutedEventArgs ex)
        {
            ReceptionistVisitorInfoPanel.Visibility = Visibility.Collapsed;
        }

        private void Button_SearchEmployee(object sender, RoutedEventArgs ex)
        {
            List<Employee> employeeList = new List<Employee>();
            Employee employee = new Employee();
            employee.Name = TbxEmployeeSearch.Text;
            
            if(!employee.Name.Equals(""))
            {
                employeeList = BLT.searchEmployeeName(employee.Name);

                if (employeeList != null)
                {
                    if(employeeList.Count != 0)
                    {
                        AlertEmployeeSearch.Text = "Results Found";
                        AlertEmployeeSearch.Foreground = new SolidColorBrush(Color.FromRgb(0, 153, 0));
                        AlertEmployeeSearch.Visibility = Visibility.Visible;
                        ReceptionistEmployeeDG.ItemsSource = employeeList;
                        ReceptionistEmployeeDG.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        AlertEmployeeSearch.Text = "No Result Found";
                        AlertEmployeeSearch.Foreground = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                        AlertEmployeeSearch.Visibility = Visibility.Visible;
                        ReceptionistEmployeeDG.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    AlertEmployeeSearch.Text = "Connection Error! Please Try Again Later";
                    AlertEmployeeSearch.Foreground = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                    AlertEmployeeSearch.Visibility = Visibility.Visible;
                }
            }
            else
            {
                AlertEmployeeSearch.Text = "The Search Field Is Empty";
                AlertEmployeeSearch.Foreground = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                AlertEmployeeSearch.Visibility = Visibility.Visible;
            }
        }

        private void InsertEmployeeIDInVisitorForm(object sender, RoutedEventArgs ex)
        {
            string objString = ex.Source.ToString();
            string[] portion = objString.Split(' ');
            txtMeetingwith.Text = portion[1];
        }

        private void Button_ShowAllEmployees(object sender, RoutedEventArgs ex)
        {
            List<Employee> employeeList = new List<Employee>();
            employeeList = BLT.displayAllEmployees();

            if(employeeList != null)
            {
                ReceptionistEmployeeDG.ItemsSource = employeeList;
                ReceptionistEmployeeDG.Visibility = Visibility.Visible;

                AlertEmployeeSearch.Text = "Results Found";
                AlertEmployeeSearch.Foreground = new SolidColorBrush(Color.FromRgb(0, 153, 0));
                AlertEmployeeSearch.Visibility = Visibility.Visible;

                TbxEmployeeSearch.Text = "";
            }
            else
            {
                AlertEmployeeSearch.Text = "Connection Error! Please Try Again Later";
                AlertEmployeeSearch.Foreground = new SolidColorBrush(Color.FromRgb(211, 47, 47));
                AlertEmployeeSearch.Visibility = Visibility.Visible;
                ReceptionistEmployeeDG.Visibility = Visibility.Collapsed;

                TbxEmployeeSearch.Text = "";
            }
        }

        private void Button_ProfileUpdate(object sender, RoutedEventArgs ex)
        {
            BLP.Username = TbxUsername_Change.Text;
            BLP.Password = TbxPassword_Change.Text;
            BLP.RepeatPassword = TbxRepeatPassword_Change.Text;

            if(BLP.CheckEmpty())
            {
                AlertProfilePanel.Text = "Fields Are Empty";
                AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                AlertProfilePanel.Visibility = Visibility.Visible;
            }
            else if(!BLP.CheckPassword())
            {
                AlertProfilePanel.Text = "Passwords Do Not Match";
                AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                AlertProfilePanel.Visibility = Visibility.Visible;
            }
            else if(BLP.Password.Length < 4)
            {
                AlertProfilePanel.Text = "Password Too Small! At Least 4 Characters Needed";
                AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                AlertProfilePanel.Visibility = Visibility.Visible;
            }
            else if(BLP.Username.Equals(username) && !BLP.Password.Equals(password))
            {
                string msg = BLP.UpdateProfile(loginID);

                if(msg.Equals("okay"))
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
                else if(msg.Equals("null"))
                {
                    AlertProfilePanel.Text = "Connection Error! Please Try Again Later";
                    AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                    AlertProfilePanel.Visibility = Visibility.Visible;
                }
            }
            else if(!(BLP.Username.Equals(username) && BLP.Password.Equals(password)))
            {
                string msg1 = BLP.CheckUsername(loginID);

                if(msg1.Equals("all clear"))
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
                else if(msg1.Equals("username exists"))
                {
                    AlertProfilePanel.Text = "Username Already Exists !";
                    AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                    AlertProfilePanel.Visibility = Visibility.Visible;
                }
                else if(msg1.Equals("null"))
                {
                    AlertProfilePanel.Text = "Connection Error! Please Try Again Later";
                    AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(183, 28, 28));
                    AlertProfilePanel.Visibility = Visibility.Visible;
                }
            }
            else if(BLP.Username.Equals(username) && BLP.Password.Equals(password))
            {
                AlertProfilePanel.Text = "No Changes Made";
                AlertProfilePanel.Foreground = new SolidColorBrush(Color.FromRgb(20, 90, 50));
                AlertProfilePanel.Visibility = Visibility.Visible;
            }
        }

        // #########################################################################################



        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Show();
            this.Close();
        }



        // MENU BUTTON
        private void SearchVisitor_Click(object sender, RoutedEventArgs e)
        {
            // PANELS FOR MENU
            SearchvisitorPanel.Visibility = Visibility.Visible;
            FormPanel.Visibility = Visibility.Collapsed;
            EmployeePanel.Visibility = Visibility.Collapsed;
            ProfilePanel.Visibility = Visibility.Collapsed;

            // PANELS FOR RESULT OR OUTPUT
            ShowInfoPanel.Visibility = Visibility.Collapsed;
            ReceptionistTablePanel.Visibility = Visibility.Collapsed;
            ReceptionistVisitorInfoPanel.Visibility = Visibility.Collapsed;

            // ALERT MESSAGES
            AlertVisitorSearch.Visibility = Visibility.Collapsed;
            AlertEmployeeSearch.Visibility = Visibility.Collapsed;
            FormAlert.Visibility = Visibility.Collapsed;
            AlertProfilePanel.Visibility = Visibility.Collapsed;
        }



        // MENU BUTTON
        private void VisitorForm_Click(object sender, RoutedEventArgs e)
        {
            // PANELS FOR MENU
            FormPanel.Visibility = Visibility.Visible;
            SearchvisitorPanel.Visibility = Visibility.Collapsed;
            EmployeePanel.Visibility = Visibility.Collapsed;
            ProfilePanel.Visibility = Visibility.Collapsed;

            // PANELS FOR RESULT OR OUTPUT
            ShowInfoPanel.Visibility = Visibility.Collapsed;
            ReceptionistTablePanel.Visibility = Visibility.Collapsed;
            ReceptionistVisitorInfoPanel.Visibility = Visibility.Collapsed;

            // ALERT MESSAGES
            AlertVisitorSearch.Visibility = Visibility.Collapsed;
            AlertEmployeeSearch.Visibility = Visibility.Collapsed;
            FormAlert.Visibility = Visibility.Collapsed;
            AlertProfilePanel.Visibility = Visibility.Collapsed;
        }



        // MENU BUTTON
        private void SearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            // PANELS FOR MENU
            EmployeePanel.Visibility = Visibility.Visible;
            FormPanel.Visibility = Visibility.Collapsed;
            SearchvisitorPanel.Visibility = Visibility.Collapsed;
            ProfilePanel.Visibility = Visibility.Collapsed;

            // PANELS FOR RESULT OR OUTPUT
            ShowInfoPanel.Visibility = Visibility.Collapsed;
            ReceptionistTablePanel.Visibility = Visibility.Collapsed;
            ReceptionistVisitorInfoPanel.Visibility = Visibility.Collapsed;

            // ALERT MESSAGES
            AlertVisitorSearch.Visibility = Visibility.Collapsed;
            AlertEmployeeSearch.Visibility = Visibility.Collapsed;
            FormAlert.Visibility = Visibility.Collapsed;
            AlertProfilePanel.Visibility = Visibility.Collapsed;
        }



        // MENU BUTTON
        private void UserProfile_Click(object sender, RoutedEventArgs ex)
        {
            // PANELS FOR MENU
            ProfilePanel.Visibility = Visibility.Visible;
            EmployeePanel.Visibility = Visibility.Collapsed;
            FormPanel.Visibility = Visibility.Collapsed;
            SearchvisitorPanel.Visibility = Visibility.Collapsed;

            // PANELS FOR RESULT OR OUTPUT
            ShowInfoPanel.Visibility = Visibility.Collapsed;
            ReceptionistTablePanel.Visibility = Visibility.Collapsed;
            ReceptionistVisitorInfoPanel.Visibility = Visibility.Collapsed;

            // ALERT MESSAGES
            AlertVisitorSearch.Visibility = Visibility.Collapsed;
            AlertEmployeeSearch.Visibility = Visibility.Collapsed;
            FormAlert.Visibility = Visibility.Collapsed;
            AlertProfilePanel.Visibility = Visibility.Collapsed;
        }



        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            txtName.Text = "";
            txtcontactno.Text = "";

            txtOccupation.Text = "";
            txtpurpose.Text = "";
            txtEmailid.Text = "";
            txtofficename.Text = "";
            txtMeetingwith.Text = "";
        }



        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

            setinfo();

            if (familyRelationship.IsChecked == true)
            {
                if (fm.emptyFieldsFam())
                {
                    FormAlert.Text = "Fill Up All The Fields Properly";
                    FormAlert.Visibility = Visibility.Visible;
                }
                else
                {
                    string output = fm.insertData2();

                    if(output.Equals("okay"))
                    {
                        name.Text = "Name :  " + fm.getName();
                        Gender.Text = "Gender :  " + fm.getGender();

                        // EXPLICITLY DISPLAYING THE EMPLOYEE NAME FROM EMPLOYEE ID
                        int employeeID = Int32.Parse(fm.getMeeting());
                        Meeting.Text = "Meeting With :  " + BLT.getEmployeeName(employeeID);

                        relationship.Text = "Relationship :  " + fm.getRelationship();
                        Purpose.Text = "Purpose :  " + fm.getPurpose();
                        Occupation.Text = "Occupation :  " + fm.getOccupation();
                        Company.Text = "Office Name :  N/A";
                        email.Text = "Email ID :  N/A";
                        contact.Text = "Contact Number :  " + fm.getContact();

                        ShowInfoPanel.Visibility = Visibility.Visible;
                        FormPanel.Visibility = Visibility.Collapsed;
                        SearchvisitorPanel.Visibility = Visibility.Collapsed;
                        EmployeePanel.Visibility = Visibility.Collapsed;

                        txtName.Text = "";
                        txtcontactno.Text = "";
                        txtOccupation.Text = "";
                        txtpurpose.Text = "";
                        txtMeetingwith.Text = "";
                        txtEmailid.Text = "";
                        txtofficename.Text = "";
                    }
                    else if(output.Equals("exception"))
                    {
                        FormAlert.Text = "Connection Error! Please Try Again Later";
                        FormAlert.Visibility = Visibility.Visible;
                    }

                    
                }
            }

            else if (professionalRelationship.IsChecked == true)
            {
                fm.setEmail(txtEmailid.Text);
                fm.setCompanyname(txtofficename.Text);

                if (fm.emptyFields())
                {
                    FormAlert.Text = "Fill Up All The Fields Properly";
                    FormAlert.Visibility = Visibility.Visible;
                }
                else
                {
                    string output = fm.insertData1();

                    if(output.Equals("okay"))
                    {
                        name.Text = "Name :  " + fm.getName();
                        Gender.Text = "Gender :  " + fm.getGender();

                        // EXPLICITLY DISPLAYING THE EMPLOYEE NAME FROM EMPLOYEE ID
                        int employeeID = Int32.Parse(fm.getMeeting());
                        Meeting.Text = "Meeting With :  " + BLT.getEmployeeName(employeeID);

                        relationship.Text = "Relationship :  " + fm.getRelationship();
                        Purpose.Text = "Purpose :  " + fm.getPurpose();
                        Occupation.Text = "Occupation :  " + fm.getOccupation();
                        Company.Text = "Office Name :  " + fm.getCompany_name();
                        email.Text = "Email ID :  " + fm.getEmail();
                        contact.Text = "Contact Number :  " + fm.getContact();

                        ShowInfoPanel.Visibility = Visibility.Visible;
                        FormPanel.Visibility = Visibility.Collapsed;
                        SearchvisitorPanel.Visibility = Visibility.Collapsed;
                        EmployeePanel.Visibility = Visibility.Collapsed;

                        txtName.Text = "";
                        txtcontactno.Text = "";
                        txtOccupation.Text = "";
                        txtpurpose.Text = "";
                        txtEmailid.Text = "";
                        txtofficename.Text = "";
                        txtMeetingwith.Text = "";
                    }
                    else if(output.Equals("exception"))
                    {
                        FormAlert.Text = "Connection Error! Please Try Again Later";
                        FormAlert.Visibility = Visibility.Visible;
                    }
                    
                }
            }
        }



        // RADIO BUTTON FOR PROFESSIONAL
        private void txtR_Click(object sender, RoutedEventArgs e)
        {
            CompName.Visibility = Visibility.Visible;
            txtofficename.Visibility = Visibility.Visible;
            Emailid.Visibility = Visibility.Visible;
            txtEmailid.Visibility = Visibility.Visible;
        }

        // RADIO BUTTON FOR FAMILY
        private void txtR1_Click(object sender, RoutedEventArgs e)
        {
            CompName.Visibility = Visibility.Collapsed;
            txtofficename.Visibility = Visibility.Collapsed;
            Emailid.Visibility = Visibility.Collapsed;
            txtEmailid.Visibility = Visibility.Collapsed;
        }



        private void setinfo()
        {
            fm.setName(txtName.Text);

            if (txtgender.IsChecked == true)
            {
                fm.setGender("Male");
            }
            else if (txtgender1.IsChecked == true)
            {
                fm.setGender("Female");
            }


            fm.setMeeting(txtMeetingwith.Text);

            if (professionalRelationship.IsChecked == true)
            {
                fm.setRelationship("Professional");
            }
            else if (familyRelationship.IsChecked == true)
            {
                fm.setRelationship("Friend/Family");
            }

            fm.setPurpose(txtpurpose.Text);
            fm.setOccupation(txtOccupation.Text);

            string inputContact = txtcontactno.Text;
            int number = 0;

            if(Int32.TryParse(inputContact, out number))
            {
                fm.setContact(number);
            }
            else
            {
                fm.setContact(0);
            }


            DateTime dt = DateTime.Now;

            String date = dt.ToString("yyyy-MM-dd");
            String time = dt.ToString("yyyy-MM-dd HH:mm:ss");

            fm.date = date;
            fm.time = time;


            // FOR DISPLAYING DATE AND TIME IN THE SHOWINFOPANEL
            dates.Text = "Visiting Date : " + date;
            times.Text = "Visiting Time : " + time;
        }



    }
}
