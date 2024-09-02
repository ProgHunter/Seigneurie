namespace Profession
{
    public class Mineur : AbstraitProfessionConfig
    {
        public Mineur() : base()
        {
            Id = "P3_Mineur";
            Nom = "Mineur";
            Icone = null; // TODO
            Description = "Contribut à la production de minéraux dans la Seigneurie";
        }
    }
}