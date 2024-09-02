namespace Production
{
    public abstract class AbstraitProduction
    {
        /// <summary>
        /// <see cref="EfficacitePourcent"/> 
        /// Efficacité de la production.
        /// Sert purement de configuration pour balancer la production dans le jeu. Set à 1 par défaut.
        /// </summary>
        public float EfficacitePourcent = 1.0f;
        public abstract long CalculerProduction();
    }
}
