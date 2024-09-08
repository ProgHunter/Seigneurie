using Batiment;

namespace Profession
{
    public class BucheronConfig : AbstraitProfessionConfig
    {
        public BucheronConfig() : base()
        {
            Id = "P2_Bucheron";
            Nom = "BucheronConfig";
            Icone = null; // TODO
            Description = "Contribut à la production de bois dans la Seigneurie";
            Prerequis = new LotBatiments();  // Aucun prérequis
            ProfessionPourcent = 0f;
        }
    }
}