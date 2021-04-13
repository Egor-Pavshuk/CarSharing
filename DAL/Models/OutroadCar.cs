using System;

namespace DAL
{
    public class OutroadCar: Car
    {
        public string Type { get; set; }
        public OutroadCar(int id, string model, int year) : base(id, model, year, "Out-road")
        {
        }
        
    }
}
