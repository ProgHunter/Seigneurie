using Ressource;
using Utils;

namespace Batiment
{
    public class ScierieConfig : AbstraitBatimentConfig
    {
        private float _bonusProductionPourcent;

        public ScierieConfig() : base()
        {
            Id = "B2_Scierie";
            Nom = "Scierie";
            Icone = null;  // TODO
            Description = "Permet d'augmenter production de bois.";
            Qte = new Qte(0 /*Qte de base*/, 100 /*Max*/, 0 /*Min*/);
            CoutConstruction = new LotRessources(0, 0, 50, 10);
            EffortConstruction = 20;
            BonusProductionPourcent = 0.05f;
            Prerequis = new LotBatiments(0, 0, 0, 0, 1);  // Hotel de ville
        }

        public float BonusProductionPourcent { get => _bonusProductionPourcent; private set => _bonusProductionPourcent = value; }
    }
}
