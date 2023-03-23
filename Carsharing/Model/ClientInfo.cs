using System;
using System.Collections.Generic;

namespace Carsharing.Model;

public partial class ClientInfo
{
    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public int Age { get; set; }

    public string PassportType { get; set; } = null!;

    public int PassportNum { get; set; }

    public int RiderNum { get; set; }

    public int TelephoneNum { get; set; }

    public int? CardNumber { get; set; }

    public int? Balance { get; set; }

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;
}
