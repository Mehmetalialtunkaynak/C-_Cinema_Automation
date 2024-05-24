using SinemaOtomasyonu.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SinemaOtomasyonu.controller;
using SinemaOtomasyonu.enumeration;

namespace SinemaOtomasyonu
{
    public partial class SifreDegistirme : Form
    {
        int code;



        public SifreDegistirme()
        {
            InitializeComponent();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void SifreDegistirme_Load(object sender, EventArgs e)
        {
            Controller controller = new Controller();

            List<LoginTable> loginTableList = controller.getLoginTable();
            grpBox_mailAlani.Enabled = false;
            grpBox_sifreDegistirmeAlani.Enabled = false;

            foreach (LoginTable lt in loginTableList)
            {
                combobox_guvenlikSorusu.Items.Add(lt.guvenlikSorusu.ToString());

            }
            combobox_guvenlikSorusu.SelectedIndex = 0;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void combobox_guvenlikSorusu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_DogrulamaKodu_TextChanged(object sender, EventArgs e)
        {

        }

        private void combobox_guvenlikSorusu_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controller controller = new Controller();
            LoginStatus result = controller.doAuthentication(txt_kullaniciAdi.Text.Trim().ToLower(), combobox_guvenlikSorusu.SelectedItem.ToString(), txt_guvenlikCevabi.Text.ToLower());

            if (result == LoginStatus.basarili)
            {
                grpBox_mailAlani.Enabled = true;
            }
            else if (result == LoginStatus.basarisiz)
            {
                MessageBox.Show("Girdiğiniz Bilgileri Lütfen Kontrol Ediniz !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Lütfen Tüm Alanları Doldurunuz ve Boş Alan bırakmayınız !", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (result == LoginStatus.basarili)
            {
                grpBox_mailAlani.Enabled = true;
                progressBar.Value = progressBar.Maximum / 4;
            }
        }

        private void btn_dogrulamaGonder_Click(object sender, EventArgs e)
        {

            Controller controller = new Controller();
            string txt_Mail = controller.checkEmailAddress(txt_kullaniciAdi.Text);

            Random rdn = new Random();             /* 111111 ve 999999 sayıları arasında random olarak bir sayı oluşturuyoruz  */
            code = rdn.Next(111111, 999999);


            string to = txt_Mail;
            string from = "mudureilayda2002@hotmail.com";
            string password = "ilayda2002";
            string subject = "Test Mail" + code;
            string body = "Bu bir test mailidir.";


            SmtpClient smtpClient = new SmtpClient("smtp.outlook.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(from, password);

            MailMessage mailMessage = new MailMessage(from, to, subject, body);
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;

            try
            {
                smtpClient.Send(mailMessage);
                Console.WriteLine("Mail gönderildi.");
                progressBar.Value = (progressBar.Maximum / 4) * 2;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Mail gönderirken hata oluştu: " + ex.Message);
            }
        }



            private void btn_Onayla_Click(object sender, EventArgs e)
            {
                if (txt_DogrulamaKodu.Text == code.ToString())
                {
                    grpBox_sifreDegistirmeAlani.Enabled = true;
                    progressBar.Value = (progressBar.Maximum / 4) * 3;

                }
                else
                {
                    MessageBox.Show("Doğrulama Kodunuz Yanlıştır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }




            private void btn_Degistir_Click(object sender, EventArgs e)
            {
                Controller controller = new Controller();

                if (txt_yeniSifre.Text == txt_yeniSifreTkr.Text)
                {
                    LoginStatus result = controller.changePassword(txt_kullaniciAdi.Text, txt_yeniSifre.Text);

                    if (result == LoginStatus.basarili)
                    {
                        MessageBox.Show("Şifreniz Değiştirilmiştir.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        progressBar.Value = progressBar.Maximum;
                    }
                    else
                    {
                        MessageBox.Show("Gerekli alanları doldurunuz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("İki Şifre birbirleriyle aynı değildir!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void label6_Click(object sender, EventArgs e)
            {

            }

            private void progressBar1_Click(object sender, EventArgs e)
            {

            }

            private void btn_geri_Click(object sender, EventArgs e)
            {
                Form Form1 = new AdminGiris();
                Form1.Show();
                this.Hide();
            }




        }
    }
