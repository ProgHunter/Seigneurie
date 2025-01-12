namespace Utils
{
    public class Qte
    {
        public long qte;
        public long qteMax;
        public long qteMin;

        public Qte(long qte = 0, long qteMax = 0, long qteMin = 0)
        {
            this.qte = qte;
            this.qteMax = qteMax;
            this.qteMin = qteMin;
        }

        /// <summary>
        /// Crée un clone profond de la Qte
        /// </summary>
        /// <returns>Nouvel objet Qte avec les mêmes valeurs</returns>
        public Qte Clone()
        {
            return new Qte(qte, qteMax, qteMin);
        }
    }
}
