namespace Ressource
{
    public class PopulationConfig : AbstraitRessourceConfig
    {
        public int croissancePourcent;
        public int mortaliteFaminePourcent;
        public int faimPourcent;

        public PopulationConfig() : base()
        {
            Id = "R0_Population";
            Nom = "Population";
            Icone = null;  // TODO
            Description = "";
            QteBase = 100;
            QteMax = 1_000_000;
            QteMin = 2;
            croissancePourcent = 5;
            mortaliteFaminePourcent = 5;
            faimPourcent = 50;
        }
    }
}
