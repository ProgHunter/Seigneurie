using Batiment;

namespace Profession
{
    public class Fermier : AbstraitProfessionConfig
    {
        public Fermier() : base()
        {
            Id = "P1_Fermier";
            Nom = "Fermier";
            Icone = null; // TODO
            Description = "Contribut à la production de nouriture dans la Seigneurie";
            Prerequis = new LotBatiments();  // Aucun prérequis
        }
    }
}