using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Invoicer.Services;
using Invoicer.Models;
using Invoicer.Helpers;

namespace kohRental
{
    public partial class closeAgreement : Form
    {
        string searchProvince = "Ontario";
        string lname = "";
        int useIndex;
        ArrayList users = new ArrayList();

        Dictionary<string, string> keys = new Dictionary<string, string>();

        public closeAgreement(string lastName)
        {
            this.lname = lastName;
            InitializeComponent();
            ToolTip tip = new ToolTip();
            tip.SetToolTip(btnClose, "Print screen to follow");
        }

        private void newAgreement_Load(object sender, EventArgs e)
        {
            if (!lookupLastName(lname))
            {
                MessageBox.Show("Last name not found");
                this.Close();
            }
            else
            {
                loadUser(useIndex);
                cmbProvince.SelectedIndex = 8; //auto set to ontario
                changeCity(); //loads in the cities
                loadVehicles(); //loads in the vehicles
            }
        }

        private void loadUser(int useIndex)
        {

            string currentUser;
            if (useIndex != 0)
                currentUser = users[useIndex - 1].ToString();
            else
                currentUser = users[useIndex].ToString();

            string fname = currentUser.Split('-')[0].Trim();
            string lname = currentUser.Split('-')[1].Trim();
            string phone = currentUser.Split('-')[2].Trim();

            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = dbConnect.GetDBConnection();
            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                string sqlSelectAll = "select * from users where fname = @fname AND lname = @lname AND phone = @phone";

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlSelectAll;
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@phone", phone);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    txtfname.Text = reader.GetString(1);
                    txtlname.Text = reader.GetString(2);
                    txtPhone.Text = reader.GetString(3);
                    txtEmail.Text = reader.GetString(4);
                    txtAddress.Text = reader.GetString(5);
                    cmbCity.SelectedIndex = cmbCity.FindStringExact(reader.GetString(6));
                    cmbProvince.SelectedIndex = cmbProvince.FindStringExact(reader.GetString(7));
                }

                Console.WriteLine("Connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private bool lookupLastName(string last)
        {
            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = dbConnect.GetDBConnection();
            int counter = 0;

            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                string sqlSelectAll = "SELECT * from users where lname = @lname";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlSelectAll;
                cmd.Parameters.AddWithValue("@lname", last);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    counter++;
                    users.Add(reader.GetString(1) + " - " + reader.GetString(2) + " - " + reader.GetString(3));
                }

                Console.WriteLine("Connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine(counter);
            if (counter == 1)
            {
                useIndex = 0;
                return true;
            }
            else if (counter > 1)
            {
                string output = "";
                int c = 0;
                foreach (string x in users)
                {
                    output += ++c + ": " + x + "\n";
                }

                useIndex = Int32.Parse(Microsoft.VisualBasic.Interaction.InputBox("Please select one of the following users\n" + output, "Multi user select"));
                return true;
            }
            else
                return false;
        }
        private void loadVehicles()
        {
            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = dbConnect.GetDBConnection();

            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                string sqlSelectAll = "SELECT * from vehicles";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlSelectAll;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cmbVehicle.Items.Add(reader.GetString(3) + " - " + reader.GetString(1));
                    keys.Add(reader.GetString(3), reader.GetString(0)); // Stock # key Vin Value
                }

                Console.WriteLine("Connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            cmbVehicle.SelectedIndex = 0;

        }

        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProvince.SelectedIndex != -1)
            {
                searchProvince = cmbProvince.Items[cmbProvince.SelectedIndex].ToString();
            }
            changeCity();
            Console.WriteLine("Searching w/ " + searchProvince);
        }

        private void changeCity()
        {
            cmbCity.Items.Clear();
            using (StreamReader r = new StreamReader("ca.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var x in array)
                {
                    if (x.admin == searchProvince)
                    {
                        cmbCity.Items.Add(x.city);
                        cmbCity.SelectedIndex = 0;
                    }
                }
            }

            //changes it to hamilton because that is the orgin city
            if (searchProvince.Equals("Ontario"))
                cmbCity.SelectedIndex = cmbCity.FindStringExact("Hamilton");
        }

        private void cmbVehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVehicle.SelectedIndex != -1)
            {
                string stock = cmbVehicle.Items[cmbVehicle.SelectedIndex].ToString().Split('-')[0].Trim();
                Console.WriteLine(stock);
                lblVin.Text = keys[stock];
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            bool flag = false;
            foreach (Control c in Controls)
                if (c is TextBox)
                    if (c.Enabled == true)
                        if (c.Text == "")
                            flag = true;
            if (flag)
                MessageBox.Show("Please fill out all values");
            else
            {
                //insert into databases
                int userID = insertToUser(txtfname.Text, txtlname.Text, txtPhone.Text, txtEmail.Text, txtAddress.Text, cmbCity.Items[cmbCity.SelectedIndex].ToString(), cmbProvince.Items[cmbProvince.SelectedIndex].ToString());
                if (userID != 0) //if we didnt incounter an error
                {
                    insertToRentals(txtkmout.Text, dtpOut.Value.ToShortDateString(), lblVin.Text, userID);
                }
            }

        }

        private int insertToUser(string fname, string lname, string phone, string email, string address, string city, string province)
        {
            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = dbConnect.GetDBConnection();
            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                string sqlSelectAll = "INSERT INTO users(fname, lname, phone, email, address,city,province) VALUES (@fname, @lname, @phone, @email, @address, @city, @province)";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlSelectAll;
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@province", province);
                cmd.ExecuteNonQuery();

                Console.WriteLine("Connection successful!");

                return (int)cmd.LastInsertedId;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return 0; //we had an error
        }

        private void insertToRentals(string km, string date, string vin, int userid)
        {
            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = dbConnect.GetDBConnection();
            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                string sqlSelectAll = "INSERT INTO rentals(kmOut, dateOut, VIN, userID, status) VALUES (@km, @date, @vin, @id, @status)";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlSelectAll;
                cmd.Parameters.AddWithValue("@km", km);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@vin", vin);
                cmd.Parameters.AddWithValue("@ID", userid);
                cmd.Parameters.AddWithValue("@status", "OPEN");
                cmd.ExecuteNonQuery();

                Console.WriteLine("Connection successful!"); ;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] name = new string[4] { "Kia of Hamilton", "1885 Upper James Street", "Hamilton, Ontario, Cananada", "L9B1B8" };
            new InvoicerApi(SizeOption.A4, OrientationOption.Portrait, "$")
                .TextColor("#CC0000")
                .BackColor("#FFD6CC")
                .Image(@"logo.jpg", 90, 80)
                .Company(Address.Make("FROM", name))
                .Client(Address.Make("BILLING TO", name))
                .Items(new List<ItemRow> {
                    ItemRow.Make("2018 Soul", "Car rental",3,30, 90), //stock
                })
                .Totals(new List<TotalRow> {
                    TotalRow.Make("Total", (decimal)631.99, true),
                })
                .Details(new List<DetailRow> {
                    DetailRow.Make("RENTAL INFORMATION", "\n1. I am the only driver authorized by Kia of Hamilton to operate the Vehicle unless others are listed below. The Vehicle is in good and safe mechanical condition and I shall return the Vehicle in the same condition. If applicable, list other authorized drivers here . \n\n 2. I shall obey all Federal, Provincial and Municipal laws, rules and regulations relating to the operation and use of the Vehicle and I shall indemnify Kia of Hamilton for any infractions of such laws, rules and regulations.\n\n 3. I shall pay for all loss or damage to the Vehicle whether or not caused by my negligence. \n\n 4. I have confirmed with my insurance company that this Vehicle is covered under my insurance policy as a substitute vehicle. Insurance Company Name: Policy Number: . \n\n 5. For consideration in the amount of $10.00 which is acknowledged to have been received by Kia of Hamilton, the Vehicle shall be considered to be a Leased Vehicle and I and any other authorized drivers of the Vehicle ('Authorized Drivers') shall be considered to be Lessees for the purposes of determining the order of third party liability established by Section 277 of the Insurance Act R.S.O. 1990, c. I. 8.\n\n 6. I understand that I am being provided with the Vehicle for normal personal use only and will not allow the Vehicle to be used for commercial purposes or for hire, or for any illegal purpose. \n\n 7. During the time that I am authorized to use the Vehicle I shall not operate it in an area outside a 100 km radius from Kia of Hamilton’s location and in no event shall I allow the Vehicle to be driven outside of Ontario without the express written consent of Kia of Hamilton. A charge of 10 cents per km will apply if the Vehicle is driven more than km per day that I am in possession of the Vehicle. \n\n 8. I shall be responsible to pay all fees, tolls and costs of operating the Vehicle while in my custody and shall return the Vehicle with a full tank of gas at my expense. \n\n 9. I shall indemnify Kia of Hamilton against all other liabilities, losses, costs, damages, fines, fees, tolls and expenses of any kind arising while the Vehicle is in my custody. \n\n 10. I shall return the Vehicle to the location where I received it no later than or forthwith upon demand.\n\n 11. I agree to provide Kia of Hamilton with a credit card imprint in my name which I hereby authorize to be processed for a deposit immediately of $ and any additional sum for which I might become liable under this authorization.\n\n / VISA MASTERCARD Name on Card Card # Exp. Date 12. I agree to pay and authorize Kia of Hamilton to process any applicable credit card voucher for advance deposits and all charges incurred including parking violations, traffic violations, toll charges, damages to and theft of the Vehicle. \n\n 13. I acknowledge, in providing a copy of my driver’s licence and number that Kia of Hamilton at its discretion will run a driver’s abstract to ensure that I am legally permitted to operate the Vehicle. \n\n", "", "", "Thank you for your business.")
                })
                .Footer("http://www.kiaofhamilton.com")
                .Save();
        }
    }
}
