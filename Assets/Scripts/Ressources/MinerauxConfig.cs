namespace Ressource
{
    public class MinerauxConfig : AbstraitRessourceConfig
    {
        public MinerauxConfig() : base()
        {
            Id = "R3_Mineraux";
            Nom = "Minéraux";
            Icone = null;  // TODO
            Description = "";
            QteBase = 100;
            QteMax = 10_000;
            QteMin = 0;
        }
    }
}
