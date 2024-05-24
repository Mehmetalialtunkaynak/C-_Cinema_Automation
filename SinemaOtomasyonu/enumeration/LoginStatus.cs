using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SinemaOtomasyonu.enumeration;
using SinemaOtomasyonu.model;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;
using SinemaOtomasyonu.dao;

namespace SinemaOtomasyonu.enumeration
{
    public enum LoginStatus
    {
        basarili, basarisiz, eksikParametre
    }

}
