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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Rupnagar_Theme_Park
{
    public partial class Package3 : Form
    {
        public Package3()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=ZAKI\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True");
        private void Package3_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'loginDataSet7.Ticket' table. You can move, or remove it, as needed.
            //this.ticketTableAdapter.Fill(this.loginDataSet7.Ticket);
            //// TODO: This line of code loads data into the 'loginDataSet2.Ticket_entry' table. You can move, or remove it, as needed.
            //this.ticket_entryTableAdapter.Fill(this.loginDataSet2.Ticket_entry);

            int a;
            
            con.Open();
            string query = "Select Max(Serial_no) from Ticket";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                string val = dr[0].ToString();
                if (val == "")
                {
                    textBox4.Text = "1";
                }
                else
                {
                    a = Convert.ToInt32(dr[0].ToString());
                    a = a + 1;
                    textBox4.Text = a.ToString();

                }
                con.Close();
            }
            //dataGridView1.Rows.Clear();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Package_and_Price s1 = new Package_and_Price();
            s1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Total_Price
            double sum4 = 0, total, discount, qty;

            qty = double.Parse(textBox3.Text);
            total = double.Parse(textBox2.Text);
            discount = double.Parse(textBox5.Text);
            sum4 = ((total - ((discount / 100) * total)) * qty);




            dataGridView1.Rows[0].Cells[0].Value = dateTimePicker1.Text;
            dataGridView1.Rows[0].Cells[1].Value = textBox4.Text;
            dataGridView1.Rows[0].Cells[2].Value = textBox1.Text;
            dataGridView1.Rows[0].Cells[3].Value = textBox2.Text;
            dataGridView1.Rows[0].Cells[4].Value = textBox3.Text;
            dataGridView1.Rows[0].Cells[5].Value = sum4.ToString();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            double sum4 = 0, total, discount, qty, cash, sum3 = 0;
            cash = double.Parse(textBox6.Text);
            qty = double.Parse(textBox3.Text);
            total = double.Parse(textBox2.Text);
            discount = double.Parse(textBox5.Text);
            sum4 = ((total - ((discount / 100) * total)) * qty);



            sum3 = (cash - sum4);
            textBox7.Text = sum3.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Please Input Product List...!!!");
            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Ticket VALUES('" + dataGridView1.Rows[i].Cells[0].Value + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "')", con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Saved!!");
                    con.Close();
                }


                textBox3.Text = "";


                int a;
                //string str = (@"Data Source=ZAKI\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True");
                //SqlConnection con = new SqlConnection(str);
                con.Open();
                string query = "Select Max(Serial_no) from Ticket";
                SqlCommand cd = new SqlCommand(query, con);
                SqlDataReader dr;
                dr = cd.ExecuteReader();
                if (dr.Read())
                {
                    string val = dr[0].ToString();
                    if (val == "")
                    {
                        textBox4.Text = "1";
                    }
                    else
                    {
                        a = Convert.ToInt32(dr[0].ToString());
                        a = a + 1;
                        textBox4.Text = a.ToString();

                    }
                    con.Close();
                }
                //dataGridView1.Rows.Clear();

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter pdf = PdfWriter.GetInstance(doc, new FileStream("Bill.pdf", FileMode.Create));
            doc.Open();
            //image
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("Logo.png");
            //Resize image
            logo.ScalePercent(70f);
            doc.Add(logo);
            Paragraph head = new Paragraph("   Ticket:");
            head.IndentationLeft = 250f;
            doc.Add(head);
            Paragraph p = new Paragraph("   ...............................................................................................................................................................................");
            doc.Add(p);

            Paragraph Inv = new Paragraph("   Serial No:" + textBox4.Text + "\n");
            doc.Add(Inv);

            Paragraph Date = new Paragraph("   Date:" + dateTimePicker1.Text + "\n");
            doc.Add(Date);

            Paragraph pkg = new Paragraph("   Package Name:" + textBox1.Text + "\n");
            doc.Add(pkg);


            Paragraph s = new Paragraph("\n\n");
            doc.Add(s);
            PdfPTable table = new PdfPTable(dataGridView1.Columns.Count);
            //add header for the pdf
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                table.AddCell(new Phrase(dataGridView1.Columns[j].HeaderText));
            }
            table.HeaderRows = 1;
            //Add Rows from datagridview
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int k = 0; k < dataGridView1.Columns.Count; k++)
                {
                    if (dataGridView1[k, i].Value != null)
                    {
                        table.AddCell(new Phrase(dataGridView1[k, i].Value.ToString()));
                    }
                }
            }
            doc.Add(table);

            Paragraph csh = new Paragraph("   CASH:" + textBox6.Text);
            csh.IndentationLeft = 50f;
            doc.Add(csh);
            Paragraph dis = new Paragraph("   Discount:" + textBox5.Text+ "%");
            dis.IndentationLeft = 50f;
            doc.Add(dis);
            Paragraph sub = new Paragraph("         Sub Total:" + dataGridView1.Rows[0].Cells[5].Value);
            dis.IndentationLeft = 50f;
            doc.Add(sub);
            Paragraph Change = new Paragraph("   Change Due:" + textBox7.Text);
            Change.IndentationLeft = 50f;
            doc.Add(Change);
            Paragraph s1 = new Paragraph("\n\n\n");
            doc.Add(s1);


            doc.Add(s);
            Paragraph line = new Paragraph("---------------------------------------------------Thank You for Your Custom------------------------------------------------");
            doc.Add(line);

            doc.Close();

            System.Diagnostics.Process.Start("Bill.pdf");
        }
    }
}
