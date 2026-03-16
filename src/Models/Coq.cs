using ZooSimulator.Enums;

namespace ZooSimulator.Models
{
    public class Coq : Animal
    {
        public Coq(int moisArrivee, int ageInitial) : base("Coq", Sexe.Male, moisArrivee, ageInitial)
        {
        }

        public override double GetConsommationMensuelle()
        {
            return 0.18;
        }

        public override decimal GetPrixAchat()
        {
            return 100m;
        }

        public override decimal GetPrixVente()
        {
            return 20m;
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
            return 0;
        }

        public override int GetNombrePetitsParPortee()
        {
            return 0;
        }

        public override double GetMortaliteInfantile()
        {
            return 0.0;
        }

        public override int GetCooldownReproductionMois()
        {
            return 0;
        }
    }
}