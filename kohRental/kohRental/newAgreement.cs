using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kohRental
{
    public partial class newAgreement : Form
    {
        string searchProvince = "Ontario";

        Dictionary<string, string> keys = new Dictionary<string, string>();

        public newAgreement()
        {
            InitializeComponent();
        }

        private void newAgreement_Load(object sender, EventArgs e)
        {
            cmbProvince.SelectedIndex = 8; //auto set to ontario
            changeCity(); //loads in the cities

            loadVehicles();

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
            using (WebClient wc = new WebClient())
            {
                string json = wc.DownloadString("https://pastebin.com/raw/5VvHrWxU");
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
                int userID = insertToUser(txtfname.Text, txtlname.Text, txtPhone.Text, txtEmail.Text, txtAddress.Text, cmbCity.Items[cmbCity.SelectedIndex].ToString(), cmbProvince.Items[cmbProvince.SelectedIndex].ToString(), txtCC.Text, dtpExpiry.Value.ToString(), txtCVV.Text);
                if(userID != 0) //if we didnt incounter an error
                {
                    insertToRentals(txtkmout.Text, dtpOut.Value.ToShortDateString(), lblVin.Text, userID);
                } else
                {
                    MessageBox.Show("Could not find last name");
                }
                MessageBox.Show("Created Agreement. Make sure to refresh dashboard", "Success");
                this.Close();
            }
            
        }

        private int insertToUser(string fname, string lname, string phone, string email, string address, string city, string province, string cc, string expiry, string cvv)
        {
            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = dbConnect.GetDBConnection();
            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                string sqlSelectAll = "INSERT INTO users(fname, lname, phone, email, address,city,province,cc,expiry,cvv) VALUES (@fname, @lname, @phone, @email, @address, @city, @province, @cc, @ex, @cvv)";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlSelectAll;
                cmd.Parameters.AddWithValue("@fname", fname);
                cmd.Parameters.AddWithValue("@lname", lname);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@province", province);
                cmd.Parameters.AddWithValue("@cc", cc);
                cmd.Parameters.AddWithValue("@ex", expiry);
                cmd.Parameters.AddWithValue("@cvv", cvv);
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

                Console.WriteLine("Connection successful!");;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
