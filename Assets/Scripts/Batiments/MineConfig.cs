using Ressource;

namespace Batiment
{
    public class MineConfig : AbstraitBatimentConfig
    {
        public MineConfig() : base()
        {
            id = "B3_Mine";
            nom = "Mine";
            icone = null;  // TODO
            description = "Permet d'augmenter production de minéraux.";
            coutConstruction = new LotRessources(0, 0, 50, 10);
            effortConstruction = 20;
        }
    }
}
