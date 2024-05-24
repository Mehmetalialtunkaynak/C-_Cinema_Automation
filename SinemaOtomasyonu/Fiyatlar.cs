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
    public partial class Fiyatlar : Form
    {

        public Fiyatlar()
        {
            InitializeComponent();
        }
        //SqlConnection bgl = new SqlConnection("Data Source=desktop-o4fqdka;Initial Catalog=sinemayeni;Integrated Security=True");
        SqlConnection bgl = new SqlConnection(ConfigurationManager.ConnectionStrings["SinemaOtomasyon.Properties.Settings.sinemayeniConnectionString"].ConnectionString);
        private void Fiyatlar_Load(object sender, EventArgs e)
        {

            // TODO: Bu kod satırı 'sinemayeniDataSet13.Fiyat' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.

        }

        private void SimpleButton1_Click(object sender, EventArgs e)
        {


        }

        private void Txt_Fiyat_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void Txt_Fiyattürü_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void SimpleButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = MessageBox.Show("Yeni tarife eklemek istediğinize emin misiniz ?", "Tarife Ekleme Onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    if (Txt_Fiyattürü.Text != "" && Txt_Fiyat.Text != "")
                    {
                        //fiyat.FiyatEkle(Txt_Fiyattürü.Text, Convert.ToInt32(Txt_Fiyat.Text));
                        MessageBox.Show("Yeni Tarife başarıyla eklendi.");
                    }
                    else
                    {
                        MessageBox.Show("Tarife adı veya tarife ücreti eksik girilmiş, lütfen formu kontrol ediniz.");

                    }
                }
                else
                {
                    Txt_Fiyattürü.Text = "";
                    Txt_Fiyat.Text = "";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("HATA!!!", "Hata Mesajı");
            }
        }
        int fiyatid;
        /*
        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {

                fiyatid = Convert.ToInt32(dr[0]);
                Txt_FiyattürüDüzenle.Text = dr[1].ToString();
                Txt_FiyatDüzenle.Text = dr[2].ToString();
            }
        }*/
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (DataGridViewFiyatlar.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = DataGridViewFiyatlar.SelectedRows[0];
                int index = selectedRow.Index;

                // Örnek olarak 0. hücredeki değeri alıyoruz (sizin verinizin yapısına göre bu indeks değişebilir)
                int fiyatid = Convert.ToInt32(DataGridViewFiyatlar.Rows[index].Cells[0].Value);
                string fiyatTuru = DataGridViewFiyatlar.Rows[index].Cells[1].Value.ToString();
                string fiyatDuzenle = DataGridViewFiyatlar.Rows[index].Cells[2].Value.ToString();

                Txt_Fiyattürü.Text = fiyatTuru;
                Txt_Fiyat.Text = fiyatDuzenle;
            }
        }






        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = MessageBox.Show("Mevcut tarifeyi değiştirmek istediğinize emin misiniz ?", "Tarife Değişiklik Onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                  //  fiyat.FiyatGüncelle(Txt_Fiyattürü.Text, Convert.ToInt32(Txt_Fiyat.Text), fiyatid, fiyatid);
                    MessageBox.Show("Tarife güncellendi.");
                    Txt_Fiyattürü.Text = "";
                    Txt_Fiyat.Text = "";
                }
                else
                {
                    Txt_Fiyat.Text = "";
                    Txt_Fiyattürü.Text = "";
                }
            }
            catch (Exception)
            {

                MessageBox.Show("HATA!!!", "Hata Mesajı");
            }
        }

        private void SimpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = MessageBox.Show("Mevcut tarifeyi silmek üzeresiniz, bu işlemi onaylıyor musunuz?", "Tarife Silme Onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    //fiyat.FiyatSil(fiyatid);
                    MessageBox.Show("Tarife silindi, bu işlem geri alınamaz.");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Beklenmedik bir sorun oluştu, lütfen yetkili birisine danışınız.", "Hata Mesajı");
            }
        }
        /*
        private void GridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                e.Appearance.BackColor = Color.Green;

            }
        }
        */

        private void DataGridViewFiyatlar_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewFiyatlar.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                // Tüm satırların arka plan rengini yeşil olarak ayarlar
            }
        }



    }
}
