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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SinemaOtomasyonu
{
    public partial class Seanslar : Form
    {
        public Seanslar()
        {
            InitializeComponent();
        }
        //SqlConnection bgl = new SqlConnection("Data Source=desktop-o4fqdka;Initial Catalog=sinemayeni;Integrated Security=True");
        SqlConnection bgl = new SqlConnection(ConfigurationManager.ConnectionStrings["SinemaOtomasyon.Properties.Settings.sinemayeniConnectionString"].ConnectionString);
        //sinemayeniDataSetTableAdapters.SeansTableAdapter seans = new sinemayeniDataSetTableAdapters.SeansTableAdapter();
        //sinemayeniDataSetTableAdapters.DataTable1TableAdapter filmadi = new sinemayeniDataSetTableAdapters.DataTable1TableAdapter();

        private void Seanslar_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'sinemayeniDataSet6.Seans' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
          //  this.seansTableAdapter.Fill(this.sinemayeniDataSet6.Seans);
            salonidcek();
            filmidcek();
            combo_filmad.SelectedItem = null;
            combo_salonad.SelectedItem = null;
            combo_salonad.SelectedItem = null;
            combo_filmad.SelectedItem = null;

        }
        public void salonidcek()
        {

            SqlCommand komut = new SqlCommand("SELECT SalonID,Salon_Ad From Salonlar", bgl);
            SqlDataAdapter tid = new SqlDataAdapter(komut);
            DataTable komut1 = new DataTable();
            tid.Fill(komut1);
            combo_salonad.DisplayMember = "Salon_Ad";
            combo_salonad.ValueMember = "SalonID";
            combo_salonad.DataSource = komut1;
            combo_salonad.DisplayMember = "Salon_Ad";
            combo_salonad.ValueMember = "SalonID";
            combo_salonad.DataSource = komut1;
        }
        public void filmidcek()
        {

            SqlCommand komut = new SqlCommand("SELECT FilmID,FilmAdi From Filmler", bgl);
            SqlDataAdapter tid = new SqlDataAdapter(komut);
            DataTable komut1 = new DataTable();
            tid.Fill(komut1);
            combo_filmad.DisplayMember = "FilmAdi";
            combo_filmad.DisplayMember = "FilmAdi";
            combo_filmad.ValueMember = "FilmID";
            combo_filmad.ValueMember = "FilmID";
            combo_filmad.DataSource = komut1;
            combo_filmad.DataSource = komut1;

        }
        int filmsuresi;
        public void filmsrs()
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("Select Film_Sure From Filmler where FilmAdi='" + combo_filmad.Text + "'", bgl);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                filmsuresi = Convert.ToInt32(dr["Film_Sure"]);
            }
            bgl.Close();
        }
        bool tarihkontrol;
        void Tarihcek()
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM Seans where Salon_Ad='" + combo_salonad.Text + "'AND Seans_tarih='" + DateTime.Text + "'AND Seans_saat='" + Txt_Seans.Text + "'", bgl);
            SqlDataReader read = komut.ExecuteReader();

            if (read.Read())
            {

                tarihkontrol = false;

            }
            else
                tarihkontrol = true;




            bgl.Close();
        }
        private void SimpleButton1_Click(object sender, EventArgs e)
        {
            filmsrs();
            Tarihcek();
            try
            {

                DataRowView sb2 = combo_filmad.SelectedItem as DataRowView;
                string sb3 = sb2.Row["FilmID"].ToString();
                DataRowView sb = combo_salonad.SelectedItem as DataRowView;
                string sb1 = sb.Row["Salon_Ad"].ToString();
                DialogResult rs = MessageBox.Show("Seans eklemek istediğinize emin misiniz ?", "Seans ekleme onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    if (tarihkontrol == false)
                    {
                        MessageBox.Show("Bu saatte başka bir seans var.");
                        Txt_Seans.Text = "";
                    }
                    else
                    {
                        if (System.DateTime.Parse(DateTime.Text) == System.DateTime.Parse(System.DateTime.Now.ToShortDateString()))
                        {
                            if (System.DateTime.Parse(Txt_Seans.Text) < System.DateTime.Parse(System.DateTime.Now.ToShortTimeString()))
                            {
                                MessageBox.Show("Geçmiş saate yönelik işlem yapamazsınız.");
                            }
                            else
                            {
                                if (combo_filmad.SelectedItem != null && combo_salonad.SelectedItem != null)
                                {
                                   // seans.SeansEkleSorgu(Convert.ToInt32(sb3), combo_salonad.Text, DateTime.Text, Txt_Seans.Text);
                                    MessageBox.Show(combo_filmad.Text + " film " + DateTime.Text + " tarihinde saat " + Txt_Seans.Text + " gösterime girecektir.");
                                   // this.seansTableAdapter.Fill(this.sinemayeniDataSet6.Seans);
                                    combo_filmad.SelectedItem = null;
                                    combo_salonad.SelectedItem = null;
                                    Txt_Seans.Text = "";
                                }
                            }
                        }
                        else
                        {
                            if (combo_filmad.SelectedItem != null && combo_salonad.SelectedItem != null)
                            {
                               // seans.SeansEkleSorgu(Convert.ToInt32(sb3), combo_salonad.Text, DateTime.Text, Txt_Seans.Text);
                                MessageBox.Show(combo_filmad.Text + " film " + DateTime.Text + " tarihinde saat " + Txt_Seans.Text + " gösterime girecektir.");
                               // this.seansTableAdapter.Fill(this.sinemayeniDataSet6.Seans);
                                combo_filmad.SelectedItem = null;
                                combo_salonad.SelectedItem = null;
                                Txt_Seans.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    combo_filmad.SelectedItem = null;
                    combo_salonad.SelectedItem = null;
                    DateTime.Text = System.DateTime.Now.ToShortDateString();
                    Txt_Seans.Text = "";
                }




            }
            catch (Exception)
            {
                MessageBox.Show("Seans eklenirken hata oluştu", "Uyarı");
            }
        }

        private void combo_salonad_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime.Text = System.DateTime.Now.ToShortDateString();
        }

        private void DateTime_EditValueChanged(object sender, EventArgs e)
        {
            DateTime bugün = System.DateTime.Parse(System.DateTime.Now.ToShortDateString());
            DateTime yeni = System.DateTime.Parse(DateTime.Text);
            if (yeni < bugün)
            {
                MessageBox.Show("Geriye dönük işlem yapamazsınız");
                DateTime.Text = System.DateTime.Now.ToShortDateString();
            }
        }
        string filmadi1 = "";
        private void filmadicek()
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("Select FilmAdi from Filmler where FilmID=@p1", bgl);
            komut.Parameters.AddWithValue("@p1", filmid);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                filmadi1 = Convert.ToString(dr[0]);
            }
            bgl.Close();
        }
        int seansid;
        int filmid;

        //private void DataGridSeans_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        //{

        //    DataRow dr = DataGridSeans.GetDataRow(DataGridSeans.FocusedRowHandle);
        //    if (dr != null)
        //    {
        //        filmid = Convert.ToInt32(dr[0]);

        //        combo_salonad.Text = dr[1].ToString();
        //        DateTime.Text = dr[3].ToString();
        //        Txt_Seans.Text = dr[4].ToString();
        //        seansid = Convert.ToInt32(dr[2]);
        //        //foreach(var item in combo_filmad.Items)
        //        //{
        //        //    DataRowView row1 =item as DataRowView;
        //        //    string dp = row1["FilmID"].ToString();
        //        //    if(dp==dr[0].ToString())
        //        //    {
        //        //        combo_filmad.SelectedIndex = j;
        //        //    }
        //        //    j = j + 1;
        //        //}
        //    }
        //    filmadicek();
        //    combo_filmad.Text = filmadi1.ToString();

        //}

        private void SimpleButton2_Click(object sender, EventArgs e)
        {
            DataRowView sb5 = combo_filmad.SelectedItem as DataRowView;
            string sb6 = sb5.Row["FilmID"].ToString();

            Tarihcek();
            try
            {
                DialogResult rs = MessageBox.Show("Seçili seansı düzenlemek istediğinize emin misiniz ?", "Seans düzenleme onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    if (tarihkontrol == false)
                    {
                        MessageBox.Show("Bu saatte başka bir seans bulunuyor.");
                    }
                    else
                    {
                        if (System.DateTime.Parse(DateTime.Text) == System.DateTime.Parse(System.DateTime.Now.ToShortDateString()))
                        {
                            if (System.DateTime.Parse(Txt_Seans.Text) < System.DateTime.Parse(System.DateTime.Now.ToShortTimeString()))
                            {
                                MessageBox.Show("Geçmiş saate yönelik işlem yapamazsınız.");
                            }
                            else
                            {
                                if (combo_salonad.SelectedItem != null && combo_filmad.SelectedItem != null)
                                {
                                    bgl.Open();
                                    SqlCommand komut = new SqlCommand("Update Seans set FilmID=@p1,Salon_Ad=@p2,Seans_tarih=@p3,Seans_saat=@p4 where SeansID=@p5", bgl);
                                    komut.Parameters.AddWithValue("@p1", sb6);
                                    komut.Parameters.AddWithValue("@p2", combo_salonad.Text);
                                    komut.Parameters.AddWithValue("@p3", DateTime.Text);
                                    komut.Parameters.AddWithValue("@p4", Txt_Seans.Text);
                                    komut.Parameters.AddWithValue("@p5", seansid);
                                    komut.ExecuteNonQuery();
                                    bgl.Close();
                                    MessageBox.Show("Seans başarıyla güncellendi.");
                                   // this.seansTableAdapter.Fill(this.sinemayeniDataSet6.Seans);
                                    combo_salonad.SelectedItem = null;
                                    combo_filmad.SelectedItem = null;
                                    Txt_Seans.Text = "";
                                }
                            }
                        }
                        else
                        {
                            if (combo_filmad.SelectedItem != null && combo_salonad.SelectedItem != null)
                            {
                                bgl.Open();
                                SqlCommand komut = new SqlCommand("Update Seans set FilmID=@p1,Salon_Ad=@p2,Seans_tarih=@p3,Seans_saat=@p4 where SeansID=@p5", bgl);
                                komut.Parameters.AddWithValue("@p1", sb6);
                                komut.Parameters.AddWithValue("@p2", combo_salonad.Text);
                                komut.Parameters.AddWithValue("@p3", DateTime.Text);
                                komut.Parameters.AddWithValue("@p4", Txt_Seans.Text);
                                komut.Parameters.AddWithValue("@p5", seansid);
                                komut.ExecuteNonQuery();
                                bgl.Close();
                                MessageBox.Show("Seans başarıyla güncellendi.");
                             //   this.seansTableAdapter.Fill(this.sinemayeniDataSet6.Seans);
                                combo_salonad.SelectedItem = null;
                                combo_filmad.SelectedItem = null;
                                Txt_Seans.Text = "";
                            }
                        }
                    }
                }
                else
                {
                    combo_salonad.SelectedItem = null;
                    combo_filmad.SelectedItem = null;
                    DateTime.Text = System.DateTime.Now.ToShortDateString();
                    Txt_Seans.Text = "";
                }



            }
            catch (Exception)
            {
                MessageBox.Show("Seans güncellenemedi.");

            }
        }

        private void SimpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult rs = MessageBox.Show("Seçili seansı düzenlemek istediğinize emin misiniz ?", "Seans düzenleme onayı", MessageBoxButtons.YesNo);
                if (rs == DialogResult.Yes)
                {
                    bgl.Open();
                    SqlCommand komut = new SqlCommand("Delete From Seans where SeansID=@p1", bgl);
                    komut.Parameters.AddWithValue("@p1", seansid);
                    komut.ExecuteNonQuery();
                    bgl.Close();
                    MessageBox.Show("Seans başarıyla silindi.");
                  //  this.seansTableAdapter.Fill(this.sinemayeniDataSet6.Seans);
                    combo_salonad.SelectedItem = null;
                    combo_filmad.SelectedItem = null;
                    Txt_Seans.Text = "";
                }
                else
                {
                    combo_salonad.SelectedItem = null;
                    combo_filmad.SelectedItem = null;
                    DateTime.Text = System.DateTime.Now.ToShortDateString();
                    Txt_Seans.Text = "";
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Seans silinemedi.");
            }
        }

        //private void DataGridSeans_RowStyle(object sender, RowStyleEventArgs e)
        //{
        //    GridView View = sender as GridView;

        //    if (e.RowHandle >= 0)
        //    {
        //        string Kategori = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Seans_tarih"]); // Kolon adı ile

        //        if (System.DateTime.Parse(Kategori) < System.DateTime.Parse(System.DateTime.Now.ToShortDateString()))
        //        {

        //            e.Appearance.BackColor = Color.Red;


        //        }
        //        else
        //            e.Appearance.BackColor = Color.Green;
        //    }
        //}
    }
}
