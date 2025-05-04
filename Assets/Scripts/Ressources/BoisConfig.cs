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
            Description = "Ressource essentiel à la construction de bâtiments.";
            Qte = new Qte(500 /*Qte de base*/, 100_000 /*Max*/, 0 /*Min*/);
            Prerequis = new LotBatiments();  // Aucun prérequis
        }
    }
}
