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
        private float _croissancePourcent;
        /// <summary>
        /// <see cref="MortaliteFaminePourcent"/> 
        /// Indice de mortalité des membres de la population en famine. Impacte la production de population.
        /// </summary>
        private float _mortaliteFaminePourcent;
        /// <summary>
        /// <see cref="FaimPourcent"/> 
        /// Indice de faim de la population. Impacte la consomation de nourriture de la population.
        /// </summary>
        private float _faimPourcent;
        #endregion membres

        public PopulationConfig() : base()
        {
            Id = "R0_Population";
            Nom = "Population";
            Icone = null;  // TODO
            Description = "";
            Qte = new Qte(50 /*Qte de base*/, 1_000_000 /*Max*/, 2 /*Min*/);
            CroissancePourcent = 0.05f;
            MortaliteFaminePourcent = 0.25f;
            FaimPourcent = 0.5f;
            Prerequis = new LotBatiments();  // Aucun prérequis
        }

        #region accesseurs_mutateurs
        public float CroissancePourcent { get => _croissancePourcent; private set => _croissancePourcent = value; }
        public float MortaliteFaminePourcent { get => _mortaliteFaminePourcent; private set => _mortaliteFaminePourcent = value; }
        public float FaimPourcent { get => _faimPourcent; private set => _faimPourcent = value; }
        #endregion accesseurs_mutateurs
    }
}
