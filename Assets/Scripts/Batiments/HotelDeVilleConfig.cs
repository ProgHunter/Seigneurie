using Ressource;

namespace Batiment
{
    public class HotelDeVilleConfig : AbstraitBatimentConfig
    {
        public HotelDeVilleConfig() : base()
        {
            id = "B4_HotelDeVille";
            nom = "Hotel de ville";
            icone = null;  // TODO
            description = "Essentiel � l'organisation d'une seigneurie. Permet d'acc�der aux batiments plus avanc�s.";
            coutConstruction = new LotRessources(0, 0, 2000, 500);
            effortConstruction = 1000;
        }
    }
}
