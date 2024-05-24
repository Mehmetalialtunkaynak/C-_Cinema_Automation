using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using SinemaOtomasyonu.controller;
using SinemaOtomasyonu.model;
using SinemaOtomasyonu.enumeration;
using SinemaOtomasyonu.dao;

namespace SinemaOtomasyonu
{
    public partial class Filmler : Form
    {
        controller.Controller controller = new controller.Controller();
        public Filmler()
        {
            InitializeComponent();
        }

        //SqlConnection bgl = new SqlConnection(ConfigurationManager.ConnectionStrings["SinemaOtomasyon.Properties.Settings.sinemayeniConnectionString"].ConnectionString);

        private void Filmler_Load(object sender, EventArgs e)
        {
            defaultDegerleriDoldur();
            DataGridViewFilmler.DataSource = controller.tumfilmlerigetir();
            
        }

        private void tumFilmleriGetir()
        {
            DataGridViewFilmler.DataSource = controller.tumfilmlerigetir();
        }

        private void defaultDegerleriDoldur()
        {
            turidcek();
        }

        private void DataGridKullanıcılar_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int adminid;
            if (int.TryParse(DataGridViewFilmler.CurrentRow.Cells[0].Value.ToString(), out adminid))
            {
                // adminid değeri başarıyla alındı.
                // Burada adminid kullanabilirsiniz.
            }
            else
            {
                // adminid değeri başarıyla alınamadı. Hata durumuyla başa çıkabilirsiniz.
            }

            Txt_FilmAdi.Text = DataGridViewFilmler.CurrentRow.Cells[1].Value.ToString();
            DateTime_Ekle.Text = DataGridViewFilmler.CurrentRow.Cells[2].Value.ToString();
            Txt_Yönetmen.Text = DataGridViewFilmler.CurrentRow.Cells[3].Value.ToString();
            Txt_SüresiEkle.Text = DataGridViewFilmler.CurrentRow.Cells[4].Value.ToString();
            combo_FilmTürü.Text = DataGridViewFilmler.CurrentRow.Cells[6].Value.ToString();

        }

        public void turidcek()
        {
            Repository repository = new Repository();
            repository.con.Open();

            SqlCommand komut = new SqlCommand("SELECT FilmTurID,TurAD FROM FilmTurler", repository.con);
            SqlDataAdapter tid = new SqlDataAdapter(komut);
            DataTable komut1 = new DataTable();
            tid.Fill(komut1);
            combo_FilmTürü.DisplayMember = "TurAD";
            combo_FilmTürü.ValueMember = "FilmTurID";
            combo_FilmTürü.DataSource = komut1;
            repository.con.Close();
        }

        bool filmadi;


       
        private void Btn_AfisSeç_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Dosya seçim iletişim kutusunu görüntüle
            DialogResult result = openFileDialog1.ShowDialog();

            // Kullanıcı bir dosya seçtiyse
            if (result == DialogResult.OK)
            {
                // Seçilen dosyanın yolunu PictureBox kontrolüne atayın
                PictureBox_Ekle.ImageLocation = openFileDialog1.FileName;
            }
        }


        public void filmadictrl()
        {
            Repository repository = new Repository();
            repository.con.Open();

            SqlCommand komut = new SqlCommand("Select * From Filmler where FilmAdi=@p1", repository.con);
            komut.Parameters.AddWithValue("@p1", Txt_FilmAdi.Text);
            SqlDataReader read = komut.ExecuteReader();
            if (read.Read())
            {
                filmadi = false;
            }
            else
                filmadi = true;

            repository.con.Close();
        }


        private void Btn_FilmEkle_Click(object sender, EventArgs e)
        {
            try
            {
                filmadictrl();
                DataRowView sb = combo_FilmTürü.SelectedItem as DataRowView;
                string sb1 = sb.Row["FilmTurID"].ToString();

                if (Txt_FilmAdi.Text == "" || Txt_Yönetmen.Text == "" || Txt_SüresiEkle.Text == "")
                {
                    MessageBox.Show("Film Adı, Film Türü ve Film Yönetmeni alanları boş geçilemez", "Uyarı");
                }
                else if ((DateTime.Parse(DateTime.Now.ToShortTimeString()) < DateTime.Parse(DateTime_Ekle.Text)))
                {
                    MessageBox.Show("Yapım yılı şuanki zamanı geçemez", "Uyarı");
                }
                else if (filmadi == false)
                {
                    MessageBox.Show("Bu film zaten kayıtlı.");
                }
                else
                {
                    DialogResult rs = MessageBox.Show("Film ekleme işlemini onaylıyor musunuz ? ", "Film ekleme onayı", MessageBoxButtons.YesNo);
                    if (rs == DialogResult.Yes)
                    {
                        string ResimYolu = "";
                        if (PictureBox_Ekle.Image != null )
                            ResimYolu = PictureBox_Ekle.ImageLocation;

                        // controller sınıfındaki FilmEkleSorgu metodunu kullanarak film ekle
                        controller.FilmEkleSorgu(Txt_FilmAdi.Text, DateTime_Ekle.Text, Txt_Yönetmen.Text, Txt_SüresiEkle.Text, ResimYolu, sb1);
                        MessageBox.Show("Film Ekleme Başarılı", "Tamamlandı");
                        Txt_FilmAdi.Text = "";
                        Txt_Yönetmen.Text = "";
                        Txt_SüresiEkle.Text = "";
                        DateTime_Ekle.Text = DateTime.Now.ToShortDateString();
                        combo_FilmTürü.SelectedItem = null;
                        tumFilmleriGetir(); // DataGridView'i güncelle
                    }
                    else
                    {
                        Txt_FilmAdi.Text = "";
                        Txt_Yönetmen.Text = "";
                        Txt_SüresiEkle.Text = "";
                        DateTime_Ekle.Text = DateTime.Now.ToShortDateString();
                        combo_FilmTürü.SelectedItem = null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata   " + ex.Message);
            }
        }

        int filmid;

        private void DataGridViewFilmler_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            filmid = int.Parse(DataGridViewFilmler.CurrentRow.Cells[0].Value.ToString());
            Txt_FilmAdi.Text = DataGridViewFilmler.CurrentRow.Cells[1].Value.ToString();
            Txt_SüresiEkle.Text = DataGridViewFilmler.CurrentRow.Cells[2].Value.ToString();
            Txt_Yönetmen.Text = DataGridViewFilmler.CurrentRow.Cells[3].Value.ToString();
            combo_FilmTürü.Text = DataGridViewFilmler.CurrentRow.Cells[4].Value.ToString();
            DateTime_Ekle.Text = DataGridViewFilmler.CurrentRow.Cells[5].Value.ToString();
        }

        private void Btn_FilmDüzenle_Click(object sender, EventArgs e)
        {
            Repository repository = new Repository();

            try
            {
                if (Txt_FilmAdi.Text == "" && Txt_Yönetmen.Text == "" && Txt_SüresiEkle.Text == "")
                {
                    MessageBox.Show("Tüm alanları doldurunuz.");
                }
                else if ((DateTime.Parse(DateTime.Now.ToShortTimeString()) < DateTime.Parse(DateTime_Ekle.Text)))
                {
                    MessageBox.Show("Yapım yılı şuanki zamanı geçemez", "Uyarı");
                }

                else
                {
                    DialogResult rs = MessageBox.Show("Filmi güncellemek istediğinizden emin misiniz ?", "Film güncelleme onayı", MessageBoxButtons.YesNo);
                    if (rs == DialogResult.Yes)
                    {
                        repository.con.Open();
                        
                        SqlCommand komut = new SqlCommand("Update Filmler set FilmAdi=@p1, FilmYapimYili=@p2,FilmYonetmen=@p3,Film_Sure=@p4 where FilmID=@p5 ", repository.con);
                        komut.Parameters.AddWithValue("@p1", Txt_FilmAdi.Text);
                        komut.Parameters.AddWithValue("@p2", DateTime_Ekle.Text);
                        komut.Parameters.AddWithValue("@p3", Txt_Yönetmen.Text);
                        komut.Parameters.AddWithValue("@p4", Txt_SüresiEkle.Text);
                        komut.Parameters.AddWithValue("@p5", filmid);
                        komut.ExecuteNonQuery();
                        repository.con.Close();

                        MessageBox.Show("Film başarıyla güncellendi.");
                        Txt_FilmAdi.Text = "";
                        DateTime_Ekle.Text = DateTime.Now.ToShortDateString();
                        Txt_Yönetmen.Text = "";
                        Txt_SüresiEkle.Text = "";
                        PictureBox_Ekle.InitialImage = null;
                    }
                    else
                    {
                        Txt_FilmAdi.Text = "";
                        DateTime_Ekle.Text = DateTime.Now.ToShortDateString();
                        Txt_Yönetmen.Text = "";
                        Txt_SüresiEkle.Text = "";
                        PictureBox_Ekle.InitialImage = null;
                    }
                }

                tumFilmleriGetir();
            }
            catch (Exception ex)
            {
                repository.con.Close();
                MessageBox.Show("Film ekleme hatası " + ex.Message);
            }
            repository.con.Close();
        }

        private void Btn_AfisSeçGuncelle_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Dosya seçim iletişim kutusunu görüntüle
            DialogResult result = openFileDialog1.ShowDialog();

            // Kullanıcı bir dosya seçtiyse
            if (result == DialogResult.OK)
            {
                // Seçilen dosyanın yolunu PictureBox kontrolüne atayın
                PictureBox_Ekle.ImageLocation = openFileDialog1.FileName;
            }
        }

        
        
        private void Btn_FilmiSil_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = MessageBox.Show("Seçili filmi silmek istediğinizden emin misiniz ? ", "Film silme onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    // controller sınıfındaki FilmSil metodu ile filmi sil
                    controller.FilmSil(filmid.ToString());
                    MessageBox.Show("Silme işlemi başarılı.");
                    Txt_FilmAdi.Text = "";
                    DateTime_Ekle.Text = DateTime.Now.ToShortDateString();
                    Txt_Yönetmen.Text = "";
                    Txt_SüresiEkle.Text = "";
                    PictureBox_Ekle.InitialImage = null;
                    tumFilmleriGetir(); // DataGridView'i güncelle
                }
                else
                {
                    Txt_FilmAdi.Text = "";
                    DateTime_Ekle.Text = DateTime.Now.ToShortDateString();
                    Txt_Yönetmen.Text = "";
                    Txt_SüresiEkle.Text = "";
                    PictureBox_Ekle.InitialImage = null;
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Film bulunamadı.");
            }
        }

        private void DataGridViewFilmler_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewFilmler.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                // Tüm satırların arka plan rengini yeşil olarak ayarlar
            }
        }

        private void DataGridViewFilmler_CellContentDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int adminid;
            if (int.TryParse(DataGridViewFilmler.CurrentRow.Cells[0].Value.ToString(), out adminid))
            {
                // adminid değeri başarıyla alındı.
                // Burada adminid kullanabilirsiniz.
            }
            else
            {
                MessageBox.Show("Admin ID Değeri Başarı ile alınamadı ");
                // adminid değeri başarıyla alınamadı. Hata durumuyla başa çıkabilirsiniz.
            }


            filmid =int.Parse(DataGridViewFilmler.CurrentRow.Cells[0].Value.ToString());
            Txt_FilmAdi.Text = DataGridViewFilmler.CurrentRow.Cells[1].Value.ToString();
            DateTime_Ekle.Text = DataGridViewFilmler.CurrentRow.Cells[2].Value.ToString();
            Txt_Yönetmen.Text = DataGridViewFilmler.CurrentRow.Cells[3].Value.ToString();
            Txt_SüresiEkle.Text = DataGridViewFilmler.CurrentRow.Cells[4].Value.ToString();
            combo_FilmTürü.Text = DataGridViewFilmler.CurrentRow.Cells[7].Value.ToString();
        }

        private void guna2GradientButton7_Click(object sender, EventArgs e)
        {

        }

        
    }
}
