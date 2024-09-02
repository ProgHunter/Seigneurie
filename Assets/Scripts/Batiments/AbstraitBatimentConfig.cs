using UnityEngine;
using Ressource;

namespace Batiment
{
    public abstract class AbstraitBatimentConfig
    {
        #region membres
        /// <summary>
        /// <see cref="Prerequis"/> Indique les bâtiments prérequis pour débloquer celui-ci.
        /// </summary>
        public string Id;
        public string Nom;
        public Sprite Icone;
        public string Description;
        public LotRessources CoutConstruction;
        public int EffortConstruction;
        public LotBatiments Prerequis;
        #endregion membres
    }

    public enum BatimentEnum
    {
        MAISON = 0,
        FERME = 1,
        SCIERIE = 2,
        MINE = 3,
        HOTELDEVILLE = 4,
    }
}
