using ZooSimulator.Models;

namespace ZooSimulator.Services
{
    public class EvenementAleatoire
    {
        private readonly Random random;

        public string DernierEvenement { get; private set; }
        public double DernierePerteViande { get; private set; }
        public double DernierePerteGraines { get; private set; }
        public string DernierHabitatPerdu { get; private set; }
        public string DernierAnimalPerdu { get; private set; }

        public EvenementAleatoire()
        {
            random = new Random();
            DernierEvenement = "Aucun";
            DernierePerteViande = 0;
            DernierePerteGraines = 0;
            DernierHabitatPerdu = "";
            DernierAnimalPerdu = "";
        }

        public void TraiterEvenement(Zoo zoo)
        {
            DernierEvenement = "Aucun";
            DernierePerteViande = 0;
            DernierePerteGraines = 0;
            DernierHabitatPerdu = "";
            DernierAnimalPerdu = "";

            double tirage = random.NextDouble();

            if (tirage < 0.01)
            {
                GererIncendie(zoo);
                return;
            }

            if (tirage < 0.02)
            {
                GererVol(zoo);
                return;
            }

            if (tirage < 0.22)
            {
                GererNuisibles(zoo);
                return;
            }

            if (tirage < 0.32)
            {
                GererViandeAvariee(zoo);
                return;
            }
        }

        private void GererIncendie(Zoo zoo)
        {
            if (zoo.Habitats.Count == 0)
            {
                DernierEvenement = "Incendie évité : aucun habitat à détruire";
                return;
            }

            int index = random.Next(zoo.Habitats.Count);
            Habitat habitatPerdu = zoo.Habitats[index];

            DernierHabitatPerdu = habitatPerdu.Type;
            DernierEvenement = $"Incendie : un habitat {habitatPerdu.Type} a été détruit";

            foreach (Animal animal in habitatPerdu.Animaux.ToList())
            {
                zoo.Animaux.Remove(animal);
            }

            zoo.Habitats.RemoveAt(index);
        }

        private void GererVol(Zoo zoo)
        {
            if (zoo.Animaux.Count == 0)
            {
                DernierEvenement = "Vol évité : aucun animal à voler";
                return;
            }

            int index = random.Next(zoo.Animaux.Count);
            Animal animalPerdu = zoo.Animaux[index];

            DernierAnimalPerdu = animalPerdu.Espece;
            DernierEvenement = $"Vol : un {animalPerdu.Espece} a été volé";

            Habitat? habitat = zoo.Habitats.FirstOrDefault(h => h.Animaux.Contains(animalPerdu));
            if (habitat != null)
            {
                habitat.RetirerAnimal(animalPerdu);
            }

            zoo.Animaux.Remove(animalPerdu);
        }

        private void GererNuisibles(Zoo zoo)
        {
            double perte = zoo.Stock.RetirerPourcentageGraines(0.10);
            DernierePerteGraines = perte;
            DernierEvenement = $"Nuisibles : perte de {perte:F2} kg de graines";
        }

        private void GererViandeAvariee(Zoo zoo)
        {
            double perte = zoo.Stock.RetirerPourcentageViande(0.20);
            DernierePerteViande = perte;
            DernierEvenement = $"Viande avariée : perte de {perte:F2} kg de viande";
        }
    }
}