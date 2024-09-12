using Batiment;

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
            ProfessionPourcent = 0.30f;
        }
    }
}
