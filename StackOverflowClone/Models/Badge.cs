using System;
using System.Collections.Generic;

namespace StackOverflowClone.Models;

public partial class Badge
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int UserId { get; set; }

    public DateTime Date { get; set; }
}
