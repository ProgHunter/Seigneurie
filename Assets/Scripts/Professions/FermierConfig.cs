using Batiment;
using Utils;

namespace Profession
{
    public class FermierConfig : AbstraitProfessionConfig
    {
        public FermierConfig() : base()
        {
            Id = "P1_Fermier";
            Nom = "Fermier";
            Icone = null; // TODO
            Description = "Contribut é la production de nouriture dans la Seigneurie";
            Prerequis = new LotBatiments();  // Aucun prérequis
            ProfessionPourcent = new Qte(50 /*Qte de base*/, 100 /*Max*/, 0 /*Min*/);
        }
    }
}