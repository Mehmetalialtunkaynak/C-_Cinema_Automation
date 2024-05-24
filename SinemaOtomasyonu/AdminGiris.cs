using SinemaOtomasyonu.controller;
using SinemaOtomasyonu.enumeration;
using SinemaOtomasyonu.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace SinemaOtomasyonu
{
    public partial class AdminGiris : Form
    {
        public AdminGiris()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt_Sifre.UseSystemPasswordChar = true;
            m_checkPaswordGoster.Checked = true;
        }
        
        public static int kasiyerid;
        public static string kullanici;
        public static string adsoyad;

        private void btn_girisYap_Click_1(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            User result = controller.login(txt_Kullanici_Adi.Text, txt_Sifre.Text);
            
            if (result != null && result.status == LoginStatus.basarili && result.Yetki == "Admin")
            {
                AdminSayfa admin = new AdminSayfa();
                admin.Show();
                this.Hide();
            }
            else if (result != null && result.status == LoginStatus.basarili && result.Yetki == "kasiyer")
            {

                AnaSayfa Kullanici = new AnaSayfa();
                Kullanici.Show();
                this.Hide();
            }
            else if (result != null && result.status == LoginStatus.basarisiz)
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                MessageBox.Show("Eksik parametre hatası", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void LoginExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SimgeDurumunaButonu_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void btn_Sifremiunuttum_Click(object sender, EventArgs e)
        { 
            SifreDegistirme SD = new SifreDegistirme();
            SD.Show();
            this.Hide();
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

        private void m_checkPaswordGoster_Click(object sender, EventArgs e)
        {
            if(m_checkPaswordGoster.Checked)
            {
                txt_Sifre.UseSystemPasswordChar = true;
            }
            else
            {
                txt_Sifre.UseSystemPasswordChar = false;
            }

        }
    }
}
