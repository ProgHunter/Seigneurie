using Utils;
using Batiment;

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
            Qte = new Qte(1000 /*Qte de base*/, 1_000_000 /*Max*/, 0 /*Min*/);
            Prerequis = new LotBatiments();  // Aucun prérequis
        }
    }
}
