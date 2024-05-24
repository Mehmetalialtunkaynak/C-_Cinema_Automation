using SinemaOtomasyonu.model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using SinemaOtomasyonu.enumeration;
using LoginStatus = SinemaOtomasyonu.enumeration.LoginStatus;
using System.Data;

namespace SinemaOtomasyonu.dao
{
    // *Repository Klasının Görevi Veritabanı İle Konuşmaktır. 
    

    public class Repository
    {
        public SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        int returnvalue;
        List<LoginTable> loginTablelist;

        public Repository()
        {
            
            con = new SqlConnection(@"Data Source =.; Initial Catalog = sinemayeni; Persist Security Info = True; User ID = sa;Password=SQLserver6236");
        }

        public void baglantiAyarla()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                baglantiAyarla();
            }
        }

        public User login(string Kullanici_Adi, string Sifre)
        {

            con.Open();
            cmd = new SqlCommand("select * from Admin where Kullanici_Adi=@Kullanici_Adi and Sifre=@Sifre", con);
            cmd.Parameters.AddWithValue("@Kullanici_Adi", Kullanici_Adi);
            cmd.Parameters.AddWithValue("@Sifre", Sifre);
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                User user = new User();
                user.ID = int.Parse(dr["ID"].ToString());
                user.Kullanici_Adi = dr["Kullanici_Adi"].ToString();
                user.Sifre = dr["Sifre"].ToString();
                user.Yetki = dr["Yetki"].ToString();
                user.Mail = dr["Mail"].ToString();
                user.guvenlikSorusu = dr["guvenlikSorusu"].ToString();
                user.guvenlikCevabi = dr["guvenlikCevabi"].ToString();
                user.status = LoginStatus.basarili;
                return user;
            }
            else
            {
                User user = new User();
                user.status = LoginStatus.basarisiz;
                return user;
            }

        }

        public List<LoginTable> getLoginTable()
        {
            loginTablelist = new List<LoginTable>();
            try
            {
                con.Open();
                cmd = new SqlCommand("guvenlikSorusuGetir_sp", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    LoginTable loginTable = new LoginTable();
                    loginTable.ID = int.Parse(dr["ID"].ToString());
                    loginTable.Kullanici_Adi = dr["Kullanici_Adi"].ToString();
                    loginTable.Sifre = dr["Sifre"].ToString();
                    loginTable.Yetki = dr["Yetki"].ToString();
                    loginTable.Mail = dr["Mail"].ToString();
                    loginTable.guvenlikSorusu = dr["guvenlikSorusu"].ToString();
                    loginTable.guvenlikCevabi = dr["guvenlikCevabi"].ToString();
                    loginTablelist.Add(loginTable);
                }
                con.Close();
            }
            catch (Exception)
            {
                Console.WriteLine("Hata oluştu");
            }
            return loginTablelist;
        }

        public LoginStatus doAuthentication(string Kullanici_Adi, string guvenlikSorusu, string guvenlikCevabi)
        {
            con.Open();
            cmd = new SqlCommand("select count(*) from loginTable where Kullanici_Adi=@Kullanici_Adi and guvenlikSorusu=@guvSorusu and guvenlikCevabi=@guvCevabi", con);
            cmd.Parameters.AddWithValue("@Kullanici_Adi", Kullanici_Adi);
            cmd.Parameters.AddWithValue("@guvSorusu", guvenlikSorusu);
            cmd.Parameters.AddWithValue("@guvCevabi", guvenlikCevabi);
            returnvalue = (int)cmd.ExecuteScalar();
            con.Close();

            if (returnvalue == 1)
            {
                return LoginStatus.basarili;
            }
            else
            {
                return LoginStatus.basarisiz;
            }


        }

        public LoginStatus changePassword(string Kullanici_Adi, string Sifre)
        {
            con.Open();
            cmd = new SqlCommand("SifreGuncelle_sp", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Kullanici_Adi", Kullanici_Adi);
            cmd.Parameters.AddWithValue("@Sifre", Sifre);
            returnvalue = (int)cmd.ExecuteNonQuery();
            con.Close();
            return LoginStatus.basarili;
        }

        public string checkEmailAddress(string Kullanici_Adi)
        {
            con.Open();
            cmd = new SqlCommand("select Mail from loginTable where Kullanici_Adi=@Kullanici_Adi", con);
            cmd.Parameters.AddWithValue("@Kullanici_Adi", Kullanici_Adi);
            string Mail = (string)cmd.ExecuteScalar();
            con.Close();
            return Mail;

        }

        

        public List<User> tumKullanicilariGetir()
        {
            List<User> userList = new List<User>();
            con.Open();
            cmd = new SqlCommand("select * from Admin", con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                User user = new User();
                user.ID = int.Parse(dr["ID"].ToString());
                user.Kullanici_Adi = dr["Kullanici_Adi"].ToString();
                user.Sifre = dr["Sifre"].ToString();
                user.TC = int.Parse(dr["TC"].ToString());
                user.Yetki = dr["Yetki"].ToString();
                user.Mail = dr["Mail"].ToString();
                user.Ad = dr["Ad"].ToString();
                user.Soyad = dr["Soyad"].ToString();
                user.guvenlikSorusu = dr["guvenlikSorusu"].ToString();
                user.guvenlikCevabi = dr["guvenlikCevabi"].ToString();
                userList.Add(user);
            }
            con.Close();
            return userList;
        }


        public LoginStatus kullaniciEkle(User user)
        {
            con.Open();

            cmd = new SqlCommand("sp_kullaniciEkle", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Kullanici_Adi", user.Kullanici_Adi);
            cmd.Parameters.AddWithValue("@Sifre", user.Sifre);
            cmd.Parameters.AddWithValue("@Yetki", user.Yetki);
            cmd.Parameters.AddWithValue("@Mail", user.Mail);
            cmd.Parameters.AddWithValue("@guvenlikSorusu", user.guvenlikSorusu);
            cmd.Parameters.AddWithValue("@guvenlikCevabi", user.guvenlikCevabi);

            int returnvalue = cmd.ExecuteNonQuery();
            con.Close();

            if (returnvalue == 1)
            {
                return LoginStatus.basarili;
            }
            else
            {
                return LoginStatus.basarisiz;
            }

        }

        public LoginStatus kullaniciGuncelle(User user)
        {
            con.Open();
            cmd = new SqlCommand("sp_kullaniciGuncelle", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", user.ID);
            cmd.Parameters.AddWithValue("@Kullanici_Adi", user.Kullanici_Adi);
            cmd.Parameters.AddWithValue("@Sifre", user.Sifre);
            cmd.Parameters.AddWithValue("@Yetki", user.Yetki);
            cmd.Parameters.AddWithValue("@Mail", user.Mail);
            cmd.Parameters.AddWithValue("@guvenlikSorusu", user.guvenlikSorusu);
            cmd.Parameters.AddWithValue("@guvenlikCevabi", user.guvenlikCevabi);
            int returnvalue = cmd.ExecuteNonQuery();
            con.Close();

            if (returnvalue == 1)
            {
                return LoginStatus.basarili;
            }
            return LoginStatus.basarisiz;
        }

        public LoginStatus kullaniciSil(int ID)
        {
            con.Open();
            cmd = new SqlCommand("Delete From LoginTable where ID=@ID", con);
            cmd.Parameters.AddWithValue("@ID", ID);
            int returnvalue = cmd.ExecuteNonQuery();
            con.Close();

            if (returnvalue == 1)
            {
                return LoginStatus.basarili;
            }
            return LoginStatus.basarisiz;
        }











        /*
        public List<Film> tumfilmleriGetir()
        {
            List<Film> filmlist = new List<Film>();
            con.Open();
            cmd = new SqlCommand("select * from Filmler", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Film film = new Film();
                film.FilmID = int.Parse(dr["FilmID"].ToString());
                film.FilmAdi = dr["FilmAdi"].ToString();
                film.FilmYapimYili = dr["FilmYapimYili"].ToString();
                film.FilmYonetmen = (dr["FilmYonetmen"].ToString());
                film.Film_Sure = dr["Film_Sure"].ToString();
                film.FilmResim = dr["FilmResim"].ToString();
                film.FilmTurID = dr["FilmTurID"].ToString();
                filmlist.Add(film);
            }
            con.Close();
            return filmlist;
        }
        */


        public List<Film> tumfilmleriGetir()
        {
            List<Film> filmlist = new List<Film>();
            con.Open();
            SqlCommand cmd = new SqlCommand(@"SELECT
	                                                F.*,
	                                                FT.TurAD
                                                 FROM
	                                                  Filmler F,
	                                                  FilmTurler FT
                                                WHERE
	                                                 F.FilmTurID = FT.FilmTurID", con);

            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Film film = new Film();
                film.FilmID = int.Parse(dr["FilmID"].ToString());
                film.FilmAdi = dr["FilmAdi"].ToString();
                film.FilmYapimYili = dr["FilmYapimYili"].ToString();
                film.FilmYonetmen = (dr["FilmYonetmen"].ToString());
                film.Film_Sure = dr["Film_Sure"].ToString();
                film.FilmResim = dr["FilmResim"].ToString();
                film.FilmTurID = dr["FilmTurID"].ToString();
                film.TurAD = dr["TurAD"].ToString();
                filmlist.Add(film);
            }
            con.Close();
            return filmlist;
        }


        public LoginStatus FilmEkleSorgu(Film film)
        {
            con.Open();
            cmd = new SqlCommand("sp_filmEkle", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FilmAdi", film.FilmAdi);
            cmd.Parameters.AddWithValue("@FilmYapimYili", film.FilmYapimYili);
            cmd.Parameters.AddWithValue("@FilmYonetmen", film.FilmYonetmen);
            cmd.Parameters.AddWithValue("@Film_Sure", film.Film_Sure);
            cmd.Parameters.AddWithValue("@FilmResim", film.FilmResim);
           cmd.Parameters.AddWithValue("@FilmTurID", Convert.ToInt32(film.FilmTurID));
            
        int returnvalue = cmd.ExecuteNonQuery();

            con.Close();
            if (returnvalue == 1)
            {
                return LoginStatus.basarili;
            }
            return LoginStatus.basarisiz;
        }





        public LoginStatus FilmSil(string FilmID)
        {
            con.Open();
            cmd = new SqlCommand("delete from Filmler where FilmID=@FilmID", con);
            cmd.Parameters.AddWithValue("@FilmID", FilmID);
            int returnvalue = cmd.ExecuteNonQuery();
            con.Close();

            if (returnvalue == 1)
            {
                return LoginStatus.basarili;
            }
            return LoginStatus.basarisiz;
        }



        /* Hatalı 
        public LoginStatus SalonEkle(string salon)
        {
            con.Open();
            using (SqlCommand command = new SqlCommand("SalonEkle", con))
            cmd.Parameters.AddWithValue("@Salon_Ad", salon);
            int returnvalue = cmd.ExecuteNonQuery();
            con.Close();

            if (returnvalue == 1)
            {
                return LoginStatus.basarili;
            }
            else
            {
                return LoginStatus.basarisiz;

            }
        }
        */


        //Doğrusu 
        public LoginStatus SalonEkle(Salon salon)
        {
            con.Open();
            using (SqlCommand command = new SqlCommand("SalonEkle", con))
            {
                command.CommandType = CommandType.StoredProcedure; // Stored procedure kullanılacak

                // @SalonAd parametresini ekleyin
                command.Parameters.AddWithValue("@SalonAd", salon.Salon_Ad);

                int returnvalue = command.ExecuteNonQuery();
                con.Close();

                if (returnvalue == 1)
                {
                    return LoginStatus.basarili;
                }
                else
                {
                    return LoginStatus.basarisiz;
                }
            }
        }




    }
}
