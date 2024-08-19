namespace Ressource
{
    public class PopulationConfig : AbstraitRessourceConfig
    {
        #region membres
        /// <summary>
        /// <see cref="croissancePourcent"/> 
        /// Indice de croissance de la population. Impacte la production de population.
        /// </summary>
        public float croissancePourcent;
        /// <summary>
        /// <see cref="mortaliteFaminePourcent"/> 
        /// Indice de mortalité des membres de la population en famine. Impacte la production de population.
        /// </summary>
        public float mortaliteFaminePourcent;
        /// <summary>
        /// <see cref="faimPourcent"/> 
        /// Indice de faim de la population. Impacte la consomation de nourriture de la population.
        /// </summary>
        public float faimPourcent;
        #endregion membres

        public PopulationConfig() : base()
        {
            Id = "R0_Population";
            Nom = "Population";
            Icone = null;  // TODO
            Description = "";
            QteBase = 100;
            QteMax = 1_000_000;
            QteMin = 2;
            croissancePourcent = 0.05f;
            mortaliteFaminePourcent = 0.05f;
            faimPourcent = 0.5f;
        }
    }
}
