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


namespace Kütüphaneprojesi
{
    public partial class Form8 : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=users;Integrated Security=True");

        public Form8()
        {
            InitializeComponent();

            this.Load += Form8_Load;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string searchQuery = textBox1.Text;
            SqlCommand komut = new SqlCommand("SELECT * FROM DenetimKaydi WHERE IslemDetayi LIKE '%" + searchQuery + "%'", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {

                MessageBox.Show("Bakımda");
            }
            else
            {
                MessageBox.Show("Kayıt Bulunamadı.");
            }
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM DenetimKaydi", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 giris = new Form7();
            giris.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
