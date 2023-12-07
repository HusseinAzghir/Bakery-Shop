using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace Bakery_Software
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = Color.Green;
            radioButton2.ForeColor = Color.Red;

            cmb_items.Items.Clear();
            cmb_items.Items.Add("Sweets Artikel 1");
            cmb_items.Items.Add("Sweets Artikel 2");
            cmb_items.Items.Add("Sweets Artikel 3");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.ForeColor = Color.Red;
            radioButton2.ForeColor = Color.Green;

            cmb_items.Items.Clear();
            cmb_items.Items.Add("Bakery Artikel 1");
            cmb_items.Items.Add("Bakery Artikel 2");
            cmb_items.Items.Add("Bakery Artikel 3");
        }

        private void cmb_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_items.SelectedItem.ToString())
            {
                case "Sweets Artikel 1":
                    txt_price.Text = "50";
                    break;
                case "Sweets Artikel 2":
                    txt_price.Text = "100";
                    break;
                case "Sweets Artikel 3":
                    txt_price.Text = "150";
                    break;
                case "Bakery Artikel 1":
                    txt_price.Text = "200";
                    break;
                case "Bakery Artikel 2":
                    txt_price.Text = "250";
                    break;
                case "Bakery Artikel 3":
                    txt_price.Text = "300";
                    break;
                default:
                    txt_price.Text = "0";
                    break;
            }

            txt_total.Text = "";
            txt_qty.Text = "";
        }

        private void txt_qty_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txt_qty.Text, out int quantity) && int.TryParse(txt_price.Text, out int price))
            {
                txt_total.Text = (quantity * price).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txt_total.Text, out int total))
            {
                string[] arr = { cmb_items.SelectedItem.ToString(), txt_price.Text, txt_qty.Text, total.ToString() };
                ListViewItem lvi = new ListViewItem(arr);
                listView1.Items.Add(lvi);

                txt_sub.Text = (Convert.ToInt16(txt_sub.Text) + total).ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    txt_sub.Text = (Convert.ToInt16(txt_sub.Text) - Convert.ToInt16(item.SubItems[3].Text)).ToString();
                    item.Remove();
                }
            }
        }

        private void txt_discount_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txt_discount.Text, out int discount))
            {
                txt_net.Text = (Convert.ToInt16(txt_sub.Text) - discount).ToString();
            }
        }

        private void txt_paid_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(txt_paid.Text, out int paid))
            {
                txt_balance.Text = (Convert.ToInt16(txt_net.Text) - paid).ToString();
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                try
                {
                    string ConnectionString = "Server=localhost;Database=AB_Inventory_DB;User=root;Password=;";
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        


                    using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "INSERT INTO Test_Invoice_Master (InvoiceDate, Sub_Total, Discount, Net_Amount, Paid_Amount) VALUES " +
                                                  "(GETDATE(), @SubTotal, @Discount, @NetAmount, @PaidAmount); SELECT SCOPE_IDENTITY();";

                            command.Parameters.AddWithValue("@SubTotal", txt_sub.Text);
                            command.Parameters.AddWithValue("@Discount", txt_discount.Text);
                            command.Parameters.AddWithValue("@NetAmount", txt_net.Text);
                            command.Parameters.AddWithValue("@PaidAmount", txt_paid.Text);

                            string invoiceID = command.ExecuteScalar().ToString();

                            foreach (ListViewItem listItem in listView1.Items)
                            {
                                command.CommandText = "INSERT INTO Test_Invoice_Detail (MasterID, ItemName, ItemPrice, ItemQtty, ItemTotal) VALUES " +
                                                      "(@InvoiceID, @ItemName, @ItemPrice, @ItemQtty, @ItemTotal)";

                                command.Parameters.Clear();
                                command.Parameters.AddWithValue("@InvoiceID", invoiceID);
                                command.Parameters.AddWithValue("@ItemName", listItem.SubItems[0].Text);
                                command.Parameters.AddWithValue("@ItemPrice", listItem.SubItems[1].Text);
                                command.Parameters.AddWithValue("@ItemQtty", listItem.SubItems[2].Text);
                                command.Parameters.AddWithValue("@ItemTotal", listItem.SubItems[3].Text);

                                command.ExecuteNonQuery();
                            }

                            MessageBox.Show("Verkauf erfolgreich erstellt, mit Rechnungsnummer # " + invoiceID);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Verkauf nicht erstellt. Fehler: " + ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Sie müssen mindestens einen Artikel zur Liste hinzufügen.");
            }
        }
    }
}
