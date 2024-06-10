using System.ComponentModel.DataAnnotations.Schema;

namespace Demographic_Website.Models
{
    public class Wards
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WardsId { get; set; }
        public int DistrictId { get; set; }
        public string Name { get; set; }
    }
}
