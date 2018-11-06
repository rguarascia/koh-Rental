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
using System.Diagnostics;
using System.Net;
using System.Globalization;

namespace kohRental
{
    public partial class viewAgreemnt : Form
    {
        string searchProvince = "Ontario";
        string lname = "";
        int useIndex;
        int userID = 0;
        ArrayList users = new ArrayList();

        Dictionary<string, string> keys = new Dictionary<string, string>();
        private int totalDaysRented;

        public viewAgreemnt(int id)
        {
            this.userID = id;
            InitializeComponent();
            ToolTip tip = new ToolTip();
            tip.SetToolTip(btnClose, "Print screen to follow");
        }

        private void newAgreement_Load(object sender, EventArgs e)
        {

                loadUser(userID);
                cmbProvince.SelectedIndex = 8; //auto set to ontario
                changeCity(); //loads in the cities
                loadVehicles(); //loads in the vehicles
            
        }

        private void loadUser(int id)
        {
            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = dbConnect.GetDBConnection();
            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();
                string sqlSelectAll = "select * from users WHERE userID = @id";

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlSelectAll;
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    userID = reader.GetInt32(0);
                    txtfname.Text = reader.GetString(1);
                    txtlname.Text = reader.GetString(2);
                    txtPhone.Text = reader.GetString(3);
                    txtEmail.Text = reader.GetString(4);
                    txtAddress.Text = reader.GetString(5);
                    cmbCity.SelectedIndex = cmbCity.FindStringExact(reader.GetString(6));
                    cmbProvince.SelectedIndex = cmbProvince.FindStringExact(reader.GetString(7));
                    txtCC.Text = reader.GetString(8);
                    txtMM.Text = DateTime.Parse(reader.GetString(9)).Month.ToString();
                    txtYY.Text = DateTime.Parse(reader.GetString(9)).Year.ToString();
                    txtCVV.Text = reader.GetString(10);
                }
                reader.Close();

                sqlSelectAll = "select * from rentals where userID = @id";

                cmd = conn.CreateCommand();
                cmd.CommandText = sqlSelectAll;
                cmd.Parameters.AddWithValue("@id", id);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    txtkmout.Text = reader.GetString(4);
                    if(reader.GetString(6) == "0")
                        txtkmin.Text = "Still out";
                    else 
                        txtkmin.Text = reader.GetString(6);
                    dtpOut.Value = DateTime.Parse(reader.GetString(3));
                    if (reader.GetString(5) == "0")
                        dtpIn.Value = DateTime.Today;
                    else
                        dtpIn.Value = DateTime.Parse(reader.GetString(5));
                }

                Console.WriteLine("Connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
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

        bool flag = false;
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if(!flag)
            {
                totalDaysRented = calculateDays();
                preformInvoice();
            } else
            {
                MessageBox.Show("Please make sure all text boxes are filled");
            }
        }


        private int calculateDays()
        {
            DateTime oDate = dtpOut.Value;
            DateTime inDate = dtpIn.Value;

            int daysRented = (inDate - oDate).Days;
            if(daysRented < 0)
            {
                flag = true;
                MessageBox.Show("Please make sure date is after out Date");
            } else if (daysRented == 0)
            {
                MessageBox.Show("You have selected today. No rental cost proceed");
            }

            return daysRented;
        }

        private void preformInvoice()
        {
            int cost = totalDaysRented * 30;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            saveFileDialog.ShowDialog();
            string filePath = saveFileDialog.FileName;

            string[] name = new string[4] { "Kia of Hamilton", "1885 Upper James Street", "Hamilton, Ontario, Cananada", "L9B1B8" };
            string[] customer = new string[4] { txtfname.Text + " " + txtlname.Text , txtAddress.Text, cmbCity.Items[cmbCity.SelectedIndex] + ", " + cmbProvince.Items[cmbProvince.SelectedIndex], txtPhone.Text};


            new InvoicerApi(SizeOption.A4, OrientationOption.Portrait, "$")
                .TextColor("#CC0000")
                .BackColor("#FFD6CC")
                .Image(@"logo.jpg", 90, 80)
                .Company(Address.Make("FROM", name))
                .Client(Address.Make("BILLING TO", customer))
                .Items(new List<ItemRow> {
                    ItemRow.Make("2018 Soul", "Car rental",totalDaysRented,30, cost), 
                })
                .Totals(new List<TotalRow> {
                    TotalRow.Make("Total", cost, true),
                })
                .Details(new List<DetailRow> {
                    DetailRow.Make("RENTAL INFORMATION", "\n1. I am the only driver authorized by Kia of Hamilton to operate the Vehicle unless others are listed below. The Vehicle is in good and safe mechanical condition and I shall return the Vehicle in the same condition. If applicable, list other authorized drivers here . \n\n 2. I shall obey all Federal, Provincial and Municipal laws, rules and regulations relating to the operation and use of the Vehicle and I shall indemnify Kia of Hamilton for any infractions of such laws, rules and regulations.\n\n 3. I shall pay for all loss or damage to the Vehicle whether or not caused by my negligence. \n\n 4. I have confirmed with my insurance company that this Vehicle is covered under my insurance policy as a substitute vehicle. Insurance Company Name: Policy Number: . \n\n 5. For consideration in the amount of $10.00 which is acknowledged to have been received by Kia of Hamilton, the Vehicle shall be considered to be a Leased Vehicle and I and any other authorized drivers of the Vehicle ('Authorized Drivers') shall be considered to be Lessees for the purposes of determining the order of third party liability established by Section 277 of the Insurance Act R.S.O. 1990, c. I. 8.\n\n 6. I understand that I am being provided with the Vehicle for normal personal use only and will not allow the Vehicle to be used for commercial purposes or for hire, or for any illegal purpose. \n\n 7. During the time that I am authorized to use the Vehicle I shall not operate it in an area outside a 100 km radius from Kia of Hamilton’s location and in no event shall I allow the Vehicle to be driven outside of Ontario without the express written consent of Kia of Hamilton. A charge of 10 cents per km will apply if the Vehicle is driven more than km per day that I am in possession of the Vehicle. \n\n 8. I shall be responsible to pay all fees, tolls and costs of operating the Vehicle while in my custody and shall return the Vehicle with a full tank of gas at my expense. \n\n 9. I shall indemnify Kia of Hamilton against all other liabilities, losses, costs, damages, fines, fees, tolls and expenses of any kind arising while the Vehicle is in my custody. \n\n 10. I shall return the Vehicle to the location where I received it no later than or forthwith upon demand.\n\n 11. I agree to provide Kia of Hamilton with a credit card imprint in my name which I hereby authorize to be processed for a deposit immediately of $ and any additional sum for which I might become liable under this authorization.\n\n 12. Card Number: " +  txtCC.Text + " Expiry: " + txtMM.Text + "/" + txtYY.Text + " CVC: " + txtCVV.Text + " \nI agree to pay and authorize Kia of Hamilton to process any applicable credit card voucher for advance deposits and all charges incurred including parking violations, traffic violations, toll charges, damages to and theft of the Vehicle. \n\n 13. I acknowledge, in providing a copy of my driver’s licence and number that Kia of Hamilton at its discretion will run a driver’s abstract to ensure that I am legally permitted to operate the Vehicle. \n\n", "", "", "Thank you for your business.")
                })
                .Footer("http://www.kiaofhamilton.com | The Power to Surprise")
                .Save(filePath);
            Process.Start(filePath);


        }
    }
}
