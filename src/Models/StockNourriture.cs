using ZooSimulator.Enums;

namespace ZooSimulator.Models
{
    public class StockNourriture
    {
        public double ViandeKg { get; private set; }
        public double GrainesKg { get; private set; }

        public StockNourriture()
        {
            ViandeKg = 0;
            GrainesKg = 0;
        }

        public void AcheterViande(double quantite)
        {
            ViandeKg += quantite;
        }

        public void AcheterGraines(double quantite)
        {
            GrainesKg += quantite;
        }

        public bool Consommer(TypeNourriture type, double quantite)
        {
            if (type == TypeNourriture.Viande)
            {
                if (ViandeKg < quantite)
                {
                    return false;
                }

                ViandeKg -= quantite;
                return true;
            }

            if (GrainesKg < quantite)
            {
                return false;
            }

            GrainesKg -= quantite;
            return true;
        }

        public double RetirerPourcentageViande(double pourcentage)
        {
            double perte = ViandeKg * pourcentage;
            ViandeKg -= perte;

            if (ViandeKg < 0)
            {
                ViandeKg = 0;
            }

            return perte;
        }

        public double RetirerPourcentageGraines(double pourcentage)
        {
            double perte = GrainesKg * pourcentage;
            GrainesKg -= perte;

            if (GrainesKg < 0)
            {
                GrainesKg = 0;
            }

            return perte;
        }

        public override string ToString()
        {
            return $"Stock nourriture | Viande : {ViandeKg:F2} kg | Graines : {GrainesKg:F2} kg";
        }
    }
}