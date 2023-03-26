using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Market_Automation.Forms;

namespace Market_Automation.Forms
{

    public partial class FormSettings : Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;


        public FormSettings()
        {
            InitializeComponent();
            random = new Random();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btns.BackColor = ThemeColor.PrimaryColor;
                    btns.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }
        }
        private void FormSettings_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            { activeForm.Close(); }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPanel.Controls.Add(childForm);
            this.panelDesktopPanel.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }
        public void getOpenChildForm(object sender,EventArgs e)
        {
            OpenChildForm(new Forms.FormSettings(), sender);
        }
        private void btn_listUsers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Settings.List_Users(), sender);
        }

        private void btn_createAccount_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Settings.Create_an_Account(), sender);
        }

        private void btn_changePassword_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Settings.Change_Password(), sender);
        }
    }
}
