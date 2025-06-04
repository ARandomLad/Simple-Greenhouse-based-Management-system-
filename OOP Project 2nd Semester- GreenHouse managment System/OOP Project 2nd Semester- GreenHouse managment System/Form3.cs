using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OOP_Project_2nd_Semester__GreenHouse_managment_System
{
    public partial class Form3 : Form
    {
        private Form1 _form1;

        public Form3(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _form1.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string plantId = textBox1.Text.Trim(); // Get Plant ID from input

            if (string.IsNullOrEmpty(plantId))
            {
                MessageBox.Show("Please enter a Plant ID.");
                return;
            }

            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Thinpad\\Desktop\\GHD.accdb"))
            {
                // Adjust the column name [PlantID] to match your actual column name
                string query = "SELECT * FROM Plants WHERE [PlantID] = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("?", plantId);

                    try
                    {
                        con.Open();
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Example: Fill other textboxes with data from the found record
                                // Adjust indexes or column names as needed
                                textBox2.Text = reader["Common Name"].ToString();
                                textBox3.Text = reader["Planting Date"].ToString();
                                textBox4.Text = reader["Exp Harvesting Date"].ToString();
                                textBox5.Text = reader["Plot"].ToString();
                                textBox6.Text = reader["WaterReq"].ToString();
                                textBox7.Text = reader["TempN"].ToString();
                                textBox8.Text = reader["TempD"].ToString();
                                textBox9.Text = reader["LightReq"].ToString();
                                textBox10.Text = reader["SoilpH"].ToString();
                                textBox11.Text = reader["Notes"].ToString();
                                // If you store image path:
                                pictureBox1.ImageLocation = reader["Img"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No record found with that Plant ID.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
