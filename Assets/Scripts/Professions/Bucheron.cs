namespace Profession
{
    public class Bucheron : AbstraitProfessionConfig
    {
        public Bucheron() : base()
        {
            Id = "P2_Bucheron";
            Nom = "Bucheron";
            Icone = null; // TODO
            Description = "Contribut à la production de bois dans la Seigneurie";
        }
    }
}