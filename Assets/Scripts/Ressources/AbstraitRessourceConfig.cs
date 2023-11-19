using UnityEngine;

namespace Ressource
{
    public abstract class AbstraitRessourceConfig
    {
        #region membres
        public string id;
        public string nom;
        public Sprite icone;
        public string description;
        public int qteBase;
        public int qteMax;
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
