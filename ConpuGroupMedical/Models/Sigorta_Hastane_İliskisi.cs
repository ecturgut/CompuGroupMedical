using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConpuGroupMedical.Models
{
public class Sigorta_Hastane_İliskisi
{
[Key]
public int Id { get; set; }
public string SKRS { get; set; }
public int Hastane_Id { get; set; }
public int Sigorta_Id { get; set; }

}

}