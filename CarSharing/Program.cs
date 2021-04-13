using System;
using System.Collections.Generic;

namespace CarSharing
{
    class Program
    {
        static void Main(string[] args)
        {
            /*List<Car> cars = new List<Car>()
            {
                new SportCar("Mustang", 1999),
                new SportCar("KIA", 2000),
                new OutroadCar("Ford", 2001),
                new Minivan("KIA", 2010),
                new Minivan("BMW", 2015),
                new OutroadCar("BMW", 2011),
                new SportCar("Porsche", 2002),
                new OutroadCar("KIA", 2012),
                new Minivan("Ford", 2013),
            };
            List<Rent> rents1 = new List<Rent>();
            List<Rent> rents2 = new List<Rent>();

            for (int i = 0; i < 9; i++)
            {
                if(i%2 == 0)
                    rents1.Add(new Rent(cars[i], new Random().Next(0,10)));
                else
                    rents2.Add(new Rent(cars[i], new Random().Next(0, 10)));
            }
            
            RentalSalon salon1 = new RentalSalon("Harold", "First", rents1);
            RentalSalon salon2 = new RentalSalon("Rod", "Second", rents2);

            Console.WriteLine("First salon report:");
            salon1.GetReport();
            Console.WriteLine("Second salon report:");
            salon2.GetReport();
            */
            Console.ReadKey();
        }
    }
}
