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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsMain
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
            ShowName();
        }

        int count = 0;
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=Jahid@12172003;database=railwaysystem;");
        private void button1_Click(object sender, EventArgs e)
        {
            string username, userpassword;
            username = UserName.Text;
            userpassword = UserPassword.Text;

            count += count;
            if (count >= 3)
            {
                MessageBox.Show("\t \t System Is Block \t \t");
                Application.Exit();
            }
            if (username == "")
            {
                MessageBox.Show("\t\t Blank User Name \t\t");
            }
            else if (userpassword == "")
            {
                MessageBox.Show(" Blank Password ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string qurry = "Select * From usertable Where User_Id = '" + UserName.Text + "' And Password = '" + UserPassword.Text + "'";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(qurry, con);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        username = UserName.Text;
                        userpassword = UserPassword.Text;
                        Form1 form2 = new Form1();
                        form2.Hide();
                        Form6 form1 = new Form6();
                        this.Hide();
                        form1.Show();
                       
                    }
                    else
                    {
                        MessageBox.Show(" Invalide Username & Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        UserName.Clear();
                        UserPassword.Clear();
                        UserName.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }
        private void ShowName()
        {
            try
            {
                con.Open();

                string query = "SELECT * FROM usertable";
                MySqlCommand cmd = new MySqlCommand(query, con);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // Check if there are rows before trying to read
                    {
                        string firstName = reader.GetString(0); // Assuming the first name is in the first column
                        string lastName = reader.GetString(1);  // Assuming the last name is in the second column

                        // Assuming 'welcomName' is a control that you want to set the text for
                        welcomName.Text = $"{firstName} {lastName}";
                    }
                    else
                    {
                        // Handle the case where no rows are returned
                        welcomName.Text = "No user found";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, e.g., log or display an error message.
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                UserPassword.UseSystemPasswordChar = false;
            }
            else
            {
                UserPassword.UseSystemPasswordChar = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();

        }
    }
}
