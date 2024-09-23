using Batiment;
using Utils;

namespace Profession
{
    public class MineurConfig : AbstraitProfessionConfig
    {
        public MineurConfig() : base()
        {
            Id = "P3_Mineur";
            Nom = "Mineur";
            Icone = null; // TODO
            Description = "Contribut à la production de minéraux dans la Seigneurie";
            Prerequis = new LotBatiments();  // Aucun prérequis
            ProfessionPourcent = new Qte(0 /*Qte de base*/, 100 /*Max*/, 0 /*Min*/);
        }
    }
}