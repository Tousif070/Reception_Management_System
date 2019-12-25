using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Reception_Management_System
{
    class BL_Tables
    {
        public List<VisitorEmployeeView> searchVisitorByName(string name)
        {
            List<VisitorEmployeeView> visitorEmployeeViewList = new List<VisitorEmployeeView>();
            DataAccessLayer.Instance.createDatabaseConnection();
            string query = "SELECT visiting_time, visitor_id, visitor_name, employee_id, employee_name, relationship, purpose FROM visitoremployeeview WHERE visitor_name REGEXP '^" + name + "';";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                    while(dataReader.Read())
                    {
                        VisitorEmployeeView visitorEmployeeView = new VisitorEmployeeView();
                        visitorEmployeeView.VisitingTime = dataReader.GetString(0);
                        visitorEmployeeView.VisitorID = dataReader.GetInt32(1);
                        visitorEmployeeView.VisitorName = dataReader.GetString(2);
                        visitorEmployeeView.EmployeeID = dataReader.GetInt32(3);
                        visitorEmployeeView.EmployeeName = dataReader.GetString(4);
                        visitorEmployeeView.Relationship = dataReader.GetString(5);
                        visitorEmployeeView.Purpose = dataReader.GetString(6);
                        visitorEmployeeViewList.Add(visitorEmployeeView);
                    }
                    return visitorEmployeeViewList;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " SearchVisitorByName Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public List<VisitorEmployeeView> searchVisitorByDate(string time1, string time2)
        {
            List<VisitorEmployeeView> visitorEmployeeViewList = new List<VisitorEmployeeView>();
            DataAccessLayer.Instance.createDatabaseConnection();
            string query = "SELECT visiting_time, visitor_id, visitor_name, employee_id, employee_name, relationship, purpose FROM visitoremployeeview WHERE visiting_time BETWEEN '" + time1 + "' AND '" + time2 + "';";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        VisitorEmployeeView visitorEmployeeView = new VisitorEmployeeView();
                        visitorEmployeeView.VisitingTime = dataReader.GetString(0);
                        visitorEmployeeView.VisitorID = dataReader.GetInt32(1);
                        visitorEmployeeView.VisitorName = dataReader.GetString(2);
                        visitorEmployeeView.EmployeeID = dataReader.GetInt32(3);
                        visitorEmployeeView.EmployeeName = dataReader.GetString(4);
                        visitorEmployeeView.Relationship = dataReader.GetString(5);
                        visitorEmployeeView.Purpose = dataReader.GetString(6);
                        visitorEmployeeViewList.Add(visitorEmployeeView);
                    }
                    return visitorEmployeeViewList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " SearchVisitorByDate Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public Visitor getAllInfo(int id)
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            string query = "SELECT id, name, gender, employee_id, relationship, purpose, occupation, office_name, email_id, contact_number, visiting_date, visiting_time FROM visitor WHERE id=" + id + ";";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                    Visitor visitor = new Visitor();
                    if (dataReader.Read())
                    {
                        visitor.ID = dataReader.GetInt32(0);
                        visitor.Name = dataReader.GetString(1);
                        visitor.Gender = dataReader.GetString(2);
                        visitor.EmployeeID = dataReader.GetInt32(3);
                        visitor.Relationship = dataReader.GetString(4);
                        visitor.Purpose = dataReader.GetString(5);
                        visitor.Occupation = dataReader.GetString(6);

                        visitor.OfficeName = dataReader["office_name"].ToString();
                        visitor.EmailID = dataReader["email_id"].ToString();

                        if(visitor.OfficeName.Equals("") && visitor.EmailID.Equals(""))
                        {
                            visitor.OfficeName = "N/A";
                            visitor.EmailID = "N/A";
                        }

                        visitor.ContactNumber = dataReader.GetInt32(9);

                        string date = dataReader.GetString(10);
                        string[] portion = date.Split(' ');
                        visitor.VisitingDate = portion[0];

                        visitor.VisitingTime = dataReader.GetString(11);
                    }
                    return visitor;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " GetAllInfo Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public string getEmployeeName(int id)
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            string query = "SELECT name FROM employee WHERE id=" + id + ";";
            string name = "";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                    if(dataReader.Read())
                    {
                        name = dataReader["name"].ToString();
                    }
                    return name;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " GetEmployeeName Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return "null";
            }
        }

        public List<Employee> searchEmployeeName(string name)
        {
            List<Employee> employeeList = new List<Employee>();
            DataAccessLayer.Instance.createDatabaseConnection();
            string query = "SELECT id, name, gender, designation FROM employee WHERE name REGEXP '^" + name + "';";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                    while(dataReader.Read())
                    {
                        Employee employee = new Employee();
                        employee.ID = dataReader.GetInt32(0);
                        employee.Name = dataReader.GetString(1);
                        employee.Gender = dataReader.GetString(2);
                        employee.Designation = dataReader.GetString(3);
                        employeeList.Add(employee);
                    }
                    return employeeList;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " SearchEmployeeName Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public List<Employee> displayAllEmployees()
        {
            List<Employee> employeeList = new List<Employee>();
            DataAccessLayer.Instance.createDatabaseConnection();
            string query = "SELECT id, name, gender, designation FROM employee";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        Employee employee = new Employee();
                        employee.ID = dataReader.GetInt32(0);
                        employee.Name = dataReader.GetString(1);
                        employee.Gender = dataReader.GetString(2);
                        employee.Designation = dataReader.GetString(3);
                        employeeList.Add(employee);
                    }
                    return employeeList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " SearchEmployeeName Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }



        public int deleteAllInfo(int id)
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            string query = "DELETE FROM visitor WHERE id=" + id + ";";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    return mySqlCommand.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " DeleteAllInfo Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }



        public int modifyVisitorInfo(int id, string purpose, string occupation, string officeName)
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            string query = "UPDATE visitor SET purpose='" + purpose + "', occupation='" + occupation + "', office_name='" + officeName + "' WHERE id=" + id + ";";

            try
            {
                using (MySqlCommand mySqlCommand = new MySqlCommand(query, DataAccessLayer.Instance.Connection))
                {
                    return mySqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.ToString() + " ModifyVisitorInfo Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }


    }
}
