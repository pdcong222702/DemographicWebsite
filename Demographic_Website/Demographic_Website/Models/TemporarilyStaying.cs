using System;
using System.Collections.Generic;

namespace Demographic_Website.Models;

public partial class TemporarilyStaying
{
    public int TemporarilyStayingId { get; set; }

    public string? PopulationName { get; set; }

    public bool? Gender { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Nationality { get; set; }

    public string? CitizenIdentificationCard { get; set; }

    public bool? IsNewStay { get; set; }

    public string? TemporaryAddress { get; set; }

    public string? BirthPlace { get; set; }

    public DateOnly? FromDate { get; set; }

    public string? Reason { get; set; }
    public string? Phone {  get; set; }
    public string? DocumentCodeStaying { get; set; }
    public int? PopulationId { get; set; }

    public virtual Population? Population { get; set; }
}
