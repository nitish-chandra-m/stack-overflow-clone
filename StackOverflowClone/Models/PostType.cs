using System;
using System.Collections.Generic;

namespace StackOverflowClone.Models;

public partial class PostType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;
}
