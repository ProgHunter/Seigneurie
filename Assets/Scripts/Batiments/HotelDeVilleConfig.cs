using Codice.Client.BaseCommands;
using JetBrains.Annotations;
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
            description = "Essentiel à l'organisation d'une seigneurie. Permet d'accéder aux batiments plus avancés.";
            coutConstruction = new LotRessources(0, 0, 2000, 500);
            effortConstruction = 1000;
            prerequis = new LotBatiments(10, 1);
        }
    }
}
