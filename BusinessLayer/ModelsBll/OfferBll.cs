using System;

namespace CarSharing
{
    public abstract class OfferBll
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }
        public float DailyCost { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        protected OfferBll(int id, string model, int year, string image, string description, string type)
        {
            Id = id;
            Model = model;
            Year = year;
            Image = image;
            Description = description;
            Type = type;
        }

        public abstract int CountShareCost(int term);

    }
    
}
