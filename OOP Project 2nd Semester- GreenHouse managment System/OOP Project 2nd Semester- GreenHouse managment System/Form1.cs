using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace OOP_Project_2nd_Semester__GreenHouse_managment_System
{
    public partial class Form1 : Form
    {
        private Form3 _form3;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _form3 = new Form3(this);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Thinpad\\Desktop\\GHD.accdb");
            OleDbDataAdapter da = new OleDbDataAdapter("select * from Plants", con);
            con.Open();
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            textBox5.Text = ds.Tables[0].Rows[0][0].ToString();
            textBox1.Text = ds.Tables[0].Rows[0][1].ToString();
            textBox2.Text = ds.Tables[0].Rows[0][2].ToString();
            textBox3.Text = ds.Tables[0].Rows[0][3].ToString();
            textBox4.Text = ds.Tables[0].Rows[0][4].ToString();
            pictureBox1.ImageLocation = ds.Tables[0].Rows[0][13].ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void maintanaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_form3 == null || _form3.IsDisposed)
                _form3 = new Form3(this);

            _form3.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        OleDbConnection con1 = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Thinpad\\Desktop\\GHD.accdb");
        OleDbDataAdapter da = new OleDbDataAdapter("select * from Plants", "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Thinpad\\Desktop\\GHD.accdb");
        DataSet ds = new DataSet();
        int counter = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            da.Fill(ds, "Plants");
            int lastIndex = ds.Tables["Plants"].Rows.Count - 1;
            if (counter < lastIndex)
            {
                counter++;
                textBox5.Text = ds.Tables["Plants"].Rows[counter][0].ToString();
                textBox1.Text = ds.Tables["Plants"].Rows[counter][1].ToString();
                textBox2.Text = ds.Tables["Plants"].Rows[counter][2].ToString();
                textBox3.Text = ds.Tables["Plants"].Rows[counter][3].ToString();
                textBox4.Text = ds.Tables["Plants"].Rows[counter][4].ToString();
                pictureBox1.ImageLocation = ds.Tables["Plants"].Rows[counter][13].ToString();
            }
            else
            {
                MessageBox.Show("Last record reached.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            da.Fill(ds, "Plants");
            if(counter > 0)
            {
                counter--;
                textBox5.Text = ds.Tables["Plants"].Rows[counter][0].ToString();
                textBox1.Text = ds.Tables["Plants"].Rows[counter][1].ToString();
                textBox2.Text = ds.Tables["Plants"].Rows[counter][2].ToString();
                textBox3.Text = ds.Tables["Plants"].Rows[counter][3].ToString();
                textBox4.Text = ds.Tables["Plants"].Rows[counter][4].ToString();
                pictureBox1.ImageLocation = ds.Tables["Plants"].Rows[counter][13].ToString();

            }
            else
            {
                MessageBox.Show("First record");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string plantId = textBox5.Text.Trim();
            if (string.IsNullOrEmpty(plantId))
            {
                MessageBox.Show("Please enter or select a Plant ID to delete.");
                return;
            }

            var confirmResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmResult != DialogResult.Yes)
                return;

            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Thinpad\\Desktop\\GHD.accdb"))
            {
                // Replace [PlantID] with your actual Plant ID column name if different
                string deleteQuery = "DELETE FROM Plants WHERE [PlantID] = ?";
                using (OleDbCommand cmd = new OleDbCommand(deleteQuery, con))
                {
                    cmd.Parameters.AddWithValue("?", plantId);

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record deleted successfully.");
                            // Optionally clear textboxes or refresh the DataGridView here
                        }
                        else
                        {
                            MessageBox.Show("No record found with the specified Plant ID.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4(this);
            f4.Show();
            this.Hide();
        }
    }
}