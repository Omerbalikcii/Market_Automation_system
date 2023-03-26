using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using System.IO;
using System.Drawing.Printing;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace Market_Automation.Product_Section
{
    public partial class Sale : Form
    {
        SqlConnection con = new SqlConnection(@"Data source=.; initial catalog=marketDB; integrated Security=True");
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        public Sale()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string constr = @"Data source=.; initial catalog=marketDB; integrated Security=True";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("select * from productsTB where productsID = '" + txtBarcode.Text + "'"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            dr.Read();
                            lblBarcode.Text = dr["productsID"].ToString();
                            lblName.Text = dr["productsName"].ToString();
                            txtUnitPrice.Text = dr["product_UnitPrice"].ToString();
                            txtKgPrice.Text = dr["product_KgPrice"].ToString();
                            lblUnitPrice.Text = dr["product_UnitPrice"].ToString();
                            lblKgPrice.Text = dr["product_KgPrice"].ToString();
                            lblCategory.Text = dr["categoryName"].ToString();
                            lblTotalUnit.Text = dr["productUnitCount"].ToString();
                            lblTotalWeight.Text = dr["productKgCount"].ToString();
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("You have chosen a product that we do not have", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lblBarcode.Text = "";
            lblName.Text = "";
            txtUnitPrice.Text = "";
            txtKgPrice.Text = "";
            lblUnitPrice.Text = "";
            lblKgPrice.Text = "";
            lblCategory.Text = "";
            lblTotalUnit.Text = "";
            lblTotalWeight.Text = "";

            txtUnitPrice.Text = "";
            txtKgPrice.Text = "";
            txtBarcode.Text = "";
            unitCount.Text = "0";        
        }

        float count = 0;
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                float Kg = float.Parse(txtKgPrice.Text);
                float Unit = float.Parse(txtUnitPrice.Text);
                float total = (Kg + Unit) * float.Parse(unitCount.Text);

                count = count + total;

                string totalPrice = Convert.ToString(count);
                listReceipt.Items.Add(unitCount.Text + " Unit '" + lblName.Text + "', Kg Price:" + txtKgPrice.Text + ", Unit Price:" + txtUnitPrice.Text + ", Total Price:" + total);
                listBoxCount.Items.Add(unitCount.Text);
                listBoxNameCount.Items.Add(lblName.Text);
                listTotalUnit.Items.Add(lblTotalUnit.Text);
                listTotalWeight.Items.Add(lblTotalWeight.Text);
                lbltotalPrice.Text = (totalPrice);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error: " + error);
                throw;
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            lblBarcode.Text = "";
            lblName.Text = "";
            txtUnitPrice.Text = "";
            txtKgPrice.Text = "";
            lblUnitPrice.Text = "";
            lblKgPrice.Text = "";
            lblCategory.Text = "";
            lblTotalUnit.Text = "";
            lblTotalWeight.Text = "";
            lbltotalPrice.Text = "";

            txtUnitPrice.Text = "";
            txtKgPrice.Text = "";
            txtBarcode.Text = "";
            unitCount.Text = "0";

            listReceipt.Items.Clear();
            count = 0;

            txtMoney.Text = "";
            txtRemainder.Text = "";

            listBoxCount.Items.Clear();
            listBoxNameCount.Items.Clear();
            listTotalUnit.Items.Clear();
            listTotalWeight.Items.Clear();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.PageUnit = GraphicsUnit.Millimeter;
            int leading = 5;
            int leftMargin = 150;
            int topMargin = 10;

            // a few simple formatting options..
            StringFormat FmtRight = new StringFormat() { Alignment = StringAlignment.Far };
            StringFormat FmtLeft = new StringFormat() { Alignment = StringAlignment.Near };
            StringFormat FmtCenter = new StringFormat() { Alignment = StringAlignment.Near };

            StringFormat fmt = FmtRight;

            using (Font font = new Font("Arial Narrow", 12f))
            {
                SizeF sz = e.Graphics.MeasureString("_|", Font);
                float h = sz.Height + leading;

                for (int i = 0; i < listReceipt.Items.Count; i++)
                    e.Graphics.DrawString(listReceipt.Items[i].ToString(), font, Brushes.Black, leftMargin, topMargin + h * i, fmt);
            }
        }

        private void btnWater_Click(object sender, EventArgs e)
        {
            txtBarcode.Text = "16";
            btnSearch_Click(sender, e);
        }

        private void btnPochette_Click(object sender, EventArgs e)
        {
            txtBarcode.Text = "17";
            btnSearch_Click(sender, e);
        }

        private void btnBread_Click(object sender, EventArgs e)
        {
            txtBarcode.Text = "18";
            btnSearch_Click(sender, e);
        }

        private void btnSell_Click(object sender, EventArgs e)
        {
            try
            {
                double remainder = double.Parse(txtRemainder.Text);
                if (remainder < 0 || remainder.ToString() == "")
                {
                    MessageBox.Show("The balance is insufficient", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    SqlConnection con = new SqlConnection(@"Data source=.; initial catalog=marketDB; integrated Security=True");
                    con.Open();


                    for (int i = 0; i < listReceipt.Items.Count; i++)
                    {
                        listReceipt.Items[i].ToString();

                        if (listTotalUnit.Items[i].ToString() == "0" && listTotalWeight.Items[i].ToString() != "0")
                        {
                            double doubleTotal1 = double.Parse(lblTotalWeight.Text);
                            double doubleTotal2 = double.Parse(listBoxCount.Items[i].ToString());
                            double doubleTotal3 = doubleTotal1 - doubleTotal2;

                            if (doubleTotal3 > 0)
                            {
                                SqlCommand cmd1 = new SqlCommand("update productsTB SET productKgCount = '" + doubleTotal3 + "' where productsName = '" + listBoxNameCount.Items[i].ToString() + "'", con);
                                cmd1.ExecuteNonQuery();
                            }
                            else
                            {
                                MessageBox.Show("There are not enough of this product in stock (KG)");
                            }

                        }

                        else if (listTotalUnit.Items[i].ToString() != "0" && listTotalWeight.Items[i].ToString() == "0")
                        {
                            int intTotal1 = int.Parse(lblTotalUnit.Text);
                            int intTotal2 = int.Parse(listBoxCount.Items[i].ToString());
                            int intTotal3 = intTotal1 - intTotal2;

                            if (intTotal3 > 0)
                            {
                                SqlCommand cmd2 = new SqlCommand("update productsTB SET productUnitCount = '" + intTotal3 + "' where productsName = '" + listBoxNameCount.Items[i].ToString() + "'", con);
                                cmd2.ExecuteNonQuery();
                            }
                            else
                            {
                                MessageBox.Show("There are not enough of this product in stock (Unit)");
                            }
                        }

                        else if (listTotalUnit.Items[i].ToString() == "0" && listTotalWeight.Items[i].ToString() == "0")
                        {
                            MessageBox.Show("The product is not available in our stocks");
                        }
                    }

                    PrintPreviewDialog ppd = new PrintPreviewDialog();
                    ppd.Document = printDocument1;
                    ppd.Document.DocumentName = "TESTING";
                    printDocument1.PrintPage += printDocument1_PrintPage;
                    ppd.ShowDialog();

                    btnCancel_Click(sender, e);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error:" + error);
            }
        }
        

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            try
            {
                double sum_of_money = Convert.ToDouble(txtMoney.Text);
                double total_price = Convert.ToDouble(lbltotalPrice.Text);
                if (sum_of_money > total_price)
                {
                    double remainder = sum_of_money - total_price;
                    txtRemainder.Text = remainder.ToString();
                }
                else
                {
                    MessageBox.Show("The balance is insufficient", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Error:" + error);
            }
        }

        private void Sale_Load(object sender, EventArgs e)
        {
            listBoxCount.Visible = false;
            listBoxNameCount.Visible = false;
            listTotalUnit.Visible = false;
            listTotalWeight.Visible = false;
        }
    }
}
