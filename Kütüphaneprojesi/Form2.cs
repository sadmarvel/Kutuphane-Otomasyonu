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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("server=.; Initial Catalog=kutuphane;Integrated Security=SSPI");

        SqlCommand komut;
        string komutSatiri;
        private void Form2_Load(object sender, EventArgs e)
        {
            Listele();
          
        }
        public void Listele()
        {
            try
            {
                komutSatiri = "Select * From ogrenciIslemleri";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(komutSatiri, baglanti);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["okulNo"].HeaderText = "Öğrenci Numarası";
                dataGridView1.Columns["ad"].HeaderText = "Ad";
                dataGridView1.Columns["soyad"].HeaderText = "Soyad";
                dataGridView1.Columns["sinif"].HeaderText = "Sınıf";
                dataGridView1.Columns["cinsiyet"].HeaderText = "Cinsiyet";
                dataGridView1.Columns["telefon"].HeaderText = "Telefon";
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hata oluştu", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        public void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int no = int.Parse(textBox1.Text);
                string ad = textBox2.Text;
                string soyad = textBox3.Text;
                string sinif = comboBox1.SelectedItem.ToString();
                string cinsiyet = comboBox2.SelectedItem.ToString();
                string telefon = textBox5.Text;

                SqlCommand islemyap = new SqlCommand("INSERT INTO ogrenciIslemleri (okulNo, ad, soyad, sinif, cinsiyet, telefon) VALUES(@no, @ad, @soyad, @sinif, @cinsiyet, @telefon)", baglanti);

                islemyap.Parameters.AddWithValue("@no", no);
                islemyap.Parameters.AddWithValue("@ad", ad);
                islemyap.Parameters.AddWithValue("@soyad", soyad);
                islemyap.Parameters.AddWithValue("@sinif", sinif);
                islemyap.Parameters.AddWithValue("@cinsiyet", cinsiyet);
                islemyap.Parameters.AddWithValue("@telefon", telefon);

                baglanti.Open();
                islemyap.ExecuteNonQuery();
                baglanti.Close();

                Temizle();
                MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();
            }
                
            
                     catch (Exception ex) { MessageBox.Show(ex.Message, "Hata oluştu"); }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            OgrenciArama(textBox4.Text);
        }
        public void OgrenciArama(string aranacakKelime)
        {
            try
            {
                baglanti.Open();
                komut = new SqlCommand();
                komut.Connection = baglanti;
                komut.CommandText = "Select * From ogrenciIslemleri Where ad LIKE '" + aranacakKelime + "%'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(komut);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                baglanti.Close();
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hata oluştu"); }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells["okulNo"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["ad"].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells["soyad"].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
                comboBox1.SelectedItem = dataGridView1.CurrentRow.Cells["sinif"].Value.ToString();
           
                comboBox2.SelectedItem = dataGridView1.CurrentRow.Cells["cinsiyet"].Value.ToString();
            
            }
            catch 
            {
                MessageBox.Show("Hata oluştu", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
            komutSatiri = "DELETE FROM ogrenciIslemleri WHERE okulNo = @no";
            komut = new SqlCommand(komutSatiri, baglanti);
            komut.Parameters.AddWithValue("@no", dataGridView1.CurrentRow.Cells["okulNo"].Value.ToString());
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close  ();
            Temizle();
            MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
           
                komutSatiri = "UPDATE ogrenciIslemleri SET ad=@ad, soyad=@soyad,sinif=@sinif, cinsiyet=@cinsiyet, telefon=@telefon where okulNo=@no";
                komut = new SqlCommand(komutSatiri, baglanti);
                komut.Parameters.AddWithValue("@no", int.Parse(dataGridView1.CurrentRow.Cells["okulNo"].Value.ToString()));
                komut.Parameters.AddWithValue("@ad", textBox2.Text);
                komut.Parameters.AddWithValue("@soyad", textBox3.Text);
                komut.Parameters.AddWithValue("@sinif", int.Parse(comboBox1.SelectedItem.ToString()));
                komut.Parameters.AddWithValue("@cinsiyet", comboBox2.SelectedItem.ToString());
                komut.Parameters.AddWithValue("@telefon", textBox5.Text);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close ();
                Temizle();
                MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();




            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hata oluştu"); }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 kitap = new Form1();
            kitap.ShowDialog();
            this.Close();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
