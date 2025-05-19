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
            CoutConstruction = new LotRessources(0, 0, 500, 100);
            EffortConstruction = 800;
            BonusProductionPourcent = 0.1f;
            Prerequis = new LotBatiments(qteHotelDeVille:1);
        }

        public float BonusProductionPourcent { get => _bonusProductionPourcent; private set => _bonusProductionPourcent = value; }
    }
}
