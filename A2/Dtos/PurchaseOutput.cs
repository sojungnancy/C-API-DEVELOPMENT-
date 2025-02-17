using System.ComponentModel.DataAnnotations;
namespace A2TEMPLATE.Dtos
{
    public class PurchaseOutput{
        [Key]
        public string UserName { get; set; }
        [Key]
        public string SignID { get; set; }

    }
}