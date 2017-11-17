using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;


namespace TheDriver
{
    public partial class Login : System.Web.UI.Page
    {
        public User _user = new User();
        bool _access = false;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginUser(string _emailAddress, string _password)
        {
            string ConnStr = ConfigurationManager.ConnectionStrings["UserContext"].ConnectionString;
            string sqlStatement = "dbo.uspGetUserByEmail";

            using (SqlConnection _connection = new SqlConnection(ConnStr))
            {
                _connection.Open();
                using (SqlCommand _command = new SqlCommand(sqlStatement, _connection) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    _command.Parameters.AddWithValue("@EmailAddress", _emailAddress);
                    using (SqlDataReader reader = _command.ExecuteReader())
                    {
                        if (reader.HasRows == false)
                        {
                            result.Visible = true;
                            result.InnerText = "Email Address does not match our records";
                        }
                        else
                        {
                            while (reader.Read())
                            {
                                if (Password.Value == reader["Password"].ToString())
                                {
                                    _user.ID = Convert.ToInt64(reader["ID"]);
                                    _user.FirstName = reader["FirstName"].ToString();
                                    _user.LastName = reader["LastName"].ToString();
                                    _user.EmailAddress = reader["EmailAddress"].ToString();
                                    _user.PhoneNumber = reader["PhoneNumber"].ToString();
                                    _access = true;
                                    AccessGranted();
                                }
                                else
                                {
                                    result.Visible = true;
                                    result.InnerText = "Password is incorrect.";
                                }

                            }
                        }

                    }
                }
            }

        }

        private void AccessGranted()
        {
            HttpCookie myCookie = new HttpCookie("UserSettings");
            myCookie["FirstName"] = _user.FirstName;
            myCookie["LastName"] = _user.LastName;
            myCookie["EmailAddress"] = _user.EmailAddress;
            myCookie["PhoneNumber"] = _user.PhoneNumber;
            myCookie["Id"] = _user.ID.ToString();
            myCookie.Expires.AddDays(12);
            Response.Cookies.Add(myCookie);
            Response.Redirect("/Default.aspx");
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
            LoginUser(EmailAddress.Value, Password.Value);
        }
    }
}