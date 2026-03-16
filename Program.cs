using ZooSimulator.Enums;
using ZooSimulator.Models;

namespace ZooSimulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo("Le jeu du Zoo");
            bool quitter = false;

            while (!quitter)
            {
                Console.Clear();
                AfficherEntete(zoo);

                Console.WriteLine("1. Acheter un habitat");
                Console.WriteLine("2. Acheter de la nourriture");
                Console.WriteLine("3. Acheter un animal");
                Console.WriteLine("4. Vendre un animal");
                Console.WriteLine("5. Voir l'état du zoo");
                Console.WriteLine("6. Passer au tour suivant");
                Console.WriteLine("7. Quitter");
                Console.WriteLine();
                Console.Write("Ton choix : ");

                string? choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        AcheterHabitat(zoo);
                        break;

                    case "2":
                        AcheterNourriture(zoo);
                        break;

                    case "3":
                        AcheterAnimal(zoo);
                        break;

                    case "4":
                        VendreAnimal(zoo);
                        break;

                    case "5":
                        VoirEtatZoo(zoo);
                        break;

                    case "6":
                        PasserTourSuivant(zoo);
                        break;

                    case "7":
                        quitter = true;
                        Console.WriteLine("Fermeture du jeu...");
                        break;

                    default:
                        Console.WriteLine("Choix invalide.");
                        Pause();
                        break;
                }
            }
        }

        static void AfficherEntete(Zoo zoo)
        {
            Console.WriteLine("======================================");
            Console.WriteLine("         SIMULATEUR DE ZOO");
            Console.WriteLine("======================================");
            Console.WriteLine($"Zoo : {zoo.Nom}");
            Console.WriteLine($"Budget : {zoo.Budget:F2} €");
            Console.WriteLine($"Mois : {zoo.Mois} ({zoo.GetNomMoisActuel()})");
            Console.WriteLine($"Année : {zoo.GetAnneeActuelle()}");
            Console.WriteLine($"Saison : {zoo.GetSaisonActuelle()}");
            Console.WriteLine($"Animaux : {zoo.Animaux.Count}");
            Console.WriteLine($"Habitats : {zoo.Habitats.Count}");
            Console.WriteLine("======================================");
            Console.WriteLine();
        }

        static void AcheterHabitat(Zoo zoo)
        {
            Console.Clear();
            Console.WriteLine("=== ACHETER UN OU PLUSIEURS HABITATS ===");
            Console.WriteLine("1. Habitat Tigre  - 2000 €");
            Console.WriteLine("2. Habitat Aigle  - 2000 €");
            Console.WriteLine("3. Habitat Poules - 300 €");
            Console.WriteLine();
            Console.Write("Choix : ");

            string? choix = Console.ReadLine();

            string? typeHabitat = null;
            decimal prixUnitaire = 0m;

            switch (choix)
            {
                case "1":
                    typeHabitat = "Tigre";
                    prixUnitaire = 2000m;
                    break;

                case "2":
                    typeHabitat = "Aigle";
                    prixUnitaire = 2000m;
                    break;

                case "3":
                    typeHabitat = "Poules";
                    prixUnitaire = 300m;
                    break;

                default:
                    Console.WriteLine("Choix invalide.");
                    Pause();
                    return;
            }

            int quantite = DemanderQuantite("Combien d'habitats veux-tu acheter ? ");
            decimal coutTotal = quantite * prixUnitaire;

            Console.WriteLine();
            Console.WriteLine($"Prix unitaire : {prixUnitaire:F2} €");
            Console.WriteLine($"Coût total si tout passe : {coutTotal:F2} €");
            Console.WriteLine();

            int achetes = 0;

            for (int i = 0; i < quantite; i++)
            {
                if (zoo.AcheterHabitat(typeHabitat))
                {
                    achetes++;
                }
                else
                {
                    break;
                }
            }

            int echecs = quantite - achetes;

            Console.WriteLine($"Habitats achetés : {achetes}");
            Console.WriteLine($"Achats échoués : {echecs}");
            Console.WriteLine($"Budget restant : {zoo.Budget:F2} €");

            Pause();
        }

        static void AcheterNourriture(Zoo zoo)
        {
            Console.Clear();
            Console.WriteLine("=== ACHETER DE LA NOURRITURE ===");
            Console.WriteLine("1. Viande  - 5 €/kg");
            Console.WriteLine("2. Graines - 2,5 €/kg");
            Console.WriteLine();
            Console.Write("Choix : ");

            string? choix = Console.ReadLine();

            Console.Write("Quantité en kg : ");
            string? saisieQuantite = Console.ReadLine();

            if (!double.TryParse(saisieQuantite, out double quantite) || quantite <= 0)
            {
                Console.WriteLine("Quantité invalide.");
                Pause();
                return;
            }

            bool succes = false;
            decimal prixTotal = 0m;

            switch (choix)
            {
                case "1":
                    prixTotal = (decimal)quantite * 5m;
                    Console.WriteLine($"Prix total : {prixTotal:F2} €");
                    succes = zoo.AcheterNourriture(TypeNourriture.Viande, quantite);
                    break;

                case "2":
                    prixTotal = (decimal)quantite * 2.5m;
                    Console.WriteLine($"Prix total : {prixTotal:F2} €");
                    succes = zoo.AcheterNourriture(TypeNourriture.Graines, quantite);
                    break;

                default:
                    Console.WriteLine("Choix invalide.");
                    Pause();
                    return;
            }

            Console.WriteLine();
            Console.WriteLine(succes
                ? "Nourriture achetée avec succès."
                : "Achat impossible : budget insuffisant.");

            Pause();
        }

        static void AcheterAnimal(Zoo zoo)
        {
            Console.Clear();
            Console.WriteLine("=== ACHETER UN OU PLUSIEURS ANIMAUX ===");
            Console.WriteLine("1. Tigre");
            Console.WriteLine("   - 6 mois : 3 000 €");
            Console.WriteLine("   - 4 ans  : 120 000 €");
            Console.WriteLine("   - 14 ans : 60 000 €");
            Console.WriteLine();
            Console.WriteLine("2. Aigle");
            Console.WriteLine("   - 6 mois : 1 000 €");
            Console.WriteLine("   - 4 ans  : 4 000 €");
            Console.WriteLine("   - 14 ans : 2 000 €");
            Console.WriteLine();
            Console.WriteLine("3. Poule");
            Console.WriteLine("   - 6 mois : 20 €");
            Console.WriteLine();
            Console.WriteLine("4. Coq");
            Console.WriteLine("   - 6 mois : 100 €");
            Console.WriteLine();
            Console.Write("Choix : ");

            string? choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    AcheterLotTigres(zoo);
                    break;

                case "2":
                    AcheterLotAigles(zoo);
                    break;

                case "3":
                    AcheterLotPoules(zoo);
                    break;

                case "4":
                    AcheterLotCoqs(zoo);
                    break;

                default:
                    Console.WriteLine("Choix invalide.");
                    Pause();
                    break;
            }
        }

        static void AcheterLotTigres(Zoo zoo)
        {
            Console.Clear();
            Console.WriteLine("=== ACHAT LOT DE TIGRES ===");

            Sexe sexe = DemanderSexe("Sexe des tigres (M/F) : ");
            int age = DemanderAgeTigreOuAigle();
            int quantite = DemanderQuantite("Combien de tigres veux-tu acheter ? ");

            Tigre exemple = new Tigre(sexe, zoo.Mois, age);
            decimal prixUnitaire = exemple.GetPrixAchat();
            decimal coutTotal = prixUnitaire * quantite;

            Console.WriteLine();
            Console.WriteLine($"Prix unitaire : {prixUnitaire:F2} €");
            Console.WriteLine($"Coût total si tout passe : {coutTotal:F2} €");
            Console.WriteLine();

            int achetes = 0;

            for (int i = 0; i < quantite; i++)
            {
                Tigre tigre = new Tigre(sexe, zoo.Mois, age);

                if (zoo.AcheterAnimal(tigre))
                {
                    achetes++;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine($"Tigres achetés : {achetes}");
            Console.WriteLine($"Achats échoués : {quantite - achetes}");
            Console.WriteLine($"Budget restant : {zoo.Budget:F2} €");
            Pause();
        }

        static void AcheterLotAigles(Zoo zoo)
        {
            Console.Clear();
            Console.WriteLine("=== ACHAT LOT D'AIGLES ===");

            Sexe sexe = DemanderSexe("Sexe des aigles (M/F) : ");
            int age = DemanderAgeTigreOuAigle();
            int quantite = DemanderQuantite("Combien d'aigles veux-tu acheter ? ");

            Aigle exemple = new Aigle(sexe, zoo.Mois, age);
            decimal prixUnitaire = exemple.GetPrixAchat();
            decimal coutTotal = prixUnitaire * quantite;

            Console.WriteLine();
            Console.WriteLine($"Prix unitaire : {prixUnitaire:F2} €");
            Console.WriteLine($"Coût total si tout passe : {coutTotal:F2} €");
            Console.WriteLine();

            int achetes = 0;

            for (int i = 0; i < quantite; i++)
            {
                Aigle aigle = new Aigle(sexe, zoo.Mois, age);

                if (zoo.AcheterAnimal(aigle))
                {
                    achetes++;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine($"Aigles achetés : {achetes}");
            Console.WriteLine($"Achats échoués : {quantite - achetes}");
            Console.WriteLine($"Budget restant : {zoo.Budget:F2} €");
            Pause();
        }

        static void AcheterLotPoules(Zoo zoo)
        {
            Console.Clear();
            Console.WriteLine("=== ACHAT LOT DE POULES ===");
            Console.WriteLine("Prix unitaire : 20 €");
            Console.WriteLine();

            int quantite = DemanderQuantite("Combien de poules veux-tu acheter ? ");
            decimal prixUnitaire = 20m;
            decimal coutTotal = prixUnitaire * quantite;

            Console.WriteLine();
            Console.WriteLine($"Coût total si tout passe : {coutTotal:F2} €");
            Console.WriteLine();

            int achetees = 0;

            for (int i = 0; i < quantite; i++)
            {
                Poule poule = new Poule(zoo.Mois, 6);

                if (zoo.AcheterAnimal(poule))
                {
                    achetees++;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine($"Poules achetées : {achetees}");
            Console.WriteLine($"Achats échoués : {quantite - achetees}");
            Console.WriteLine($"Budget restant : {zoo.Budget:F2} €");
            Pause();
        }

        static void AcheterLotCoqs(Zoo zoo)
        {
            Console.Clear();
            Console.WriteLine("=== ACHAT LOT DE COQS ===");
            Console.WriteLine("Prix unitaire : 100 €");
            Console.WriteLine();

            int quantite = DemanderQuantite("Combien de coqs veux-tu acheter ? ");
            decimal prixUnitaire = 100m;
            decimal coutTotal = prixUnitaire * quantite;

            Console.WriteLine();
            Console.WriteLine($"Coût total si tout passe : {coutTotal:F2} €");
            Console.WriteLine();

            int achetes = 0;

            for (int i = 0; i < quantite; i++)
            {
                Coq coq = new Coq(zoo.Mois, 6);

                if (zoo.AcheterAnimal(coq))
                {
                    achetes++;
                }
                else
                {
                    break;
                }
            }

            Console.WriteLine($"Coqs achetés : {achetes}");
            Console.WriteLine($"Achats échoués : {quantite - achetes}");
            Console.WriteLine($"Budget restant : {zoo.Budget:F2} €");
            Pause();
        }

        static int DemanderQuantite(string message)
        {
            Console.Write(message);
            string? saisie = Console.ReadLine();

            if (!int.TryParse(saisie, out int quantite) || quantite <= 0)
            {
                Console.WriteLine("Quantité invalide. Valeur par défaut : 1");
                return 1;
            }

            return quantite;
        }

        static int DemanderAgeTigreOuAigle()
        {
            Console.WriteLine("Choisis l'âge :");
            Console.WriteLine("1. 6 mois");
            Console.WriteLine("2. 4 ans");
            Console.WriteLine("3. 14 ans");
            Console.Write("Choix : ");

            string? choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    return 6;
                case "2":
                    return 48;
                case "3":
                    return 168;
                default:
                    Console.WriteLine("Choix invalide, âge par défaut : 6 mois.");
                    return 6;
            }
        }

        static Sexe DemanderSexe(string message)
        {
            Console.Write(message);
            string? saisie = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(saisie) && saisie.Trim().ToUpper() == "F")
            {
                return Sexe.Femelle;
            }

            return Sexe.Male;
        }

        static void VendreAnimal(Zoo zoo)
        {
            Console.Clear();
            Console.WriteLine("=== VENDRE UN ANIMAL ===");

            if (zoo.Animaux.Count == 0)
            {
                Console.WriteLine("Aucun animal à vendre.");
                Pause();
                return;
            }

            for (int i = 0; i < zoo.Animaux.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {zoo.Animaux[i]}");
            }

            Console.WriteLine();
            Console.Write("Numéro de l'animal à vendre : ");
            string? saisie = Console.ReadLine();

            if (!int.TryParse(saisie, out int numero))
            {
                Console.WriteLine("Saisie invalide.");
                Pause();
                return;
            }

            int index = numero - 1;

            if (index < 0 || index >= zoo.Animaux.Count)
            {
                Console.WriteLine("Numéro invalide.");
                Pause();
                return;
            }

            Animal animal = zoo.Animaux[index];
            decimal prixVente = animal.GetPrixVente();

            bool succes = VendreAnimalDuZoo(zoo, animal);

            Console.WriteLine();
            Console.WriteLine(succes
                ? $"Animal vendu avec succès pour {prixVente} €."
                : "Vente impossible.");

            Pause();
        }

        static bool VendreAnimalDuZoo(Zoo zoo, Animal animal)
        {
            Habitat? habitat = zoo.Habitats.FirstOrDefault(h => h.Animaux.Contains(animal));

            if (habitat != null)
            {
                habitat.RetirerAnimal(animal);
            }

            bool retire = zoo.Animaux.Remove(animal);

            if (!retire)
            {
                return false;
            }

            zoo.Budget += animal.GetPrixVente();
            return true;
        }

        static void VoirEtatZoo(Zoo zoo)
        {
            Console.Clear();
            Console.WriteLine("=== ETAT DU ZOO ===");
            Console.WriteLine();
            zoo.AfficherEtat();
            Pause();
        }

        static void PasserTourSuivant(Zoo zoo)
        {
            Console.Clear();
            Console.WriteLine("=== PASSAGE AU MOIS SUIVANT ===");
            Console.WriteLine();

            zoo.PasserAuTourSuivant();

            Console.WriteLine($"Nous sommes maintenant au mois {zoo.Mois} ({zoo.GetNomMoisActuel()}).");
            Console.WriteLine($"Visiteurs gagnés ce mois-ci : {zoo.DerniersVisiteurs}");
            Console.WriteLine($"Revenu visiteurs ce mois-ci : {zoo.DernierRevenuVisiteurs:F2} €");
            Console.WriteLine($"Subvention reçue ce mois-ci : {zoo.DerniereSubvention:F2} €");
            Console.WriteLine($"Nouveaux malades ce mois-ci : {zoo.DerniersNouveauxMalades}");
            Console.WriteLine($"Guérisons ce mois-ci : {zoo.DerniersGueris}");
            Console.WriteLine($"Morts par maladie ce mois-ci : {zoo.DerniersMortsMaladie}");
            Console.WriteLine($"Grossesses ce mois-ci : {zoo.DernieresGrossesses}");
            Console.WriteLine($"Naissances ce mois-ci : {zoo.DernieresNaissances}");
            Console.WriteLine($"Morts infantiles ce mois-ci : {zoo.DernieresMortsInfantiles}");
            Console.WriteLine($"Fausses couches ce mois-ci : {zoo.DernieresFaussesCouches}");
            Console.WriteLine($"Événement aléatoire : {zoo.DernierEvenementAleatoire}");
            Console.WriteLine();
            Console.WriteLine("Etat actuel du zoo :");
            Console.WriteLine();
            zoo.AfficherEtat();

            Pause();
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Appuie sur Entrée pour continuer...");
            Console.ReadLine();
        }
    }
}