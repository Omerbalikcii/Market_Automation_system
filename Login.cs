using Market_Automation.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Market_Automation
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        SqlConnection con = new SqlConnection(@"Data source=.;initial catalog=marketDB; integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;

        private void txtUsername_Click(object sender, EventArgs e)
        {
            txtUsername.BackColor = Color.White;
            panel3.BackColor = Color.White;
            panel4.BackColor = SystemColors.Control;
            txtPassword.BackColor = SystemColors.Control;
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {
            txtPassword.BackColor = Color.White;
            panel4.BackColor = Color.White;
            txtUsername.BackColor = SystemColors.Control;
            panel3.BackColor = SystemColors.Control;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        public void pullData()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select distinct userAuthority from userTB ", con);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                authorityCombobox.Items.Add(dr[0]).ToString();
            }
            dr.Close();
            con.Close();
        }
        public void loginActivity(string username, string password)
        {
            try
            {
                Login login = new Login();
                cmd = new SqlCommand("Select userName,userPassword,userAuthority From userTB where userName='" + username + "' and userPassword='" + password + "' and userAuthority='" + authorityCombobox.Text + "'", con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {   
                    MessageBox.Show("You have successfully logged in as " + authorityCombobox.Text, "Informing", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //--------------------------------------------------------
                    string authority_str = authorityCombobox.SelectedItem.ToString();
                    string str = "Cashier";

                    if (authority_str == str)
                    {
                        MainMenu mn = new MainMenu();
                        mn.disableButton();
                        
                        dr.Close();
                        login.Hide();
                        mn.Show();
                    }
                    //---------------------------------------------------------
                    else
                    {
                        dr.Close();
                        login.Hide();
                        MainMenu mm = new MainMenu();
                        mm.Show();
                    }                 
                }
                else
                {
                    panelRight.BackColor = Color.Red;
                    MessageBox.Show("You entered your information incorrectly.", "You Could Not Log in", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    panelRight.BackColor = Color.FromArgb(51, 51, 76);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message);
            }
            con.Close();
            cmd.Dispose();
        }
        
        private void loginbtn_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            loginActivity(username,password);
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                loginActivity(username,password);
            }
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception error)
            {
                MessageBox.Show("Unable to open link that was clicked. " + error.Message);
            }
        }
        private void VisitLink()
        {
            // Change the color of the link text by setting LinkVisited
            linkLabel1.LinkVisited = true;

            //Call the Process.

            //System.Diagnostics.Process.Start("http://www.microsoft.com"); //with this method we go to the link

            //System.Diagnostics.Process.Start("mailto:" + linkLabel1.Text) ;//with this method, the sending e-mail part opens

            System.Diagnostics.Process.Start("mailto:yesirensar@gmail.com");
        }

        private void forgetPasswordbtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ensaryesir.great-site.net");
        }

        private void closebtn_MouseHover(object sender, EventArgs e)
        {
            closebtn.BackColor = Color.Red;
        }

        private void closebtn_MouseLeave(object sender, EventArgs e)
        {
            closebtn.BackColor = Color.FromArgb(51, 51, 76);
        }

        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panelRightTop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Login_Load(object sender, EventArgs e)
        {
            pullData();
        }
    }
}
