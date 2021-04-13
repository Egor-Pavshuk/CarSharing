using System;

namespace CarSharing
{
    public class Minivan: Car
    {
        public string Type { get; set; }
        public Minivan(int id, string model, int year) : base(id, model, year)
        {
            Type = "Minivan";
        }

        public override int CountShareCost(int term) => term <=0 ? throw new Exception("Term can`t be below or equal 0!") : 30 * term;

    }
}
