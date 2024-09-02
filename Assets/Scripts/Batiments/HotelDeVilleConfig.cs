using Codice.Client.BaseCommands;
using JetBrains.Annotations;
using Ressource;

namespace Batiment
{
    public class HotelDeVilleConfig : AbstraitBatimentConfig
    {
        public HotelDeVilleConfig() : base()
        {
            Id = "B4_HotelDeVille";
            Nom = "Hotel de ville";
            Icone = null;  // TODO
            Description = "Essentiel à l'organisation d'une seigneurie. Permet d'accéder aux batiments plus avancés.";
            CoutConstruction = new LotRessources(0, 0, 2000, 500);
            EffortConstruction = 1000;
            Prerequis = new LotBatiments(10, 1);
        }
    }
}
