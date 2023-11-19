namespace Production
{
    public abstract class AbstraitProduction
    {
        /// <summary>
        /// <see cref="efficacitePourcent"/> Efficacité de la production.
        ///                                  Sert purement de configuration pour balancer la production dans le jeu.
        /// </summary>
        public int efficacitePourcent = 100;
        public abstract int CalculerProduction();
    }
}
