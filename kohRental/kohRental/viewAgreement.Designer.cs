namespace kohRental
{
    partial class viewAgreemnt
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtfname = new System.Windows.Forms.TextBox();
            this.txtlname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbProvince = new System.Windows.Forms.ComboBox();
            this.cmbCity = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbVehicle = new System.Windows.Forms.ComboBox();
            this.lblVin = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpOut = new System.Windows.Forms.DateTimePicker();
            this.dtpIn = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtkmout = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtkmin = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "First Name";
            // 
            // txtfname
            // 
            this.txtfname.Enabled = false;
            this.txtfname.Location = new System.Drawing.Point(75, 24);
            this.txtfname.Name = "txtfname";
            this.txtfname.Size = new System.Drawing.Size(152, 20);
            this.txtfname.TabIndex = 1;
            // 
            // txtlname
            // 
            this.txtlname.Enabled = false;
            this.txtlname.Location = new System.Drawing.Point(75, 50);
            this.txtlname.Name = "txtlname";
            this.txtlname.Size = new System.Drawing.Size(152, 20);
            this.txtlname.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(12, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Last Name";
            // 
            // txtPhone
            // 
            this.txtPhone.Enabled = false;
            this.txtPhone.Location = new System.Drawing.Point(75, 76);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(152, 20);
            this.txtPhone.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Phone";
            // 
            // txtEmail
            // 
            this.txtEmail.Enabled = false;
            this.txtEmail.Location = new System.Drawing.Point(75, 102);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(152, 20);
            this.txtEmail.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Enabled = false;
            this.label4.Location = new System.Drawing.Point(12, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Email";
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Location = new System.Drawing.Point(75, 128);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(152, 20);
            this.txtAddress.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Enabled = false;
            this.label5.Location = new System.Drawing.Point(12, 131);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Address";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(12, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Province";
            // 
            // cmbProvince
            // 
            this.cmbProvince.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProvince.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProvince.Enabled = false;
            this.cmbProvince.FormattingEnabled = true;
            this.cmbProvince.Items.AddRange(new object[] {
            "Alberta",
            "British Columbia",
            "Manitoba",
            "New Brunswick",
            "Newfoundland and Labrador",
            "Northwest Territories",
            "Nova Scotia",
            "Nunavut",
            "Ontario",
            "Prince Edward Island",
            "Québec",
            "Saskatchewan",
            "Yukon"});
            this.cmbProvince.Location = new System.Drawing.Point(75, 154);
            this.cmbProvince.Name = "cmbProvince";
            this.cmbProvince.Size = new System.Drawing.Size(152, 21);
            this.cmbProvince.Sorted = true;
            this.cmbProvince.TabIndex = 12;
            this.cmbProvince.SelectedIndexChanged += new System.EventHandler(this.cmbProvince_SelectedIndexChanged);
            // 
            // cmbCity
            // 
            this.cmbCity.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCity.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCity.Enabled = false;
            this.cmbCity.FormattingEnabled = true;
            this.cmbCity.Location = new System.Drawing.Point(75, 181);
            this.cmbCity.Name = "cmbCity";
            this.cmbCity.Size = new System.Drawing.Size(152, 21);
            this.cmbCity.Sorted = true;
            this.cmbCity.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(12, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "City";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(261, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Vehicle Out";
            // 
            // cmbVehicle
            // 
            this.cmbVehicle.Enabled = false;
            this.cmbVehicle.FormattingEnabled = true;
            this.cmbVehicle.Location = new System.Drawing.Point(329, 24);
            this.cmbVehicle.Name = "cmbVehicle";
            this.cmbVehicle.Size = new System.Drawing.Size(178, 21);
            this.cmbVehicle.TabIndex = 16;
            this.cmbVehicle.SelectedIndexChanged += new System.EventHandler(this.cmbVehicle_SelectedIndexChanged);
            // 
            // lblVin
            // 
            this.lblVin.AutoSize = true;
            this.lblVin.Location = new System.Drawing.Point(513, 27);
            this.lblVin.Name = "lblVin";
            this.lblVin.Size = new System.Drawing.Size(0, 13);
            this.lblVin.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(261, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Date Out";
            // 
            // dtpOut
            // 
            this.dtpOut.Enabled = false;
            this.dtpOut.Location = new System.Drawing.Point(329, 50);
            this.dtpOut.Name = "dtpOut";
            this.dtpOut.Size = new System.Drawing.Size(178, 20);
            this.dtpOut.TabIndex = 19;
            this.dtpOut.Value = new System.DateTime(2018, 10, 28, 17, 32, 20, 0);
            // 
            // dtpIn
            // 
            this.dtpIn.Enabled = false;
            this.dtpIn.Location = new System.Drawing.Point(329, 102);
            this.dtpIn.Name = "dtpIn";
            this.dtpIn.Size = new System.Drawing.Size(178, 20);
            this.dtpIn.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Enabled = false;
            this.label10.Location = new System.Drawing.Point(262, 108);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 20;
            this.label10.Text = "Date In";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(388, 230);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(119, 23);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "Close Rental";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtkmout
            // 
            this.txtkmout.Enabled = false;
            this.txtkmout.Location = new System.Drawing.Point(329, 76);
            this.txtkmout.Name = "txtkmout";
            this.txtkmout.Size = new System.Drawing.Size(176, 20);
            this.txtkmout.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Enabled = false;
            this.label11.Location = new System.Drawing.Point(262, 79);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "KM Out";
            // 
            // txtkmin
            // 
            this.txtkmin.Enabled = false;
            this.txtkmin.Location = new System.Drawing.Point(329, 131);
            this.txtkmin.Name = "txtkmin";
            this.txtkmin.Size = new System.Drawing.Size(176, 20);
            this.txtkmin.TabIndex = 26;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Enabled = false;
            this.label12.Location = new System.Drawing.Point(262, 134);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "KM In";
            // 
            // viewAgreemnt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 263);
            this.Controls.Add(this.txtkmin);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtkmout);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dtpIn);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dtpOut);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblVin);
            this.Controls.Add(this.cmbVehicle);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbCity);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbProvince);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtlname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtfname);
            this.Controls.Add(this.label1);
            this.Name = "viewAgreemnt";
            this.Text = "KOH Rental - View Closed Agreement";
            this.Load += new System.EventHandler(this.newAgreement_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtfname;
        private System.Windows.Forms.TextBox txtlname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbProvince;
        private System.Windows.Forms.ComboBox cmbCity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbVehicle;
        private System.Windows.Forms.Label lblVin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpOut;
        private System.Windows.Forms.DateTimePicker dtpIn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtkmout;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtkmin;
        private System.Windows.Forms.Label label12;
    }
}