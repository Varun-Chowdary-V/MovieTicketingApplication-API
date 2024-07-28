using System;
using System.Collections.Generic;

namespace MovieTicketingApplication.Models;

public partial class User
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHashed { get; set; } = null!;

    public string? Phone { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
