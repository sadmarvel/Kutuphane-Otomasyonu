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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kütüphaneprojesi
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 giris = new Form1();
            giris.ShowDialog();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
  
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string baglanti = "Data Source=.;Initial Catalog=users;Integrated Security=True";
                SqlConnection komut = new SqlConnection(baglanti);

                komut.Open();
                SqlCommand command = new SqlCommand("INSERT INTO users (username, password, rütbe) VALUES (@username, @password, @rütbe)", komut);
                command.Parameters.AddWithValue("@username", textBox1.Text);
                command.Parameters.AddWithValue("@password", textBox2.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information );
                komut.Close();

            }
            catch { MessageBox.Show("Hata"); }   




        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string baglanti = "Data Source=.;Initial Catalog=users;Integrated Security=True";
                SqlConnection komut = new SqlConnection(baglanti);

                komut.Open();
                SqlCommand command = new SqlCommand("DELETE FROM users WHERE username = @username", komut);
                command.Parameters.AddWithValue("@username", textBox1.Text);
                command.Parameters.AddWithValue("@password", textBox2.Text);
                command.ExecuteNonQuery();
                MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                komut.Close();

            }
            catch { MessageBox.Show("Hata"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                komutSatiri = "UPDATE users SET username=@username, password=@password where username=@username";
                komut = new SqlCommand(komutSatiri, baglanti);
                komut.Parameters.AddWithValue("@username", dataGridView1.CurrentRow.Cells["username"].Value.ToString());
                komut.Parameters.AddWithValue("@password", textBox2.Text);
    
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
            
                MessageBox.Show("İşlem Başarılı", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Listele();




            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Hata oluştu"); }

        }
        SqlConnection baglanti = new SqlConnection("server=.; Initial Catalog=users;Integrated Security=SSPI");

        SqlCommand komut;
        string komutSatiri;
        public void Listele()
        {
           
          
                komutSatiri = "Select * From users";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(komutSatiri, baglanti);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns["username"].HeaderText = "Kullanıcı Adı";
                dataGridView1.Columns["username"].Width = 100;
                dataGridView1.Columns["password"].HeaderText = "Şifresi";
                dataGridView1.Columns["password"].Width = 300;
                dataGridView1.Columns["rütbe"].HeaderText = "Yönetici";
                dataGridView1.Columns["rütbe"].Width = 100;


        }

        private void Form7_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form8 denetim = new Form8();
            denetim.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form9 log = new Form9();
            log.ShowDialog();
            this.Close();

        }
    }
}
