namespace Production
{
    public abstract class AbstraitProduction
    {
        /// <summary>
        /// <see cref="EfficacitePourcent"/> 
        /// Efficacité de la production.
        /// Sert purement de configuration pour balancer la production dans le jeu. Set à 1 par défaut.
        /// </summary>
        private float _efficacitePourcent = 1.0f;

        public float EfficacitePourcent { get => _efficacitePourcent; protected set => _efficacitePourcent = value; }

        public abstract long CalculerProduction();
    }
}
