using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConpuGroupMedical.Models
{
public class Kullanici
{
[Key]
public int Id { get; set; }
public string Kullanici_Adi { get; set; }
public string Parola { get; set; }
public string Kullanici_Ad_Soyad { get; set; }
public string Kullanici_Mail { get; set; }
public int Rol_Id { get; set; }
public int Kullanici_Telefon { get; set; }
public string Kullanici_Adres { get; set; }
public int Bagli_Oldugu_Hastane_Id { get; set; }
public int Bagli_Oldugu_Sigorta_Id { get; set; }
public bool Aktif_Pasif { get; set; }
}

}
