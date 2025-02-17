﻿using System.ComponentModel.DataAnnotations;

namespace A2TEMPLATE.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }
}