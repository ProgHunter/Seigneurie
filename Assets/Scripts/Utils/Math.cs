using System;

namespace Utils
{
    public static class MathUtils
    {
        /// <summary>
        /// P(t) = (K * Po * e^(r*t)) / (Po * e^(r*t) + K + Po)
        /// </summary>
        /// <param name="tick">t, entier positif dans l'interval [0, max double]</param>
        /// <param name="popMin">Po, entier positif dans l'interval [1, max long]</param>
        /// <param name="capaciteMax">K, entier positif dans l'interval [popMin+1, max long]</param>
        /// <param name="croissance">r, taux de croissance dans l'interval [0.01, 1]</param>
        /// <returns>P(t) soit la population au tick t. [popMin, capaciteMax]</returns>
        public static double FonctionLogistique(double tick, long popMin, long capaciteMax, float croissance)
        {
            if (croissance <= 0)
                return popMin;
            // Prévenir une division par zéro
            popMin = popMin < 1 ? 1 : popMin;
            capaciteMax = capaciteMax <= popMin ? popMin + 1 : capaciteMax;

            double expP0 = Math.Exp(tick * croissance) * popMin;

            double numerateur = expP0 * capaciteMax;
            double denominateur = expP0 + capaciteMax + popMin;

            var resultat = numerateur / denominateur;
            return resultat;
        }

        /// <summary>
        /// t(P) = ln(-(P * (K - Po)) / ((P - K) * Po)) / r
        /// </summary>
        /// <param name="popActuelle">P, entier positif dans l'interval [popMin, capaciteMax[</param>
        /// <param name="popMin">Po, entier positif dans l'interval [1, max long]</param>
        /// <param name="capaciteMax">K, entier positif dans l'interval [popMin+1, max long]</param>
        /// <param name="croissance">r, taux de croissance dans l'interval [0.01, 1]</param>
        /// <returns>t(P) soit le tick correspodant à la quantité de population actuelle sur la courbe. [0, max double]</returns>
        public static double FonctionLogistiqueTickIsole(long popActuelle, long popMin, long capaciteMax, float croissance)
        {
            popActuelle = popActuelle <= popMin ? popMin : popActuelle;
            // Empêcher la division par zéro ou le log d'un négatif
            capaciteMax = capaciteMax <= popActuelle ? capaciteMax + 1 : capaciteMax;
            
            croissance = croissance <= 0 ? 0.01f : croissance;

            long numerateurLog = popActuelle * (capaciteMax - popMin);
            long denominateurlog = (popActuelle - capaciteMax) * popMin;
            double numerateur = Math.Log(-(double)numerateurLog / denominateurlog);

            var resultat = numerateur / croissance;
            return resultat;
        }

        /// <summary>
        /// T(s) = (1 - p)T + (p/s)T
        /// </summary>
        /// <param name="parallelisable">p</param>
        /// <param name="nbPersonnes">s</param>
        /// <returns>S(s) soit </returns>
        public static ulong TempsExecutionParalleleAmdahl(float parallelisable, long nbPersonnes)
        {
            return 1; //?
        }
    }
}
