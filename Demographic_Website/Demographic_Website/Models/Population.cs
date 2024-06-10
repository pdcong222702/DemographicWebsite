using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Demographic_Website.Models;

public partial class Population
{
    public int PopulationId { get; set; }
    [Required(ErrorMessage ="Bạn phải nhập họ tên")]
    public string PopulationName { get; set; }
    public string? PopulationNickName { get; set; }
    public bool? Gender { get; set; }
    public string? Image { get; set; }
    [Required(ErrorMessage ="Bạn phải nhập ngày sinh")]
    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfDeath { get; set; }
    [Required(ErrorMessage = "Bạn phải nhập nơi sinh")]
    public string BirthPlace { get; set; }
    [Required(ErrorMessage = "Bạn phải dân tộc")]
    public string? Ethnicity { get; set; }
    public string? Religion { get; set; }
    public string? WorkPlace { get; set; }
    public string? Occupation { get; set; }
    public bool? Alive { get; set; }
    public bool? IsChoose { get; set; }
    [Required(ErrorMessage = "Bạn phải nhập CCCD")]
    [MinLength(12,ErrorMessage ="CCCD không phù hợp")]
    public string CitizenIdentificationCard { get; set; }
    [Required(ErrorMessage = "Bạn phải nhập ngày cấp CCCD")]
    public DateTime? DateOfIssue { get; set; }
    [Required(ErrorMessage = "Bạn phải nhập nơi cấp CCCD")]
    public string? PlaceOfIssue { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? ResidenceBookletId { get; set; }
    public string? Relationship { get; set; }
    public string? EducationalLevels { get; set; }
    public virtual ResidenceBooklet? ResidenceBooklet { get; set; }
    public virtual ICollection<TemporarilyAbsent> TemporarilyAbsents { get; set; } = new List<TemporarilyAbsent>();
    public virtual ICollection<TemporarilyStaying> TemporarilyStayings { get; set; } = new List<TemporarilyStaying>();
    public virtual ICollection<SpiltResidentce> SpiltResidentces { get; set; } = new List<SpiltResidentce>();
    public virtual ICollection<MoveResidence> MoveResidences { get; set; } = new List<MoveResidence>();
}
