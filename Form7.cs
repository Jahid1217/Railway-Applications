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
    public partial class Form7 : Form
    {
        private readonly IdleTimeChecker idleTimeChecker;
        public Form7()
        {
            InitializeComponent();
            idleTimeChecker = new IdleTimeChecker();
            idleTimeChecker.StartMonitoring();
            Trinetab();

        }
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=Jahid@12172003;database=railwaysystem;");
        private void timer1_Tick(object sender, EventArgs e)
        {
            timeColck.Text = DateTime.Now.ToString("T");
            Date.Text = DateTime.Now.ToLongDateString();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        public void Trinetab()
        {
            con.Open();
            string Query = "SELECT Train_ID, Train_Name, `From`, `To`, Train_Time,Train_Delay_Time FROM train_list";
            MySqlDataAdapter sda = new MySqlDataAdapter(Query, con);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Exit this Application.", "Error", MessageBoxButtons.RetryCancel | MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (result == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }
    }
}
