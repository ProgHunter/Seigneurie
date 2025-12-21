using Ressource;
using Utils;

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
            Qte = new Qte(0 /*Qte de base*/, 1 /*Max*/, 0 /*Min*/);
            CoutConstruction = new LotRessources(0, 0, 25000, 10000);
            EffortConstruction = 10000;
            Prerequis = new LotBatiments(10, 1);
        }
    }
}
