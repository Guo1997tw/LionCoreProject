using System;
using System.Collections.Generic;

namespace prjLionMVC.Models.Entity;

public partial class MessageBoardTable
{
    public int MessageBoardId { get; set; }

    public int MemberId { get; set; }

    public string MessageText { get; set; } = null!;

    public DateTime MessageTime { get; set; }

    public virtual MemberTable Member { get; set; } = null!;
}
