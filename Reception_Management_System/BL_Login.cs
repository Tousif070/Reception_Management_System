using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Reception_Management_System
{
    class BL_Login
    {
        private string username = "";
        private string password = "";

        public string Username
        {
            set
            {
                username = value;
            }
        }

        public string Password
        {
            set
            {
                password = value;
            }
            get
            {
                return password;
            }
        }

        public bool emptyFields()
        {
            if(username.Equals("") || password.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public LoginView getLoginInfo()
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            string query = "Select id, employee_id, username, password, designation FROM loginview WHERE username='" + username + "';";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                    LoginView loginView = new LoginView();
                    loginView.Username = "emptystring";
                    if(dataReader.Read())
                    {
                        loginView.LoginID = dataReader.GetInt32(0);
                        loginView.EmployeeID = dataReader.GetInt32(1);
                        loginView.Username = dataReader.GetString(2);
                        loginView.Password = dataReader.GetString(3);
                        loginView.Designation = dataReader.GetString(4);
                    }
                    return loginView;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " GetLoginInfo Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

    }
}
