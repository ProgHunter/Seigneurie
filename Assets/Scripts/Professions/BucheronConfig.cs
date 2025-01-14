using Batiment;
using Utils;

namespace Profession
{
    public class BucheronConfig : AbstraitProfessionConfig
    {
        public BucheronConfig() : base()
        {
            Id = "P2_Bucheron";
            Nom = "Bucheron";
            Icone = null; // TODO
            Description = "Contribut é la production de bois dans la Seigneurie";
            Prerequis = new LotBatiments();  // Aucun prérequis
            ProfessionPourcent = new Qte(0 /*Qte de base*/, 100 /*Max*/, 0 /*Min*/);
        }
    }
}