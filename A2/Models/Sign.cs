using System.ComponentModel.DataAnnotations;

namespace A2TEMPLATE.Models
{
    public class Sign
    {
        [Key]
        public string Id { get; set; }
        public string Description { get; set; }
    }
}