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
using System.Data.SqlClient;

namespace Kütüphaneprojesi
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("server=.; Initial Catalog=kutuphane;Integrated Security=SSPI");

        SqlCommand komut;
        string komutSatiri;
        private void Form3_Load(object sender, EventArgs e)
        {
            TurleriListele();
        }
        public void TurleriListele()
        {
            try
            {
                komutSatiri = "Select * From turIslemleri";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(komutSatiri, baglanti);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["turId"].HeaderText = "ID";
                dataGridView1.Columns["turId"].Width = 100;
                dataGridView1.Columns["turAdi"].HeaderText = "Tür Adı";
                dataGridView1.Columns["turAdi"].Width = 300;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "INSERT INTO turIslemleri (turAdi) VALUES(@turAdi)";
                komut.Parameters.AddWithValue("@turAdi", textBox1.Text);

                komut.ExecuteNonQuery();
                textBox1.Clear();
                MessageBox.Show("İşlem Başarılı", "Mesaj",  MessageBoxButtons.OK, MessageBoxIcon.Information);
                baglanti.Close();

                TurleriListele();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells["turAdi"].Value.ToString();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "DELETE FROM turIslemleri WHERE turId = @turId";

                komut.Parameters.AddWithValue("@turId", dataGridView1.CurrentRow.Cells["turId"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                textBox1.Clear();
                MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TurleriListele();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "UPDATE turIslemleri SET turAdi=@turAdi where turId=@turId";

                komut.Parameters.AddWithValue("@turAdi", textBox1.Text);
                komut.Parameters.AddWithValue("@turId", int.Parse(dataGridView1.CurrentRow.Cells["turId"].Value.ToString()));
                komut.ExecuteNonQuery();
                baglanti.Close();
                textBox1.Clear();
                MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TurleriListele();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 kitap = new Form1();
            kitap.ShowDialog();
            this.Close();
           
        }
    }
}
