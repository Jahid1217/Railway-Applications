using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace WindowsFormsMain
{
    public partial class Form6 : Form
    {
        //Mouse hasn't moved for 10 seconds. Exiting application.
        private readonly IdleTimeChecker idleTimeChecker;
        //MySqlCommandBuilder commandBuilder;
        public Form6()
        {
            InitializeComponent();
            tab();
            Admintab();
            Employeetab();
            //Mouse hasn't moved for 10 seconds. Exiting application.
            idleTimeChecker = new IdleTimeChecker();
            idleTimeChecker.StartMonitoring();
            Passtab();
        }

        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=Jahid@12172003;database=railwaysystem;");
        

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Exit this Application.", "Error", MessageBoxButtons.RetryCancel | MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (result == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
            
        }

        public void tab()
        {
            con.Open();
            string Query = "SELECT * FROM train_list";
            MySqlDataAdapter sda = new MySqlDataAdapter(Query, con);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();
        }
        public void Employeetab()
        {
            con.Open();
            string Query = "SELECT * FROM employees";
            MySqlDataAdapter sda = new MySqlDataAdapter(Query, con);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView3.DataSource = ds.Tables[0];
            con.Close();
        }
        public void Passtab()
        {
            con.Open();
            string Query = "SELECT * FROM passenger_list";
            MySqlDataAdapter sda = new MySqlDataAdapter(Query, con);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView4.DataSource = ds.Tables[0];
            con.Close();
        }
        public void Admintab()
        {
            con.Open();
            string Query = "SELECT Frist_Name,Last_Name,Email,User_Id FROM usertable";
            MySqlDataAdapter sda = new MySqlDataAdapter(Query, con);
            MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView2.DataSource = ds.Tables[0];
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Train_ID, Train_Name, Train_Seat_Number, Tickets_Purchase, from, To, Train_Time, Ac_Price, Non_Ac_Price;
            Train_ID = textBoxTrainID.Text;
            Train_Name = textBoxTrainName.Text;
            Train_Seat_Number = textBoxTrainSeatNo.Text;
            Tickets_Purchase = textBoxTrainPurchase.Text;
            from = textBoxFrom.Text;
            To = textBoxTO.Text;
            Train_Time = textBoxTrainTime.Text;
            Ac_Price = textBoxAcSeatPrice.Text;
            Non_Ac_Price = textBoxNonAcSeatPrice.Text;

            if (textBoxTrainID.Text == null && textBoxTrainName.Text == null && textBoxTrainSeatNo.Text == null && textBoxTrainPurchase.Text == null && textBoxFrom.Text == null&& textBoxTO.Text == null && textBoxTrainTime.Text == null && textBoxAcSeatPrice.Text == null  && textBoxNonAcSeatPrice.Text == null)
            {
                MessageBox.Show("Missing Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlTransaction transaction;

                transaction = con.BeginTransaction();
                cmd.Connection = con;
                cmd.Transaction = transaction;

                try
                {
                    cmd.CommandText = "INSERT INTO train_list(Train_ID, Train_Name, Train_Seat_Number, Tickets_Purchase, `From`, `To`, Train_Time, Ac_Price, Non_Ac_Price) VALUES (@Train_ID, @Train_Name, @Train_Seat_Number, @Tickets_Purchase, @From, @To, @Train_Time, @Ac_Price, @Non_Ac_Price)";

                    cmd.Parameters.AddWithValue("@Train_ID", Train_ID);
                    cmd.Parameters.AddWithValue("@Train_Name", Train_Name);
                    cmd.Parameters.AddWithValue("@Train_Seat_Number", Train_Seat_Number);
                    cmd.Parameters.AddWithValue("@Tickets_Purchase", Tickets_Purchase);
                    cmd.Parameters.AddWithValue("@From", from);
                    cmd.Parameters.AddWithValue("@To", To);
                    cmd.Parameters.AddWithValue("@Train_Time", Train_Time);
                    cmd.Parameters.AddWithValue("@Ac_Price", Ac_Price);
                    cmd.Parameters.AddWithValue("@Non_Ac_Price", Non_Ac_Price);

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("Record Added");

                    // Resetting text boxes
                    textBoxTrainID.Text = null;
                    textBoxTrainName.Text = null;
                    textBoxTrainSeatNo.Text = null;
                    textBoxTrainPurchase.Text = null;
                    textBoxFrom.Text = null;
                    textBoxTO.Text = null;
                    textBoxTrainTime.Text = null;
                    textBoxAcSeatPrice.Text = null;
                    textBoxNonAcSeatPrice.Text = null;
                }
                
            
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                    tab();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if any row is selected
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the value from the selected row's "Train_ID" column
                    string trainIDToDelete = dataGridView1.SelectedRows[0].Cells["Train_ID"].Value.ToString();

                    Console.WriteLine("Train_ID to delete: " + trainIDToDelete);

                    con.Open();

                    // Delete the record from the database
                    using (MySqlCommand deleteCmd = new MySqlCommand("DELETE FROM train_list WHERE Train_ID = @Train_ID", con))
                    {
                        deleteCmd.Parameters.AddWithValue("@Train_ID", trainIDToDelete);
                        deleteCmd.ExecuteNonQuery();
                        //Console.WriteLine("Record deleted successfully.");
                        MessageBox.Show("Information Delete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Remove the selected row from the DataGridView
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                       // MessageBox.Show("Information Delete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No row selected.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        MySqlDataAdapter adap;
        DataSet ds;
        MySqlCommandBuilder commandBuilder;

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();

                // Make sure adap is initialized before using it
                if (adap == null)
                {
                    adap = new MySqlDataAdapter("SELECT * FROM train_list", con);
                    // Adjust the SQL query based on your actual scenario
                }

                // Initialize or reload the DataSetg
                ds = new DataSet();
                adap.Fill(ds, "train_list");

                commandBuilder = new MySqlCommandBuilder(adap);
                adap.Update(ds, "train_list");
                MessageBox.Show("Information Updated", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Handle the exception appropriately, log it, show an error message, etc.
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        private void AdminDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if any row is selected
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    // Get the value from the selected row's "Train_ID" column
                    string UserIDToDelete = dataGridView2.SelectedRows[0].Cells["User_Id"].Value.ToString();

                    Console.WriteLine("User_Id to delete: " + UserIDToDelete);

                    con.Open();

                    // Delete the record from the database
                    using (MySqlCommand deleteCmd = new MySqlCommand("DELETE FROM usertable WHERE User_Id = @User_Id", con))
                    {
                        deleteCmd.Parameters.AddWithValue("@User_Id", UserIDToDelete);
                        deleteCmd.ExecuteNonQuery();
                        //Console.WriteLine("Record deleted successfully.");
                        MessageBox.Show("Information Delete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Remove the selected row from the DataGridView
                        dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);
                        // MessageBox.Show("Information Delete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No row selected.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if any row is selected
                if (dataGridView3.SelectedRows.Count > 0)
                {
                    // Get the value from the selected row's "Train_ID" column
                    string EmployeeIDToDelete = dataGridView3.SelectedRows[0].Cells["idEmployees"].Value.ToString();

                    Console.WriteLine("idEmployees to delete: " + EmployeeIDToDelete);

                    con.Open();

                    // Delete the record from the database
                    using (MySqlCommand deleteCmd = new MySqlCommand("DELETE FROM employees WHERE idEmployees = @idEmployees", con))
                    {
                        deleteCmd.Parameters.AddWithValue("@idEmployees", EmployeeIDToDelete);
                        deleteCmd.ExecuteNonQuery();
                        //Console.WriteLine("Record deleted successfully.");
                        MessageBox.Show("Information Delete", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        // Remove the selected row from the DataGridView
                        dataGridView3.Rows.Remove(dataGridView3.SelectedRows[0]);
                       // MessageBox.Show("Information Delete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No row selected.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if any row is selected
                if (dataGridView4.SelectedRows.Count > 0)
                {
                    // Get the value from the selected row's column
                    string NID_NumberToDelete = dataGridView4.SelectedRows[0].Cells["NID_Number"].Value.ToString();

                    Console.WriteLine("NID_Number to delete: " + NID_NumberToDelete);

                    con.Open();

                    // Delete the record from the database
                    using (MySqlCommand deleteCmd = new MySqlCommand("DELETE FROM passenger_list WHERE NID_Number = @NID_Number", con))
                    {
                        deleteCmd.Parameters.AddWithValue("@NID_Number", NID_NumberToDelete);
                        deleteCmd.ExecuteNonQuery();
                        //Console.WriteLine("Record deleted successfully.");
                        MessageBox.Show("Information Delete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Remove the selected row from the DataGridView
                        dataGridView4.Rows.Remove(dataGridView4.SelectedRows[0]);
                        // MessageBox.Show("Information Delete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
                else
                {
                    MessageBox.Show("No row selected.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
                Passtab();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            con.Open();
            MySqlCommand mySqlCommand = new MySqlCommand("UPDATE train_list SET Train_Delay_Time = @Train_Delay_Time WHERE Train_ID = @Train_ID", con);
            mySqlCommand.Parameters.AddWithValue("@Train_Delay_Time", textBoxDelayTime.Text);
            mySqlCommand.Parameters.AddWithValue("@Train_ID", textBoxTrainID.Text); // Replace yourTrainIDValue with the actual value or parameter for Train_ID
            mySqlCommand.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Train_Delay_Time Update");

            // Resetting text boxes
            textBoxTrainID.Text = null;
            textBoxDelayTime.Text= null;
            tab();

        }
    }
}
