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
using System.Net;

namespace Kütüphaneprojesi
{
    public partial class Form6 : Form
    {

      
        
        
          
       
    
    private int rütbeValue;

        public Form6()
        {
            InitializeComponent();
        }
        public void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            string connectionString = "Data Source=.;Initial Catalog=users;Integrated Security=SSPI";
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT * FROM users WHERE UserName = @UserName AND Password = @Password ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", textBox1.Text);
            command.Parameters.AddWithValue("@Password", textBox2.Text);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            
            temizle();
            if (reader.Read())
            {
                Form1 form1 = new Form1();
                bool rütbeValue = (bool)reader["rütbe"];
                if (rütbeValue)
                    MessageBox.Show("Yönetici Hesabı ile başarılı şekilde giriş yapıldı hesap: " + reader["username"].ToString(), "Sistem", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Hoşgeldiniz " + reader["username"].ToString(), "Sistem", MessageBoxButtons.OK, MessageBoxIcon.Information);


                Button button7 = form1.Controls.Find("button7", true).FirstOrDefault() as Button;
                if (button7 != null)
                {
                    button7.Visible = rütbeValue;
                }
                using (SqlConnection connectiona = new SqlConnection(connectionString))
                {
                    connectiona.Open();





                    string ipAddress = GetClientIpAddress();

                    string insertLog = "INSERT INTO log (ip_adresi) VALUES (@ipAdresi)";
                    using (SqlCommand commandInsertLog = new SqlCommand(insertLog, connectiona))
                    {
                        commandInsertLog.Parameters.AddWithValue("@ipAdresi", ipAddress);
                        commandInsertLog.ExecuteNonQuery();
                    }
                }

                form1.Show();
             }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre yanlış.");
            }
            

            connection.Close();

        }

        private void Form6_Load(object sender, EventArgs e)
        {
       


        }
        private string GetClientIpAddress()
        {
            string hostAdi = Dns.GetHostName();
            IPHostEntry yerel = Dns.GetHostEntry(hostAdi);

           return yerel.AddressList[0].ToString();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
