using SinemaOtomasyonu.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace SinemaOtomasyonu
{
    public partial class Kullanıcılar : Form
    {
        controller.Controller controller = new controller.Controller();
        int adminid;
        bool tarihGosteriliyor = true;
        Timer timer;

        public Kullanıcılar()
        {
            InitializeComponent();


            btn_tarih_saat.Text = DateTime.Now.ToLongDateString(); // Başlangıçta tarihi göster

            timer = new Timer();
            timer.Interval = 1000; // Her 1 saniyede bir güncelleme yapacak
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Kullanıcılar_Load_1(object sender, EventArgs e)
        {
            defaultDegerleriDoldur();
            tumkullanicilariDoldur();

            /* lbl_saat.Parent = btn_Saat_Tarih;    Bu kodu saat tarihin arkaplanını saydam yapmak için denedim ancak olmadı çok uğrasmadım sonra bakarız : )
              lbl_saat.BackColor = Color.Transparent; */
        }


        private void tumkullanicilariDoldur()
        {

            List<User> userList = controller.tumKullanicilariGetir();
            DataGridKullanıcılar.DataSource = userList;

            // userList üzerinde istediğiniz işlemleri yapabilirsiniz
            // Örneğin, userList verilerini DataGridView'e aktarabilirsiniz

        }

        private void defaultDegerleriDoldur()
        {
            combo_yetki.Items.Add("Admin");
            combo_yetki.Items.Add("Kasiyer");
            combo_yetki.SelectedIndex = 0;

            //--------------------------




            combo_guvenlikSorusu.Items.Add("En Sevdiğiniz İl");
            combo_guvenlikSorusu.Items.Add("En Sevdiğiniz Şehir");
            combo_guvenlikSorusu.Items.Add("En Sevdiğiniz Osmanlı ili");
            combo_guvenlikSorusu.Items.Add("En Sevdiğiniz Çiçek");
            combo_guvenlikSorusu.Items.Add("En Sevdiğiniz Mutfak Aleti");
            combo_guvenlikSorusu.Items.Add("En Sevdiginiz Hayvan Nedir ?");
            combo_guvenlikSorusu.Items.Add("En Sevdiginiz Araba ?");
            combo_guvenlikSorusu.Items.Add("Birinci sinif ögretmeninizin ismi nedir?");
            combo_guvenlikSorusu.Items.Add("En sevdiginiz hayvanin ismi nedir?");
            combo_guvenlikSorusu.Items.Add("Annenizin kizlik soyadi nedir?");
            combo_guvenlikSorusu.Items.Add("Hangi sehirde dogdunuz?");
            combo_guvenlikSorusu.Items.Add("Babanizin ortanca ismi nedir?");
            combo_guvenlikSorusu.Items.Add("Çocukluk lakabiniz nedir?");
            combo_guvenlikSorusu.Items.Add("Ilk telefonuzun modeli nedir?");
            combo_guvenlikSorusu.SelectedIndex = 0;



        }

        private void btn_tarih_saat_Click(object sender, EventArgs e)
        {
            tarihGosteriliyor = !tarihGosteriliyor; // Her tıklamada durumu tersine çevir

            if (tarihGosteriliyor)
            {
                btn_tarih_saat.Text = DateTime.Now.ToLongDateString(); // Tarihi ve günü göster
            }
            else
            {
                btn_tarih_saat.Text = DateTime.Now.ToString("HH:mm:ss"); // Saati ve saniyeyi göster
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (!tarihGosteriliyor)
            {
                btn_tarih_saat.Text = DateTime.Now.ToString("HH:mm:ss"); // Saati ve saniyeyi güncelle
            }
        }

        SqlConnection bgl = new SqlConnection("Data Source=.;Initial Catalog=sinemayeni;Integrated Security=True");
        //SqlConnection bgl = new SqlConnection(ConfigurationManager.ConnectionStrings["SinemaOtomasyon.Properties.Settings.sinemayeniConnectionString"].ConnectionString);
        private void Kullanıcılar_Load(object sender, EventArgs e)
        {


        }



        private void DataGridKullanıcılar_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int adminid;
            if (int.TryParse(DataGridKullanıcılar.CurrentRow.Cells[0].Value.ToString(), out adminid))
            {
                // adminid değeri başarıyla alındı.
                // Burada adminid kullanabilirsiniz.
            }
            else
            {
                // adminid değeri başarıyla alınamadı. Hata durumuyla başa çıkabilirsiniz.
            }

            txt_kullaniciAdi.Text = DataGridKullanıcılar.CurrentRow.Cells[1].Value.ToString();
            txt_sifre.Text = DataGridKullanıcılar.CurrentRow.Cells[2].Value.ToString();
            txt_tc.Text = DataGridKullanıcılar.CurrentRow.Cells[3].Value.ToString();
            txt_mail.Text = DataGridKullanıcılar.CurrentRow.Cells[4].Value.ToString();
            combo_yetki.Text = DataGridKullanıcılar.CurrentRow.Cells[5].Value.ToString();
            txt_ad.Text = DataGridKullanıcılar.CurrentRow.Cells[6].Value.ToString();
            txt_soyad.Text = DataGridKullanıcılar.CurrentRow.Cells[7].Value.ToString();
            combo_guvenlikSorusu.Text = DataGridKullanıcılar.CurrentRow.Cells[8].Value.ToString();
            combo_guvenlikCevabi.Text = DataGridKullanıcılar.CurrentRow.Cells[9].Value.ToString();

        }









        /*
        private void GridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.Green;


            }
        }*/
        private void DataGridKullanıcılar_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridKullanıcılar.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
            }
        }



        bool kontrol;
        bool kontrol1;
        bool kontrol2;
        public void control()
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("Select Kullanici_Adi From Admin", bgl);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                if ((dr[0].ToString()) == (txt_kullaniciAdi.Text))
                {
                    kontrol = false;
                }
                else
                    kontrol = true;
            }
            bgl.Close();
        }
        public void control1()
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("Select TC From Admin", bgl);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                if ((dr[0].ToString()) == (txt_tc.Text))
                {
                    kontrol1 = false;
                }
                else
                    kontrol1 = true;
            }
            bgl.Close();
        }
        public void control2()
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("Select Mail From Admin", bgl);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                if ((dr[0].ToString()) == (txt_mail.Text))
                {
                    kontrol2 = false;
                }
                else
                    kontrol2 = true;
            }
            bgl.Close();
        }
        private void btn_kayitEkle_Click(object sender, EventArgs e)
        {

            control();
            control1();
            control2();
            if (txt_kullaniciAdi.Text == "" && txt_sifre.Text == "" && txt_mail.Text == "" && txt_tc.Text == "" && combo_yetki.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz");
            }
            else if (kontrol == false)
            {
                MessageBox.Show("Bu kullanıcı adı zaten var");
            }
            else if (kontrol1 == false)
            {
                MessageBox.Show("Bu T.C. Kimlik No sistemde kayıtlı");
            }
            else if (kontrol2 == false)
            {
                MessageBox.Show("Bu Mail sistemde kayıtlı");
            }
            else
            {
                DialogResult rs = MessageBox.Show("Kullanıcı eklemek istediğinizden emin misiniz ?", "Kullanıcı ekleme onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    User user = new User();
                    user.Kullanici_Adi = txt_kullaniciAdi.Text;
                    user.Ad = txt_ad.Text;
                    user.Soyad = txt_soyad.Text;
                    user.Sifre = txt_sifre.Text;
                    user.Yetki = combo_yetki.SelectedItem.ToString();
                    user.Mail = txt_mail.Text;
                    user.guvenlikSorusu = combo_guvenlikSorusu.SelectedItem.ToString();
                    user.guvenlikCevabi = combo_guvenlikCevabi.Text;

                    //LoginStatus sonuc = controller.kullaniciEkle(user);
                    //if (sonuc == LoginStatus.basarili)
                    //{
                    //    MessageBox.Show("Kayıt Eklendi 😊", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    DataGridKullanıcılar.DataSource = controller.tumKullanicilariGetir();
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Gerekli Alanların Hepsini Doldurun !..", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
            }
        }





        private void btn_kayitGuncelle_Click(object sender, EventArgs e)
        {
            if (txt_kullaniciAdi.Text != "" && txt_sifre.Text != "" && txt_tc.Text != "" && txt_mail.Text != "" && combo_yetki.Text != "" && txt_ad.Text != "" && txt_soyad.Text != "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz");
            }
            else
            {
                DialogResult rs = MessageBox.Show("Kullanıcı güncellemek istediğinizden emin misiniz ?", "Kullanıcı güncelleme onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    //admin.AdminGüncelle(txt_kullaniciAdi.Text, txt_sifre.Text, txt_tc.Text, txt_mail.Text, combo_yetki.Text, txt_ad.Text, txt_soyad.Text, adminid, adminid);
                    MessageBox.Show("Kullanıcı başarıyla güncellendi.");
                    combo_yetki.Text = "";
                    txt_mail.Text = "";
                    txt_tc.Text = "";
                    txt_sifre.Text = "";
                    txt_kullaniciAdi.Text = "";
                    txt_soyad.Text = "";
                    txt_ad.Text = "";
                }
            }
        }
        


        private void btn_kayitSil_Click(object sender, EventArgs e)
        {
            if (txt_kullaniciAdi.Text != "" && txt_sifre.Text != "" && txt_tc.Text != "" && txt_mail.Text != "" && combo_yetki.Text != "")
            {
                MessageBox.Show("Lütfen kullanıcı seçiniz");
            }
            else
            {
                DialogResult rs = MessageBox.Show("Kullanıcı silmek istediğinizden emin misiniz ?", "Kullanıcı silme onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    //admin.SilSorgu(adminid);
                    MessageBox.Show("Kullanıcı başarıyla silindi.");
                    combo_yetki.Text = "";
                    txt_mail.Text = "";
                    txt_tc.Text = "";
                    txt_sifre.Text = "";
                    txt_kullaniciAdi.Text = "";
                    txt_soyad.Text = "";
                    txt_ad.Text = "";
                }
            }
        }
    }
}
