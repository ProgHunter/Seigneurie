using Ressource;

namespace Batiment
{
    public class ScierieConfig : AbstraitBatimentConfig
    {
        public int bonusProductionPourcent;
        public ScierieConfig() : base()
        {
            id = "B2_Scierie";
            nom = "Scierie";
            icone = null;  // TODO
            description = "Permet d'augmenter production de bois.";
            coutConstruction = new LotRessources(0, 0, 50, 10);
            effortConstruction = 20;
            bonusProductionPourcent = 5;
        }
    }
}
