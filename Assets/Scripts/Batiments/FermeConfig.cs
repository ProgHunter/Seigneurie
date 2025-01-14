using Ressource;
using Utils;

namespace Batiment
{
    public class FermeConfig : AbstraitBatimentConfig
    {
        private float _bonusProductionPourcent;

        public FermeConfig() : base()
        {
            Id = "B1_Ferme";
            Nom = "Ferme";
            Icone = null;  // TODO
            Description = "Permet d'augmenter production de nourriture.";
            Qte = new Qte(0 /*Qte de base*/, 1000 /*Max*/, 0 /*Min*/);
            CoutConstruction = new LotRessources(0, 0, 50, 10);
            EffortConstruction = 20;
            BonusProductionPourcent = 0.05f;
            Prerequis = new LotBatiments();  // Aucun prérequis
        }

        public float BonusProductionPourcent { get => _bonusProductionPourcent; private set => _bonusProductionPourcent = value; }
    }
}
