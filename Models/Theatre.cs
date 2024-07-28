using System;
using System.Collections.Generic;

namespace MovieTicketingApplication.Models;

public partial class Theatre
{
    public int Id { get; set; }

    public string TheatreName { get; set; } = null!;

    public string ShowTimings { get; set; } = null!;

    public string Location { get; set; } = null!;

    public decimal ConvFee { get; set; }

    public decimal TicketFare { get; set; }

    public string? BookedSeats { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
