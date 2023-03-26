using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Market_Automation.Settings
{
    public partial class Create_an_Account : Form
    {
        public Create_an_Account()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data source=.;initial catalog=marketDB; integrated Security=True");

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtIdentity.Clear();
            txtName.Clear();
            txtSurname.Clear();
            txtBirthdate.Clear();
            txtMothername.Clear();
            txtFathername.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtCity.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            btnMale.Checked = false;
            btnFemale.Checked = false;
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    if (txtUsername.Text == "" || txtPassword.Text == "")
                    {
                        MessageBox.Show("Please fill in the Required fields");
                    }
                    string query = "insert into userInformationTB(userIdentity, userRealName, userSurname, userBirthdate, userMotherName, userFatherName, userPhone, userEmail, userCity, userGender) values (@userIdentity, @userRealName, @userSurname, @userBirthdate, @userMotherName, @userFatherName, @userPhone, @userEmail, @userCity, @userGender)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("@userIdentity", txtIdentity.Text.Trim()); //Trim, System.String sınıfından çağrılan ve string değerin başındaki ve sonundaki boşlukları silen fonksiyona denir.
                    cmd.Parameters.AddWithValue("@userRealName", txtName.Text);
                    cmd.Parameters.AddWithValue("@userSurname", txtSurname.Text);
                    cmd.Parameters.AddWithValue("@userBirthdate", txtBirthdate.Text.Trim());
                    cmd.Parameters.AddWithValue("@userMotherName", txtMothername.Text);
                    cmd.Parameters.AddWithValue("@userFatherName", txtFathername.Text);
                    cmd.Parameters.AddWithValue("@userPhone", txtPhone.Text.Trim());
                    cmd.Parameters.AddWithValue("@userEmail", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@userCity", txtCity.Text);

                    if (btnMale.Enabled = true)
                    {
                        cmd.Parameters.AddWithValue("@userGender", "1");
                        btnFemale.Enabled = false;
                    }
                    else if (btnFemale.Enabled = true)
                    {
                        cmd.Parameters.AddWithValue("@userGender", "0");
                        btnMale.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Please select gender.");
                    }

                    cmd.ExecuteNonQuery();
                    con.Close();


                    string query2 = "insert into userTB(userName, userPassword, userAuthority) values (@userName, @userPassword, @userAuthority)";

                    con.Open();
                    SqlCommand cmd2 = new SqlCommand(query2, con);
                    cmd2.Connection = con;

                    cmd2.Parameters.AddWithValue("@userName", txtUsername.Text.Trim());
                    cmd2.Parameters.AddWithValue("@userPassword", txtPassword.Text.Trim());
                    cmd2.Parameters.AddWithValue("@userAuthority", comboBox1.Text.Trim());

                    cmd2.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("The registration process was carried out successfully.");
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("An Error Occurred During The Operation: " + error.Message);
            }
        }

        private void btnClear_MouseHover(object sender, EventArgs e)
        {
            btnClear.BackColor = Color.FromArgb(178, 34, 34);
        }

        private void btnClear_MouseLeave(object sender, EventArgs e)
        {
            btnClear.BackColor = Color.FromArgb(51, 51, 76);
        }

        private void btnCreateAccount_MouseHover(object sender, EventArgs e)
        {
            btnCreateAccount.BackColor = Color.FromArgb(0, 191, 255);
        }

        private void btnCreateAccount_MouseLeave(object sender, EventArgs e)
        {
            btnCreateAccount.BackColor= Color.FromArgb(51, 51, 76);
        }
    }
}
