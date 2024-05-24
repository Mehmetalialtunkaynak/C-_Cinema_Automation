using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace SinemaOtomasyonu
{
    public partial class SatisRapor : Form
    {
        public SatisRapor()
        {


            InitializeComponent();



        }



        //SqlConnection bgl = new SqlConnection("Data Source=.;Initial Catalog=sinemayeni;Integrated Security=True");
        SqlConnection bgl = new SqlConnection(ConfigurationManager.ConnectionStrings["SinemaOtomasyon.Properties.Settings.sinemayeniConnectionString"].ConnectionString);
        private void SatisRapor_Load(object sender, EventArgs e)
        {

            maskedTextBox5.Text = "00:00";
            maskedTextBox6.Text = "23:59";
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DataGridSatisRapor.DataSource = null;
            DataTable tablo = new DataTable();
            bgl.Open();
            // Satis tablosu ve ilgili alanlar üzerinden filtreleme yap
            SqlDataAdapter adtr = new SqlDataAdapter(
                "SELECT * FROM Satis WHERE Satis_Tarih BETWEEN '" + maskedTextBox1.Text + "' AND '" + maskedTextBox2.Text + "' " +
                "AND Satis_Saat BETWEEN '" + maskedTextBox5.Text + "' AND '" + maskedTextBox6.Text + "'", bgl);
            adtr.Fill(tablo);
            DataGridSatisRapor.DataSource = tablo;
            bgl.Close();
        }




        private void btn_pdfcıktı_Click(object sender, EventArgs e)
        {
            PDF_Disa_Aktar(DataGridSatisRapor);
        }

        public static void PDF_Disa_Aktar(DataGridView dataGridView)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.OverwritePrompt = false;
            save.Title = "PDF Dosyaları";
            save.DefaultExt = "pdf";
            save.Filter = "PDF Dosyaları (*.pdf)|*.pdf|Tüm Dosyalar(*.*)|*.*";

            if (save.ShowDialog() == DialogResult.OK)
            {
                PdfPTable pdfTable = new PdfPTable(dataGridView.ColumnCount);

                pdfTable.DefaultCell.Padding = 3;
                pdfTable.WidthPercentage = 80;
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
                pdfTable.DefaultCell.BorderWidth = 1;

                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                    pdfTable.AddCell(cell);
                }

                try
                {
                    foreach (DataGridViewRow row in dataGridView.Rows)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            pdfTable.AddCell(cell.Value.ToString());
                        }
                    }
                }
                catch (NullReferenceException)
                {
                }

                using (FileStream stream = new FileStream(save.FileName + ".pdf", FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();
                }
            }
        }











        int kid = 0;
        public void idcek()
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("Select * from Admin where Mail=@p1", bgl);
            komut.Parameters.AddWithValue("@p1", txt_Mail.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                kid = Convert.ToInt32(dr[0]);
            }
            bgl.Close();
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DataGridSatisRapor.DataSource = null;
            idcek();

            if (kid > 0 && checkBox1.Checked)
            {
                DataGridSatisRapor.DataSource = null;
                DataTable tablo = new DataTable();
                bgl.Open();
                // Satis tablosu ve ilgili alanlar üzerinden filtreleme yap
                SqlDataAdapter adtr = new SqlDataAdapter(
                    "SELECT * FROM Satis WHERE Satis_Tarih BETWEEN '" + maskedTextBox1.Text + "' AND '" + maskedTextBox2.Text + "' " +
                    "AND Satis_Saat BETWEEN '" + maskedTextBox5.Text + "' AND '" + maskedTextBox6.Text + "' " +
                    "AND KasiyerID = " + kid, bgl);
                adtr.Fill(tablo);
                DataGridSatisRapor.DataSource = tablo;
                bgl.Close();
            }
            else if (kid > 0)
            {
                DataGridSatisRapor.DataSource = null;
                DataTable tablo = new DataTable();
                bgl.Open();
                // Satis tablosu ve ilgili alanlar üzerinden filtreleme yap
                SqlDataAdapter adtr = new SqlDataAdapter(
                    "SELECT * FROM Satis WHERE KasiyerID = " + kid, bgl);
                adtr.Fill(tablo);
                DataGridSatisRapor.DataSource = tablo;
                bgl.Close();
            }
            else
            {
                DataGridSatisRapor.DataSource = null;
            }
        }


        ArrayList seansid = new ArrayList();

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click_1(object sender, EventArgs e)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("yemredogrubp@gmail.com");
            //
            ePosta.To.Add("yemredogru@hotmail.com");
            ;

            ePosta.Subject = "";
            //
            ePosta.Body = "";
            Attachment attachment;
            attachment = new Attachment(@"C:\Users\yemre\OneDrive\Masaüstü\test1.pdf");
            ePosta.Attachments.Add(attachment);
            //
            SmtpClient smtp = new SmtpClient();
            //
            smtp.Credentials = new System.Net.NetworkCredential("yemredogrubp@gmail.com", "159357qM*");

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            object userState = ePosta;
            try
            {
                smtp.SendAsync(ePosta, (object)ePosta);
            }
            catch (SmtpException ex)
            {
                MessageBox.Show(ex.Message, "PDF dosyası mail adresine gönderilemedi");
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
