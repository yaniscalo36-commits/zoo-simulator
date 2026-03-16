using ZooSimulator.Enums;

namespace ZooSimulator.Models
{
    public class Tigre : Animal
    {
        public Tigre(Sexe sexe, int moisArrivee, int ageInitial) : base("Tigre", sexe, moisArrivee, ageInitial)
        {
        }

        public override double GetConsommationMensuelle()
        {
            return Sexe == Sexe.Male ? 12.0 : 10.0;
        }

        public override decimal GetPrixAchat()
        {
            if (Age == 6) return 3000m;
            if (Age == 48) return 120000m;
            if (Age == 168) return 60000m;

            return 3000m;
        }

        public override decimal GetPrixVente()
        {
            if (Age == 6) return 1500m;
            if (Age == 48) return 60000m;
            if (Age == 168) return 10000m;

            return 1500m;
        }

        public override int GetVisiteursSaisonHaute()
        {
            return 30;
        }

        public override double GetVisiteursSaisonBasse()
        {
            return 5;
        }

        public override double GetProbabiliteMaladieAnnuelle()
        {
            return 0.30;
        }

        public override int GetDureeMaladieBaseJours()
        {
            return 15;
        }

        public override int GetAgeMaturiteSexuelleMois()
        {
            return Sexe == Sexe.Male ? 72 : 48;
        }

        public override int GetAgeFinReproductionMois()
        {
            return 168;
        }

        public override int GetDureeGestationMois()
        {
            return Sexe == Sexe.Femelle ? 3 : 0;
        }

        public override int GetNombrePetitsParPortee()
        {
            return Sexe == Sexe.Femelle ? 3 : 0;
        }

        public override double GetMortaliteInfantile()
        {
            return Sexe == Sexe.Femelle ? 0.33 : 0.0;
        }

        public override int GetCooldownReproductionMois()
        {
            return 20;
        }
    }
}