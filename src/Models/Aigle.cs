using ZooSimulator.Enums;

namespace ZooSimulator.Models
{
    public class Aigle : Animal
    {
        public Aigle(Sexe sexe, int moisArrivee, int ageInitial) : base("Aigle", sexe, moisArrivee, ageInitial)
        {
        }

        public override double GetConsommationMensuelle()
        {
            return Sexe == Sexe.Male ? 0.25 : 0.30;
        }

        public override decimal GetPrixAchat()
        {
            if (Age == 6) return 1000m;
            if (Age == 48) return 4000m;
            if (Age == 168) return 2000m;

            return 1000m;
        }

        public override decimal GetPrixVente()
        {
            if (Age == 6) return 500m;
            if (Age == 48) return 2000m;
            if (Age == 168) return 400m;

            return 500m;
        }

        public override int GetVisiteursSaisonHaute()
        {
            return 15;
        }

        public override double GetVisiteursSaisonBasse()
        {
            return 7;
        }

        public override double GetProbabiliteMaladieAnnuelle()
        {
            return 0.10;
        }

        public override int GetDureeMaladieBaseJours()
        {
            return 30;
        }

        public override int GetAgeMaturiteSexuelleMois()
        {
            return 48;
        }

        public override int GetAgeFinReproductionMois()
        {
            return 168;
        }

        public override int GetDureeGestationMois()
        {
            return Sexe == Sexe.Femelle ? 2 : 0;
        }

        public override int GetNombrePetitsParPortee()
        {
            return Sexe == Sexe.Femelle ? 2 : 0;
        }

        public override double GetMortaliteInfantile()
        {
            return Sexe == Sexe.Femelle ? 0.50 : 0.0;
        }

        public override int GetCooldownReproductionMois()
        {
            return 12;
        }
    }
}