namespace Ressource
{
    public class MinerauxConfig : AbstraitRessourceConfig
    {
        public MinerauxConfig() : base()
        {
            id = "R3_Mineraux";
            nom = "Min�raux";
            icone = null;  // TODO
            description = "";
            qteBase = 100;
            qteMax = 10_000;
        }
    }
}
