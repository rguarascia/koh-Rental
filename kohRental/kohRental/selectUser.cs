using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace kohRental
{
    public partial class selectUser : Form
    {
        ArrayList myUsers = new ArrayList();
        int index = -1;
        public bool selected = false;
        public selectUser(ArrayList users)
        {
            this.myUsers = users;
            InitializeComponent();
        }

        private void selectUser_Load(object sender, EventArgs e)
        {
            foreach(string x in myUsers)
            {
                listBox1.Items.Add(x);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selected = true;
            if(listBox1.SelectedIndex != -1)
            {
                index = listBox1.SelectedIndex;
            }
        }

        public int getIndex()
        {
            return index;
        }
    }
}
