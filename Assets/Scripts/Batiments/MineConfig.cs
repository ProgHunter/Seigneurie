using Ressource;
using Utils;

namespace Batiment
{
    public class MineConfig : AbstraitBatimentConfig
    {
        private float _bonusProductionPourcent;

        public MineConfig() : base()
        {
            Id = "B3_Mine";
            Nom = "Mine";
            Icone = null;  // TODO
            Description = "Permet d'augmenter production de minéraux.";
            Qte = new Qte(0 /*Qte de base*/, 100 /*Max*/, 0 /*Min*/);
            CoutConstruction = new LotRessources(0, 0, 50, 10);
            EffortConstruction = 20;
            BonusProductionPourcent = 0.05f;
            Prerequis = new LotBatiments(0, 0, 0, 0, 1);  // Hotel de ville
        }

        public float BonusProductionPourcent { get => _bonusProductionPourcent; private set => _bonusProductionPourcent = value; }
    }
}
