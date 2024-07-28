using System;
using System.Collections.Generic;

namespace MovieTicketingApplication.Models;

public partial class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Lang { get; set; } = null!;

    public int Duration { get; set; }

    public string? Poster { get; set; }

    public string? Trailer { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
