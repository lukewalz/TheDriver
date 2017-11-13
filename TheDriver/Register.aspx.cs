using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheDriver
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterUser(string _firstName, string _lastName, string _password, string _emailAddress, string _phoneNumber)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;
            string sqlStatement = "dbo.uspInsertUser";

            using (SqlConnection _connection = new SqlConnection(ConnStr))
            {
                _connection.Open();
                using (SqlCommand _command = new SqlCommand(sqlStatement, _connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    _command.Parameters.AddWithValue("@FirstName", _firstName);
                    _command.Parameters.AddWithValue("@LastName", _lastName);
                    _command.Parameters.AddWithValue("@Password", _password);
                    _command.Parameters.AddWithValue("@PhoneNumber", _phoneNumber);
                    _command.Parameters.AddWithValue("@EmailAddress", _emailAddress);
                    SqlDataReader reader = _command.ExecuteReader();
                }
            }
        }

        protected void submiteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Password.Value == ConfirmPassword.Value)
                {
                    RegisterUser(FirstName.Value, LastName.Value, Password.Value, EmailAddress.Value, PhoneNumber.Value);
                }
                else
                {
                    result.Visible = true;
                    result.InnerText = "Passwords do not match.";
                }
            }
            catch (Exception _ex)
            {

                throw new Exception(_ex.Message);
            }

        }
    }
}