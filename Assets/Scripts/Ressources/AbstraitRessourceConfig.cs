using UnityEngine;

namespace Ressource
{
    public abstract class AbstraitRessourceConfig
    {
        #region membres
        public string Id;
        public string Nom;
        public Sprite Icone;
        public string Description;
        public int QteBase;
        public int QteMin;
        public int QteMax;
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
