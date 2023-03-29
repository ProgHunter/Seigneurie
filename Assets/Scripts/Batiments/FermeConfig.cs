using Ressource;

namespace Batiment
{
    public class FermeConfig : AbstraitBatimentConfig
    {
        public FermeConfig() : base()
        {
            id = "B1_Ferme";
            nom = "Ferme";
            icone = null;  // TODO
            description = "Permet d'augmenter production de nourriture.";
            coutConstruction = new LotRessources(0, 0, 50, 10);
            effortConstruction = 20;
        }
    }
}
