using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System;


namespace SinemaOtomasyonu
{
    public partial class BiletSatisİslemi : Form
    {
        SqlConnection bgl = new SqlConnection(ConfigurationManager.ConnectionStrings["SinemaOtomasyon.Properties.Settings.sinemayeniConnectionString"].ConnectionString);

        int sid;
        double deger = 0;

        public BiletSatisİslemi()
        {
            InitializeComponent();
        }

        private void BiletSatisİslemi_Load(object sender, EventArgs e)
        {
            Btn_BosKoltuk.BackColor = Color.White;
            Btn_DoluKoltuk.BackColor = Color.Red;
            Txt_KoltukNo.ReadOnly = true;
            ComboFilmTarih.Text = "Tarih seçiniz.";

            filmler();
            Bos_Koltuklar();
        }

        private void filmler()
        {
            SqlCommand komut = new SqlCommand("SELECT FilmID, FilmAdi FROM Filmler", bgl);
            SqlDataAdapter tid = new SqlDataAdapter(komut);
            DataTable komut1 = new DataTable();
            tid.Fill(komut1);
            ComboFilmAd.DisplayMember = "FilmAdi";
            ComboFilmAd.ValueMember = "FilmID";
            ComboFilmAd.DataSource = komut1;
        }

        private void Bos_Koltuklar()
        {
            // Önceki kodu buraya taşıdık
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            // Önceki kodu buraya taşıdık
        }

        private void YenidenRenklendir()
        {
            // Önceki kodu buraya taşıdık
        }

        private void SimpleBtn_BosKoltuk_Click(object sender, EventArgs e)
        {
            try
            {
                fiyatcek();
                seans_id_cek();

                DialogResult rs = MessageBox.Show("Bilet satışını onaylıyor musunuz ?", "Bilet Satış Onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    // Satis tablosuna veri ekleme işlemi
                    bgl.Open();
                    SqlCommand satisEkleKomut = new SqlCommand("INSERT INTO Satis (KoltukNo, SeansID, KasiyerID, Fiyat, Tarih, Saat) VALUES (@KoltukNo, @SeansID, @KasiyerID, @Fiyat, @Tarih, @Saat)", bgl);
                    satisEkleKomut.Parameters.AddWithValue("@KoltukNo", Txt_KoltukNo.Text);
                    satisEkleKomut.Parameters.AddWithValue("@SeansID", sid);
                    satisEkleKomut.Parameters.AddWithValue("@KasiyerID", Convert.ToInt32(AdminGiris.kasiyerid));
                    satisEkleKomut.Parameters.AddWithValue("@Fiyat", deger);
                    satisEkleKomut.Parameters.AddWithValue("@Tarih", DateTime.Now.ToShortDateString());
                    satisEkleKomut.Parameters.AddWithValue("@Saat", DateTime.Now.ToShortTimeString());
                    satisEkleKomut.ExecuteNonQuery();

                    MessageBox.Show("Bilet satışı başarıyla gerçekleşti.");

                    bgl.Close();

                    Txt_KoltukNo.Text = "";
                    ComboSalonAd.SelectedItem = null;
                    ComboFilmTarih.SelectedItem = null;
                    ComboFilmSaat.SelectedItem = null;
                    ComboTarife.SelectedItem = null;
                    comboKoltukNo.Items.Clear();
                    comboKoltukNo.Text = "";
                    Text_indirimKupon.Text = "";
                }
                else
                {
                    Txt_KoltukNo.Text = "";
                    ComboSalonAd.SelectedItem = null;
                    ComboFilmTarih.SelectedItem = null;
                    ComboFilmSaat.SelectedItem = null;
                    ComboTarife.SelectedItem = null;
                    comboKoltukNo.Items.Clear();
                    comboKoltukNo.Text = "";
                    Text_indirimKupon.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

            YenidenRenklendir();
        }

        private void ComboFilmSaat_SelectedIndexChanged(object sender, EventArgs e)
        {
            fiyatcek();
            Txt_KoltukNo.Text = "";
            YenidenRenklendir();
            dolu_koltuklar_database();
            Dolu_Koltuklar();
            ComboTarife.SelectedItem = null;
            Text_indirimKupon.Text = "";
        }

        private void comboKoltukNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            iadetl();
        }

        private void SimpleBtn_DoluKoltuk_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = MessageBox.Show("Bileti iptal etmek üzeresiniz, onaylıyor musunuz ?", "Bilet İptal Onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    bgl.Open();
                    SqlCommand komut = new SqlCommand("DELETE FROM Satis WHERE SeansID=@p1 AND KoltukNo=@p2", bgl);
                    komut.Parameters.AddWithValue("@p1", sid);
                    komut.Parameters.AddWithValue("@p2", comboKoltukNo.SelectedItem);
                    komut.ExecuteNonQuery();
                    MessageBox.Show(comboKoltukNo.Text + " koltuk numaralı bilet için " + Txt_iadePara.Text + " TL iade ediniz.");

                    bgl.Close();
                    comboKoltukNo.SelectedItem = null;
                    Txt_iadePara.Text = "";
                    YenidenRenklendir();
                    dolu_koltuklar_database();
                }
                else
                {
                    comboKoltukNo.SelectedItem = null;
                    Txt_iadePara.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void Dolu_Koltuklar()
        {
            // Önceki kodu buraya taşıdık
        }

        private void dolu_koltuklar_database()
        {
            seans_id_cek();
            bgl.Open();
            SqlCommand komut = new SqlCommand("SELECT KoltukNo FROM Satis WHERE SeansID=@p1", bgl);
            komut.Parameters.AddWithValue("@p1", sid);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                foreach (Control item in KoltukPanel.Controls)
                {
                    if (item is Button)
                    {
                        if (Convert.ToString(dr[0]) == item.Text)
                        {
                            item.BackColor = Color.Red;
                            item.Enabled = false;
                        }
                    }
                }
            }
            bgl.Close();
        }

        private void iadetl()
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("SELECT Fiyat FROM Satis WHERE SeansID=@p1 AND KoltukNo=@p2", bgl);
            komut.Parameters.AddWithValue("@p1", sid);
            komut.Parameters.AddWithValue("@p2", Txt_KoltukNo.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Txt_iadePara.Text = dr[0].ToString();
            }
            bgl.Close();
        }

        private void seans_id_cek()
        {
            DataRowView sb2 = ComboFilmAd.SelectedItem as DataRowView;
            string sb3 = sb2.Row["FilmID"].ToString();
            bgl.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM Seans WHERE FilmID=@p1 AND Salon_Ad=@p2 AND Seans_Tarih=@p3 AND Seans_Saat=@p4", bgl);
            komut.Parameters.AddWithValue("@p1", sb3);
            komut.Parameters.AddWithValue("@p2", ComboSalonAd.SelectedItem);
            komut.Parameters.AddWithValue("@p3", ComboFilmTarih.SelectedItem);
            komut.Parameters.AddWithValue("@p4", ComboFilmSaat.SelectedItem);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                sid = Convert.ToInt32(dr["SeansID"]);
            }
            bgl.Close();
        }

        public void fiyatcek()
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM Fiyat", bgl);
            SqlDataAdapter tid = new SqlDataAdapter(komut);
            DataTable komut1 = new DataTable();
            tid.Fill(komut1);
            ComboTarife.DisplayMember = "Fiyat_Tur";
            ComboTarife.ValueMember = "FiyatID";
            ComboTarife.DataSource = komut1;
            bgl.Close();
        }

        private void BiletSatisİslemi_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Form kapatıldığında gerekli temizleme işlemleri burada yapılabilir.
        }

        // Diğer event handler, metotlar vb. buraya eklenebilir.
    }
}
