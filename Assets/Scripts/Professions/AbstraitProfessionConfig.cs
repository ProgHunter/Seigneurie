using UnityEngine;

namespace Profession
{
    public abstract class AbstraitProfessionConfig
    {
        #region membres
        public string id;
        public string nom;
        public Sprite icone;
        public string description;
        /// <summary>
        /// <see cref="professionPourcent"/> Pourcentage de la population affect� � la profession
        ///                                  TODO : � changer de place �ventuellement?
        /// </summary>
        public int professionPourcent = 0;
        #endregion membres

        /// <summary>
        /// Attribut le pourcentage � la profession
        /// Rectifie le pourcent si n'est pas entre 0 et 100 (inclusivement)
        /// </summary>
        /// <param name="pourcent"></param>
        public void AttribuerPourcentProfession(int pourcent)
        {
            if (pourcent > 100)
                pourcent = 100;
            else if (pourcent < 0)
                pourcent = 0;

            professionPourcent = pourcent;
        }
    }

    public enum ProfessionEnum
    {
        NATALITE = 0,
        FERMIER = 1,
        BUCHERON = 2,
        MINEUR = 3,
        MACON = 4
    }
}
