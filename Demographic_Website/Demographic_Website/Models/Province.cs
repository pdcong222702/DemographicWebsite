using System.ComponentModel.DataAnnotations.Schema;

namespace Demographic_Website.Models
{
    public class Province
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProvinceId { get; set; }
        public string Name { get; set; }
    }
}
