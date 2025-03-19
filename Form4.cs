using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic;
using System.Drawing.Printing;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;



namespace WindowsFormsMain
{
    public partial class Form4 : Form
    {
        private readonly IdleTimeChecker idleTimeChecker;
        string gender;
       // string price;
        int convertedPrice;
        // string price;
        //int num = Convert.ToInt32(price);
        private string price;
        private int num;
        string List;
        private Bitmap memoryImg;
        public Form4()
        {
            InitializeComponent();
            idleTimeChecker = new IdleTimeChecker();
            idleTimeChecker.StartMonitoring();
            SomeOtherMethod();    
        }

        MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=Jahid@12172003;database=railwaysystem;");
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void Form4_Load(object sender, EventArgs e)
        {
            Draw80chairs();
            TrineTO();
        }

        
        private List<Tuple<int, int>> selectedChairsIndices = new List<Tuple<int, int>>();
       // private List<Tuple<int, int>> selectedChairsIndices = new List<Tuple<int, int>>();

        // Assuming numSeatsPerRow is the number of seats in each row
        int numSeatsPerRow = 10;

        private void Draw80chairs()
        {
            int chair = 1;
            for (int i = 0; i < tableChair.RowCount; i++)
            {
                for (int j = 0; j < tableChair.ColumnCount; j++)
                {
                    Label lblchair = new Label();
                    lblchair.Text = chair + "";
                    lblchair.AutoSize = false;
                    lblchair.Dock = DockStyle.Fill;
                    lblchair.TextAlign = ContentAlignment.MiddleCenter;
                    lblchair.BackColor = Color.White;

                    // Add the click event handler before adding to the Controls collection
                    lblchair.Click += Lblchair_Click;

                    tableChair.Controls.Add(lblchair, j, i);
                    chair++;
                }
            }
        }

        private void Lblchair_Click(object sender, EventArgs e)
        {
            Label lblchair = sender as Label;

            if (lblchair.BackColor == Color.White)
            {
                // Set the background color of the label
                lblchair.BackColor = Color.LightSkyBlue;

                // Get the indices of the selected chair and add to the list
                int rowIndex = tableChair.GetRow(lblchair);
                int colIndex = tableChair.GetColumn(lblchair);
                selectedChairsIndices.Add(new Tuple<int, int>(rowIndex, colIndex));
                int numberOfColumns = 10;
                // Calculate combined index based on the last added tuple
                int lastIndex = selectedChairsIndices.Count - 1; // Get the index of the last added tuple
                int combinedIndex = selectedChairsIndices[lastIndex].Item1 * numberOfColumns + selectedChairsIndices[lastIndex].Item2;
            }
            else if (lblchair.BackColor == Color.LightSkyBlue)
            {
                // Set the background color of the label to white
                lblchair.BackColor = Color.White;

                // Get the indices of the selected chair
                int rowIndex = tableChair.GetRow(lblchair);
                int colIndex = tableChair.GetColumn(lblchair);

                // Remove the chair indices from the list if it was deselected
                selectedChairsIndices.RemoveAll(item => item.Item1 == rowIndex && item.Item2 == colIndex);
            }

            // Show the selected chairs index numbers
            ShowSelectedChairsIndices();
            // Calculate total selected chairs
            int totalChairs = selectedChairsIndices.Count;
            labeklNOTickets.Text = totalChairs.ToString(); // Corrected label name

            // Assuming you have a variable named pricePerChair representing the price per chair
            // Example: double pricePerChair = 10.0;
            double pricePerChair; // Assign a value to pricePerChair
            pricePerChair = double.Parse(labelPrice.Text);
            // Calculate and display the total price
            labelPrice.Text = pricePerChair.ToString(); // Display pricePerChair as a string
            labelTotalP.Text = (totalChairs * pricePerChair).ToString(); // Display total price as a string

            // Store the selected chairs in the database
            StoreSelectedChairsInDatabase();
        }
        int seatNumber;
        string trineID;
        private void ShowSelectedChairsIndices()
        {
            /*StringBuilder message = new StringBuilder("Selected ");
            foreach (var combinedIndex in selectedChairsIndices)
            {
                int row = combinedIndex.Item1;
                int seatInRow = combinedIndex.Item2;

                // Convert combined indices to seat number
                seatNumber = row * numSeatsPerRow + seatInRow + 1; // Adding 1 assuming seat numbers start from 1

                message.AppendLine($"Seat Number: {seatNumber}");
            }
            AvailableTicket.Text = seatNumber.ToString();
            MessageBox.Show(message.ToString(), "Selected Chairs");*/
            StringBuilder message = new StringBuilder("Selected ");
            StringBuilder seatNumbers = new StringBuilder(); // Create a new StringBuilder to store seat numbers

            foreach (var combinedIndex in selectedChairsIndices)
            {
                int row = combinedIndex.Item1;
                int seatInRow = combinedIndex.Item2;

                // Convert combined indices to seat number
                int seatNumber = row * numSeatsPerRow + seatInRow + 1; // Adding 1 assuming seat numbers start from 1

                message.AppendLine($"Seat Number: {seatNumber}");

                // Append the seat number to the seatNumbers StringBuilder
                seatNumbers.Append(seatNumber.ToString()).Append(", ");
            }

            // Remove the trailing comma and space from seatNumbers if there are seat numbers
            if (seatNumbers.Length > 0)
            {
                seatNumbers.Length -= 2;
            }

            AvailableTicket.Text = seatNumbers.ToString(); // Assign the concatenated seat numbers to AvailableTicket.Text
            MessageBox.Show(message.ToString(), "Selected Chairs");

        }

        private void StoreSelectedChairsInDatabase()
        {
            if (textBoxNidNo.Text == null || seatNumber == null || trineID == null)
            {
                MessageBox.Show("\t \t Missing Information \t \t");
            }
            else {
                try
                {
                    con.Open();

                    // int seatNumber; // Replace with your logic to get seatNumber
                    // string trainID; // Replace with your logic to get trainID

                    string nid = textBoxNidNo.Text;

                    string query = "INSERT INTO chairslist (chairsNumber, NID_Number, Train_ID) VALUES (@chairsnumber, @nid_number, @train_id)";

                    // SqlCommand cmd = new SqlCommand(query); // Create SqlCommand without using statement
                    //cmd.Connection = con;  // Set the Connection property explicitly
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@chairsnumber", seatNumber);
                    cmd.Parameters.AddWithValue("@nid_number", nid);
                    cmd.Parameters.AddWithValue("@train_id", trineID); // Corrected variable name

                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"SQL Error storing selected chairs in the database: {ex.Message}", "Error");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($" {ex.Message}", "Error");
                }
                finally
                {
                    con.Close();
                }
            }
        }

        /* private void StoreSelectedChairsInDatabase()
         {
             string nid = textBoxNidNo.Text;
             try
             {
                 con.Open();

                 foreach (var combinedIndex in selectedChairsIndices)
                 {
                     string query = "INSERT INTO chairsList (chairsNumber, NID_Number,Train_ID) VALUES (@chairsnumber, @nid_number,@train_id)";

                     using (SqlCommand cmd = new SqlCommand(query))
                     {
                         // Add parameters to prevent SQL injection
                         cmd.Parameters.AddWithValue("@chairsnumber", seatNumber); // Assuming combinedIndex is a string
                         cmd.Parameters.AddWithValue("@nid_number",nid ); // Adjust as needed
                         cmd.Parameters.AddWithValue("@train_id", trineID);
                         cmd.ExecuteNonQuery();
                     }
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show($"Error storing selected chairs in the database: {ex.Message}", "Error");
             }
             finally
             {
                 con.Close();
             }
         }*/
        /*  private void Draw80chairs()
          {
              int chair = 1;
              for (int i = 0; i < tableChair.RowCount; i++)
              {
                  for (int j = 0; j < tableChair.ColumnCount; j++)
                  {
                      Label lblchair = new Label();
                      lblchair.Text = chair + "";
                      lblchair.AutoSize = false;
                      lblchair.Dock = DockStyle.Fill;
                      lblchair.TextAlign = ContentAlignment.MiddleCenter;
                      lblchair.BackColor = Color.White;

                      // Add the click event handler before adding to the Controls collection
                      lblchair.Click += Lblchair_Click;

                      tableChair.Controls.Add(lblchair, j, i);
                      chair++;
                  }
              }
          }*/
        /* private void Lblchair_Click(object sender, EventArgs e)
         {
             // Existing code for chair selection...

             // Calculate total selected chairs
             int totalChairs = 0;
             for (int i = 0; i < tableChair.RowCount; i++)
             {
                 for (int j = 0; j < tableChair.ColumnCount; j++)
                 {
                     Label chairLabel = tableChair.GetControlFromPosition(j, i) as Label;

                     if (chairLabel != null && chairLabel.BackColor == Color.LightSkyBlue)
                     {
                         totalChairs++;
                     }
                 }
             }

             // Update database when chairs are selected
             try
             {
                 con.Open();

                 // Update the database using MySQL commands
                 string updateQuery = "UPDATE YourTableName SET TotalChairs = @TotalChairs WHERE YourCondition";
                 using (MySqlCommand cmd = new MySqlCommand(updateQuery, con))
                 {
                     cmd.Parameters.AddWithValue("@TotalChairs", totalChairs);
                     cmd.ExecuteNonQuery();
                 }
             }
             catch (Exception ex)
             {
                 // Handle exceptions, log, or display an error message
                 Console.WriteLine("Error updating the database: " + ex.Message);
             }
             finally
             {
                 con.Close();
             }

             // Update UI based on the selected chairs...
         }*/
        /*  private void Lblchair_Click(object sender, EventArgs e)
          {
              Label lblchair = sender as Label;

              if (lblchair.BackColor == Color.White)
              {
                  lblchair.BackColor = Color.LightSkyBlue;
              }
              else if (lblchair.BackColor == Color.LightSkyBlue)
              {
                  lblchair.BackColor = Color.White;
              }
             //show me the selected chairs index numbers and store the selected chairs in database
             // Calculate total selected chairs
             int totalChairs = 0;
              for (int i = 0; i < tableChair.RowCount; i++)
              {
                  for (int j = 0; j < tableChair.ColumnCount; j++)
                  {
                      Label chairLabel = tableChair.GetControlFromPosition(j, i) as Label;

                      if (chairLabel != null && chairLabel.BackColor == Color.LightSkyBlue)
                      {
                          totalChairs++;
                      }
                  }
              }

             labeklNOTickets.Text = totalChairs.ToString();
             labelPrice.Text = 2000.ToString();

             // Convert labeklNOTickets.Text to an integer before multiplication
             int selectedChairs = int.Parse(labeklNOTickets.Text);
             int pricePerChair = int.Parse(labelPrice.Text);

             labelTotalP.Text = (selectedChairs * pricePerChair).ToString();
         }
         */


        private void button1_Click(object sender, EventArgs e)
        {
            /*int totalChair = 0;

            for (int i = 0; i < tableChair.Controls.Count; i++)
            {
                Label lblchair = tableChair.Controls[i] as Label;

                if (lblchair != null && lblchair.BackColor == Color.LightSkyBlue)
                {
                    lblchair.BackColor = Color.GreenYellow;
                    totalChair++;
                }
            }

            labeklNOTickets.Text = totalChair.ToString();
            labelPrice.Text = 2000.ToString();

            // Convert labeklNOTickets.Text to an integer before multiplication
            int selectedChairs = int.Parse(labeklNOTickets.Text);
            int pricePerChair = int.Parse(labelPrice.Text);

            labelTotalP.Text = (selectedChairs * pricePerChair).ToString();*/
            Form4 form4 = new Form4();
            this.Hide();
            form4.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Exit this Application.", "Error", MessageBoxButtons.RetryCancel | MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (result == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        private void labeklNOTickets_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           // string nid, fristname, lastname, email,phone,age,from,to,trineList,totalTicketPrice,totalTickets,date;

            if (textBoxFristName.Text == "" || textBoxLastName.Text == "" || textBoxNidNo.Text == "" || gender == null&&textBoxEmail.Text == "" || textBoxPhone.Text == "" || textBoxAge.Text == "" || comboBoxFrom.Text==null
                &&comboBoxTo==null&&radioButtonFemale==null||radioButtonmMale==null&&radioButton4==null||radioButton3==null)
            {
                MessageBox.Show("\t \t Missing Information \t \t");
            }
            else
            {
                /*fristname = textBoxFristName.Text;
                nid= textBoxNidNo.Text;
                lastname = textBoxLastName.Text;
                email = textBoxEmail.Text;
                phone = textBoxPhone.Text;
                age = textBoxAge.Text;
                from = comboBoxFrom.Text;
                to = comboBoxTo.Text;
                trineList = comboBoxTrine.Text;
                totalTicketPrice = labelTotalP.Text;
                totalTickets = labeklNOTickets.Text;
                date = dateTimePicker1.Text;

                con.Open();
                MySqlCommand cmd = new MySqlCommand();
                MySqlTransaction transaction;
                transaction = con.BeginTransaction();
                cmd.Connection = con;
                cmd.Transaction = transaction;
                try
                {
                    cmd.CommandText = "insert into passenger_list(NID_Number,Name,Gander,Age, Phone_Number ,Date,Destination,Trine_List,Number_Of_Tickets,Tickets_Prices) " +
                        "values( '" + nid + "',  '" + fristname + " " + lastname + "', '" + gender + "', '" + age + "', '" + phone + "', '" +date+ "','"+ from+ " "+to+"'," +
                        "'"+ trineList + "','"+ totalTickets + "','"+ totalTicketPrice + "' )";
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
                }*/
                string firstname = textBoxFristName.Text;
                string nid = textBoxNidNo.Text;
                string lastname = textBoxLastName.Text;
                string email = textBoxEmail.Text;
                string phone = textBoxPhone.Text;
                string age = textBoxAge.Text;
                string from = comboBoxFrom.Text;
                string to = comboBoxTo.Text;
                string trineList = comboBoxTrine.Text;
                string totalTicketPrice = labelTotalP.Text;
                string totalTickets = labeklNOTickets.Text;
                string date = dateTimePicker1.Text;
                string numberofseat = labeklNOTickets.Text;
                using (MySqlConnection con = new MySqlConnection("server=localhost; uid=root; password=Jahid@12172003;database=railwaysystem;"))
                {
                    con.Open();

                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlTransaction transaction = con.BeginTransaction())
                        {
                            cmd.Connection = con;
                            cmd.Transaction = transaction;

                            try
                            {
                                cmd.CommandText = "INSERT INTO passenger_list(NID_Number, Name, Gander,Email, Age, Phone_Number, Date, Destination, Trine_List, Number_Of_Tickets, Tickets_Prices) " +
                                    "VALUES (@nid, @name, @gender,@email, @age, @phone, @date, @destination, @trineList, @totalTickets, @totalTicketPrice)";

                                cmd.Parameters.AddWithValue("@nid", nid);
                                cmd.Parameters.AddWithValue("@name", $"{firstname} {lastname}");
                                cmd.Parameters.AddWithValue("@gender", gender); // You need to define 'gender' variable
                                cmd.Parameters.AddWithValue("@email", email);
                                cmd.Parameters.AddWithValue("@age", age);
                                cmd.Parameters.AddWithValue("@phone", phone);
                                cmd.Parameters.AddWithValue("@date", date);
                                cmd.Parameters.AddWithValue("@destination", $"{from} - {to}");
                                cmd.Parameters.AddWithValue("@trineList", trineList);
                                cmd.Parameters.AddWithValue("@totalTickets", totalTickets);
                                cmd.Parameters.AddWithValue("@totalTicketPrice", totalTicketPrice);

                                cmd.ExecuteNonQuery();

                                
                              // Your SQL update statement with parameters
                                cmd.CommandText = "UPDATE train_list SET Available_Tickets = Train_Seat_Number - @numberofseat WHERE Train_ID = @TrainID";
                                
                                // Adding parameters to the command
                                cmd.Parameters.AddWithValue("@numberofseat", numberofseat); // Assuming seatNumber is a variable with the number of seats to be subtracted
                                cmd.Parameters.AddWithValue("@TrainID", trineID); // Assuming trineID is a variable representing the train ID

                                // Set the connection for the comman
                               // Execute the update query
                                cmd.ExecuteNonQuery();

                               /* cmd.CommandText = "INSERT INTO train_list (numberofseat) VALUES (@numberofseat) WHERE Train_ID = @TrainID;";
                                cmd.ExecuteNonQuery();*/

                                transaction.Commit();
                                MessageBox.Show("Record Added");
                              /*   string text = Interaction.InputBox("Input resive amount", "Input Box");
                                 MessageBox.Show("Input resive amount:" + text);*/

                                string userInput = Interaction.InputBox("Input receive amount", "Input Box");
                                double userAmount;

                                // Check if the user input is a valid double
                                if (double.TryParse(userInput, out userAmount))
                                {
                                    // Assuming totalTicketPrice is a TextBox control
                                    double totalPrice = double.Parse(totalTicketPrice);

                                    // Compare the user input with the total ticket price
                                    if (userAmount == totalPrice)
                                    {
                                        // Proceed to the next step
                                        printPreviewDialog1.Document = printDocument1;
                                        printPreviewDialog1.ShowDialog();
                                        // Add your code here for the next step
                                        Form4 form4 = new Form4();
                                        this.Hide();
                                        form4.ShowDialog();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Insufficient balance. Please enter the correct amount.");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Invalid input. Please enter a valid numeric amount.");
                                }
                                


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
                }

            }
        }

        private void comboBoxTrine_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TrinecomboBox();
            con.Open();
            string srt = "SELECT * FROM train_list WHERE CONCAT(`Train_ID`, ' - ', `Train_Name`, ' - ', `Train_Time`) = @TrainIDName";

            MySqlCommand cmd = new MySqlCommand(srt, con);

            // Use parameters to prevent SQL injection
            cmd.Parameters.AddWithValue("@TrainIDName", comboBoxTrine.Text);

            MySqlDataReader rd = cmd.ExecuteReader();

            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    trineName.Text = rd["Train_Name"].ToString(); // Assuming "Train_Name" is the correct column name
                    trineTime.Text = rd["Train_Time"].ToString(); // Replace "ColumnNameForTime" with the actual column name for time
                    AcPrice.Text = rd["Ac_Price"].ToString();
                    Non_AcPrice.Text = rd["Non_Ac_Price"].ToString();
                    AvailableTicket.Text = rd["Available_Tickets"].ToString();
                }
            }
            con.Close();
        }
        /* private void TrineList()
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
                        comboBoxTrine.Items.Add(trineNo + " - " + trineList);
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
        }*/
        /* private void TrinecomboBox()
         {
             try
             {
                 con.Open();
                 string selectedComboItem = comboBoxTrine.SelectedItem.ToString();
                 string[] values = selectedComboItem.Split('-');

                 if (values.Length == 2)
                 {
                     string trainID = values[0].Trim();
                     string trainName = values[1].Trim();

                     string query = "SELECT * FROM train_list WHERE comboBoxT0 = @T0 AND Train_Name = @TrainName";


                     using (MySqlCommand cmd = new MySqlCommand(query, con))
                     {
                         cmd.Parameters.AddWithValue("@TrainID", trainID);
                         cmd.Parameters.AddWithValue("@TrainName", trainName);

                         using (MySqlDataReader reader = cmd.ExecuteReader())
                         {
                             while (reader.Read())
                             {
                                 trineName.Text = reader["Train_Name"].ToString(); // Assuming "Train_Name" is the correct column name
                                 trineTime.Text = reader["Train_Time"].ToString(); // Assuming "Train_Time" is the correct column name
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
         }*/
        /* private void TrinecomboBox()
        {
            try
            {
                con.Open();

                if (comboBoxTrine.SelectedItem != null && comboBoxTo.SelectedItem != null)
                {
                    string selectedComboItem = comboBoxTrine.SelectedItem.ToString();
                    string[] values = selectedComboItem.Split('-');

                    if (values.Length == 2)
                    {
                        string trainID = values[0].Trim();
                        string trainName = values[1].Trim();

                        string to = comboBoxTo.SelectedItem.ToString();
                        string query = "SELECT * FROM train_list WHERE ColumnName = @T0 AND Train_Name = @TrainName"; // Replace ColumnName with the actual column name in your database

                        using (MySqlCommand cmd = new MySqlCommand(query, con))
                        {
                            cmd.Parameters.AddWithValue("@T0", to);
                            cmd.Parameters.AddWithValue("@TrainName", trainName);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                comboBoxTrine.Items.Clear();

                                if (reader.HasRows) // Check if there are any rows returned
                                {
                                    while (reader.Read())
                                    {
                                        string trineList = reader.GetString(1);
                                        string trineNo = reader.GetString(0);

                                        // Add the new item
                                        comboBoxTrine.Items.Add(trineNo + " - " + trineList);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("No matching records found.");
                                }
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
        }*/

        private void TrineTO()
        {
            try
            {
                con.Open();
                string Query = "SELECT DISTINCT `To` FROM train_list"; // Use backticks for reserved keywords or column names with spaces
                MySqlCommand cmd = new MySqlCommand(Query, con);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string trainTo = reader.GetString(0); // Use index 0 to get the first column's value
                        comboBoxTo.Items.Add(trainTo);
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


        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void availableTicket_Click(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void bogiName_Click(object sender, EventArgs e)
        {

        }

        private void trineTime_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void trineName_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxBogi_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label25_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBoxNidNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void labelTotalP_Click(object sender, EventArgs e)
        {

        }

        private void labelPrice_Click(object sender, EventArgs e)
        {

        }

        private void label_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
       
        private void comboBoxTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            string srt = "SELECT * FROM train_list WHERE `To` ='" + comboBoxTo.Text + "'";
            MySqlCommand cmd = new MySqlCommand(srt, con);
            MySqlDataReader rd = cmd.ExecuteReader();

            // Clear existing items in comboBoxTrine before adding new items
            comboBoxTrine.Items.Clear();

            while (rd.Read())
            {
                trineID = rd.GetString(0);
                string trineName = rd.GetString(1); // Set the text based on the desired column
                string trinetime = rd.GetString(6);
                comboBoxTrine.Items.Add(trineID + " - " + trineName+" - "+ trinetime);
            }
            rd.Close(); // Close the reader after use
            con.Close();
            // You can also set other controls if needed
            // labelAmount1.Text = rd[6].ToString();
        con.Close();


        }

        private void comboBoxFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBoxAge_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Female";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            gender = "Male";
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBoxPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBoxFristName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelTicketList_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        /*  private void radioButton3_CheckedChanged(object sender, EventArgs e)
          {
             string price = AcPrice.ToString();

          }

          private void radioButton4_CheckedChanged(object sender, EventArgs e)
          {
              string price = Non_AcPrice.ToString();   

          }*/
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            price = AcPrice.Text;
            labelPrice.Text = price;
            List = "AC";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            price = Non_AcPrice.Text;
            labelPrice.Text = price;
            List = "Non AC";
        }

        private void SomeOtherMethod()
        {
            // Now you can use the 'price' variable here or in other methods
            int num = Convert.ToInt32(price);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.ShowDialog();
        }
        private void GetPrintArea(Panel pnl)
        {
            // Ensure pnl is not null before proceeding
            if (pnl == null)
            {
                // Handle the case where the Panel is null
                return;
            }

            // Dispose of the existing memoryImg if it exists
            memoryImg?.Dispose();

            // Create a new Bitmap
            memoryImg = new Bitmap(pnl.Width, pnl.Height);

            // Use Graphics.FromImage to draw the Panel onto the bitmap
            using (Graphics memoryGfx = Graphics.FromImage(memoryImg))
            {
                // Ensure pnl is visible before drawing
                pnl.Visible = true;

                memoryGfx.CopyFromScreen(pnl.PointToScreen(Point.Empty), Point.Empty, pnl.Size);

                // Restore pnl visibility
                pnl.Visible = false;
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
             string imagePath = @"D:\CODE\C#\WindowsFormsMain\Resources\Ticket.png";
            Image image = Image.FromFile(imagePath);

            // Set the page orientation to Landscape
            e.PageSettings.Landscape = true;

            // Draw the image
            e.Graphics.DrawImage(image, 10, 20, image.Width, image.Height);

            // Dispose of the image to release resources
            image.Dispose();
            // Draw the date
            e.Graphics.DrawString("Date: " + DateTime.Now, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(20, 150));

             /*Draw the panel
             DrawPanel(panel2, e.Graphics);
            Rectangle pageArea = e.PageBounds;
            e.Graphics.DrawImage(memoryImg, (pageArea.Width - this.panel2.Width) / 2, this.panel2.Location.Y);*/

            Rectangle pageArea = e.PageBounds;

            // Calculate the X-coordinate to center the image horizontally
            //int xCoordinate = (pageArea.Width - memoryImg.Width) / 2;

            // Draw the image on the print page
            //e.Graphics.DrawImage(memoryImg, new Point(0, 0));
            if (memoryImg != null)
            {
                // Draw the image at the top-left corner of the panel
                e.Graphics.DrawImage(memoryImg, new Point(200, 0));
            }
            else
            {
                // Handle the case where memoryImg is null
                MessageBox.Show("Error: Image is null");
            }


        }
        /*private void GetPrintArea(Panel pnl)
        {
            // Ensure pnl is not null before proceeding
            if (pnl == null)
            {
                // Handle the case where the Panel is null
                return;
            }

            // Dispose of the existing memoryImg if it exists
            memoryImg?.Dispose();

            // Create a new Bitmap
            memoryImg = new Bitmap(pnl.Width, pnl.Height);

            // Use Graphics.FromImage to draw the Panel onto the bitmap
            using (Graphics memoryGfx = Graphics.FromImage(memoryImg))
            {
                // Ensure pnl is visible before drawing
                pnl.Visible = true;

                memoryGfx.CopyFromScreen(pnl.PointToScreen(Point.Empty), Point.Empty, pnl.Size);

                // Restore pnl visibility
                pnl.Visible = false;
            }
        }*/
        /* private void DrawPanel(Panel panel, Graphics graphics)
         {
             // Customize this method based on how you want to draw the contents of your panel
             // For example:
             graphics.FillRectangle(Brushes.LightGray, panel.ClientRectangle);
             graphics.DrawString("Panel Content", new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(panel.Left + 10, panel.Top + 200));
         }*/
        /*private void Print(Panel pnl)
         {
             PrinterSettings printerSettings = new PrinterSettings();
             panel2 = pnl;
             getprintarea(pnl);
             printPreviewDialog1.Document = printDocument1;
             printPreviewDialog1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
             printPreviewDialog1.ShowDialog();
         }
         private Bitmap memoryimg;
         private void getprintarea(Panel pnl)
         {
             memoryimg= new Bitmap(pnl.Width, pnl.Height);
             pnl.DrawToBitmap(memoryimg,new Rectangle(0, 0 , pnl.Width, pnl.Height));
         }*/
        /* private void Print(Panel pnl)
        {
            PrinterSettings printerSettings = new PrinterSettings();
            panel2 = pnl;
            GetPrintArea(pnl);

            // Assign the PrintPage event handler directly to the PrintDocument
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

            // Set the PrintDocument for the PrintPreviewDialog
            printPreviewDialog1.Document = printDocument1;

            // Show the PrintPreviewDialog
            printPreviewDialog1.ShowDialog();
        }*/

       // private Panel panel2; // Assuming panel2 is a class-level variable
       
        private void Print(Panel pnl)
        {
            if (pnl == null)
            {
                // Handle the case where the Panel is null
                MessageBox.Show("Error: Panel is null");
                return;
            }

            try
            {
                PrinterSettings printerSettings = new PrinterSettings();
                panel2 = pnl;
                GetPrintArea(pnl);

                // Dispose of existing resources
                printDocument1.Dispose();
                printPreviewDialog1.Dispose();

                // Create new instances of printDocument1 and printPreviewDialog1
                printDocument1 = new PrintDocument();
                printPreviewDialog1 = new PrintPreviewDialog();

                // Attach event handler
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

                // Set the PrintDocument for the PrintPreviewDialog
                printPreviewDialog1.Document = printDocument1;

                // Show the PrintPreviewDialog
                printPreviewDialog1.ShowDialog();
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        /* private void GetPrintArea(Panel pnl)
         {
             memoryImg = new Bitmap(pnl.Width, pnl.Height);

             using (Graphics memoryGfx = Graphics.FromImage(memoryImg))
             {
                 memoryGfx.CopyFromScreen(pnl.PointToScreen(Point.Empty), Point.Empty, pnl.Size);
             }
         }*/
    }
}
