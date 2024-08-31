using Ressource;

namespace Batiment
{
    public class MineConfig : AbstraitBatimentConfig
    {
        public float bonusProductionPourcent;
        public MineConfig() : base()
        {
            id = "B3_Mine";
            nom = "Mine";
            icone = null;  // TODO
            description = "Permet d'augmenter production de minéraux.";
            coutConstruction = new LotRessources(0, 0, 50, 10);
            effortConstruction = 20;
            bonusProductionPourcent = 0.05f;
            prerequis = new LotBatiments(0, 0, 0, 0, 1);  // Hotel de ville
        }
    }
}
