using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConpuGroupMedical.Models
{
public class Grup_Tanimi
{
[Key]
public int Id { get; set; }
public int Grup_Kodu { get; set; }
public string Grup_Adi { get; set; }

}
}
