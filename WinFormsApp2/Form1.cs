using System;
using System.Data;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        // Initialize the data table and binding source for displaying in DataGridView
        private DataTable dt = new();
        private BindingSource bindingSource = new();

        public Form1()
        {
            InitializeComponent();

            // Clear any existing items in the customer type combobox
            cbCustomerType.Items.Clear();

            // Add customer types to the combobox
            cbCustomerType.Items.AddRange(new object[]
{
    "Household",
    "Business",
    "Government",
    "Other"
});

            cbCustomerType.SelectedIndex = 0; // Select the first type by default

            SetupDataTable(); // Create the data table structure

            bindingSource.DataSource = dt; // Bind DataTable to BindingSource
            dgvCustomerList.DataSource = bindingSource; // Bind to DataGridView

            // Attach event handlers for buttons and input fields
            btnCalculate.Click += BtnCalculate_Click;
            btnClear.Click += BtnClear_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            btnPrintInvoice.Click += BtnPrintInvoice_Click;
            btnPrintAllInvoices.Click += BtnPrintAllInvoices_Click;
            btnReset.Click += btnReset_Click;
        }

        // Create the data table structure with corresponding columns
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

        // Handle the "Calculate" button click
        private void BtnCalculate_Click(object? sender, EventArgs e)
        {
            try
            {
                // Get and validate customer name
                string customerName = txtCustomerName.Text.Trim();
                if (string.IsNullOrWhiteSpace(customerName))
                {
                    MessageBox.Show("Please enter customer name!");
                    return;
                }

                // Check if a customer type is selected
                if (cbCustomerType.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cbCustomerType.Text))
                {
                    MessageBox.Show("Please select a customer type!");
                    return;
                }

                // Get previous and current month meter readings
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

                // For household type, number of people is required
                if (customerType == "Household")
                {
                    if (!int.TryParse(txtNumberOfPeople.Text.Trim(), out numberOfPeople) || numberOfPeople <= 0)
                    {
                        MessageBox.Show("Please enter a valid number of people (greater than 0)!");
                        return;
                    }
                }

                // Calculate water consumption
                decimal consumption = thisMonth - lastMonth;
                decimal total = 0;

                if (customerType == "Household")
                {
                    // Apply tiered pricing based on number of people
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
                        // Usage above 30 m³/person
                        total += remaining * 17521.9m;
                    }
                }
                else
                {
                    // Base price for other customer types
                    decimal basePrice = customerType switch
                    {
                        "Business" => 22068m,
                        "Government" => 11615m,
                        "Other" => 11615m,
                        _ => 11615m
                    };

                    // Add 10% environmental protection fee
                    decimal unitPriceWithEnv = basePrice * 1.10m;
                    total = consumption * unitPriceWithEnv;
                }

                // Add 10% VAT
                total *= 1.10m;

                // Check for duplicate customer name in the list
                foreach (DataRow row in dt.Rows)
                {
                    if (row["Customer Name"].ToString() == customerName)
                    {
                        MessageBox.Show("Customer already exists. Please enter a different name!");
                        return;
                    }
                }

                // Add new row to the table
                dt.Rows.Add(customerName, customerType, numberOfPeople, lastMonth, thisMonth, consumption, total);
                MessageBox.Show("Calculation successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Clear all input fields on the form
        private void BtnClear_Click(object? sender, EventArgs e)
        {
            txtCustomerName.Clear();
            txtNumberOfPeople.Clear();
            txtLastMonthMeter.Clear();
            txtThisMonthMeter.Clear();
            cbCustomerType.SelectedIndex = -1;
        }

        // Search for customer by name
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

        // Clear all data and reset to default
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

        // Print invoice for the currently selected customer row in the table
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

        // Print invoices for all customers in the list
        private void BtnPrintAllInvoices_Click(object? sender, EventArgs e)
        {
            foreach (DataRow row in dt.Rows)
            {
                PrintRow(row);
            }
        }

        // Print one row of data (one invoice)
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
