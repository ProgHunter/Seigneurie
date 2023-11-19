namespace Ressource
{
    public class NourritureConfig : AbstraitRessourceConfig
    {
        public NourritureConfig() : base()
        {
            id = "R1_Nourriture";
            nom = "Nourriture";
            icone = null;  // TODO
            description = "";
            qteBase = 1000;
            qteMax = 1_000_000;
        }
    }
}
