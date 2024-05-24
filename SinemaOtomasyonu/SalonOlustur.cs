//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;

//namespace SinemaOtomasyonu
//{
//    public partial class SalonOlustur : Form
//    {
//        public SalonOlustur()
//        {
//            InitializeComponent();
//        }

//        public void SalonDuzeniOlusturma()
//        {
//            int say = 0;
//            //Panel içeriği temizleniyor
//            KoltukPanel.Controls.Clear();

//            //  i değişkeni satırlar ve j ise sütunları temsil etmektedir
//            //TextBox'taki satır (i) ve sütunlar (j) üzerinde hareket ediliyor
//            for (int i = 0; i < TxtB_KoltukDuzeni.Lines.Count(); i++)
//            {
//                for (int j = 0; j < TxtB_KoltukDuzeni.Lines[i].Count(); j++)
//                {
//                    string satir = TxtB_KoltukDuzeni.Lines[i];

//                    //Satır üzerinde * karakteri oluşturulası gereken koltuk anlamındadır.
//                    if (satir[j] == '*')
//                    {
//                        //Nesne dinamik olarak oluşturuluyor
//                        Button nesne = new Button();
//                        nesne.Name = "buton" + i;
//                        nesne.Text = (++say).ToString();
//                        nesne.BackColor = Color.Red;
//                        nesne.Width = nesne.Height = 30;
//                        nesne.Left = 35 * j;
//                        nesne.Top = 35 * i;


//                        //Buton üzerine tıklandığında hangi metodun çalıştırılacağı belirtiliyor
//                        nesne.Click += koltukSecildi;

//                        //Oluşturulan buton nesnesi panel1 üzerine yerleştiriliyor
//                        KoltukPanel.Controls.Add(nesne);
//                    }


//                }


//            }

//            void koltukSecildi(object sender, EventArgs e)
//            {
//                Button btn = (Button)sender;

//                if (btn.BackColor == Color.Red)
//                    btn.BackColor = Color.Aqua;
//                else
//                    btn.BackColor = Color.Red;

//            }








//        }
//    }
//}

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using SinemaOtomasyonu.model;
using SinemaOtomasyonu.enumeration;
using LoginStatus = SinemaOtomasyonu.enumeration.LoginStatus;
using SinemaOtomasyonu.controller;

namespace SinemaOtomasyonu
{


    public partial class SalonOlustur : Form
    {

        controller.Controller controller = new controller.Controller();

        public SalonOlustur()
        {
            InitializeComponent();
            // ImageList'e resim eklemek
            imageList1.Images.Add(Properties.Resources.SİnemaKoltuğuBeyaz);
            imageList1.Images.Add(Properties.Resources.SeciliKoltuk);
        }

        private void Btn_SalonOlustur_Click(object sender, EventArgs e)
        {
            SalonDuzeniOlusturma();

            Salon salon = new Salon();
            salon.Salon_Ad = Txt_SalonAd.Text;

            LoginStatus sonuc = controller.SalonEkle(salon);
            if (sonuc == LoginStatus.basarili)
            {
                MessageBox.Show("Salon başarıyla eklendi.");
            }
            else
            {
                MessageBox.Show("Sanırım Bir Hata ile Karşılaştık.");
            }
        }

        public void SalonDuzeniOlusturma()
        {
            int say = 0;
            KoltukPanel.Controls.Clear();

            for (int i = 0; i < TxtB_KoltukDuzeni.Lines.Length; i++)
            {
                for (int j = 0; j < TxtB_KoltukDuzeni.Lines[i].Length; j++)
                {
                    char karakter = TxtB_KoltukDuzeni.Lines[i][j];

                    if (karakter == '*')
                    {
                        Button koltukButton = new Button();
                        koltukButton.Name = "buton" + i;
                        koltukButton.Text = (++say).ToString();
                        koltukButton.BackColor = Color.Transparent; // Şeffaf arka plan
                        koltukButton.Width = koltukButton.Height = 100;
                        koltukButton.Left = 110 * j;
                        koltukButton.Top = 110 * i;

                        // ImageList'te belirtilen resmi kullan
                        koltukButton.ImageList = imageList1;
                        koltukButton.ImageIndex = 1; // Başlangıçta beyaz olacak

                        koltukButton.ImageAlign = ContentAlignment.MiddleCenter;
                        koltukButton.TextAlign = ContentAlignment.BottomCenter;
                        koltukButton.Font = new Font("Arial", 10, FontStyle.Bold);
                        koltukButton.ForeColor = Color.White;

                        koltukButton.Click += KoltukSecildi;

                        KoltukPanel.Controls.Add(koltukButton);
                    }
                }
            }

            void KoltukSecildi(object sender, EventArgs e)
            {
                Button btn = (Button)sender;

                if (btn.ImageIndex == 1)
                {
                    btn.ImageIndex = 0; // Tıklandığında görseli değiştir
                }
                else
                {
                    btn.ImageIndex = 1; // Değiştirilen görseli geri al
                }
            }
        }

        private void Txt_SalonAd_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
