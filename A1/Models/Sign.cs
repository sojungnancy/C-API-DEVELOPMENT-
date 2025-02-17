using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using A1.Data;
using A1.Dtos;

namespace A1.Models{
public class Sign {
  [Key]
  public string Id { get; set; }
  public string Description { get; set; } 
  }
}
