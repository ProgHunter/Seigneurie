using System;

namespace Utils
{
    public static class MathUtils
    {
        /// <summary>
        /// P(t) = (K * Po * e^(r*t)) / (Po * e^(r*t) + K + Po)
        /// </summary>
        /// <param name="tick">t</param>
        /// <param name="nbPopBase">Po</param>
        /// <param name="capaciteMax">K</param>
        /// <param name="croissancePourcent">r</param>
        /// <returns>P(t) soit la population au tick t (arondi plafond)</returns>
        public static int FonctionLogistique(double tick, int nbPopBase, int capaciteMax, float croissance)
        {
            double expP0 = Math.Exp((double)croissance * tick) * nbPopBase;
            return (int)Math.Ceiling((capaciteMax * expP0) / (expP0 + capaciteMax + nbPopBase));
        }

        /// <summary>
        /// t(P) = ln((-P * (K - Po)) / ((P - K) * Po)) / r
        /// </summary>
        /// <param name="popActuelle">P</param>
        /// <param name="nbPopBase">Po</param>
        /// <param name="capaciteMax">K</param>
        /// <param name="croissance">r</param>
        /// <returns>t(P) soit le tick correspodant à la quantité de population actuelle sur la courbe</returns>
        public static double FonctionLogistiqueTickIsole(int popActuelle, int nbPopBase, int capaciteMax, float croissance)
        {
            return Math.Log((double)(-popActuelle * (capaciteMax - nbPopBase)) / ((popActuelle - capaciteMax) * nbPopBase)) / croissance;
        }

        /// <summary>
        /// T(s) = (1 - p)T + (p/s)T
        /// </summary>
        /// <param name="parallelisable">p</param>
        /// <param name="nbPersonnes">s</param>
        /// <returns>S(s) soit </returns>
        public static int TempsExecutionParalleleAmdahl(float parallelisable, int nbPersonnes)
        {
            return 1; //?
        }
    }
}
