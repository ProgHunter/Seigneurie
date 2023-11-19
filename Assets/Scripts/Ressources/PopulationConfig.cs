namespace Ressource
{
    public class PopulationConfig : AbstraitRessourceConfig
    {
        public int croissancePourcent;
        public int mortaliteFaminePourcent;
        public int faimPourcent;

        public PopulationConfig() : base()
        {
            id = "R0_Population";
            nom = "Population";
            icone = null;  // TODO
            description = "";
            qteBase = 100;
            qteMax = 1_000_000;
            croissancePourcent = 5;
            mortaliteFaminePourcent = 5;
            faimPourcent = 50;
        }
    }
}
