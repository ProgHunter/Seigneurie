namespace Profession
{
    public class Fermier : AbstraitProfessionConfig
    {
        public Fermier() : base()
        {
            id = "P1_Fermier";
            nom = "Fermier";
            icone = null; // TODO
            description = "Contribut à la production de nouriture dans la Seigneurie";
        }
    }
}