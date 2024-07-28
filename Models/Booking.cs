using System;
using System.Collections.Generic;

namespace MovieTicketingApplication.Models;

public partial class Booking
{
    public long Id { get; set; }

    public long Userid { get; set; }

    public int Movieid { get; set; }

    public int Theatreid { get; set; }

    public DateOnly BookingDate { get; set; }

    public TimeOnly BookingTime { get; set; }

    public string SeatsString { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual Theatre Theatre { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
