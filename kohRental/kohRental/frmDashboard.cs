using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kohRental
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = dbConnect.GetDBConnection();

            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                string sqlSelectAll = "SELECT * from rentals";
                MyDA.SelectCommand = new MySqlCommand(sqlSelectAll, conn);

                DataTable table = new DataTable();
                MyDA.Fill(table);

                BindingSource bSource = new BindingSource();
                bSource.DataSource = table;

                dgvViewOpen.DataSource = bSource;

                Console.WriteLine("Connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            int r = dgvViewOpen.Rows.Count;
            for (int x = 0; x < r; x++)
            {
                if (dgvViewOpen.Rows[x].Cells[7].Value.ToString() == "OPEN")
                    dgvViewOpen.Rows[x].Cells[7].Style.BackColor = Color.Red;
                else
                    dgvViewOpen.Rows[x].Cells[7].Style.BackColor = Color.ForestGreen;

                dgvViewOpen.Columns[0].Width = 45;
                dgvViewOpen.Columns[1].Width = 45;
                dgvViewOpen.Columns[2].Width = 140;

            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("KOH Rental Agreement Software V1.2.0\nCopyright Kia of Hamilton 2018\nRyan Guarascia Productions", "Kia of Hamilton Rental Agreements", MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            newAgreement newAgreement = new newAgreement();
            newAgreement.Show();
        }

        private void dgvViewOpen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvViewOpen.SelectedCells.Count > 0)
            {
                btnView.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter custom last name", "Query");

            if(input != "")
            {
                closeAgreement closeAgreement = new closeAgreement(input);
                closeAgreement.Show();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            int row = dgvViewOpen.CurrentCell.RowIndex;
            int userID = 0;

            if(dgvViewOpen[7,row].Value.ToString() == "CLOSED")
            {
                userID = Int32.Parse(dgvViewOpen[1, row].Value.ToString());
                viewAgreemnt viewAgreemnt = new viewAgreemnt(getLastName(userID));
                viewAgreemnt.Show();
            } else
            {
                MessageBox.Show("Cannot view open rental");
            }
        }

        private string getLastName(int id)
        {
            string lastName = "";
            Console.WriteLine("Getting Connection ...");
            MySqlConnection conn = dbConnect.GetDBConnection();

            try
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();
                MySqlDataAdapter MyDA = new MySqlDataAdapter();
                string sqlSelectAll = "SELECT * from users WHERE userID = @id";
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sqlSelectAll;
                cmd.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lastName = reader.GetString(2);
                }

                Console.WriteLine("Connection successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return lastName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
            Environment.Exit(0);
            Environment.Exit(0);
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmMain_Load(sender, e);
        }
    }
}
