using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Transactions;

namespace WindowsFormsMain
{
    public partial class Loginform : Form
    {
        private readonly IdleTimeChecker idleTimeChecker;
        public Loginform()
        {
            InitializeComponent();
            idleTimeChecker = new IdleTimeChecker();
            idleTimeChecker.StartMonitoring();
        }
        int count=0;
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=Jahid@12172003;database=railwaysystem;");
        private void button2_Click(object sender, EventArgs e)
        {
            string username, userpassword;
            username = UserName.Text;
            userpassword = UserPassword.Text;

            count += count;
            if(count >= 3)
            {
                MessageBox.Show("\t \t System Is Block \t \t");
                Application.Exit();
            }
            if (username == "")
            {
                MessageBox.Show("\t\t Blank User Name \t\t");
            }
            else if(userpassword == "")
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
                    if(dt.Rows.Count > 0)
                    {
                        username = UserName.Text;
                        userpassword = UserPassword.Text;

                        Form1 form11 = new Form1();
                        this.Hide();
                        form11.Show();
                    }
                    else
                    {
                        MessageBox.Show(" Invalide Username & Password","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) {
                UserPassword.UseSystemPasswordChar = false;
            }
            else
            {
                UserPassword.UseSystemPasswordChar = true;
            }

        }

        private void WELCOM_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form5 form11 = new Form5();
            this.Hide();
            form11.Show();
        }

        private void Loginform_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
