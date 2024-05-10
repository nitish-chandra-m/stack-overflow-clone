using System;
using System.Collections.Generic;

namespace StackOverflowClone.Models;

public partial class Client
{
    public string Name { get; set; } = null!;

    public string Auth { get; set; } = null!;

    public string Endpoint { get; set; } = null!;

    public string P256dh { get; set; } = null!;
}
