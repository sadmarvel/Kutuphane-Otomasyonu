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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("server=.; Initial Catalog=kutuphane;Integrated Security=SSPI");

        SqlCommand komut;
        string komutSatiri;
        private void Form4_Load(object sender, EventArgs e)
        {
            KitapTurYukle();
            KitapListele();

        }
        public void KitapTurYukle()
        {
            try
            {
                komutSatiri = "Select * From turIslemleri";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(komutSatiri, baglanti);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                comboBox1.DataSource = dataTable;
                comboBox1.ValueMember = "turId";
                comboBox1.DisplayMember = "turAdi";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        public void KitapListele()
        {
            komutSatiri = "select kitapId,turAdi,kitapAdi,yazar,yayinevi,sayfaSayisi From kitaplar,turIslemleri where kitaplar.turId=turIslemleri.turId";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komutSatiri, baglanti);
            DataTable datatable = new DataTable();
            dataAdapter.Fill(datatable);
            dataGridView1.DataSource = datatable;
            dataGridView1.Columns["kitapId"].HeaderText = "ID";
            dataGridView1.Columns["kitapId"].Width = 20;
            dataGridView1.Columns["turAdi"].HeaderText = "Tür";
            dataGridView1.Columns["turAdi"].Width = 30;
            dataGridView1.Columns["kitapAdi"].HeaderText = "Adı";
            dataGridView1.Columns["kitapAdi"].Width = 90;
            dataGridView1.Columns["yazar"].HeaderText = "Yazar";
            dataGridView1.Columns["yazar"].Width = 80;
            dataGridView1.Columns["yayinevi"].HeaderText = "Yayınevi";
            dataGridView1.Columns["yayinevi"].Width = 80;
            dataGridView1.Columns["sayfaSayisi"].HeaderText = "Sayfa Sayısı";
            dataGridView1.Columns["sayfaSayisi"].Width = 50;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "insert into kitaplar (turId,kitapAdi,yazar,yayinevi,sayfaSayisi) values(@turId,@kitapAdi,@yazar,@yayinevi,@sayfaSayisi)";
            komut.Parameters.AddWithValue("@turId", int.Parse(comboBox1.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@kitapAdi", textBox1.Text);
            komut.Parameters.AddWithValue("@yazar", textBox2.Text);
            komut.Parameters.AddWithValue("@yayinevi", textBox3.Text);
            komut.Parameters.AddWithValue("@sayfaSayisi", int.Parse(textBox4.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Temizle();
            MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            KitapListele();

        }
        public void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "delete from kitaplar where kitapId=@kitapId";
            komut.Parameters.AddWithValue("@kitapId", dataGridView1.CurrentRow.Cells["kitapId"].Value.ToString());
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Temizle();
            MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            KitapListele();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells["kitapAdi"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["sayfaSayisi"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["yayinevi"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["yazar"].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells["turAdi"].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand();
            komut.Connection = baglanti;
            komut.CommandText = "update kitaplar set turId=@turId,kitapAdi=@kitapAdi,yazar=@yazar,yayinevi=@yayinevi,sayfaSayisi=@sayfaSayisi where kitapId=@kitapId";
            komut.Parameters.AddWithValue("@kitapId", int.Parse(dataGridView1.CurrentRow.Cells["kitapId"].Value.ToString()));
            komut.Parameters.AddWithValue("@turId", int.Parse(comboBox1.SelectedValue.ToString()));
            komut.Parameters.AddWithValue("@kitapAdi", textBox1.Text);
            komut.Parameters.AddWithValue("@yazar", textBox2.Text);
            komut.Parameters.AddWithValue("@yayinevi", textBox3.Text);
            komut.Parameters.AddWithValue("@sayfaSayisi", int.Parse(textBox4.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            Temizle();
            MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            KitapListele();

        }
        public void kitapArama(string aranacakKelime)
        {
            komutSatiri = "select kitapId,turAdi,kitapAdi,yazar,yayinevi,sayfaSayisi from kitaplar,turIslemleri where kitaplar.turId=turIslemleri.turId and kitapAdi LIKE '" + aranacakKelime + "%' ";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(komutSatiri, baglanti);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            baglanti.Close();
            dataGridView1.DataSource = dataTable;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            kitapArama(textBox5.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 kitap = new Form1();
            kitap.ShowDialog();
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

}

