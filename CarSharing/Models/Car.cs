using System;

namespace CarSharing
{
    public abstract class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        protected Car(int id, string model, int year)
        {
            Id = id;
            Model = model;
            Year = year;
        }
        
        public abstract int CountShareCost(int term);
    }
}
