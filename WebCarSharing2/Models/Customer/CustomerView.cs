﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCarSharing2.Models.Customer
{
    public class CustomerView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}