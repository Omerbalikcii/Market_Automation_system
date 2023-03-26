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
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Market_Automation.Settings
{
    public partial class Change_Password : Form
    {
        public Change_Password()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data source=.;initial catalog=marketDB; integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        DataSet ds;

        private void passwordCondition()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from userTB Where userName = '" + selectUser.Text + "'and userPassword = '" + currentPassword.Text + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count.ToString() == "1")
            {
                if (txtPassword1.Text == txtPassword2.Text)
                {

                    SqlCommand cmd = new SqlCommand("UPDATE userTB SET userPassword = '" + txtPassword1.Text + "' where userName = '" + selectUser.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Password has been changed");
                    con.Close();
                }
                else
                {
                    MessageBox.Show("The passwords did not match");
                }
            }
            else
            {
                MessageBox.Show("Your current password is incorrect");
            }
        }
        private void changePassword_Click(object sender, EventArgs e)
        {
            passwordCondition();
        }
        public void pullData()
        {
            cmd = new SqlCommand("Select userName From userTB", con);

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                selectUser.Items.Add(dr[0]).ToString();
            }
            dr.Close();
        }
        private void Change_Password_Load(object sender, EventArgs e)
        {
            con.Open();
            pullData();     
        }

        private void changePassword_MouseHover(object sender, EventArgs e)
        {
            changePassword.BackColor = Color.FromArgb(178, 34, 34);
        }

        private void changePassword_MouseLeave(object sender, EventArgs e)
        {
            changePassword.BackColor = Color.FromArgb(51, 51, 76);

        }
    }
}
