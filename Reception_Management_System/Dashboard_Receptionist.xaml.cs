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
        private MainWindow MainWindow = null;

        public Dashboard_Receptionist()
        {
            InitializeComponent();
        }

        // #########################################################################################

        public void setMainWindow(MainWindow inputMainWindow)
        {
            MainWindow = inputMainWindow;
        }
        
        // #########################################################################################



        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Show();
            this.Close();
        }



        private void search_Click(object sender, RoutedEventArgs e)
        {
            SearchvisitorPanel.Visibility = Visibility.Visible;
            FormPanel.Visibility = Visibility.Collapsed;
            ShowInfoPanel.Visibility = Visibility.Collapsed;
            EmployeePanel.Visibility = Visibility.Collapsed;
        }



        private void form_Click(object sender, RoutedEventArgs e)
        {
            FormPanel.Visibility = Visibility.Visible;
            SearchvisitorPanel.Visibility = Visibility.Collapsed;
            ShowInfoPanel.Visibility = Visibility.Collapsed;
            EmployeePanel.Visibility = Visibility.Collapsed;
        }



        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            EmployeePanel.Visibility = Visibility.Visible;
            FormPanel.Visibility = Visibility.Collapsed;
            SearchvisitorPanel.Visibility = Visibility.Collapsed;
            ShowInfoPanel.Visibility = Visibility.Collapsed;
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
                        Meeting.Text = "Meeting With :  " + fm.getMeeting();
                        relationship.Text = "Relationship :  " + fm.getRelationship();
                        Purpose.Text = "Purpose :  " + fm.getPurpose();
                        Occupation.Text = "Occupation :  " + fm.getOccupation();
                        contact.Text = "Contact Number :   " + fm.getContact();

                        Company.Text = "Office Name: N/A";
                        email.Text = "Email ID: N/A";

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

                        FormAlert.Visibility = Visibility.Collapsed;
                    }
                    else if(output.Equals("exception"))
                    {
                        FormAlert.Text = "Error! Please Try Again Later";
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
                        Meeting.Text = "Meeting With :  " + fm.getMeeting();
                        relationship.Text = "Relationship :  " + fm.getRelationship();
                        Purpose.Text = "Purpose :  " + fm.getPurpose();
                        Occupation.Text = "Occupation :  " + fm.getOccupation();
                        Company.Text = "Office Name :  " + fm.getCompany_name();
                        email.Text = "Email Id :  " + fm.getEmail();
                        contact.Text = "Contact Number :   " + fm.getContact();

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

                        FormAlert.Visibility = Visibility.Collapsed;
                    }
                    else if(output.Equals("exception"))
                    {
                        FormAlert.Text = "Error! Please Try Again Later";
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
                fm.setRelationship("Family");
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
            dates.Text = "Visiting Date : " + date;
            times.Text = "Visiting Time : " + time;

            fm.date = date;
            fm.time = time;
        }



    }
}
