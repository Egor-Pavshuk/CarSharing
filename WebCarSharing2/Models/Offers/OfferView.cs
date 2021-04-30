using System;
using System.Collections.Generic;


namespace WebCarSharing.Models.Offers
{
    public class OfferView
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public float DailyCost { get; set; }
        public string Description { get; set; }
    }
}