using Batiment;
using Utils;

namespace Ressource
{
    public class PopulationConfig : AbstraitRessourceConfig
    {
        #region membres
        /// <summary>
        /// <see cref="CroissancePourcent"/> 
        /// Indice de croissance de la population. Impacte la production de population.
        /// </summary>
        public float CroissancePourcent;
        /// <summary>
        /// <see cref="MortaliteFaminePourcent"/> 
        /// Indice de mortalité des membres de la population en famine. Impacte la production de population.
        /// </summary>
        public float MortaliteFaminePourcent;
        /// <summary>
        /// <see cref="FaimPourcent"/> 
        /// Indice de faim de la population. Impacte la consomation de nourriture de la population.
        /// </summary>
        public float FaimPourcent;
        #endregion membres

        public PopulationConfig() : base()
        {
            Id = "R0_Population";
            Nom = "Population";
            Icone = null;  // TODO
            Description = "";
            Qte = new Qte(100 /*Qte de base*/, 1_000_000 /*Max*/, 2 /*Min*/);
            CroissancePourcent = 0.05f;
            MortaliteFaminePourcent = 0.05f;
            FaimPourcent = 0.5f;
            Prerequis = new LotBatiments();  // Aucun prérequis
        }
    }
}
