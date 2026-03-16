using ZooSimulator.Enums;

namespace ZooSimulator.Models
{
    public class Poule : Animal
    {
        public Poule(int moisArrivee, int ageInitial) : base("Poule", Sexe.Femelle, moisArrivee, ageInitial)
        {
        }

        public override double GetConsommationMensuelle()
        {
            return 0.15;
        }

        public override decimal GetPrixAchat()
        {
            return 20m;
        }

        public override decimal GetPrixVente()
        {
            return 10m;
        }

        public override int GetVisiteursSaisonHaute()
        {
            return 2;
        }

        public override double GetVisiteursSaisonBasse()
        {
            return 0.5;
        }

        public override double GetProbabiliteMaladieAnnuelle()
        {
            return 0.05;
        }

        public override int GetDureeMaladieBaseJours()
        {
            return 5;
        }

        public override int GetAgeMaturiteSexuelleMois()
        {
            return 6;
        }

        public override int GetAgeFinReproductionMois()
        {
            return 96;
        }

        public override int GetDureeGestationMois()
        {
            return 2;
        }

        public override int GetNombrePetitsParPortee()
        {
            return 8;
        }

        public override double GetMortaliteInfantile()
        {
            return 0.50;
        }

        public override int GetCooldownReproductionMois()
        {
            return 2;
        }
    }
}