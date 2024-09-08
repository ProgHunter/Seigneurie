using Ressource;

namespace Batiment
{
    public class ScierieConfig : AbstraitBatimentConfig
    {
        public float BonusProductionPourcent;
        public ScierieConfig() : base()
        {
            Id = "B2_Scierie";
            Nom = "Scierie";
            Icone = null;  // TODO
            Description = "Permet d'augmenter production de bois.";
            CoutConstruction = new LotRessources(0, 0, 50, 10);
            EffortConstruction = 20;
            BonusProductionPourcent = 0.05f;
            Prerequis = new LotBatiments(0, 0, 0, 0, 1);  // Hotel de ville
        }
    }
}
