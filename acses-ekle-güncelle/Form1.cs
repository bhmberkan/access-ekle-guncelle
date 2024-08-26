using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace acses_ekle_güncelle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=okul.accdb");
       
        
        public void yenile()
        {
            DataTable dt = new DataTable();
            baglanti.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * From sınıf", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            yenile();

        }
        public void bosmu()
        {
            if (textBox1.Text.Trim() == "" || textBox2.Text.Trim() == "" || textBox3.Text.Trim() == "" || textBox4.Text.Trim() == "" || textBox5.Text.Trim() == "" || textBox6.Text.Trim() == "")
            {
                MessageBox.Show("textboxları doldurunuz.");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bosmu();
            try
            {
                OleDbCommand ekle = new OleDbCommand("insert into sınıf(okul_no,ad,soyad,sınıf,sehir,yas)" +
                    "values('" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')", baglanti);
               
                baglanti.Open();
                ekle.ExecuteNonQuery();
                baglanti.Close();


                yenile();
                MessageBox.Show("veriler eklenmiştir");
                
                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (Controls[i] is TextBox) Controls[i].Text = "";
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
                baglanti.Close();
            }

        }


        private void button4_Click(object sender, EventArgs e)
        {
            bosmu();
            try
            {
                OleDbCommand db = new OleDbCommand("UPDATE  sınıf  Set okul_no = '" + textBox1.Text + "',ad= '" + textBox2.Text + "', soyad = '" + textBox3.Text + "',sınıf = '" + textBox4.Text + "' ,sehir = '" + textBox5.Text + "',yas = '" + textBox6.Text + "' WHERE okul_no='" + textBox7.Text + "'", baglanti);
                baglanti.Open();
                db.ExecuteNonQuery();
                baglanti.Close();
                yenile();
                MessageBox.Show("güncellenmiştir.");

                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (Controls[i] is TextBox) Controls[i].Text = "";
                }
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("silmek istediğinize emin misiniz.", "dikkat tüm verileriniz silinebilir.", MessageBoxButtons.YesNoCancel);
            bosmu();
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand sil = new OleDbCommand("DELETE  FROM  sınıf WHERE okul_no='" + textBox7.Text + "'", baglanti);
                    baglanti.Open();
                    sil.ExecuteNonQuery();
                    baglanti.Close();
                    yenile();
                    MessageBox.Show("kaydınız sılınmıştır.");

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (Controls[i] is TextBox) Controls[i].Text = "";
                    }
                }
                catch (Exception hata)
                {
                    MessageBox.Show(hata.Message);
                }
            }
            else if (cevap == DialogResult.No)
            {
                MessageBox.Show("işleminiz iptal edildi.");
            }
            else
                MessageBox.Show("işlemi sonlandırdınız.");
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                baglanti.Open();
                OleDbDataAdapter ara = new OleDbDataAdapter("SELECT *  FROM sınıf WHERE okul_no LIKE '" + textBox7.Text + "%'", baglanti);
                ara.Fill(dt);
                dataGridView1.DataSource = dt;
                baglanti.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show(hata.Message);

            }
        }
    }
}

