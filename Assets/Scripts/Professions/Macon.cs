using Batiment;

namespace Profession
{
    public class Macon : AbstraitProfessionConfig
    {
        public float ParallelisablePourcent;

        public Macon() : base()
        {
            Id = "P4_Macon";
            Nom = "Maçon";
            Icone = null; // TODO
            Description = "Contribut à la construction dans la Seigneurie";
            ParallelisablePourcent = 0.75f;
            Prerequis = new LotBatiments();  // Aucun prérequis
        }
    }
}
