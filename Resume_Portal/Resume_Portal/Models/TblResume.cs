using System;
using System.Collections.Generic;

namespace Resume_Portal.Models;

public partial class TblResume
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Designation { get; set; }

    public string? Resume { get; set; }

    public string? Title { get; set; }

    public string? KeyWords { get; set; }

    public bool? Status { get; set; }

    public string? UploadedBy { get; set; }

    public string? FilePath { get; set; }

    public virtual TblDesignation? DesignationNavigation { get; set; }

    public virtual TblUser? EmailNavigation { get; set; }
}
