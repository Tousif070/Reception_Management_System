﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Reception_Management_System
{
    class DataAccessLayer
    {
        private MySqlConnection connection = null;
        private static DataAccessLayer instance = null;
        private String server = "";
        private String port = "";
        private String database = "";
        private String uid = "";
        private String pass = "";
        private String connectionString = "";

        private DataAccessLayer()
        {
            server = "localhost";
            port = "3306";
            database = "reception_management_system_database";
            uid = "root";
            pass = "";
            connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" + "DATABASE=" + database + ";" + "USERNAME=" + uid + ";" + "PASSWORD=" + pass + ";";
        }

        public static DataAccessLayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataAccessLayer();
                    return instance;
                }
                else
                {
                    return instance;
                }
            }
        }

        public void createDatabaseConnection()
        {
            if (connection == null)
            {
                try
                {
                    connection = new MySqlConnection(connectionString);
                    connection.Open();
                }
                catch (Exception ex)
                {
                    connection = null;
                    MessageBox.Show(ex.Message, this.ToString() + " CreateDatabaseConnection Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public MySqlConnection Connection
        {
            get
            {
                return connection;
            }
        }

    }
}
