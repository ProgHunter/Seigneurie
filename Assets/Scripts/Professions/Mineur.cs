namespace Profession
{
    public class Mineur : AbstraitProfessionConfig
    {
        public Mineur() : base()
        {
            id = "P3_Mineur";
            nom = "Mineur";
            icone = null; // TODO
            description = "Contribut � la production de min�raux dans la Seigneurie";
        }
    }
}