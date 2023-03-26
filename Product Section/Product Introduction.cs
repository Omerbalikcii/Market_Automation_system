using Market_Automation.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Market_Automation.Product_Section
{
    public partial class Product_Introduction : Form
    {
        public MainMenu mainmenu;
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        int ID = 0;

        StringFormat strFormat;
        ArrayList arrColumnLefts = new ArrayList();
        ArrayList arrColumnWidths = new ArrayList();
        int iCellHeight = 0;
        int iTotalWidth = 0;
        int iRow = 0;
        bool bFirstPage = false;
        bool bNewPage = false;
        int iHeaderHeight = 0;

        SqlDataReader dr;
        public Product_Introduction()
        {
            InitializeComponent();
        }

        void fillProducts()
        {
            con = new SqlConnection(@"Data source=.; initial catalog=marketDB; integrated Security=True");
            da = new SqlDataAdapter("select productsID as 'ID', productsName as 'Product Name', product_UnitPrice as 'Unit Price', product_KgPrice as 'Kilogram Price', categoryName as 'Category', productUnitCount as 'Unit Total', productKgCount as 'Total Weight (KG)' from productsTB", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "productsTB");
            dataGridView_Products.DataSource = ds.Tables["productsTB"];
            con.Close();
        }

        void fillCategory()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            con = new SqlConnection(@"Data source=.; initial catalog=marketDB; integrated Security=True");
            cmd = new SqlCommand();
            SqlDataReader dr;
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from categoryTB";
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBox1.Items.Add(dr["categoryName"]);
                comboBox2.Items.Add(dr["categoryName"]);
            }
            con.Close();
        }

        private void categoryID()
        {
            con = new SqlConnection(@"Data source=.; initial catalog=marketDB; integrated Security=True");
            con.Open();
            cmd = new SqlCommand("select categoryID from categoryTB where categoryName= '" + comboBox1.Text + "'", con);
            SqlDataReader dr = cmd.ExecuteReader();
            con.Close();
        }

        void Control()
        {
            MainMenu mainmenu = new MainMenu();
        }

        private void Product_Introduction_Load(object sender, EventArgs e)
        {
            //columnsHeader();
            fillProducts();
            fillCategory();
            Control();
            getID();
        }

        /*private void columnsHeader()
        {
            dataGridView_Products.Columns[0].HeaderText = "Product ID";
            dataGridView_Products.Columns[1].HeaderText = "Product Name";
            dataGridView_Products.Columns[2].HeaderText = "Unit Price";
            dataGridView_Products.Columns[3].HeaderText = "Kilogram (kg) Price";
            dataGridView_Products.Columns[4].HeaderText = "Category";
        }*/

        private void insert_productsTB()
        {
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into productsTB (productsName,product_UnitPrice,product_KgPrice,categoryName,productUnitCount,productKgCount) values ('" + productName.Text + "','" + product_UnitPrice.Text + "','" + product_KgPrice.Text + "','" + comboBox1.Text + "','" + txtUnitTotal.Text + "','" + txtKgTotal.Text + "')";
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {      
                insert_productsTB();

                fillProducts();
            }
            catch (Exception error)
            {

                MessageBox.Show("Error:" + error);
            }
        }

        private void deleteRecords_productsTB()
        {
            if (ID != 0)
            {
                cmd = new SqlCommand("delete from productsTB where productsID=@productsID", con);
                con.Open();
                cmd.Parameters.AddWithValue("@productsID", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult option = MessageBox.Show(productName.Text + " " + "Do you want to delete it?", "Information Window", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (option == DialogResult.Yes)
            {
                cmd = new SqlCommand();
                deleteRecords_productsTB();
                fillProducts();
            }
            else if (option == DialogResult.No)
            {
                MessageBox.Show("The deletion operation was not performed", "The Deletion Process", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_Products_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView_Products.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                //categoryID();
                cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "update productsTB set productsName='" + productName.Text + "',product_UnitPrice='" + product_UnitPrice.Text + "',product_KgPrice='" + product_KgPrice.Text + "',categoryName='" + comboBox1.Text + "',productUnitCount='" + txtUnitTotal.Text + "',productKgCount='" + txtKgTotal.Text + "' where productsID='" + comboBoxId.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                fillProducts();
            }
            catch (Exception error)
            {
                MessageBox.Show("Error:" + error);
                con.Close();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillProducts();
            comboBox2.SelectedItem = null;
        }

        private void searchProduct_TextChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data source=.; initial catalog=marketDB; integrated Security=True");
            da = new SqlDataAdapter("Select * from productsTB where productsName like '" + searchProduct.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "productsTB");
            dataGridView_Products.DataSource = ds.Tables["productsTB"];
            con.Close();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            con = new SqlConnection(@"Data source=.; initial catalog=marketDB; integrated Security=True");
            da = new SqlDataAdapter("Select * from categoryTB where categoryName like '" + comboBox2.Text + "%'", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "categoryTB");
            dataGridView_Products.DataSource = ds.Tables["categoryTB"];
            con.Close();
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                printDocument1.OriginAtMargins = true;
                printDocument1.DefaultPageSettings.Margins.Left = 10;
                printDocument1.DefaultPageSettings.Margins.Right = 40;
                printDocument1.DefaultPageSettings.Margins.Top = 40;
                printDocument1.DefaultPageSettings.Margins.Bottom = 0;
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();
                iCellHeight = 0;
                iRow = 0;
                bFirstPage = true;
                bNewPage = true;

                iTotalWidth = 0;
                foreach (DataGridViewColumn GridCol in dataGridView_Products.Columns)
                {
                    iTotalWidth += GridCol.Width;
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                int iLeftMargin = e.MarginBounds.Left;
                int iTopMargin = e.MarginBounds.Top;
                bool bMorePagesToPrint = false;
                int iTmpWidth = 0;
                bFirstPage = true;

                if (bFirstPage)
                {
                    foreach (DataGridViewColumn GridCol in dataGridView_Products.Columns)
                    {
                        iTmpWidth = (int)(Math.Floor((double)((double)GridCol.Width / (double)iTotalWidth * (double)iTotalWidth * ((double)e.MarginBounds.Width / (double)iTotalWidth))));

                        iHeaderHeight = (int)(e.Graphics.MeasureString(GridCol.HeaderText, GridCol.InheritedStyle.Font, iTmpWidth).Height) + 11;

                        arrColumnLefts.Add(iLeftMargin);
                        arrColumnWidths.Add(iTmpWidth);
                        iLeftMargin += iTmpWidth;
                    }
                }

                while (iRow <= dataGridView_Products.Rows.Count - 1)
                {
                    DataGridViewRow GridRow = dataGridView_Products.Rows[iRow];

                    iCellHeight = GridRow.Height + 5;
                    int iCount = 0;

                    if (iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top)
                    {
                        bNewPage = true;
                        bFirstPage = false;
                        bMorePagesToPrint = true;
                        break;
                    }
                    else
                    {
                        if (bNewPage)
                        {
                            e.Graphics.DrawString("Product List", new Font(dataGridView_Products.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top - e.Graphics.MeasureString("Product List", new Font(dataGridView_Products.Font, FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            String strDate = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();

                            e.Graphics.DrawString(strDate, new Font(dataGridView_Products.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(strDate, new Font(dataGridView_Products.Font, FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top - e.Graphics.MeasureString("Product List", new Font(new Font(dataGridView_Products.Font, FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13);

                            iTopMargin = e.MarginBounds.Top;
                            foreach (DataGridViewColumn GridCol in dataGridView_Products.Columns)
                            {
                                e.Graphics.FillRectangle(new SolidBrush(Color.LightGray), new Rectangle((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount], iHeaderHeight));

                                e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font, new SolidBrush(GridCol.InheritedStyle.ForeColor), new RectangleF((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount], iHeaderHeight), strFormat);
                                iCount++;
                            }
                            bNewPage = false;
                            iTopMargin += iHeaderHeight;
                        }
                        iCount = 0;

                        foreach (DataGridViewCell Cel in GridRow.Cells)
                        {
                            if (Cel.Value != null)
                            {
                                e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font, new SolidBrush(Cel.InheritedStyle.ForeColor), new RectangleF((int)arrColumnLefts[iCount], (float)iTopMargin, (int)arrColumnWidths[iCount], (float)iCellHeight), strFormat);
                            }

                            e.Graphics.DrawRectangle(Pens.Black, new Rectangle((int)arrColumnLefts[iCount], iTopMargin, (int)arrColumnWidths[iCount], iCellHeight));

                            iCount++;
                        }
                    }
                    iRow++;
                    iTopMargin += iCellHeight;
                }

                if (bMorePagesToPrint)
                    e.HasMorePages = true;
                else
                    e.HasMorePages = false;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message, "Error:", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void product_UnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57)
            {
                e.Handled = false; //if it's a number, print it.
            }

            else if ((int)e.KeyChar == 44)
            {
                e.Handled = false; //if the key pressed is a comma, print it.
            }
            else if ((int)e.KeyChar == 08)
            {
                e.Handled = false; //if the pressed key is backspace, print it.
            }

            else
            {
                e.Handled = true; //if it's other than these, don't print any of them
            }
        }

        private void product_KgPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57)
            {
                e.Handled = false; //if it's a number, print it.
            }

            else if ((int)e.KeyChar == 44)
            {
                e.Handled = false; //if the key pressed is a comma, print it.
            }
            else if ((int)e.KeyChar == 08)
            {
                e.Handled = false; //if the pressed key is backspace, print it.
            }

            else
            {
                e.Handled = true; //if it's other than these, don't print any of them
            }
        }

        private void dataGridView_Products_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView_Products.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            productName.Clear();
            product_UnitPrice.Clear();
            product_KgPrice.Clear();
            searchProduct.Clear();
            comboBox1.SelectedItem= null;
            txtUnitTotal.Clear();
            txtKgTotal.Clear();
        }

        private void btnDelete_MouseHover(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.FromArgb(255, 99, 71);
        }

        private void btnDelete_MouseLeave(object sender, EventArgs e)
        {
            btnDelete.BackColor = DefaultBackColor;
        }

        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            btnAdd.BackColor = Color.FromArgb(30, 144, 255);
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.BackColor = DefaultBackColor;
        }

        private void btnUpdate_MouseHover(object sender, EventArgs e)
        {
            btnUpdate.BackColor = Color.FromArgb(255, 99, 71);
        }

        private void btnUpdate_MouseLeave(object sender, EventArgs e)
        {
            btnUpdate.BackColor = DefaultBackColor;
        }

        private void btnRefresh_MouseHover(object sender, EventArgs e)
        {
            btnRefresh.BackColor = Color.FromArgb(238, 232, 170);
        }

        private void btnRefresh_MouseLeave(object sender, EventArgs e)
        {
            btnRefresh.BackColor = DefaultBackColor;
        }

        private void btnReport_MouseHover(object sender, EventArgs e)
        {
            btnReport.BackColor = Color.FromArgb(255, 235, 205);
        }

        private void btnReport_MouseLeave(object sender, EventArgs e)
        {
            btnReport.BackColor = DefaultBackColor;
        }

        private void btnClear_MouseHover(object sender, EventArgs e)
        {
            btnClear.BackColor = Color.FromArgb(250, 235, 215);
        }

        private void btnClear_MouseLeave(object sender, EventArgs e)
        {
            btnClear.BackColor = DefaultBackColor;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = printDocument1;
            preview.ShowDialog();
        }
        private void getID()
        {
            SqlConnection con = new SqlConnection(@"Data source=.; initial catalog=marketDB; integrated Security=True");
            con.Open();
            cmd = new SqlCommand("Select productsID From productsTB", con);

            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                comboBoxId.Items.Add(dr[0]).ToString();
            }
            dr.Close();
        }
    }
}

