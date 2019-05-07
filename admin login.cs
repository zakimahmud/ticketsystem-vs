using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Rupnagar_Theme_Park
{
    public partial class admin_login : Form
    {
        public admin_login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection sq = new SqlConnection(@"Data Source=ZAKI\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True");
            sq.Open();
            SqlDataAdapter sda = new SqlDataAdapter("Select count (*) From Administration where USERNAME = '" + textBox1.Text + "' and PASSWORD = '" + textBox2.Text + "'", sq);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                Package_and_Price at = new Package_and_Price();
                at.Show();
            }
            else
            {
                label1.Hide();
                label2.Hide();
                button1.Hide();
                textBox1.Hide();
                textBox2.Hide();

                
                
                pictureBox2.Show();
                label3.Show();
                button2.Show();
            }
        }

        private void admin_login_Load(object sender, EventArgs e)
        {
            pictureBox2.Hide();
            label3.Hide();
            button2.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Show();
            label2.Show();
            button1.Show();
            textBox1.Show();
            textBox2.Show();
            pictureBox2.Hide();
            label3.Hide();
            button2.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
