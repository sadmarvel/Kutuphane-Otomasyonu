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
    public partial class Form5 : Form
    {
        SqlConnection baglanti = new SqlConnection("server=.; Initial Catalog=kutuphane;Integrated Security=SSPI");

        SqlCommand komut;
        string komutSatiri;
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Listele();
            KitapYukle();
        }
        public void KitapYukle()
        {
            try
            {
                komutSatiri = "select * from kitaplar where kitapId not in (select kitapId from oduncIslemleri where teslimTarihi IS NULL)";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(komutSatiri, baglanti);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                comboBox1.DataSource = dataTable;
                comboBox1.ValueMember = "kitapId";
                comboBox1.DisplayMember = "kitapAdi";
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Hata oluştu");
            }
            
        }
        public void Listele()
        {
            try
            {
                komutSatiri = "Select id,okulNo,ad,soyad,kitapAdi,verilisTarihi,teslimTarihi,aciklama " +
                    "From kitaplar,ogrenciIslemleri,oduncIslemleri " +
                    "where okulNo=ogrenciNo and kitaplar.kitapId=oduncIslemleri.kitapId";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(komutSatiri, baglanti);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

                dataGridView1.Columns["id"].HeaderText = "ID";
                dataGridView1.Columns["id"].Width = 30;
                dataGridView1.Columns["okulNo"].HeaderText = "Öğrenci No";
                dataGridView1.Columns["okulNo"].Width = 40;
                dataGridView1.Columns["ad"].HeaderText = "Ad";
                dataGridView1.Columns["ad"].Width =70;
                dataGridView1.Columns["soyad"].HeaderText = "Soyad";
                dataGridView1.Columns["soyad"].Width = 70;
                dataGridView1.Columns["kitapAdi"].HeaderText = "Kitap Adı";
                dataGridView1.Columns["kitapAdi"].Width = 100;
                dataGridView1.Columns["verilisTarihi"].HeaderText = "Veriliş Tarihi";
                dataGridView1.Columns["verilisTarihi"].Width = 70;
                dataGridView1.Columns["teslimTarihi"].HeaderText = "Teslim Tarihi";
                dataGridView1.Columns["teslimTarihi"].Width = 70;
                dataGridView1.Columns["aciklama"].HeaderText = "Açıklama";
                dataGridView1.Columns["aciklama"].Width = 150;






            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State != ConnectionState.Open) baglanti.Open();
                komut = new SqlCommand();
                komut.Connection = baglanti;

                komut.CommandText = "SELECT COUNT(*) FROM ogrenciIslemleri WHERE okulNo = @ogrenciNo";
                komut.Parameters.AddWithValue("@ogrenciNo", int.Parse(textBox1.Text));
                int ogrenciSayisi = (int)komut.ExecuteScalar();

                if (ogrenciSayisi == 0)
                {
                    MessageBox.Show("Bu öğrenci numarası kayıtlı değil!", "Uyarı");
                    return;
                }

            
                komut.CommandText = "INSERT INTO oduncIslemleri (ogrenciNo, kitapId, verilisTarihi, aciklama)" +
                    "VALUES (@ogrenciNo, @kitapId, @verilisTarihi, @aciklama)";
                komut.Parameters.Clear(); 
                komut.Parameters.AddWithValue("@ogrenciNo", int.Parse(textBox1.Text));
                komut.Parameters.AddWithValue("@kitapId", int.Parse(comboBox1.SelectedValue.ToString()));
                komut.Parameters.AddWithValue("@verilisTarihi", DateTime.Now.ToString("yyyy/MM/dd"));
                komut.Parameters.AddWithValue("@aciklama", textBox2.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();

                KitapYukle();
                Listele();
                MessageBox.Show("İşlem başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        
            
                try
                {
                    if (baglanti.State != ConnectionState.Open) baglanti.Open();
                    komut = new SqlCommand();
                    komut.Connection = baglanti;

             
                    komut.CommandText = "SELECT COUNT(*) FROM ogrenciIslemleri WHERE okulNo = @ogrenciNo";
                    komut.Parameters.AddWithValue("@ogrenciNo", int.Parse(dataGridView1.CurrentRow.Cells["okulNo"].Value.ToString()));
                    int ogrenciSayisi = (int)komut.ExecuteScalar();

                    if (ogrenciSayisi == 0)
                    {
                        MessageBox.Show("Bu öğrenci numarası kayıtlı değil!", "Uyarı");
                        return;
                    }

               
                    komut.CommandText = "DELETE FROM oduncIslemleri WHERE id = @id";
                    komut.Parameters.Clear();
                    komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells["id"].Value.ToString());
                    komut.ExecuteNonQuery();
                    baglanti.Close();

                    KitapYukle();
                    Listele();
                    MessageBox.Show("İşlem başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Hata oluştu");
                }
            

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox2.Text = dataGridView1.CurrentRow.Cells["aciklama"].Value.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow.Cells["teslimTarihi"].Value.ToString() != String.Empty)
                {
                    MessageBox.Show("Bu kitap teslim alınmış", "Uyarı");
                    return;
                }
                baglanti.Open();
                komutSatiri = "UPDATE oduncIslemleri SET teslimTarihi = @teslimTarihi, aciklama=@aciklama where id=@id";
                komut = new SqlCommand(komutSatiri, baglanti);
                komut.Parameters.AddWithValue("@id", int.Parse(dataGridView1.CurrentRow.Cells["id"].Value.ToString()));
                komut.Parameters.AddWithValue("@teslimTarihi", DateTime.Now.ToString("yyyy/MM/dd"));
                komut.Parameters.AddWithValue("@aciklama", textBox2.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                KitapYukle();
                Listele();
                MessageBox.Show("İşlem başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            OduncKitapOgrenciArama(textBox3.Text);
        }
        private void OduncKitapOgrenciArama(string aranacakKelime)
        {
            try
            {
                baglanti.Open();
                komutSatiri = "Select id,ogrenciNo,ad,soyad,kitapAdi,verilisTarihi,teslimTarihi,aciklama " +
                    "From kitaplar,ogrenciIslemleri,oduncIslemleri " +
                    "where okulNo=ogrenciNo and kitaplar.kitapId=oduncIslemleri.kitapId and ad LIKE '" + aranacakKelime + "%'";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(komutSatiri, baglanti);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata oluştu");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 kitap = new Form1();
            kitap.ShowDialog();
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
