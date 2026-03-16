using ZooSimulator.Enums;
using ZooSimulator.Models;

namespace ZooSimulator.Services
{
    public class Reproduction
    {
        private readonly Random random;

        public int DernieresGrossesses { get; private set; }
        public int DernieresNaissances { get; private set; }
        public int DernieresMortsInfantiles { get; private set; }
        public int DernieresFaussesCouches { get; private set; }

        public Reproduction()
        {
            random = new Random();
        }

        public void TraiterReproduction(List<Habitat> habitats, int moisCourant)
        {
            DernieresGrossesses = 0;
            DernieresNaissances = 0;
            DernieresMortsInfantiles = 0;
            DernieresFaussesCouches = 0;

            GererFaussesCouches(habitats);
            AvancerGestationsEtNaissances(habitats, moisCourant);
            DemarrerNouvellesGrossesses(habitats, moisCourant);
        }

        private void GererFaussesCouches(List<Habitat> habitats)
        {
            foreach (Habitat habitat in habitats)
            {
                foreach (Animal animal in habitat.Animaux)
                {
                    if (animal.EstFemelleGestante() && animal.Faim > 0)
                    {
                        animal.PerdreFoetus();
                        DernieresFaussesCouches++;
                    }
                }
            }
        }

        private void AvancerGestationsEtNaissances(List<Habitat> habitats, int moisCourant)
        {
            foreach (Habitat habitat in habitats)
            {
                List<Animal> femellesGestantes = habitat.Animaux
                    .Where(a => a.EstFemelleGestante())
                    .ToList();

                foreach (Animal femelle in femellesGestantes)
                {
                    femelle.AvancerGestation();

                    if (femelle.Gestation == 0)
                    {
                        FaireNaissance(habitats, femelle, moisCourant);
                    }
                }
            }
        }

        private void FaireNaissance(List<Habitat> habitats, Animal mere, int moisCourant)
        {
            int nombrePetits = mere.GetNombrePetitsParPortee();

            for (int i = 0; i < nombrePetits; i++)
            {
                if (random.NextDouble() < mere.GetMortaliteInfantile())
                {
                    DernieresMortsInfantiles++;
                    continue;
                }

                Animal? bebe = CreerNouveauNe(mere.Espece, moisCourant);

                if (bebe == null)
                {
                    continue;
                }

                Habitat? habitatDisponible = TrouverHabitatDisponiblePourBebe(habitats, bebe);

                if (habitatDisponible != null && habitatDisponible.AjouterAnimal(bebe))
                {
                    DernieresNaissances++;
                }
            }

            mere.EnregistrerReproduction(moisCourant);
        }

        private Habitat? TrouverHabitatDisponiblePourBebe(List<Habitat> habitats, Animal bebe)
        {
            return habitats.FirstOrDefault(h => h.EstCompatible(bebe) && !h.VerifierSurpopulation());
        }

        private Animal? CreerNouveauNe(string espece, int moisCourant)
        {
            Sexe sexeAleatoire = random.Next(2) == 0 ? Sexe.Male : Sexe.Femelle;

            if (espece == "Tigre")
            {
                return new Tigre(sexeAleatoire, moisCourant, 0);
            }

            if (espece == "Aigle")
            {
                return new Aigle(sexeAleatoire, moisCourant, 0);
            }

            if (espece == "Poule")
            {
                if (sexeAleatoire == Sexe.Femelle)
                {
                    return new Poule(moisCourant, 0);
                }

                return new Coq(moisCourant, 0);
            }

            return null;
        }

        private void DemarrerNouvellesGrossesses(List<Habitat> habitats, int moisCourant)
        {
            foreach (Habitat habitat in habitats)
            {
                if (habitat.Type == "Tigre")
                {
                    TraiterReproductionTigres(habitats, habitat, moisCourant);
                }
                else if (habitat.Type == "Aigle")
                {
                    TraiterReproductionAigles(habitats, habitat, moisCourant);
                }
                else if (habitat.Type == "Poules")
                {
                    TraiterReproductionPoules(habitats, habitat, moisCourant);
                }
            }
        }

        private void TraiterReproductionTigres(List<Habitat> habitats, Habitat habitat, int moisCourant)
        {
            List<Animal> males = habitat.Animaux
                .Where(a => a.Espece == "Tigre" && a.Sexe == Sexe.Male && a.PeutSeReproduire(moisCourant))
                .ToList();

            List<Animal> femelles = habitat.Animaux
                .Where(a => a.Espece == "Tigre"
                            && a.Sexe == Sexe.Femelle
                            && a.PeutSeReproduire(moisCourant)
                            && moisCourant - a.DernierMoisReproduction >= a.GetCooldownReproductionMois())
                .ToList();

            if (males.Count == 0)
            {
                return;
            }

            foreach (Animal femelle in femelles)
            {
                int placesCompatibles = CompterPlacesDisponiblesCompatibles(habitats, "Tigre");

                if (placesCompatibles < femelle.GetNombrePetitsParPortee())
                {
                    continue;
                }

                femelle.CommencerGestation(femelle.GetDureeGestationMois());
                DernieresGrossesses++;
            }
        }

        private void TraiterReproductionAigles(List<Habitat> habitats, Habitat habitat, int moisCourant)
        {
            int moisAnnee = ((moisCourant - 1) % 12) + 1;

            if (moisAnnee != 3)
            {
                return;
            }

            List<Animal> males = habitat.Animaux
                .Where(a => a.Espece == "Aigle" && a.Sexe == Sexe.Male && a.PeutSeReproduire(moisCourant))
                .ToList();

            List<Animal> femelles = habitat.Animaux
                .Where(a => a.Espece == "Aigle"
                            && a.Sexe == Sexe.Femelle
                            && a.PeutSeReproduire(moisCourant)
                            && moisCourant - a.DernierMoisReproduction >= a.GetCooldownReproductionMois())
                .ToList();

            int nbPaires = Math.Min(males.Count, femelles.Count);

            for (int i = 0; i < nbPaires; i++)
            {
                Animal femelle = femelles[i];
                int placesCompatibles = CompterPlacesDisponiblesCompatibles(habitats, "Aigle");

                if (placesCompatibles < femelle.GetNombrePetitsParPortee())
                {
                    continue;
                }

                femelle.CommencerGestation(femelle.GetDureeGestationMois());
                DernieresGrossesses++;
            }
        }

        private void TraiterReproductionPoules(List<Habitat> habitats, Habitat habitat, int moisCourant)
        {
            List<Animal> males = habitat.Animaux
                .Where(a => a.Espece == "Coq" && a.PeutSeReproduire(moisCourant))
                .ToList();

            List<Animal> femelles = habitat.Animaux
                .Where(a => a.Espece == "Poule"
                            && a.PeutSeReproduire(moisCourant)
                            && moisCourant - a.DernierMoisReproduction >= a.GetCooldownReproductionMois())
                .ToList();

            if (males.Count == 0)
            {
                return;
            }

            foreach (Animal femelle in femelles)
            {
                int placesCompatibles = CompterPlacesDisponiblesCompatibles(habitats, "Poules");

                if (placesCompatibles <= 0)
                {
                    continue;
                }

                femelle.CommencerGestation(femelle.GetDureeGestationMois());
                DernieresGrossesses++;
            }
        }

        private int CompterPlacesDisponiblesCompatibles(List<Habitat> habitats, string typeHabitat)
        {
            int total = 0;

            foreach (Habitat habitat in habitats)
            {
                if (habitat.Type == typeHabitat)
                {
                    total += habitat.CapaciteMax - habitat.Animaux.Count;
                }
            }

            return total;
        }
    }
}