using Batiment;
using Utils;

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
            Qte = new Qte(100 /*Qte de base*/, 10_000 /*Max*/, 0 /*Min*/);
            Prerequis = new LotBatiments();  // Aucun prérequis
        }
    }
}
