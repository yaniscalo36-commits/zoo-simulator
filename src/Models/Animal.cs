using ZooSimulator.Enums;

namespace ZooSimulator.Models
{
    public abstract class Animal
    {
        private static int compteurId = 1;

        public int Id { get; protected set; }
        public string Espece { get; protected set; }
        public int Age { get; protected set; } // en mois
        public Sexe Sexe { get; protected set; }
        public double Faim { get; protected set; }
        public bool Malade { get; protected set; }
        public int JoursMaladie { get; protected set; }
        public int MoisArrivee { get; protected set; }
        public int Gestation { get; protected set; } // mois restants
        public int DernierMoisReproduction { get; protected set; }

        protected Animal(string espece, Sexe sexe, int moisArrivee, int ageInitial)
        {
            Id = compteurId++;
            Espece = espece;
            Age = ageInitial;
            Sexe = sexe;
            Faim = 0;
            Malade = false;
            JoursMaladie = 0;
            MoisArrivee = moisArrivee;
            Gestation = 0;
            DernierMoisReproduction = -999;
        }

        public virtual void Manger()
        {
            Faim = 0;
        }

        public virtual void Vieillir()
        {
            Age++;
            Faim += 10;

            if (Faim > 100)
            {
                Faim = 100;
            }
        }

        public virtual bool PeutSeReproduire(int moisCourant)
        {
            return !Malade
                && Faim <= 0
                && Gestation == 0
                && Age >= GetAgeMaturiteSexuelleMois()
                && Age <= GetAgeFinReproductionMois()
                && MoisArrivee < moisCourant;
        }

        public bool EstFemelleGestante()
        {
            return Sexe == Sexe.Femelle && Gestation > 0;
        }

        public void CommencerGestation(int dureeMois)
        {
            Gestation = dureeMois;
        }

        public void AvancerGestation()
        {
            if (Gestation > 0)
            {
                Gestation--;
            }
        }

        public void PerdreFoetus()
        {
            Gestation = 0;
        }

        public void EnregistrerReproduction(int moisCourant)
        {
            DernierMoisReproduction = moisCourant;
        }

        public void TomberMalade(int dureeJours)
        {
            Malade = true;
            JoursMaladie = dureeJours;
        }

        public void AvancerMaladie(int joursEcoules)
        {
            if (!Malade)
            {
                return;
            }

            JoursMaladie -= joursEcoules;

            if (JoursMaladie <= 0)
            {
                Malade = false;
                JoursMaladie = 0;
            }
        }

        public abstract double GetConsommationMensuelle();
        public abstract decimal GetPrixAchat();
        public abstract decimal GetPrixVente();
        public abstract int GetVisiteursSaisonHaute();
        public abstract double GetVisiteursSaisonBasse();

        public abstract double GetProbabiliteMaladieAnnuelle();
        public abstract int GetDureeMaladieBaseJours();

        public abstract int GetAgeMaturiteSexuelleMois();
        public abstract int GetAgeFinReproductionMois();
        public abstract int GetDureeGestationMois();
        public abstract int GetNombrePetitsParPortee();
        public abstract double GetMortaliteInfantile();
        public abstract int GetCooldownReproductionMois();

        protected string GetAgeAffichage()
        {
            if (Age < 12)
            {
                return $"{Age} mois";
            }

            int annees = Age / 12;
            int moisRestants = Age % 12;

            if (moisRestants == 0)
            {
                return $"{annees} ans";
            }

            return $"{annees} ans {moisRestants} mois";
        }

        public override string ToString()
        {
            string etatMaladie = Malade
                ? $"Oui ({JoursMaladie} jours restants)"
                : "Non";

            string etatGestation = EstFemelleGestante()
                ? $" | Gestation : {Gestation} mois"
                : "";

            return $"[{Id}] {Espece} | Sexe : {Sexe} | Age : {GetAgeAffichage()} | Faim : {Faim}% | Malade : {etatMaladie}{etatGestation}";
        }
    }
}