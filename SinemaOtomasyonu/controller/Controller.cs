using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SinemaOtomasyonu.dao;
using SinemaOtomasyonu.enumeration;
using SinemaOtomasyonu.model;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;




namespace SinemaOtomasyonu.controller
{
    public class Controller
    {
        Repository repository;


        public Controller()
        {
            repository = new Repository();
        }

        public User login(string Kullanici_Adi, string Sifre)
        {
            User result;
            if (!string.IsNullOrEmpty(Kullanici_Adi) && !string.IsNullOrEmpty(Sifre))
            {
                result = repository.login(Kullanici_Adi, Sifre);

                return result;
            }
            else
            {
                User user = new User();
                user.status = LoginStatus.eksikParametre;
                return user;
            }

        }

        public List<LoginTable> getLoginTable()
        {
            List<LoginTable> loginTableList = repository.getLoginTable();
            return loginTableList;
        }

        public LoginStatus doAuthentication(string Kullanici_Adi, string guvenlikSorusu, string guvenlikCevabi)
        {
            if (!string.IsNullOrEmpty(Kullanici_Adi) && !string.IsNullOrEmpty(guvenlikSorusu) && !string.IsNullOrEmpty(guvenlikCevabi))
            {
                LoginStatus result = repository.doAuthentication(Kullanici_Adi, guvenlikSorusu, guvenlikCevabi);

                if (result == LoginStatus.basarili)
                {
                    return result;
                }
                else
                {
                    return LoginStatus.basarisiz;
                }
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }

        public LoginStatus changePassword(string Kullanici_Adi, string Sifre)
        {
            if (!string.IsNullOrEmpty(Kullanici_Adi) && !string.IsNullOrEmpty(Sifre))
            {
                return repository.changePassword(Kullanici_Adi, Sifre);
            }

            else
            {
                return LoginStatus.eksikParametre;
            }
        }


        public string checkEmailAddress(string Kullanici_Adi)
        {
            return repository.checkEmailAddress(Kullanici_Adi);
        }

        

        public List<User> tumKullanicilariGetir()
        {
            Controller controller = new Controller();
            return repository.tumKullanicilariGetir();
        }

        public List<User> tumtumfilmleriGetir()
        {
            Controller controller = new Controller();
            return repository.tumKullanicilariGetir();
        }
        



        public LoginStatus kullaniciEkle(User user)
        {
            if (!string.IsNullOrEmpty(user.Kullanici_Adi) && !string.IsNullOrEmpty(user.Sifre) && !string.IsNullOrEmpty(user.Yetki) && !string.IsNullOrEmpty(user.Mail) && !string.IsNullOrEmpty(user.guvenlikSorusu) && !string.IsNullOrEmpty(user.guvenlikCevabi))
            {
                Controller controller = new Controller();
                LoginStatus sonuc = repository.kullaniciEkle(user);
                return sonuc;
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }

        public LoginStatus kullaniciGuncelle(User user)
        {
            return repository.kullaniciGuncelle(user);
        }

        public LoginStatus kullaniciSil(int id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                return repository.kullaniciSil(id);
            }
            else
            {
                return LoginStatus.eksikParametre;
            }

        }






        public List<Film> tumfilmlerigetir()
        {

            return repository.tumfilmleriGetir();
        }


        public LoginStatus FilmEkleSorgu(string FilmAdi, string FilmYapimYili, string FilmYonetmen, string Film_Sure, string FilmResim, string FilmTurID)
        {
            if (!string.IsNullOrEmpty(FilmAdi) && !string.IsNullOrEmpty(FilmYonetmen) && !string.IsNullOrEmpty(Film_Sure))
            {
                Film film = new Film
                {
                    FilmAdi = FilmAdi,
                    FilmYapimYili = FilmYapimYili,
                    FilmYonetmen = FilmYonetmen,
                    Film_Sure = Film_Sure,
                    FilmResim = FilmResim,
                    FilmTurID = FilmTurID
                };

                return repository.FilmEkleSorgu(film);
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }

        

        public LoginStatus FilmSil(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return repository.FilmSil(id);
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }


        //public LoginStatus SalonEkle(Salon salon)
        //{
        //    if(salon != null && !string.IsNullOrEmpty(salon.Salon_Ad)) 
        //    {
        //        Controller controller = new Controller();
        //        LoginStatus sonuc = repository.SalonEkle(salon);
        //        return sonuc;
        //    }
        //    else
        //    {
        //        return LoginStatus.eksikParametre;
        //    }

        //}

        public LoginStatus SalonEkle(Salon salon)
        {
            if (salon != null && !string.IsNullOrEmpty(salon.Salon_Ad))
            {
                LoginStatus sonuc = repository.SalonEkle(salon);
                return sonuc;
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }

    }
}
