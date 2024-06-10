using Demographic_Website.Models;
using System.ComponentModel.DataAnnotations;

namespace Demographic_Website.ModelViews
{
    public class BookLet
    {
        [Key] public int Id { get; set; }
        
        public ResidenceBooklet ResidenceBooklet { get; set; }
        public Population Population { get; set; }
    }
}
