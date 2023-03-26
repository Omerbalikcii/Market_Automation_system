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
    public partial class FormProduct : Form
    {
        MainMenu mainobj = new MainMenu();

        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        public FormProduct()
        {

            InitializeComponent();
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
        private void FormProduct_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTheme();
                
            }
            catch (Exception error )
            {
                MessageBox.Show("Error: " + error.Message);
            }
        }

        private void btn_Product_Introduction_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Product_Section.Product_Introduction(), sender);
            
        }

        private void btn_Sale_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Product_Section.Sale(), sender);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            FormProduct Setobj = new FormProduct();
            MainMenu Mainobj = new MainMenu();
            ActiveForm.Close();

            Mainobj.Show();
        }
    }
}
