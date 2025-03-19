using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsMain
{
    public partial class Form8 : Form
    {
        private readonly IdleTimeChecker idleTimeChecker;

        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=Jahid@12172003;database=railwaysystem;");
        public Form8()
        {
            InitializeComponent();
            idleTimeChecker = new IdleTimeChecker();
            idleTimeChecker.StartMonitoring();
            passtab();
            trainID();
        }

        Bitmap bmp;
        private PrintDocument printDocument1;

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
        private void button1_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument2;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
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
            e.Graphics.DrawString(" PASSENGER LIST ", new Font("Arial", 25, FontStyle.Regular), Brushes.Black, new Point(300, 180));

            // Draw the date
            e.Graphics.DrawString("Date: " + DateTime.Now, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(20, 150));

            // Draw the DataGridView
            DrawDataGridView(dataGridView1, e.Graphics);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Exit this Application.", "Error", MessageBoxButtons.RetryCancel | MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (result == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }
        public void passtab()
        {
            con.Open();
            string Query = "SELECT * FROM passenger_list";
            MySqlDataAdapter sda = new MySqlDataAdapter(Query, con);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        public void trainID()
        {
            try
            {
                {
                    con.Open();
                    string query = "SELECT DISTINCT Trine_List FROM passenger_list";
                    MySqlCommand cmd = new MySqlCommand(query, con);

                    using (MySqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            string trineList = rd.GetString(0); // Assuming "Trine_List" is the first column in the result set
                            comboBox1.Items.Add(trineList);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here, log or display an error message
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            string Query = "SELECT *  FROM passenger_list where Trine_List='" + comboBox1.Text + "'";
            MySqlDataAdapter sda = new MySqlDataAdapter(Query, con);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
    }
}
