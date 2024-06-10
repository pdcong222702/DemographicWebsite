using System.ComponentModel.DataAnnotations.Schema;

namespace Demographic_Website.Models
{
    public class SpiltResidentce
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? PopulationName { get; set; }
        public string? CitizenIdentificationCard { get; set; }
        public string? Reason {  get; set; } 
        public DateTime? CreateDate { get; set; }
        public virtual Population? Population { get; set; }
    }
}
