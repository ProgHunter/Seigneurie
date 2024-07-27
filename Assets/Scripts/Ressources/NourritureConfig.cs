namespace Ressource
{
    public class NourritureConfig : AbstraitRessourceConfig
    {
        public NourritureConfig() : base()
        {
            Id = "R1_Nourriture";
            Nom = "Nourriture";
            Icone = null;  // TODO
            Description = "";
            QteBase = 1000;
            QteMax = 1_000_000;
            QteMin = 0;
        }
    }
}
