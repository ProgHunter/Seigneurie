using Batiment;
using Utils;

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
            Qte = new Qte(100 /*Qte de base*/, 10_000 /*Max*/, 0 /*Min*/);
            Prerequis = new LotBatiments();  // Aucun prérequis
        }
    }
}
