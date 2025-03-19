using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace WindowsFormsMain
{   
    public partial class Form5 : Form
    {
        private readonly IdleTimeChecker idleTimeChecker;
        public Form5()
        {
            InitializeComponent();
            idleTimeChecker = new IdleTimeChecker();
            idleTimeChecker.StartMonitoring();
        }
        string pattern = @"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$";

        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=Jahid@12172003;database=railwaysystem;");
        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void textfrist_TextChanged(object sender, EventArgs e)
        {
            textfrist.Focus();
            if (string.IsNullOrEmpty(textfrist.Text))
            {
                textfrist.Focus();
                errorProvider1.SetError(this.textfrist, "Please Enter First Name");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void textCpassword_TextChanged(object sender, EventArgs e)
        {
            if (textpassword.TextLength < 8)
            {
                errorProvider5.SetError(this.textpassword, "Minimum 8 Characters required");
            }
            else if (!Regex.IsMatch(textpassword.Text, pattern))
            {
                errorProvider5.SetError(this.textpassword, "Password must include Uppercase, Lowercase, Number, Special Characters");
            }
            else
            {
                errorProvider5.Clear();
            }
        }

        private void textlast_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textlast.Text) == true)
            {
                errorProvider2.SetError(this.textlast, "Please Enter Last Name");
            }
            else
            {
                errorProvider2.Clear();
            }
        }

        private void textemail_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textemail.Text) == true)
            {
                errorProvider3.SetError(this.textemail, "Please Enter Email");
            }
            else
            {
                errorProvider3.Clear();
            }
        }

        private void textuserId_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textuserId.Text) == true)
            {
                errorProvider4.SetError(this.textuserId, "Please Enter User Id");
            }
            else
            {
                errorProvider4.Clear();
            }
        }

        private void textpassword_TextChanged(object sender, EventArgs e)
        {

            if (textpassword.TextLength < 8)
            {
                errorProvider5.SetError(this.textpassword, "Minimum 8 Characters required");
            }
            else if (!Regex.IsMatch(textpassword.Text, pattern))
            {
                errorProvider5.SetError(this.textpassword, "Password must include Uppercase, Lowercase, Number, Special Characters");
            }
            else
            {
                errorProvider5.Clear();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textfrist.Text == "" && textlast.Text == "" && textemail.Text == "" && textuserId.Text == "" && textpassword.Text == "" && textCpassword.Text == "")
            {
                MessageBox.Show("\t \t Missing Information \t \t");
            }
            else
            {
                string fristname, lastname, email, userid, password, Cpassword;

                fristname = textfrist.Text;
                lastname = textlast.Text;
                email = textemail.Text;
                userid = textuserId.Text;
                password = textpassword.Text;
                Cpassword = textCpassword.Text;


                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlTransaction transaction;
                transaction = con.BeginTransaction();
                cmd.Connection = con;
                cmd.Transaction = transaction;
                try
                {
                    cmd.CommandText = "insert into usertable(Frist_Name,Last_Name,Email,User_Id, Password ,C_Password) " +
                        "values( '" + fristname + "', '" + lastname + "', '" + email + "', '" + userid + "', '" + password + "', '" + Cpassword + "' )";
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("            Rccord Added            ");

                    Loginform form = new Loginform();
                    this.Hide();
                    form.Show();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Loginform form = new Loginform();
            this.Hide();
            form.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textpassword.UseSystemPasswordChar = false;
            }
            else
            {
                textpassword.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textCpassword.UseSystemPasswordChar = false;
            }
            else
            {
                textCpassword.UseSystemPasswordChar = true;
            }
        }
    }
}
