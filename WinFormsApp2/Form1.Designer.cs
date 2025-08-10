namespace WinFormsApp2
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblCustomerName = new Label();
            txtCustomerName = new TextBox();
            lblCustomerType = new Label();
            cbCustomerType = new ComboBox();
            lblNumberOfPeople = new Label();
            txtNumberOfPeople = new TextBox();
            txtLastMonthMeter = new TextBox();
            lblLastMonthMeter = new Label();
            lblThisMonthMeter = new Label();
            txtThisMonthMeter = new TextBox();
            btnCalculate = new Button();
            btnClear = new Button();
            dgvCustomerList = new DataGridView();
            grpCustomerInfo = new GroupBox();
            lblSearch = new Label();
            txtSearch = new TextBox();
            btnPrintInvoice = new Button();
            btnPrintAllInvoices = new Button();
            btnReset = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCustomerList).BeginInit();
            SuspendLayout();
            // 
            // lblCustomerName
            // 
            lblCustomerName.AutoSize = true;
            lblCustomerName.Location = new Point(20, 30);
            lblCustomerName.Name = "lblCustomerName";
            lblCustomerName.Size = new Size(141, 25);
            lblCustomerName.TabIndex = 0;
            lblCustomerName.Text = "Customer Name";
            // 
            // txtCustomerName
            // 
            txtCustomerName.Location = new Point(180, 25);
            txtCustomerName.Name = "txtCustomerName";
            txtCustomerName.Size = new Size(200, 31);
            txtCustomerName.TabIndex = 1;
            // 
            // lblCustomerType
            // 
            lblCustomerType.AutoSize = true;
            lblCustomerType.Location = new Point(20, 70);
            lblCustomerType.Name = "lblCustomerType";
            lblCustomerType.Size = new Size(131, 25);
            lblCustomerType.TabIndex = 2;
            lblCustomerType.Text = "Customer Type";
            // 
            // cbCustomerType
            // 
            cbCustomerType.Location = new Point(180, 65);
            cbCustomerType.Name = "cbCustomerType";
            cbCustomerType.Size = new Size(200, 33);
            cbCustomerType.TabIndex = 3;
            // 
            // lblNumberOfPeople
            // 
            lblNumberOfPeople.AutoSize = true;
            lblNumberOfPeople.Location = new Point(20, 110);
            lblNumberOfPeople.Name = "lblNumberOfPeople";
            lblNumberOfPeople.Size = new Size(157, 25);
            lblNumberOfPeople.TabIndex = 4;
            lblNumberOfPeople.Text = "Number of People";
            // 
            // txtNumberOfPeople
            // 
            txtNumberOfPeople.Location = new Point(180, 105);
            txtNumberOfPeople.Name = "txtNumberOfPeople";
            txtNumberOfPeople.Size = new Size(200, 31);
            txtNumberOfPeople.TabIndex = 5;
            // 
            // txtLastMonthMeter
            // 
            txtLastMonthMeter.Location = new Point(180, 145);
            txtLastMonthMeter.Name = "txtLastMonthMeter";
            txtLastMonthMeter.Size = new Size(200, 31);
            txtLastMonthMeter.TabIndex = 7;
            // 
            // lblLastMonthMeter
            // 
            lblLastMonthMeter.AutoSize = true;
            lblLastMonthMeter.Location = new Point(20, 150);
            lblLastMonthMeter.Name = "lblLastMonthMeter";
            lblLastMonthMeter.Size = new Size(152, 25);
            lblLastMonthMeter.TabIndex = 6;
            lblLastMonthMeter.Text = "Last Month Meter";
            // 
            // lblThisMonthMeter
            // 
            lblThisMonthMeter.AutoSize = true;
            lblThisMonthMeter.Location = new Point(20, 190);
            lblThisMonthMeter.Name = "lblThisMonthMeter";
            lblThisMonthMeter.Size = new Size(152, 25);
            lblThisMonthMeter.TabIndex = 8;
            lblThisMonthMeter.Text = "This Month Meter";
            // 
            // txtThisMonthMeter
            // 
            txtThisMonthMeter.Location = new Point(180, 185);
            txtThisMonthMeter.Name = "txtThisMonthMeter";
            txtThisMonthMeter.Size = new Size(200, 31);
            txtThisMonthMeter.TabIndex = 9;
            // 
            // btnCalculate
            // 
            btnCalculate.Location = new Point(20, 230);
            btnCalculate.Name = "btnCalculate";
            btnCalculate.Size = new Size(150, 35);
            btnCalculate.TabIndex = 10;
            btnCalculate.Text = "Calculate";
            this.btnCalculate.Click += new System.EventHandler(this.BtnCalculate_Click);
            // 
            // btnClear
            // 
            btnClear.Location = new Point(230, 230);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(150, 35);
            btnClear.TabIndex = 11;
            btnClear.Text = "Clear";
            // 
            // dgvCustomerList
            // 
            dgvCustomerList.ColumnHeadersHeight = 34;
            dgvCustomerList.Location = new Point(420, 25);
            dgvCustomerList.Name = "dgvCustomerList";
            dgvCustomerList.RowHeadersWidth = 62;
            dgvCustomerList.Size = new Size(1110, 300);
            dgvCustomerList.TabIndex = 12;
            // 
            // grpCustomerInfo
            // 
            grpCustomerInfo.Location = new Point(0, 0);
            grpCustomerInfo.Name = "grpCustomerInfo";
            grpCustomerInfo.Size = new Size(200, 100);
            grpCustomerInfo.TabIndex = 0;
            grpCustomerInfo.TabStop = false;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(420, 338);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(64, 25);
            lblSearch.TabIndex = 13;
            lblSearch.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(500, 335);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(406, 31);
            txtSearch.TabIndex = 14;
            // 
            // btnPrintInvoice
            // 
            btnPrintInvoice.Location = new Point(20, 291);
            btnPrintInvoice.Name = "btnPrintInvoice";
            btnPrintInvoice.Size = new Size(150, 34);
            btnPrintInvoice.TabIndex = 15;
            btnPrintInvoice.Text = "Print Selected";
            btnPrintInvoice.UseVisualStyleBackColor = true;
            btnPrintInvoice.Click += BtnPrintInvoice_Click;
            // 
            // btnPrintAllInvoices
            // 
            btnPrintAllInvoices.Location = new Point(230, 291);
            btnPrintAllInvoices.Name = "btnPrintAllInvoices";
            btnPrintAllInvoices.Size = new Size(150, 34);
            btnPrintAllInvoices.TabIndex = 16;
            btnPrintAllInvoices.Text = "Print All";
            btnPrintAllInvoices.UseVisualStyleBackColor = true;
            btnPrintAllInvoices.Click += BtnPrintAllInvoices_Click;
            // 
            // btnReset
            // 
            btnReset.Location = new Point(148, 354);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(112, 34);
            btnReset.TabIndex = 17;
            btnReset.Text = "Reset All";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // 
            // Form1
            // 
            ClientSize = new Size(1544, 400);
            Controls.Add(btnReset);
            Controls.Add(btnPrintAllInvoices);
            Controls.Add(btnPrintInvoice);
            Controls.Add(lblCustomerName);
            Controls.Add(txtCustomerName);
            Controls.Add(lblCustomerType);
            Controls.Add(cbCustomerType);
            Controls.Add(lblNumberOfPeople);
            Controls.Add(txtNumberOfPeople);
            Controls.Add(lblLastMonthMeter);
            Controls.Add(txtLastMonthMeter);
            Controls.Add(lblThisMonthMeter);
            Controls.Add(txtThisMonthMeter);
            Controls.Add(btnCalculate);
            Controls.Add(btnClear);
            Controls.Add(dgvCustomerList);
            Controls.Add(lblSearch);
            Controls.Add(txtSearch);
            Name = "Form1";
            Text = "Water Bill Calculator";
            ((System.ComponentModel.ISupportInitialize)dgvCustomerList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCustomerName;
        private TextBox txtCustomerName;
        private Label lblCustomerType;
        private ComboBox cbCustomerType;
        private Label lblNumberOfPeople;
        private TextBox txtNumberOfPeople;
        private TextBox txtLastMonthMeter;
        private Label lblLastMonthMeter;
        private Label lblThisMonthMeter;
        private TextBox txtThisMonthMeter;
        private Button btnCalculate;
        private Button btnClear;
        private DataGridView dgvCustomerList;
        private GroupBox grpCustomerInfo;
        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnPrintInvoice;
        private Button btnPrintAllInvoices;
        private Button btnReset;
    }
}
