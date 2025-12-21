using Ressource;
using Utils;

namespace Batiment
{
    public class ChateauConfig : AbstraitBatimentConfig
    {
        public ChateauConfig() : base()
        {
            Id = "B5_Chateau";
            Nom = "Château";
            Icone = null;  // TODO
            Description = "Icône de la réussite de votre Seignerie. Ceci constitue le dernier objectif du jeu, pour le moment.";
            Qte = new Qte(0 /*Qte de base*/, 1 /*Max*/, 0 /*Min*/);
            CoutConstruction = new LotRessources(0, 0, 250000, 100000);
            EffortConstruction = 100000;
            Prerequis = new LotBatiments(qteHotelDeVille: 1);
        }
    }
}
