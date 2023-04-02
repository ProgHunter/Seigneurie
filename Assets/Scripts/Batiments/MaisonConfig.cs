using Ressource;

namespace Batiment
{
    public class MaisonConfig : AbstraitBatimentConfig
    {
        public MaisonConfig() : base()
        {
            id = "B0_Maison";
            nom = "Maison";
            icone = null;  // TODO
            description = "Permet d'augmenter la croissance de la population.";
            coutConstruction = new LotRessources(0, 0, 10, 5);  // TODO: Mettres les valeurs dans un fichier de config
            effortConstruction = 5;
        }
    }
}
