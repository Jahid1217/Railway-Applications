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
    public partial class Form2 : Form
    {
        private readonly IdleTimeChecker idleTimeChecker;
        public Form2()
        {
            InitializeComponent();
            tab();
            idleTimeChecker = new IdleTimeChecker();
            idleTimeChecker.StartMonitoring();
        }
        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=Jahid@12172003;database=railwaysystem;");

        string Gander;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textName.Text == "" || textEmpID.Text == "" || textPhene.Text == "" || textEmail.Text == "")
            {
                MessageBox.Show("Missing Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string EmpID, name, email, phone, post;

                EmpID = textEmpID.Text;
                name = textName.Text;
                email = textEmail.Text;
                phone = textPhene.Text;
                post = comboBox1.Text;

                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlTransaction transaction;
                transaction = con.BeginTransaction();
                cmd.Connection = con;
                cmd.Transaction = transaction;
                try
                {
                    cmd.CommandText = "insert into employees (idEmployees,EmployeesName,Employees_Phone,Employees_Grander,Employees_Post,Employees_Email) values( '" + EmpID + "', '" + name + "', '" + phone + "', '" + Gander + "', '" + post + "', '" + email + "' )";
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("            Rccord Added            ");

                    textEmpID.Text="";
                    textName.Text = "";
                    textEmail.Text = "";
                    textPhene.Text = "";
                    comboBox1.Text= "";
                    //Gander = null;

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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Gander = "Male";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Gander = "Female";
        }
        public void tab()
        {
            con.Open();
            string Query = "SELECT * FROM employees";
            MySqlDataAdapter sda = new MySqlDataAdapter(Query, con);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView2.DataSource = ds.Tables[0];
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "SELECT * FROM employees";
            MySqlDataAdapter sda = new MySqlDataAdapter(Query, con);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string imagePath = @"D:\CODE\C#\WindowsFormsMain\Resources\print.png";
            Image image = Image.FromFile(imagePath);

            // Set the page orientation to Landscape
            e.PageSettings.Landscape = true;

            // Draw the image
            e.Graphics.DrawImage(image, 150, 20, image.Width, image.Height);

            // Dispose of the image to release resources
            image.Dispose();

            // Draw the title
            e.Graphics.DrawString("Employee List", new Font("Arial", 25, FontStyle.Regular), Brushes.Black, new Point(300, 180));

            // Draw the date
            e.Graphics.DrawString("Date: " + DateTime.Now, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(20, 150));

            // Draw the DataGridView
            DrawDataGridView(dataGridView2, e.Graphics);
        }

        Bitmap bmp;

        private void DrawDataGridView(DataGridView dataGridView, Graphics graphics)
        {
            // Define the rectangle area to draw
            Rectangle rect = new Rectangle(3, 250, dataGridView.Width, dataGridView.Height);

            // Draw the header
            foreach (DataGridViewColumn col in dataGridView.Columns)
            {
                graphics.DrawString(col.HeaderText, dataGridView.Font, Brushes.Black, rect, StringFormat.GenericTypographic);
                rect.X += col.Width;
            }

            // Draw the data
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                rect.X = 15;
                rect.Y += row.Height;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    graphics.DrawString(cell.Value?.ToString(), dataGridView.Font, Brushes.Black, rect, StringFormat.GenericTypographic);
                    rect.X += cell.OwningColumn.Width;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /* Graphics g = this.CreateGraphics();
             bmp = new Bitmap(this.Size.Width, this.Size.Height,g);
             Graphics mg = Graphics.FromImage(bmp);
             mg.CopyFromScreen(this.Location.X, this.Location.Y,0,0,this.Size);
             printPreviewDialog1.ShowDialog();*/
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
          
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }
    }
}
