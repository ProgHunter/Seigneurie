using Batiment;
using Utils;

namespace Profession
{
    public class MaconConfig : AbstraitProfessionConfig
    {
        public float ParallelisablePourcent;

        public MaconConfig() : base()
        {
            Id = "P4_Macon";
            Nom = "Maçon";
            Icone = null; // TODO
            Description = "Contribut à la construction dans la Seigneurie";
            ParallelisablePourcent = 0.75f;
            Prerequis = new LotBatiments();  // Aucun prérequis
            ProfessionPourcent = new Qte(20 /*Qte de base*/, 100 /*Max*/, 0 /*Min*/);
        }
    }
}
