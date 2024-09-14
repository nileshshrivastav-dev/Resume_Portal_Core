using System;
using System.Collections.Generic;

namespace Resume_Portal.Models;

public partial class TblUser
{
    public string Email { get; set; } = null!;

    public string? Name { get; set; }

    public long? Mobile { get; set; }

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public bool? Status { get; set; }

    public string? Designation { get; set; }

    public string? Photo { get; set; }

    public DateTime DateOfJoining { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<TblResume> TblResumes { get; set; } = new List<TblResume>();
}
