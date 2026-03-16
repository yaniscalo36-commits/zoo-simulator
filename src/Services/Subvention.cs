using ZooSimulator.Models;

namespace ZooSimulator.Services
{
    public class Subvention
    {
        public const decimal SubventionTigre = 43800m;
        public const decimal SubventionAigle = 2190m;

        public decimal CalculerSubventionAnnuelle(List<Animal> animaux)
        {
            decimal total = 0m;

            foreach (Animal animal in animaux)
            {
                if (animal.Espece == "Tigre")
                {
                    total += SubventionTigre;
                }
                else if (animal.Espece == "Aigle")
                {
                    total += SubventionAigle;
                }
            }

            return total;
        }
    }
}