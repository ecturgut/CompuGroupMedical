using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConpuGroupMedical.Models
{
public class Hastane
{
[Key]
public int Id { get; set; }
public int Tesis_Kodu { get; set; }
public string Hastane_Adi { get; set; }
public string Hastane_Telefon { get; set; }
public string Hastene_Adres { get; set; }
public int  Bagli_Oldugu_Grup_Id { get; set; }


    }

}
