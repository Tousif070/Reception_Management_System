using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace Reception_Management_System
{
    class BL_VisitorForm
    {
        private String name = "";
        private String gender = "";
        private String meeting_with = "";
        private String relationship = "";
        private String occupation = "";
        private String purpose = "";
        private String company_name = "";
        private String Email = "";
        private int contact_num = 0;

        public String date = "";
        public String time = "";



        // SET METHODS
        public void setName(String inputName)
        {
            name = inputName;
        }
        public void setGender(String inputGender)
        {
            gender = inputGender;
        }
        public void setMeeting(String inputMeeting)
        {
            meeting_with = inputMeeting;
        }
        public void setRelationship(String inputRelationship)
        {
            relationship = inputRelationship;
        }
        public void setOccupation(String inputOccupation)
        {
            occupation = inputOccupation;
        }
        public void setPurpose(String inputPurpose)
        {
            purpose = inputPurpose;
        }
        public void setCompanyname(String inputCompanyname)
        {
            company_name = inputCompanyname;
        }
        public void setEmail(String inputEmail)
        {
            Email = inputEmail;
        }
        public void setContact(int inputContact)
        {
            contact_num = inputContact;
        }



        // GET METHODS
        public String getName()
        {
            return name;
        }
        public String getGender()
        {
            return gender;
        }
        public String getMeeting()
        {
            return meeting_with;
        }
        public String getRelationship()
        {
            return relationship;
        }
        public String getOccupation()
        {
            return occupation;
        }
        public String getPurpose()
        {
            return purpose;
        }
        public String getCompany_name()
        {
            return company_name;
        }
        public String getEmail()
        {
            return Email;
        }
        public int getContact()
        {
            return contact_num;
        }



        // EMPTY CHECK FOR PROFESSIONAL
        public bool emptyFields()
        {
            if (name.Equals("") || gender.Equals("") || meeting_with.Equals("") || relationship.Equals("") || occupation.Equals("") || purpose.Equals("") || company_name.Equals("") || Email.Equals("") || contact_num == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // EMPTY CHECK FOR FAMILY
        public bool emptyFieldsFam()
        {
            if (name.Equals("") || gender.Equals("") || meeting_with.Equals("") || relationship.Equals("") || occupation.Equals("") || purpose.Equals("") || contact_num == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        
        // INSERT DATA FOR PROFESSIONAL
        public string insertData1()
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            String query = "INSERT INTO visitor (name, gender, employee_id, relationship, purpose, occupation, office_name, email_id, contact_number, visiting_date, visiting_time) values ('" + name + "','" + gender + "','" + meeting_with + "','" + relationship + "','" + purpose + "','" + occupation + "','" + company_name + "','" + Email + "','" + contact_num + "','" + date + "','" + time + "');";

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
                MessageBox.Show(ex.Message, this.ToString() + " InsertProfessionalData Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return "exception";
            }
        }



        // INSERT DATA FOR FAMILY
        public string insertData2()
        {
            DataAccessLayer.Instance.createDatabaseConnection();
            String query = "INSERT INTO visitor (name, gender, employee_id , relationship, purpose, occupation, contact_number, visiting_date, visiting_time) values ('" + name + "','" + gender + "','" + meeting_with + "','" + relationship + "','" + purpose + "','" + occupation + "','" + contact_num + "','" + date + "','" + time + "');";

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
                MessageBox.Show(ex.Message, this.ToString() + " InsertFamilyData Exception", MessageBoxButton.OK, MessageBoxImage.Error);
                return "exception";
            }
        }

    }
}
