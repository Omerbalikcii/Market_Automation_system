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
using Market_Automation.Settings;
namespace Market_Automation.Settings
{
    public partial class List_Users : Form
    {
        SqlConnection con = new SqlConnection(@"Data source=.;initial catalog=marketDB; integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        int ID = 0;

        public List_Users()
        {
            InitializeComponent();
        }

        private void List_Users_Load(object sender, EventArgs e)
        {
            getRecords();
            columnsHeader();  
        }
        private void columnsHeader()
        {
            listUsers.Columns[0].HeaderText = "User ID";
            listUsers.Columns[1].HeaderText = "User Name";
            listUsers.Columns[2].HeaderText = "Password";
            listUsers.Columns[3].HeaderText = "Authority";
            listUsers.Columns[4].HeaderText = "User Information ID";
            listUsers.Columns[5].HeaderText = "User ID";
            listUsers.Columns[6].HeaderText = "Identity";
            listUsers.Columns[7].HeaderText = "Real Name";
            listUsers.Columns[8].HeaderText = "Surname";
            listUsers.Columns[9].HeaderText = "Birthdate";
            listUsers.Columns[10].HeaderText = "Mother Name";
            listUsers.Columns[11].HeaderText = "Father Name";
            listUsers.Columns[12].HeaderText = "Phone Number";
            listUsers.Columns[13].HeaderText = "Email";
            listUsers.Columns[14].HeaderText = "City";
            listUsers.Columns[15].HeaderText = "Gender";
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query = "SELECT * from userInformationTB where userInformationID= @userInformationID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@userInformationID", txtID.Text);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                listUsers.DataSource = dt;
                con.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error.Message);
            }
        }
        private void getRecords()
        {
            con.Open();
            string query = "SELECT * FROM userTB u FULL JOIN userInformationTB ui ON u.userID = ui.userInformationID";
        
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            da.Fill(dt); // create a datatable and write the data into it with dataadapter
            listUsers.DataSource = dt;
            con.Close();
        }

        private void deleteRecords_userInformationTB()
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete from userInformationTB where userInformationID=@userInformationID", con);
                con.Open();
                cmd.Parameters.AddWithValue("@userInformationID", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                getRecords();
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }
        private void deleteRecords_userTB()
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete from userTB where userID=@userID", con);
                con.Open();
                cmd.Parameters.AddWithValue("@userID", ID);
                cmd.ExecuteNonQuery();
                con.Close();

                getRecords();
            }
            else
            {

            }
        }
        private void deleteAccount_btn_Click(object sender, EventArgs e)
        {
            deleteRecords_userTB();
            deleteRecords_userInformationTB();
        }
        private void listMembers_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(listUsers.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void listUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            listUsers.ForeColor = Color.FromArgb(51, 51, 76);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            getRecords();
          
        }

        private void btnSearch_MouseHover(object sender, EventArgs e)
        {
            btnSearch.BackColor = Color.FromArgb(0, 191, 255);
        }

        private void btnSearch_MouseLeave(object sender, EventArgs e)
        {
            btnSearch.BackColor = Color.FromArgb(51, 51, 76);
        }

        private void btnRefresh_MouseHover(object sender, EventArgs e)
        {
            btnRefresh.BackColor = Color.FromArgb(0, 191, 255);
        }

        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            btnRefresh.BackColor = Color.FromArgb(51, 51, 76);
        }

        private void deleteAccount_btn_MouseHover(object sender, EventArgs e)
        {
            deleteAccount_btn.BackColor = Color.FromArgb(178, 34, 34);
        }

        private void deleteAccount_btn_MouseLeave(object sender, EventArgs e)
        {
            deleteAccount_btn.BackColor = Color.FromArgb(51, 51, 76);
        }
    }
}
