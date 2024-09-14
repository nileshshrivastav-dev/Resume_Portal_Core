using System;
using System.Collections.Generic;

namespace Resume_Portal.Models;

public partial class TblDesignation
{
    public string Designation { get; set; } = null!;

    public bool? Status { get; set; }

    public virtual ICollection<TblResume> TblResumes { get; set; } = new List<TblResume>();
}
