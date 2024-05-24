using System;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SinemaOtomasyonu
{
    public partial class SinemaKartÜyelikleri : Form

    {
        private int _selectedCardId;
        private SqlConnection bgl = new SqlConnection("Data Source=.;Initial Catalog=sinemayeni;Integrated Security=True");
        private int Card_id;

        public SinemaKartÜyelikleri()
        {
            InitializeComponent();
        }

        private void SinemaKartÜyelikleri_Load(object sender, EventArgs e)
        {
            LoadCardData();
        }


        private void LoadCardData()
        {
            try
            {
                string query = "SELECT * FROM Card_Bilgi";
                SqlDataAdapter adapter = new SqlDataAdapter(query, bgl);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                DataGridÜye.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kart bilgileri alınamadı. Hata: " + ex.Message);
            }
        }

        private void ClearTextBoxes()
        {
            Txt_ÜyeAd.Text = "";
            Txt_ÜyeSoyad.Text = "";
            Txt_ÜyeMail.Text = "";
            Txt_ÜyeTel.Text = "";
            Txt_DogumTarihi.Text = DateTime.Now.ToShortDateString();
        }

        private string GenerateCardNumber()
        {
            Random rnd = new Random();
            return $"{rnd.Next(100, 999)}-{rnd.Next(1000, 9999)}-{rnd.Next(1000, 9999)}-{rnd.Next(100, 999)}";
        }

        private void DataGridÜye_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridÜye.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = DataGridÜye.SelectedRows[0];

                _selectedCardId = Convert.ToInt32(selectedRow.Cells["Card_id"].Value);
                Txt_ÜyeAd.Text = selectedRow.Cells["Card_Ad"].Value.ToString();
                Txt_ÜyeSoyad.Text = selectedRow.Cells["Card_Soyad"].Value.ToString();
                Txt_ÜyeMail.Text = selectedRow.Cells["Card_Mail"].Value.ToString();
                Txt_ÜyeTel.Text = selectedRow.Cells["Card_Tel"].Value.ToString();
                Txt_DogumTarihi.Text = selectedRow.Cells["Dogum_Tarihi"].Value.ToString();
            }
        }

        private void DataGridÜye_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = DataGridÜye.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = Color.Green;
            }
        }

        private void BtnKartEkle_Click(object sender, EventArgs e)
        {
            try
            {
                string cardNumber = GenerateCardNumber();
                string query = $"INSERT INTO Card_Bilgi (Card_No, Card_Ad, Card_Soyad, Card_Mail, Card_Tel, Dogum_Tarihi, Some_Column) " +
                               $"VALUES ('{cardNumber}', '{Txt_ÜyeAd.Text}', '{Txt_ÜyeSoyad.Text}', '{Txt_ÜyeMail.Text}', '{Txt_ÜyeTel.Text}', '{Txt_DogumTarihi.Text}', 0)";

                ExecuteNonQuery(query, "Kart ekleme");

                LoadCardData();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKartGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                string query = $"UPDATE Card_Bilgi SET Card_Ad = '{Txt_ÜyeAd.Text}', Card_Soyad = '{Txt_ÜyeSoyad.Text}', " +
                               $"Card_Mail = '{Txt_ÜyeMail.Text}', Card_Tel = '{Txt_ÜyeTel.Text}', Dogum_Tarihi = '{Txt_DogumTarihi.Text}' " +
                               $"WHERE Card_id = {_selectedCardId}";

                ExecuteNonQuery(query, "Kart güncelleme");

                LoadCardData();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKartSil_Click(object sender, EventArgs e)
        {
            try
            {
                string query = $"DELETE FROM Card_Bilgi WHERE Card_id = {Card_id}";

                ExecuteNonQuery(query, "Kart silme");

                LoadCardData();
                ClearTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExecuteNonQuery(string query, string action)
        {
            bgl.Open();
            try
            {
                DialogResult result = MessageBox.Show($"{action} işlemini onaylıyor musunuz?", $"{action} Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand(query, bgl);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"{action} başarıyla gerçekleştirildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                bgl.Close();
            }
        }
    }
}
