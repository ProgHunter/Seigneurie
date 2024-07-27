namespace Ressource
{
    public class BoisConfig : AbstraitRessourceConfig
    {
        public BoisConfig() : base()
        {
            Id = "R2_Bois";
            Nom = "Bois";
            Icone = null;  // TODO
            Description = "";
            QteBase = 100;
            QteMax = 10_000;
            QteMin = 0;
        }
    }
}
