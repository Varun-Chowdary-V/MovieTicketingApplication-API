﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace MovieTicketingApplication.Models;

public partial class Booking
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ShowId { get; set; }

    public DateTime BookingDateTime { get; set; }

    public int Seats { get; set; }

    public decimal? Price { get; set; }

    public virtual Show Show { get; set; }

    public virtual User User { get; set; }
}