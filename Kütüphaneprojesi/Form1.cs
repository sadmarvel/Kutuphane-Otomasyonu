using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kütüphaneprojesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 kitap = new Form4();
            kitap.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 ogrenciler = new Form2();
            ogrenciler.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 kitapTur = new Form3();
            kitapTur.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 odunc = new Form5();
            odunc.ShowDialog();
            this.Close();
        }
        public void KullanıcıKontrol()
        {
     
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            KullanıcıKontrol();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form6 giris = new Form6();
            giris.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var openForms = Application.OpenForms.Cast<Form>().ToList();

  
            foreach (var form in openForms)
            {
                form.Close();
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {

        //    MessageBox.Show("Bu Uygulama Demo Sürümü Olduğu İçin Erişiminiz Yasaktır.", "Uyarı",MessageBoxButtons.OK , MessageBoxIcon.Warning);

             Form7 giris = new Form7();
           giris.ShowDialog();
             this.Close();
        }
    }
}
