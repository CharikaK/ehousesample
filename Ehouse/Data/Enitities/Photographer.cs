﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ehouse.Data.Enitities
{
    public class Photographer
    {
        public int Id { get; set; }
        public string PFname { get; set; }
        public string PLastname { get; set; }
        public  Booking Booking { get; set; } // photagrapher owns the booking
    }
}
