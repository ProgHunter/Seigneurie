using Ressource;

namespace Batiment
{
    public class MineConfig : AbstraitBatimentConfig
    {
        public float BonusProductionPourcent;
        public MineConfig() : base()
        {
            Id = "B3_Mine";
            Nom = "Mine";
            Icone = null;  // TODO
            Description = "Permet d'augmenter production de minéraux.";
            CoutConstruction = new LotRessources(0, 0, 50, 10);
            EffortConstruction = 20;
            BonusProductionPourcent = 0.05f;
            Prerequis = new LotBatiments(0, 0, 0, 0, 1);  // Hotel de ville
        }
    }
}
