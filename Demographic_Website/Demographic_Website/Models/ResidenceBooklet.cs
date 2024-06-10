using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demographic_Website.Models;

public partial class ResidenceBooklet
{
    public int ResidenceBookletId { get; set; }
    [Required(ErrorMessage = "Bạn chưa nhập mã sổ hộ khẩu")]
    public string ResidenceBookletCode { get; set; }
    public string Address { get; set; }
    public DateTime? MoveDate { get; set; }
    public string? MoveReason { get; set; }
    public string? BookletArea { get; set; }
    public DateTime? CreateDate { get; set; }
    public virtual ICollection<Population> Populations { get; set; } = new List<Population>();
}
