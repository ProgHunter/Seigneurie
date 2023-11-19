namespace Ressource
{
    public class BoisConfig : AbstraitRessourceConfig
    {
        public BoisConfig() : base()
        {
            id = "R2_Bois";
            nom = "Bois";
            icone = null;  // TODO
            description = "";
            qteBase = 100;
            qteMax = 10_000;
        }
    }
}
