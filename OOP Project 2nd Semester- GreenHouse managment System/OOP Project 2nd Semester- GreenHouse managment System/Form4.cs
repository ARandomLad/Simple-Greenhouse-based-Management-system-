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
    public partial class Form4 : Form
    {
        private Form3 _form3;
        private string selectedImagePath = "";
        private Form1 _form1;
        public Form4(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            using (OleDbConnection con2 = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Thinpad\\Desktop\\GHD.accdb"))
            {
                string insertQuery = "INSERT INTO Plants ([Common Name], [Scientific Name], [Family], [Genus], [Planting Date], [Exp Harvesting Date], [Plot], [WaterReq], [TempN], [TempD], [LightReq], [SoilpH], [Notes], [Img]) " +
                                     "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                using (OleDbCommand cmd = new OleDbCommand(insertQuery, con2))
                {
                    cmd.Parameters.AddWithValue("?", textBox1.Text);  // Common Name
                    cmd.Parameters.AddWithValue("?", textBox2.Text);  // Scientific Name
                    cmd.Parameters.AddWithValue("?", textBox3.Text);  // Family
                    cmd.Parameters.AddWithValue("?", textBox4.Text);  // Genus
                    cmd.Parameters.AddWithValue("?", textBox5.Text);  // Planting Date
                    cmd.Parameters.AddWithValue("?", textBox6.Text);  // Exp Harvesting Date
                    cmd.Parameters.AddWithValue("?", textBox7.Text);  // Plot
                    cmd.Parameters.AddWithValue("?", textBox8.Text);  // WaterReq
                    cmd.Parameters.AddWithValue("?", textBox9.Text);  // TempN
                    cmd.Parameters.AddWithValue("?", textBox10.Text); // TempD
                    cmd.Parameters.AddWithValue("?", textBox11.Text); // LightReq
                    cmd.Parameters.AddWithValue("?", textBox12.Text); // SoilpH
                    cmd.Parameters.AddWithValue("?", textBox13.Text); // Notes
                    cmd.Parameters.AddWithValue("?", selectedImagePath); // ImagePath

                    try
                    {
                        con2.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Plant added successfully!");
                            // Clear textboxes and image after successful insert
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            textBox5.Clear();
                            textBox6.Clear();
                            textBox7.Clear();
                            textBox8.Clear();
                            textBox9.Clear();
                            textBox10.Clear();
                            textBox11.Clear();
                            textBox12.Clear();
                            textBox13.Clear();
                            pictureBox1.Image = null;
                            selectedImagePath = "";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                openFileDialog.Title = "Select an Image";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;
                    pictureBox1.ImageLocation = selectedImagePath;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void detailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _form1.Show();
            this.Hide();
        }

        private void maintenanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_form3 == null || _form3.IsDisposed)
                _form3 = new Form3(_form1);

            _form3.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string plantId = textBox14.Text.Trim();
            if (string.IsNullOrEmpty(plantId))
            {
                MessageBox.Show("Please enter a Plant ID to update.");
                return;
            }

            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Thinpad\\Desktop\\GHD.accdb"))
            {
                string updateQuery = "UPDATE Plants SET " +
                    "[Common Name]=?, [Scientific Name]=?, [Family]=?, [Genus]=?, [Planting Date]=?, [Exp Harvesting Date]=?, [Plot]=?, [WaterReq]=?, [TempN]=?, [TempD]=?, [LightReq]=?, [SoilpH]=?, [Notes]=?, [Img]=? " +
                    "WHERE [PlantID]=?";

                using (OleDbCommand cmd = new OleDbCommand(updateQuery, con))
                {
                    cmd.Parameters.AddWithValue("?", textBox1.Text);  // Common Name
                    cmd.Parameters.AddWithValue("?", textBox2.Text);  // Scientific Name
                    cmd.Parameters.AddWithValue("?", textBox3.Text);  // Family
                    cmd.Parameters.AddWithValue("?", textBox4.Text);  // Genus
                    cmd.Parameters.AddWithValue("?", textBox5.Text);  // Planting Date
                    cmd.Parameters.AddWithValue("?", textBox6.Text);  // Exp Harvesting Date
                    cmd.Parameters.AddWithValue("?", textBox7.Text);  // Plot
                    cmd.Parameters.AddWithValue("?", textBox8.Text);  // WaterReq
                    cmd.Parameters.AddWithValue("?", textBox9.Text);  // TempN
                    cmd.Parameters.AddWithValue("?", textBox10.Text); // TempD
                    cmd.Parameters.AddWithValue("?", textBox11.Text); // LightReq
                    cmd.Parameters.AddWithValue("?", textBox12.Text); // SoilpH
                    cmd.Parameters.AddWithValue("?", textBox13.Text); // Notes
                    cmd.Parameters.AddWithValue("?", selectedImagePath); // Img
                    cmd.Parameters.AddWithValue("?", plantId); // WHERE [PlantID]=?

                    try
                    {
                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully.");
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

        private void button4_Click(object sender, EventArgs e)
        {
            string plantId = textBox14.Text.Trim();
            if (string.IsNullOrEmpty(plantId))
            {
                MessageBox.Show("Please enter a Plant ID.");
                return;
            }

            using (OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Thinpad\\Desktop\\GHD.accdb"))
            {
                // Adjust [PlantID] to your actual column name if different
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
                                // Adjust column names/indexes as needed
                                textBox1.Text = reader["Common Name"].ToString();
                                textBox2.Text = reader["Scientific Name"].ToString();
                                textBox3.Text = reader["Family"].ToString();
                                textBox4.Text = reader["Genus"].ToString();
                                textBox5.Text = reader["Planting Date"].ToString();
                                textBox6.Text = reader["Exp Harvesting Date"].ToString();
                                textBox7.Text = reader["Plot"].ToString();
                                textBox8.Text = reader["WaterReq"].ToString();
                                textBox9.Text = reader["TempN"].ToString();
                                textBox10.Text = reader["TempD"].ToString();
                                textBox11.Text = reader["LightReq"].ToString();
                                textBox12.Text = reader["SoilpH"].ToString();
                                textBox13.Text = reader["Notes"].ToString();
                                pictureBox1.ImageLocation = reader["Img"].ToString();
                                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
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
