using Ressource;

namespace Batiment
{
    public class FermeConfig : AbstraitBatimentConfig
    {
        public float BonusProductionPourcent;
        public FermeConfig() : base()
        {
            Id = "B1_Ferme";
            Nom = "Ferme";
            Icone = null;  // TODO
            Description = "Permet d'augmenter production de nourriture.";
            CoutConstruction = new LotRessources(0, 0, 50, 10);
            EffortConstruction = 20;
            BonusProductionPourcent = 0.05f;
            Prerequis = new LotBatiments();  // Aucun prérequis
        }
    }
}
