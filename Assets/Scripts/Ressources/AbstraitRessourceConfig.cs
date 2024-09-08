using UnityEngine;
using Batiment;
using Utils;

namespace Ressource
{
    public abstract class AbstraitRessourceConfig
    {
        /// <summary>
        /// <see cref="Prerequis"/> Indique les bâtiments prérequis pour débloquer celle-ci.
        /// </summary>
        #region membres
        public string Id;
        public string Nom;
        public Sprite Icone;
        public string Description;
        public Qte Qte;
        public LotBatiments Prerequis;
        #endregion membres
    }

    public enum RessourceEnum
    {
        POPULATION = 0,
        NOURRITURE = 1,
        BOIS = 2,
        MINERAUX = 3,
    }
}
