using System;

namespace DAL
{
    public class SportCar: Car 
    {
        public string Type { get; set; }
        public SportCar(int id, string model, int year) : base(id, model, year, "Sport")
        {
        }
        
    }
}
