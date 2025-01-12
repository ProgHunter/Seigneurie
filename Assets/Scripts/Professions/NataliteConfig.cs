using Batiment;
using Utils;

namespace Profession
{
    public class NataliteConfig : AbstraitProfessionConfig
    {
        public NataliteConfig() : base()
        {
            Id = "P0_Natalite";
            Nom = "Natalité";
            Icone = null; // TODO
            Description = "Contribut à la natalité dans la Seigneurie";
            Prerequis = new LotBatiments();  // Aucun prérequis
            ProfessionPourcent = new Qte(30 /*Qte de base*/, 100 /*Max*/, 0 /*Min*/);
        }
    }
}
