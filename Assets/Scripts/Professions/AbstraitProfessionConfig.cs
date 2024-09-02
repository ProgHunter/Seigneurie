using UnityEngine;

namespace Profession
{
    public abstract class AbstraitProfessionConfig
    {
        #region membres
        public string Id;
        public string Nom;
        public Sprite Icone;
        public string Description;
        /// <summary>
        /// <see cref="ProfessionPourcent"/> Pourcentage de la population affecté à la profession
        ///                                  TODO : À changer de place éventuellement?
        /// </summary>
        public float ProfessionPourcent = 0f;
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
