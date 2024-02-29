using System;
using System.Collections.Generic;

namespace prjLionMVC.Models.Entity;

public partial class ErrorLogTable
{
    public int ErrorId { get; set; }

    public string? Message { get; set; }

    public string? StackTrace { get; set; }

    public DateTime? DateCreated { get; set; }
}
