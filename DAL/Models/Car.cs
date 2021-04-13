using System;

namespace DAL
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }

        public Car(int id, string model, int year, string type)
        {
            Id = id;
            Model = model;
            Year = year;
            Type = type;
        }
        
        
    }
}
