using UnityEngine;
using Ressource;

namespace Batiment
{
    public abstract class AbstraitBatimentConfig
    {
        #region membres
        /// <summary>
        /// <see cref="prerequis"/> Indique les bâtiments prérequis pour débloquer celui-ci.
        /// </summary>
        public string id;
        public string nom;
        public Sprite icone;
        public string description;
        public LotRessources coutConstruction;
        public int effortConstruction;
        public LotBatiments prerequis;
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
