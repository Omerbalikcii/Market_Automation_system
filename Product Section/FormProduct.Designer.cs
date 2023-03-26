
namespace Market_Automation.Forms
{
    partial class FormProduct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProduct));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btn_Sale = new System.Windows.Forms.Button();
            this.btn_Product_Introduction = new System.Windows.Forms.Button();
            this.panelDesktopPanel = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panelMenu.Controls.Add(this.btn_Sale);
            this.panelMenu.Controls.Add(this.btn_Product_Introduction);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 839);
            this.panelMenu.TabIndex = 14;
            // 
            // btn_Sale
            // 
            this.btn_Sale.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Sale.FlatAppearance.BorderSize = 0;
            this.btn_Sale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Sale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_Sale.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_Sale.Image = ((System.Drawing.Image)(resources.GetObject("btn_Sale.Image")));
            this.btn_Sale.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Sale.Location = new System.Drawing.Point(0, 60);
            this.btn_Sale.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Sale.Name = "btn_Sale";
            this.btn_Sale.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btn_Sale.Size = new System.Drawing.Size(220, 60);
            this.btn_Sale.TabIndex = 1;
            this.btn_Sale.Text = "Sale";
            this.btn_Sale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Sale.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Sale.UseVisualStyleBackColor = true;
            this.btn_Sale.Click += new System.EventHandler(this.btn_Sale_Click);
            // 
            // btn_Product_Introduction
            // 
            this.btn_Product_Introduction.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Product_Introduction.FlatAppearance.BorderSize = 0;
            this.btn_Product_Introduction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Product_Introduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_Product_Introduction.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_Product_Introduction.Image = ((System.Drawing.Image)(resources.GetObject("btn_Product_Introduction.Image")));
            this.btn_Product_Introduction.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Product_Introduction.Location = new System.Drawing.Point(0, 0);
            this.btn_Product_Introduction.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Product_Introduction.Name = "btn_Product_Introduction";
            this.btn_Product_Introduction.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btn_Product_Introduction.Size = new System.Drawing.Size(220, 60);
            this.btn_Product_Introduction.TabIndex = 0;
            this.btn_Product_Introduction.Text = "Product Introduction";
            this.btn_Product_Introduction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Product_Introduction.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_Product_Introduction.UseVisualStyleBackColor = true;
            this.btn_Product_Introduction.Click += new System.EventHandler(this.btn_Product_Introduction_Click);
            // 
            // panelDesktopPanel
            // 
            this.panelDesktopPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktopPanel.Location = new System.Drawing.Point(220, 0);
            this.panelDesktopPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelDesktopPanel.Name = "panelDesktopPanel";
            this.panelDesktopPanel.Size = new System.Drawing.Size(1239, 839);
            this.panelDesktopPanel.TabIndex = 15;
            // 
            // FormProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 839);
            this.Controls.Add(this.panelDesktopPanel);
            this.Controls.Add(this.panelMenu);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormProduct";
            this.Text = "FormProduct";
            this.Load += new System.EventHandler(this.FormProduct_Load);
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btn_Sale;
        private System.Windows.Forms.Button btn_Product_Introduction;
        private System.Windows.Forms.Panel panelDesktopPanel;
    }
}