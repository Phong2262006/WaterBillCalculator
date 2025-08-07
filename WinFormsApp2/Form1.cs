
using System;
using System.Data;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        // Khởi tạo bảng dữ liệu và binding source để hiển thị lên DataGridView
        private DataTable dt = new();
        private BindingSource bindingSource = new();

        public Form1()
        {
            InitializeComponent();

            // Xóa các mục có sẵn trong combobox loại khách hàng (nếu có)
            cbCustomerType.Items.Clear();

            // Thêm các loại khách hàng vào combobox
            cbCustomerType.Items.AddRange(new object[]
{
    "Household",
    "Business",
    "Government",
    "Other"
});

            cbCustomerType.SelectedIndex = 0; // Chọn mặc định loại đầu tiên

            SetupDataTable(); // Tạo cấu trúc bảng dữ liệu

            bindingSource.DataSource = dt; // Gắn DataTable vào BindingSource
            dgvCustomerList.DataSource = bindingSource; // Gắn vào DataGridView

            // Gắn các sự kiện cho nút và ô nhập liệu
            btnCalculate.Click += BtnCalculate_Click;
            btnClear.Click += BtnClear_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            btnPrintInvoice.Click += BtnPrintInvoice_Click;
            btnPrintAllInvoices.Click += BtnPrintAllInvoices_Click;
            btnReset.Click += btnReset_Click;
        }

        // Tạo cấu trúc bảng dữ liệu với các cột tương ứng
        private void SetupDataTable()
        {
            dt.Columns.Add("Customer Name", typeof(string));
            dt.Columns.Add("Customer Type", typeof(string));
            dt.Columns.Add("Number of People", typeof(int));
            dt.Columns.Add("Previous Month Meter", typeof(decimal));
            dt.Columns.Add("Current Month Meter", typeof(decimal));
            dt.Columns.Add("Consumption", typeof(decimal));
            dt.Columns.Add("Total Amount", typeof(decimal));
        }

        // Xử lý khi nhấn nút "Calculate"
        private void BtnCalculate_Click(object? sender, EventArgs e)
        {
            try
            {
                // Lấy và kiểm tra tên khách hàng
                string customerName = txtCustomerName.Text.Trim();
                if (string.IsNullOrWhiteSpace(customerName))
                {
                    MessageBox.Show("Please enter customer name!");
                    return;
                }

                // Kiểm tra loại khách hàng đã chọn chưa
                if (cbCustomerType.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cbCustomerType.Text))
                {
                    MessageBox.Show("Please select a customer type!");
                    return;
                }

                // Lấy chỉ số đồng hồ tháng trước và tháng này
                if (!decimal.TryParse(txtLastMonthMeter.Text.Trim(), out decimal lastMonth))
                {
                    MessageBox.Show("Please enter a valid previous month meter reading!");
                    return;
                }

                if (!decimal.TryParse(txtThisMonthMeter.Text.Trim(), out decimal thisMonth))
                {
                    MessageBox.Show("Please enter a valid current month meter reading!");
                    return;
                }

                if (thisMonth < lastMonth)
                {
                    MessageBox.Show("Current month meter must be greater than or equal to previous month!");
                    return;
                }

                int numberOfPeople = 0;
                string customerType = cbCustomerType.SelectedItem?.ToString() ?? "";

                // Nếu là hộ gia đình, bắt buộc nhập số người
                if (customerType == "Household")
                {
                    if (!int.TryParse(txtNumberOfPeople.Text.Trim(), out numberOfPeople) || numberOfPeople <= 0)
                    {
                        MessageBox.Show("Please enter a valid number of people (greater than 0)!");
                        return;
                    }
                }

                // Tính lượng nước tiêu thụ
                decimal consumption = thisMonth - lastMonth;
                decimal total = 0;

                if (customerType == "Household")
                {
                    // Áp dụng bảng giá bậc thang theo số người
                    decimal remaining = consumption;

                    decimal tier1Limit = 10 * numberOfPeople;
                    decimal tier1Usage = Math.Min(remaining, tier1Limit);
                    total += tier1Usage * 6570.3m;
                    remaining -= tier1Usage;

                    if (remaining > 0)
                    {
                        decimal tier2Limit = 10 * numberOfPeople;
                        decimal tier2Usage = Math.Min(remaining, tier2Limit);
                        total += tier2Usage * 7757.2m;
                        remaining -= tier2Usage;
                    }

                    if (remaining > 0)
                    {
                        decimal tier3Limit = 10 * numberOfPeople;
                        decimal tier3Usage = Math.Min(remaining, tier3Limit);
                        total += tier3Usage * 9568.9m;
                        remaining -= tier3Usage;
                    }

                    if (remaining > 0)
                    {
                        // Mức trên 30 m³/người
                        total += remaining * 17521.9m;
                    }
                }
                else
                {
                    // Đơn giá cơ bản của các loại khách hàng khác
                    decimal basePrice = customerType switch
                    {
                        "Business" => 22068m,
                        "Government" => 11615m,
                        "Other" => 11615m,
                        _ => 11615m
                    };

                    // Cộng thêm phí bảo vệ môi trường 10%
                    decimal unitPriceWithEnv = basePrice * 1.10m;
                    total = consumption * unitPriceWithEnv;
                }

                // Cộng thêm VAT 10%
                total *= 1.10m;

                // Kiểm tra trùng tên khách hàng trong danh sách
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Customer Name"].ToString() == customerName)
                    {
                        MessageBox.Show("Customer already exists. Please enter a different name!");
                        return;
                    }
                }

                // Thêm dữ liệu vào bảng
                dt.Rows.Add(customerName, customerType, numberOfPeople, lastMonth, thisMonth, consumption, total);
                MessageBox.Show("Calculation successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Xóa dữ liệu nhập liệu trên form
        private void BtnClear_Click(object? sender, EventArgs e)
        {
            txtCustomerName.Clear();
            txtNumberOfPeople.Clear();
            txtLastMonthMeter.Clear();
            txtThisMonthMeter.Clear();
            cbCustomerType.SelectedIndex = -1;
        }

        // Tìm kiếm khách hàng theo tên
        private void TxtSearch_TextChanged(object? sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrWhiteSpace(keyword))
            {
                bindingSource.RemoveFilter();
            }
            else
            {
                bindingSource.Filter = $"[Customer Name] LIKE '%{keyword}%'";
            }
        }

        // Xóa toàn bộ dữ liệu và đặt lại mặc định
        private void btnReset_Click(object? sender, EventArgs e)
        {
            txtCustomerName.Text = "";
            cbCustomerType.SelectedIndex = 0;
            txtNumberOfPeople.Text = "";
            txtLastMonthMeter.Text = "";
            txtThisMonthMeter.Text = "";
            txtSearch.Text = "";
            dt.Clear();
            bindingSource.ResetBindings(false);
        }

        // In hóa đơn cho khách hàng đang chọn trên bảng
        private void BtnPrintInvoice_Click(object? sender, EventArgs e)
        {
            if (dgvCustomerList.CurrentRow != null && dgvCustomerList.CurrentRow.Index >= 0)
            {
                int index = dgvCustomerList.CurrentRow.Index;
                if (index < dt.Rows.Count)
                {
                    PrintRow(dt.Rows[index]);
                }
            }
            else
            {
                MessageBox.Show("Please select a customer row to print!");
            }
        }

        // In hóa đơn cho toàn bộ khách hàng trong danh sách
        private void BtnPrintAllInvoices_Click(object? sender, EventArgs e)
        {
            foreach (DataRow row in dt.Rows)
            {
                PrintRow(row);
            }
        }

        // Hàm in 1 dòng dữ liệu (1 hóa đơn)
        private void PrintRow(DataRow row)
        {
            string invoice = $"--- INVOICE ---\n" +
                             $"Customer Name: {row["Customer Name"]}\n" +
                             $"Customer Type: {row["Customer Type"]}\n" +
                             $"Number of People: {row["Number of People"]}\n" +
                             $"Previous Meter: {row["Previous Month Meter"]}\n" +
                             $"Current Meter: {row["Current Month Meter"]}\n" +
                             $"Consumption: {row["Consumption"]}\n" +
                             $"Total Amount: {((decimal)row["Total Amount"]):N0} VND";

            MessageBox.Show(invoice, "Invoice");
        }
    }
}
