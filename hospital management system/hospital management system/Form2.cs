using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hospital_management_system
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=test; user ID=postgres;password=03Mfdr03.");

        private void Form2_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut1 = new NpgsqlCommand("select bolum_adi from uzmanlik", baglanti);
            NpgsqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                cmbBolum.Items.Add(dr[0].ToString());
            }
            baglanti.Close();

            // iller combobaxına illeri çekmek için
            baglanti.Open();
            NpgsqlCommand command = new NpgsqlCommand("select il_ad from il order by il_no", baglanti);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                cmbilNo.Items.Add(dataReader[0]);
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut2 = new NpgsqlCommand("insert into randevu_al (randevu_id,il_no,ilce_no,hastane_ismi,tarih,saat,muayene_yeri,hasta_id,bolum_no) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", baglanti);
            komut2.Parameters.AddWithValue("@p1", int.Parse(txtRandevuID.Text));
            komut2.Parameters.AddWithValue("@p2", cmbilNo.Text);
            komut2.Parameters.AddWithValue("@p3", cmbilceNo.Text);
            komut2.Parameters.AddWithValue("@p4", txtHastaneIsmi.Text);
            komut2.Parameters.AddWithValue("@p5", txtTarih.Text);
            komut2.Parameters.AddWithValue("@p6", txtSaat.Text);
            komut2.Parameters.AddWithValue("@p7", txtMuayeneYeri.Text);
            komut2.Parameters.AddWithValue("@p8", int.Parse(txtHastaID.Text));
            komut2.Parameters.AddWithValue("@p9", int.Parse(txtBolumNo.Text));

            komut2.ExecuteNonQuery();
           
            baglanti.Close();
            MessageBox.Show("Randevu alma işlemi başarılı bir şekilde gerçekkeşti...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmbilceNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbilNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbilceNo.Items.Clear();
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("Select ilce_ad from ilce where il_no=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", cmbilNo.SelectedIndex + 1);
            NpgsqlDataReader dataReader = komut.ExecuteReader();
            while (dataReader.Read())
            {
                cmbilceNo.Items.Add(dataReader[0].ToString());
            }
            baglanti.Close();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            string sorgu = " select * from  randevu_al";
            NpgsqlDataAdapter yenidataadapter = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet yeniDataSet = new DataSet();
            yenidataadapter.Fill(yeniDataSet);
            dataGridView1.DataSource = yeniDataSet.Tables[0];
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut3 = new NpgsqlCommand("DELETE from randevu_al where randevu_id =@p1", baglanti);
            komut3.Parameters.AddWithValue("@p1", int.Parse(txtRandevuID.Text));
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Randevu  silme islemi basarili","Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut4 = new NpgsqlCommand("update randevu_al set il_no=@p2,ilce_no=@p3,hastane_ismi=@p4,tarih=@p5,saat=@p6,muayene_yeri=@p7,bolum_no=@p9 where randevu_id=@p1 ", baglanti);
            komut4.Parameters.AddWithValue("@p1", int.Parse(txtRandevuID.Text));
            komut4.Parameters.AddWithValue("@p2", cmbilNo.Text);
            komut4.Parameters.AddWithValue("@p3", cmbilceNo.Text);
            komut4.Parameters.AddWithValue("@p4", txtHastaneIsmi.Text);
            komut4.Parameters.AddWithValue("@p5", txtTarih.Text);
            komut4.Parameters.AddWithValue("@p6", txtSaat.Text);
            komut4.Parameters.AddWithValue("@p7", txtMuayeneYeri.Text);
            komut4.Parameters.AddWithValue("@p9", int.Parse(txtBolumNo.Text));
            komut4.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Randevu guncelleme islemi basarili", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            string sorgu1 = "Select * From randevu_al where randevu_id='" + txtRandevuID.Text + "'";
            NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(sorgu1, baglanti);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            dataGridView1.DataSource = ds1.Tables[0];


        }
    }
}
