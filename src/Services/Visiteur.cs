using ZooSimulator.Enums;
using ZooSimulator.Models;

namespace ZooSimulator.Services
{
    public class Visiteur
    {
        private readonly Random random;

        public int DernierNombreVisiteurs { get; private set; }
        public decimal DernierRevenu { get; private set; }

        public const decimal TarifAdulte = 17m;
        public const decimal TarifEnfant = 13m;

        public Visiteur()
        {
            random = new Random();
            DernierNombreVisiteurs = 0;
            DernierRevenu = 0m;
        }

        public Saison ObtenirSaison(int moisJeu)
        {
            int moisAnnee = ((moisJeu - 1) % 12) + 1;

            if (moisAnnee >= 5 && moisAnnee <= 9)
            {
                return Saison.Haute;
            }

            return Saison.Basse;
        }

        public decimal CalculerRevenuMensuel(List<Animal> animaux, int moisJeu)
        {
            Saison saison = ObtenirSaison(moisJeu);

            double visiteursTheoriques = 0;

            foreach (Animal animal in animaux)
            {
                if (animal.Malade || animal.EstFemelleGestante())
                {
                    continue;
                }

                if (saison == Saison.Haute)
                {
                    visiteursTheoriques += animal.GetVisiteursSaisonHaute();
                }
                else
                {
                    visiteursTheoriques += animal.GetVisiteursSaisonBasse();
                }
            }

            double coefficientVariation = 0.8 + (random.NextDouble() * 0.4);
            double visiteursAvecVariation = visiteursTheoriques * coefficientVariation;

            DernierNombreVisiteurs = (int)Math.Round(visiteursAvecVariation, MidpointRounding.AwayFromZero);

            decimal revenuParFamille = (2 * TarifAdulte) + (2 * TarifEnfant);
            DernierRevenu = DernierNombreVisiteurs * revenuParFamille;

            return DernierRevenu;
        }
    }
}