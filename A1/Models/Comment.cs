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
public class Comment {
  [Key]
  public int Id { get; set; }
  public string UserComment { get; set; } = null!;
  public string Name { get; set; } = null!;
  public string Time { get; set; } = null!;
  public string IP { get; set; } = null!;
  }
}
