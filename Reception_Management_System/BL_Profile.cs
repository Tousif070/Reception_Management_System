using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Reception_Management_System
{
    class BL_Profile
    {
        private string username = "";
        private string password = "";
        private string repeatPassword = "";

        public String Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public String Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public String RepeatPassword
        {
            get
            {
                return repeatPassword;
            }
            set
            {
                repeatPassword = value;
            }
        }

        public bool CheckEmpty()
        {
            if (username.Equals("") || password.Equals("") || repeatPassword.Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckPassword()
        {
            if (password.Equals(repeatPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public String CheckUsername(int id)
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            String query = "SELECT username FROM login WHERE username = '" + username + "' AND NOT id=" + id + ";";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                    if(!dataReader.Read())
                    {
                        return "all clear";
                    }
                    else
                    {
                        return "username exists";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " CheckUserName Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return "null";
            }
        }

        public String UpdateProfile(int id)
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            String query = "UPDATE login SET username = '" + username + "', password = '" + password + "' WHERE id = " + id + ";";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    mySqlCommand.ExecuteNonQuery();
                    return "okay";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " UpdateProfile Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return "null";
            }
        }

    }
}
