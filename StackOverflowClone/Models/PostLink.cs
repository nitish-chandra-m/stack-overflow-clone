using System;
using System.Collections.Generic;

namespace StackOverflowClone.Models;

public partial class PostLink
{
    public int Id { get; set; }

    public DateTime CreationDate { get; set; }

    public int PostId { get; set; }

    public int RelatedPostId { get; set; }

    public int LinkTypeId { get; set; }
}
