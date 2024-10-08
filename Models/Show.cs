﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MovieTicketingApplication.Models;

public partial class Show
{
    public int Id { get; set; } 

    public int ScreenId { get; set; }

    public DateTime ShowTime { get; set; }

    public int? AvailableSeats { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Screen Screen { get; set; }
}