using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsMain
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            TrineList();
        }

        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=Jahid@12172003;database=railwaysystem;");
        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }
        private string previousComboBox1Selection = null;
        private string previousComboBox22Selection = null;
        private string previousComboBox3Selection = null;
        private string previousComboBox4Selection = null;
        private string previousComboBox5Selection = null;

        // private string previousComboBox2Selection = null;
        

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null ||(comboBox1.SelectedItem.ToString() == "" && !radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("Please select options before continuing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /*else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "1" && radioButton1.Checked)
            {
                pictureBox1.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\Up.jpg";
                Platform1.Text = comboBox2.Text;
                TrainNo.Text = "";
                comboBox2.Text = "";
                comboBox1.Text = null;
            }*/
            else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "1" && radioButton1.Checked)
            {
                string currentComboBox1Selection = comboBox1.Text;
                string currentComboBox2Selection = comboBox2.Text;

                if (currentComboBox1Selection == previousComboBox1Selection)
                {
                    MessageBox.Show("Already selected. Please choose different options.");
                }
                else
                {
                    pictureBox1.ImageLocation = @"D:\CODE\C#\Railway Reservation System\Resources\Up.jpg";
                    Platform1.Text = currentComboBox2Selection;
                    TrainNo.Text = "";
                    comboBox2.Text = "";
                    comboBox1.Text = null;

                    // Update the previous selections
                    previousComboBox1Selection = currentComboBox1Selection;
                   // previousComboBox2Selection = currentComboBox2Selection;
                   timer1.Start();
                }
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "1" && radioButton2.Checked)
            {
                pictureBox1.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
                Platform1.Text = "Platform1";
                comboBox2.Text = "";
                comboBox1.Text = null;
                previousComboBox1Selection = null;
            }
            else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "2" && radioButton1.Checked)
            {
                /*pictureBox2.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\Up.jpg";
                Platform2.Text = comboBox2.Text;
                TrainNo.Text = "";
                comboBox2.Text = "";
                comboBox1.Text = null;*/
                string currentComboBox22Selection = comboBox1.Text;
                string currentComboBox2Selection = comboBox2.Text;

                if (currentComboBox22Selection == previousComboBox22Selection)
                {
                    MessageBox.Show("Already selected. Please choose different Platform ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pictureBox2.ImageLocation = @"D:\CODE\C#\Railway Reservation System\Resources\Up.jpg";
                    Platform2.Text = currentComboBox2Selection;
                    TrainNo.Text = "";
                    comboBox2.Text = "";
                    comboBox1.Text = null;

                    // Update the previous selections
                    previousComboBox22Selection = currentComboBox22Selection;
                    // previousComboBox2Selection = currentComboBox2Selection;
                    timer2.Start();
                }
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "2" && radioButton2.Checked)
            {
                pictureBox2.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
                Platform2.Text = "Platform2";
                comboBox2.Text = "";
                comboBox1.Text = null;
                previousComboBox22Selection = null;
            }
            else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "3" && radioButton1.Checked)
            {
                /* pictureBox3.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\Up.jpg";
                 Platform3.Text = comboBox2.Text;
                 TrainNo.Text = "";
                 comboBox2.Text = "";
                 comboBox1.Text = null;*/
                string currentComboBox3Selection = comboBox1.Text;
                string currentComboBox2Selection = comboBox2.Text;

                if (currentComboBox3Selection == previousComboBox3Selection)
                {
                    MessageBox.Show("Already selected. Please choose differentPlatform ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pictureBox3.ImageLocation = @"D:\CODE\C#\Railway Reservation System\Resources\Up.jpg";
                    Platform3.Text = currentComboBox2Selection;
                    TrainNo.Text = "";
                    comboBox2.Text = "";
                    comboBox1.Text = null;

                    // Update the previous selections
                    previousComboBox3Selection = currentComboBox3Selection;
                    // previousComboBox2Selection = currentComboBox2Selection;
                    timer3.Start();
                }
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "3" && radioButton2.Checked)
            {
                pictureBox3.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
                Platform3.Text = "Platform3";
                comboBox2.Text = "";
                comboBox1.Text = null;
                previousComboBox3Selection = null;
            }
            else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "4" && radioButton1.Checked)
            {
                /* pictureBox4.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\Up.jpg";
                 Platform4.Text = comboBox2.Text;
                 TrainNo.Text = "";
                 comboBox2.Text = "";
                 comboBox1.Text = null;*/
                string currentComboBox4Selection = comboBox1.Text;
                string currentComboBox2Selection = comboBox2.Text;

                if (currentComboBox4Selection == previousComboBox4Selection)
                {
                    MessageBox.Show("Already selected. Please choose different Platform \", \"Error\", MessageBoxButtons.OK, MessageBoxIcon.Error");
                }
                else
                {
                    pictureBox4.ImageLocation = @"D:\CODE\C#\Railway Reservation System\Resources\Up.jpg";
                    Platform4.Text = currentComboBox2Selection;
                    TrainNo.Text = "";
                    comboBox2.Text = "";
                    comboBox1.Text = null;

                    // Update the previous selections
                    previousComboBox4Selection = currentComboBox4Selection;
                    // previousComboBox2Selection = currentComboBox2Selection;
                    timer4.Start();
                }
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "4" && radioButton2.Checked)
            {
                pictureBox4.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
                Platform4.Text = "Platform4";
                comboBox2.Text = "";
                comboBox1.Text = null;
                previousComboBox4Selection = null;
            }
            else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "5" && radioButton1.Checked)
            {
                /* pictureBox5.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\Up.jpg";
                 Platform5.Text = comboBox2.Text;
                 TrainNo.Text = "";
                 comboBox2.Text = "";
                 comboBox1.Text = null;*/
                string currentComboBox5Selection = comboBox1.Text;
                string currentComboBox2Selection = comboBox2.Text;

                if (currentComboBox5Selection == previousComboBox5Selection)
                {
                    MessageBox.Show("Already selected. Please choose different Platform ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pictureBox5.ImageLocation = @"D:\CODE\C#\Railway Reservation System\Resources\Up.jpg";
                    Platform5.Text = currentComboBox2Selection;
                    TrainNo.Text = "";
                    comboBox2.Text = "";
                    comboBox1.Text = null;

                    // Update the previous selections
                    previousComboBox5Selection = currentComboBox5Selection;
                    // previousComboBox2Selection = currentComboBox2Selection;
                    timer5.Start();
                }
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "5" && radioButton2.Checked)
            {
                pictureBox5.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
                Platform5.Text = "Platform5";
                comboBox2.Text = "";
                comboBox1.Text = null;
                previousComboBox5Selection = null;
            }
        }
        
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || (comboBox1.SelectedItem.ToString() == "" && !radioButton1.Checked && !radioButton2.Checked))
            {
                MessageBox.Show("Please select options before continuing.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            /*else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "1" && radioButton1.Checked)
            {
                pictureBox1.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\Up.jpg";
                Platform1.Text = comboBox2.Text;
                TrainNo.Text = "";
                comboBox2.Text = "";
                comboBox1.Text = null;
            }*/
            else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "1" && radioButton1.Checked)
            {
                string currentComboBox1Selection = comboBox1.Text;
                string currentComboBox2Selection = comboBox2.Text;

                if (currentComboBox1Selection == previousComboBox1Selection)
                {
                    MessageBox.Show("Already selected. Please choose different options.");
                }
                else
                {
                    pictureBox1.ImageLocation = @"D:\CODE\C#\Railway Reservation System\Resources\Up.jpg";
                    Platform1.Text = currentComboBox2Selection;
                    TrainNo.Text = "";
                    comboBox2.Text = "";
                    comboBox1.Text = null;

                    // Update the previous selections
                    previousComboBox1Selection = currentComboBox1Selection;
                    // previousComboBox2Selection = currentComboBox2Selection;
                }
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "1" && radioButton2.Checked)
            {
                pictureBox1.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
                Platform1.Text = "Platform1";
                comboBox2.Text = "";
                comboBox1.Text = null;
                previousComboBox1Selection = null;
            }
            else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "2" && radioButton1.Checked)
            {
                /*pictureBox2.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\Up.jpg";
                Platform2.Text = comboBox2.Text;
                TrainNo.Text = "";
                comboBox2.Text = "";
                comboBox1.Text = null;*/
                string currentComboBox22Selection = comboBox1.Text;
                string currentComboBox2Selection = comboBox2.Text;

                if (currentComboBox22Selection == previousComboBox22Selection)
                {
                    MessageBox.Show("Already selected. Please choose different Platform ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pictureBox2.ImageLocation = @"D:\CODE\C#\Railway Reservation System\Resources\Up.jpg";
                    Platform2.Text = currentComboBox2Selection;
                    TrainNo.Text = "";
                    comboBox2.Text = "";
                    comboBox1.Text = null;

                    // Update the previous selections
                    previousComboBox22Selection = currentComboBox22Selection;
                    // previousComboBox2Selection = currentComboBox2Selection;
                }
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "2" && radioButton2.Checked)
            {
                pictureBox2.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
                Platform2.Text = "Platform2";
                comboBox2.Text = "";
                comboBox1.Text = null;
                previousComboBox22Selection = null;
            }
            else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "3" && radioButton1.Checked)
            {
                /* pictureBox3.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\Up.jpg";
                 Platform3.Text = comboBox2.Text;
                 TrainNo.Text = "";
                 comboBox2.Text = "";
                 comboBox1.Text = null;*/
                string currentComboBox3Selection = comboBox1.Text;
                string currentComboBox2Selection = comboBox2.Text;

                if (currentComboBox3Selection == previousComboBox3Selection)
                {
                    MessageBox.Show("Already selected. Please choose different Platform ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pictureBox3.ImageLocation = @"D:\CODE\C#\Railway Reservation System\Resources\Up.jpg";
                    Platform3.Text = currentComboBox2Selection;
                    TrainNo.Text = "";
                    comboBox2.Text = "";
                    comboBox1.Text = null;

                    // Update the previous selections
                    previousComboBox3Selection = currentComboBox3Selection;
                    // previousComboBox2Selection = currentComboBox2Selection;
                }
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "3" && radioButton2.Checked)
            {
                pictureBox3.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
                Platform3.Text = "Platform3";
                comboBox2.Text = "";
                comboBox1.Text = null;
                previousComboBox3Selection = null;
            }
            else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "4" && radioButton1.Checked)
            {
                /* pictureBox4.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\Up.jpg";
                 Platform4.Text = comboBox2.Text;
                 TrainNo.Text = "";
                 comboBox2.Text = "";
                 comboBox1.Text = null;*/
                string currentComboBox4Selection = comboBox1.Text;
                string currentComboBox2Selection = comboBox2.Text;

                if (currentComboBox4Selection == previousComboBox4Selection)
                {
                    MessageBox.Show("Already selected. Please choose different Platform ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pictureBox4.ImageLocation = @"D:\CODE\C#\Railway Reservation System\Resources\Up.jpg";
                    Platform4.Text = currentComboBox2Selection;
                    TrainNo.Text = "";
                    comboBox2.Text = "";
                    comboBox1.Text = null;

                    // Update the previous selections
                    previousComboBox4Selection = currentComboBox4Selection;
                    // previousComboBox2Selection = currentComboBox2Selection;
                }
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "4" && radioButton2.Checked)
            {
                pictureBox4.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
                Platform4.Text = "Platform4";
                comboBox2.Text = "";
                comboBox1.Text = null;
                previousComboBox4Selection = null;
            }
            else if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox1.SelectedItem.ToString() == "5" && radioButton1.Checked)
            {
                /* pictureBox5.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\Up.jpg";
                 Platform5.Text = comboBox2.Text;
                 TrainNo.Text = "";
                 comboBox2.Text = "";
                 comboBox1.Text = null;*/
                string currentComboBox5Selection = comboBox1.Text;
                string currentComboBox2Selection = comboBox2.Text;

                if (currentComboBox5Selection == previousComboBox5Selection)
                {
                    MessageBox.Show("Already selected. Please choose different oPlatform ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pictureBox5.ImageLocation = @"D:\CODE\C#\Railway Reservation System \Resources\Up.jpg";
                    Platform5.Text = currentComboBox2Selection;
                    TrainNo.Text = "";
                    comboBox2.Text = "";
                    comboBox1.Text = null;

                    // Update the previous selections
                    previousComboBox5Selection = currentComboBox5Selection;
                    // previousComboBox2Selection = currentComboBox2Selection;
                }
            }
            else if (comboBox1.SelectedItem != null && comboBox1.SelectedItem.ToString() == "5" && radioButton2.Checked)
            {
                pictureBox5.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
                Platform5.Text = "Platform5";
                comboBox2.Text = "";
                comboBox1.Text = null;
                previousComboBox5Selection = null;
            }
        }

        private void Platform4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                try
                {
                    con.Open();
                    string selectedComboItem = comboBox2.SelectedItem.ToString();
                    string[] values = selectedComboItem.Split('-');

                    if (values.Length == 2)
                    {
                        string trainID = values[0].Trim();
                        string trainName = values[1].Trim();

                        string query = "SELECT * FROM train_list WHERE Train_ID = @TrainID AND Train_Name = @TrainName";

                        using (MySqlCommand cmd = new MySqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@TrainID", trainID);
                            cmd.Parameters.AddWithValue("@TrainName", trainName);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                // trineName.Text = reader["Train_Name"].ToString(); // Assuming "Train_Name" is the correct column name
                                TrainNo.Text = reader["Train_ID"].ToString(); // Assuming "Train_Time" is the correct column name
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    con.Close();
                }

            }
        private void TrineList()
        {
            try
            {
                con.Open();
                string Query = "Select * from train_list";
                MySqlCommand cmd = new MySqlCommand(Query, con);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string trineList = reader.GetString(1);
                        string trineNo = reader.GetString(0);
                        comboBox2.Items.Add(trineNo + " - " + trineList);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Exit this Application.", "Error", MessageBoxButtons.RetryCancel | MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (result == DialogResult.Cancel)
            {
                Application.Exit();
            }
            // Optionally, you can handle the DialogResult.OK case as well.
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
            Platform1.Text = "Platform1";
            comboBox2.Text = "";
            comboBox1.Text = null;
            TrainNo.Text = null;
            previousComboBox1Selection = null;
            timer1.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            pictureBox2.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
            Platform2.Text = "Platform2";
            comboBox2.Text = "";
            TrainNo.Text = null;
            comboBox1.Text = null;
            previousComboBox22Selection = null;
            timer2.Stop();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            pictureBox3.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
            Platform3.Text = "Platform3";
            comboBox2.Text = "";
            comboBox1.Text = null;
            TrainNo.Text = null;
            previousComboBox3Selection = null;
            timer3.Stop();
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            pictureBox4.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
            Platform4.Text = "Platform4";
            comboBox2.Text = "";
            comboBox1.Text = null;
            TrainNo.Text = null;
            previousComboBox4Selection = null;
            timer4.Stop();
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            pictureBox5.ImageLocation = @"D:\CODE\C#\WindowsFormsMain\Resources\depositphotos_74821865-stock-illustration-railroad-track-icon (2)1.jpg";
            Platform5.Text = "Platform5";
            comboBox2.Text = "";
            comboBox1.Text = null;
            TrainNo.Text = null;
            previousComboBox5Selection = null;
            timer5.Stop();
        }

        private void Platform5_Click(object sender, EventArgs e)
        {

        }
    }
}
