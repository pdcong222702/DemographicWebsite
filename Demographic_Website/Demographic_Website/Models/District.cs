using System.ComponentModel.DataAnnotations.Schema;

namespace Demographic_Website.Models
{
    public class District
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; }
    }
}
