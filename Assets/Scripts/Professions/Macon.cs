namespace Profession
{
    public class Macon : AbstraitProfessionConfig
    {
        public int parallelisablePourcent;

        public Macon() : base()
        {
            id = "P4_Macon";
            nom = "Ma�on";
            icone = null; // TODO
            description = "Contribut � la construction dans la Seigneurie";
            parallelisablePourcent = 75;
        }
    }
}
