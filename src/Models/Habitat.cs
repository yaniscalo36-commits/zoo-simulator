namespace ZooSimulator.Models
{
    public class Habitat
    {
        public string Type { get; private set; }
        public int CapaciteMax { get; private set; }
        public List<Animal> Animaux { get; private set; }

        public Habitat(string type, int capaciteMax)
        {
            Type = type;
            CapaciteMax = capaciteMax;
            Animaux = new List<Animal>();
        }

        public bool AjouterAnimal(Animal animal)
        {
            if (!EstCompatible(animal))
            {
                return false;
            }

            if (VerifierSurpopulation())
            {
                return false;
            }

            Animaux.Add(animal);
            return true;
        }

        public bool RetirerAnimal(Animal animal)
        {
            return Animaux.Remove(animal);
        }

        public bool VerifierSurpopulation()
        {
            return Animaux.Count >= CapaciteMax;
        }

        public bool EstCompatible(Animal animal)
        {
            if (Type == "Tigre")
            {
                return animal.Espece == "Tigre";
            }

            if (Type == "Aigle")
            {
                return animal.Espece == "Aigle";
            }

            if (Type == "Poules")
            {
                return animal.Espece == "Poule" || animal.Espece == "Coq";
            }

            return false;
        }

        public override string ToString()
        {
            return $"Habitat {Type} | {Animaux.Count}/{CapaciteMax} animaux";
        }
    }
}