using System;
using System.Collections.Generic;

namespace prjLionMVC.Models.Entity;

public partial class MemberTable
{
    public int MemberId { get; set; }

    public string MemberName { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string HashPassword { get; set; } = null!;

    public string SaltPassword { get; set; } = null!;

    public virtual ICollection<MessageBoardTable> MessageBoardTables { get; set; } = new List<MessageBoardTable>();
}
