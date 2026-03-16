using ZooSimulator.Enums;
using ZooSimulator.Services;

namespace ZooSimulator.Models
{
    public class Zoo
    {
        private readonly Random random;

        public string Nom { get; private set; }
        public decimal Budget { get; set; }
        public int Mois { get; private set; }
        public List<Animal> Animaux { get; private set; }
        public List<Habitat> Habitats { get; private set; }
        public StockNourriture Stock { get; private set; }
        public Visiteur GestionVisiteurs { get; private set; }
        public Subvention GestionSubventions { get; private set; }
        public Reproduction GestionReproduction { get; private set; }
        public EvenementAleatoire GestionEvenements { get; private set; }

        public int DerniersVisiteurs { get; private set; }
        public decimal DernierRevenuVisiteurs { get; private set; }
        public decimal DerniereSubvention { get; private set; }

        public int DerniersNouveauxMalades { get; private set; }
        public int DerniersMortsMaladie { get; private set; }
        public int DerniersGueris { get; private set; }

        public int DernieresGrossesses { get; private set; }
        public int DernieresNaissances { get; private set; }
        public int DernieresMortsInfantiles { get; private set; }
        public int DernieresFaussesCouches { get; private set; }

        public string DernierEvenementAleatoire { get; private set; }

        public Zoo(string nom)
        {
            random = new Random();

            Nom = nom;
            Budget = 80000m;
            Mois = 1;
            Animaux = new List<Animal>();
            Habitats = new List<Habitat>();
            Stock = new StockNourriture();
            GestionVisiteurs = new Visiteur();
            GestionSubventions = new Subvention();
            GestionReproduction = new Reproduction();
            GestionEvenements = new EvenementAleatoire();

            DerniersVisiteurs = 0;
            DernierRevenuVisiteurs = 0m;
            DerniereSubvention = 0m;
            DerniersNouveauxMalades = 0;
            DerniersMortsMaladie = 0;
            DerniersGueris = 0;
            DernieresGrossesses = 0;
            DernieresNaissances = 0;
            DernieresMortsInfantiles = 0;
            DernieresFaussesCouches = 0;
            DernierEvenementAleatoire = "Aucun";
        }

        public bool AcheterHabitat(string type)
        {
            decimal prix = 0;
            Habitat? habitat = null;

            if (type == "Tigre")
            {
                prix = 2000m;
                habitat = new Habitat("Tigre", 2);
            }
            else if (type == "Aigle")
            {
                prix = 2000m;
                habitat = new Habitat("Aigle", 4);
            }
            else if (type == "Poules")
            {
                prix = 300m;
                habitat = new Habitat("Poules", 10);
            }

            if (habitat == null || Budget < prix)
            {
                return false;
            }

            Habitats.Add(habitat);
            Budget -= prix;
            return true;
        }

        public bool AcheterNourriture(TypeNourriture type, double quantite)
        {
            decimal prix;

            if (type == TypeNourriture.Viande)
            {
                prix = (decimal)quantite * 5m;
            }
            else
            {
                prix = (decimal)quantite * 2.5m;
            }

            if (Budget < prix)
            {
                return false;
            }

            Budget -= prix;

            if (type == TypeNourriture.Viande)
            {
                Stock.AcheterViande(quantite);
            }
            else
            {
                Stock.AcheterGraines(quantite);
            }

            return true;
        }

        public bool AcheterAnimal(Animal animal)
        {
            Habitat? habitat = TrouverHabitatDisponible(animal);

            if (habitat == null)
            {
                return false;
            }

            decimal prix = animal.GetPrixAchat();

            if (Budget < prix)
            {
                return false;
            }

            bool ajoute = habitat.AjouterAnimal(animal);

            if (!ajoute)
            {
                return false;
            }

            Animaux.Add(animal);
            Budget -= prix;
            return true;
        }

        public void PasserAuTourSuivant()
        {
            Mois++;

            DerniersNouveauxMalades = 0;
            DerniersMortsMaladie = 0;
            DerniersGueris = 0;
            DerniereSubvention = 0m;
            DernieresGrossesses = 0;
            DernieresNaissances = 0;
            DernieresMortsInfantiles = 0;
            DernieresFaussesCouches = 0;
            DernierEvenementAleatoire = "Aucun";

            foreach (Animal animal in Animaux)
            {
                animal.Vieillir();
            }

            NourrirAnimaux();
            GererMaladies();
            RecalculerListeAnimauxDepuisHabitats();

            GestionReproduction.TraiterReproduction(Habitats, Mois);
            DernieresGrossesses = GestionReproduction.DernieresGrossesses;
            DernieresNaissances = GestionReproduction.DernieresNaissances;
            DernieresMortsInfantiles = GestionReproduction.DernieresMortsInfantiles;
            DernieresFaussesCouches = GestionReproduction.DernieresFaussesCouches;

            RecalculerListeAnimauxDepuisHabitats();

            decimal revenuVisiteurs = GestionVisiteurs.CalculerRevenuMensuel(Animaux, Mois);
            DerniersVisiteurs = GestionVisiteurs.DernierNombreVisiteurs;
            DernierRevenuVisiteurs = revenuVisiteurs;
            Budget += revenuVisiteurs;

            if (Mois % 12 == 0)
            {
                DerniereSubvention = GestionSubventions.CalculerSubventionAnnuelle(Animaux);
                Budget += DerniereSubvention;
            }

            GestionEvenements.TraiterEvenement(this);
            DernierEvenementAleatoire = GestionEvenements.DernierEvenement;

            RecalculerListeAnimauxDepuisHabitats();
        }

        private void NourrirAnimaux()
        {
            foreach (Animal animal in Animaux)
            {
                TypeNourriture type = ObtenirTypeNourriture(animal);
                double quantite = animal.GetConsommationMensuelle();

                if (animal.EstFemelleGestante())
                {
                    quantite *= 2;
                }

                bool aMange = Stock.Consommer(type, quantite);

                if (aMange)
                {
                    animal.Manger();
                }
            }
        }

        private void GererMaladies()
        {
            List<Animal> animauxAMourir = new List<Animal>();

            foreach (Animal animal in Animaux)
            {
                if (animal.Malade)
                {
                    bool etaitMaladeAvant = animal.Malade;
                    animal.AvancerMaladie(30);

                    if (etaitMaladeAvant && !animal.Malade)
                    {
                        DerniersGueris++;
                    }

                    continue;
                }

                double probabiliteMensuelle = animal.GetProbabiliteMaladieAnnuelle() / 12.0;
                double tirage = random.NextDouble();

                if (tirage < probabiliteMensuelle)
                {
                    int duree = CalculerDureeMaladieAvecVariation(animal.GetDureeMaladieBaseJours());
                    animal.TomberMalade(duree);
                    DerniersNouveauxMalades++;

                    if (random.NextDouble() < 0.10)
                    {
                        animauxAMourir.Add(animal);
                    }
                }
            }

            foreach (Animal animalMort in animauxAMourir)
            {
                SupprimerAnimalDuZoo(animalMort);
                DerniersMortsMaladie++;
            }
        }

        private int CalculerDureeMaladieAvecVariation(int dureeBase)
        {
            double coefficient = 0.8 + (random.NextDouble() * 0.4);
            int duree = (int)Math.Round(dureeBase * coefficient, MidpointRounding.AwayFromZero);

            if (duree < 1)
            {
                duree = 1;
            }

            return duree;
        }

        private void SupprimerAnimalDuZoo(Animal animal)
        {
            Habitat? habitat = Habitats.FirstOrDefault(h => h.Animaux.Contains(animal));

            if (habitat != null)
            {
                habitat.RetirerAnimal(animal);
            }

            Animaux.Remove(animal);
        }

        private void RecalculerListeAnimauxDepuisHabitats()
        {
            Animaux = Habitats
                .SelectMany(h => h.Animaux)
                .Distinct()
                .ToList();
        }

        private TypeNourriture ObtenirTypeNourriture(Animal animal)
        {
            if (animal.Espece == "Tigre" || animal.Espece == "Aigle")
            {
                return TypeNourriture.Viande;
            }

            return TypeNourriture.Graines;
        }

        private Habitat? TrouverHabitatDisponible(Animal animal)
        {
            return Habitats.FirstOrDefault(h => h.EstCompatible(animal) && !h.VerifierSurpopulation());
        }

        public string GetSaisonActuelle()
        {
            Saison saison = GestionVisiteurs.ObtenirSaison(Mois);
            return saison == Saison.Haute ? "Haute" : "Basse";
        }

        public int GetAnneeActuelle()
        {
            return ((Mois - 1) / 12) + 1;
        }

        public string GetNomMoisActuel()
        {
            int moisAnnee = ((Mois - 1) % 12) + 1;

            return moisAnnee switch
            {
                1 => "Janvier",
                2 => "Février",
                3 => "Mars",
                4 => "Avril",
                5 => "Mai",
                6 => "Juin",
                7 => "Juillet",
                8 => "Août",
                9 => "Septembre",
                10 => "Octobre",
                11 => "Novembre",
                12 => "Décembre",
                _ => "Inconnu"
            };
        }

        public void AfficherEtat()
        {
            Console.WriteLine($"Zoo : {Nom}");
            Console.WriteLine($"Budget : {Budget:F2} €");
            Console.WriteLine($"Mois : {Mois} ({GetNomMoisActuel()})");
            Console.WriteLine($"Année : {GetAnneeActuelle()}");
            Console.WriteLine($"Saison : {GetSaisonActuelle()}");
            Console.WriteLine($"Nombre d'animaux : {Animaux.Count}");
            Console.WriteLine($"Nombre d'habitats : {Habitats.Count}");
            Console.WriteLine(Stock);
            Console.WriteLine($"Visiteurs du dernier mois : {DerniersVisiteurs}");
            Console.WriteLine($"Revenu visiteurs du dernier mois : {DernierRevenuVisiteurs:F2} €");
            Console.WriteLine($"Subvention du dernier mois : {DerniereSubvention:F2} €");
            Console.WriteLine($"Nouveaux malades du dernier mois : {DerniersNouveauxMalades}");
            Console.WriteLine($"Guérisons du dernier mois : {DerniersGueris}");
            Console.WriteLine($"Morts par maladie du dernier mois : {DerniersMortsMaladie}");
            Console.WriteLine($"Grossesses du dernier mois : {DernieresGrossesses}");
            Console.WriteLine($"Naissances du dernier mois : {DernieresNaissances}");
            Console.WriteLine($"Morts infantiles du dernier mois : {DernieresMortsInfantiles}");
            Console.WriteLine($"Fausses couches du dernier mois : {DernieresFaussesCouches}");
            Console.WriteLine($"Événement aléatoire du dernier mois : {DernierEvenementAleatoire}");

            Console.WriteLine();
            Console.WriteLine("Animaux :");
            if (Animaux.Count == 0)
            {
                Console.WriteLine("Aucun animal.");
            }
            else
            {
                foreach (Animal animal in Animaux)
                {
                    Console.WriteLine(animal);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Habitats :");
            if (Habitats.Count == 0)
            {
                Console.WriteLine("Aucun habitat.");
            }
            else
            {
                foreach (Habitat habitat in Habitats)
                {
                    Console.WriteLine(habitat);
                }
            }
        }
    }
}