using Batiment;

namespace Profession
{
    public class FermierConfig : AbstraitProfessionConfig
    {
        public FermierConfig() : base()
        {
            Id = "P1_Fermier";
            Nom = "FermierConfig";
            Icone = null; // TODO
            Description = "Contribut à la production de nouriture dans la Seigneurie";
            Prerequis = new LotBatiments();  // Aucun prérequis
            ProfessionPourcent = 0.5f;
        }
    }
}