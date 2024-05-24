using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Web.UI.Design.WebControls;
using System.Web.UI;
using UserControl = System.Windows.Forms.UserControl;

namespace SinemaOtomasyonu
{
    public partial class AdminSayfa : Form
    {


        bool tarihGosteriliyor = true;
        Timer timer;

        
        public AdminSayfa()
        {
            InitializeComponent();
            //Bilet biletsayfasi = new Bilet();
            //addUserControl(biletsayfasi);

            btn_tarih_saat.Text = DateTime.Now.ToLongDateString(); // Başlangıçta tarihi göster
            timer = new Timer();
            timer.Interval = 1000; // Her 1 saniyede bir güncelleme yapacak
            timer.Tick += Timer_Tick;
            timer.Start();
        }


        public void addUserControl(Form userControl)
        {
            MainPanel.Controls.Clear();
            userControl.TopLevel = false;
            userControl.AutoScroll = false;
            MainPanel.Controls.Add(userControl);
            userControl.FormBorderStyle = FormBorderStyle.None;
            userControl.WindowState = FormWindowState.Maximized;    
            userControl.Show();
            
        }



        private void BtnBilet_Click(object sender, EventArgs e)
        {
            BiletSatisİslemi biletsayfasi = new BiletSatisİslemi();
            addUserControl(biletsayfasi);


            SidePanel.Height = BtnBilet.Height;
            SidePanel.Top = BtnBilet.Top;
        }

        private void BtnFilm_Click(object sender, EventArgs e)
        {
            Filmler filmsayfasi = new Filmler();
            addUserControl(filmsayfasi);

            SidePanel.Height = BtnFilm.Height;
            SidePanel.Top = BtnFilm.Top;
        }

        private void btn_satisrapor_Click(object sender, EventArgs e)
        {
            SatisRapor satisraporsayfasi = new SatisRapor();
            addUserControl(satisraporsayfasi);
        }
        private void btn_kullanicilar_Click(object sender, EventArgs e)
        {
            Kullanıcılar kullanicisayfasi = new Kullanıcılar();
            addUserControl(kullanicisayfasi);

            SidePanel.Height = btn_kullanicilar.Height;
            SidePanel.Top = btn_kullanicilar.Top;
        }

        private void BtnDown_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void BtnMaximized_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }



        private void BtnAnasayfa_Click(object sender, EventArgs e)
        {
            AnaFilmGösterim anasayfasi = new AnaFilmGösterim();
            addUserControl(anasayfasi);
            WindowState = FormWindowState.Normal;
            SidePanel.Height = BtnAnasayfa.Height;
            SidePanel.Top = BtnAnasayfa.Top;
            
        }

        private void BtnSeans_Click(object sender, EventArgs e)
        {
            Seanslar seanssayfasi= new Seanslar();
            addUserControl(seanssayfasi);


            SidePanel.Height = BtnSeans.Height;
            SidePanel.Top = BtnSeans.Top;
            
        }
        private void BtnSalon_Click(object sender, EventArgs e)
        {
            SalonOlustur salonolusturmasayfasi = new SalonOlustur();
            addUserControl(salonolusturmasayfasi);
            
            SidePanel.Height = BtnSalon.Height;
            SidePanel.Top = BtnSalon.Top;
            
        }

        
        

        private void BtnSeansListeleme_Click(object sender, EventArgs e)
        {
            SidePanel.Height = BtnSeansListeleme.Height;
            SidePanel.Top = BtnSeansListeleme.Top;
            
        }

        private void BtnHakkımızda_Click(object sender, EventArgs e)
        {
            Hakkımızda HakkımızdaSayfasi = new Hakkımızda();
            addUserControl(HakkımızdaSayfasi);


            SidePanel.Height = BtnHakkımızda.Height;
            SidePanel.Top = BtnHakkımızda.Top;
            
        }

        



        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void BtnShutdown_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void GrpBoxAnasayfa_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_sinemakart_Click(object sender, EventArgs e)
        {
            SinemaKartÜyelikleri sinemakart = new SinemaKartÜyelikleri();
            addUserControl(sinemakart);


            SidePanel.Height = btn_sinemakart.Height;
            SidePanel.Top = btn_sinemakart.Top;

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
                btn_tarih_saat.Text = DateTime.Now.ToString("HH:mm:ss"); // Saati ve saniyeyi anlık güncelle
            }
        }

        
    }
}
