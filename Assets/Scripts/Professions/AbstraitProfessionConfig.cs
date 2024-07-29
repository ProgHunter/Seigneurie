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
        /// <see cref="professionPourcent"/> Pourcentage de la population affecté à la profession
        ///                                  TODO : À changer de place éventuellement?
        /// </summary>
        public float professionPourcent = 0f;
        #endregion membres
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
