namespace Ressources
{
    public abstract class AbstractRessource
    {
        public string Nom;
        public int Quantite;
        public int QuantiteProduite = 50;

        public void FaireProduction()
        {
            Quantite += QuantiteProduite;
        }
    }
}