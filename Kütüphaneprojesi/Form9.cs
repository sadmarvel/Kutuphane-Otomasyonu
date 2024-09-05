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

namespace Kütüphaneprojesi
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=users;Integrated Security=True");
        SqlCommand komut;
        string komutSatiri;
        private void Form9_Load(object sender, EventArgs e)
        {
            Listele();
        }
        public void Listele()
        {

            komutSatiri = "Select * From log";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komutSatiri, baglanti);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["ip_adresi"].HeaderText = "Modem İp Adresi";
            dataGridView1.Columns["ip_adresi"].Width = 100;
            dataGridView1.Columns["tarih"].HeaderText = "Tarihi";
            dataGridView1.Columns["tarih"].Width = 300;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form7 giris = new Form7();
            giris.ShowDialog();
            this.Close();
        }
    }
}
