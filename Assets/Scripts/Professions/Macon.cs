namespace Profession
{
    public class Macon : AbstraitProfessionConfig
    {
        public int parallelisablePourcent;

        public Macon() : base()
        {
            id = "P4_Macon";
            nom = "Maçon";
            icone = null; // TODO
            description = "Contribut à la construction dans la Seigneurie";
            parallelisablePourcent = 75;
        }
    }
}
