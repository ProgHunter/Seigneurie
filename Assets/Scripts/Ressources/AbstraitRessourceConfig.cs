using System.Collections.Generic;
using UnityEngine;

namespace Ressource
{
    public abstract class AbstraitRessourceConfig
    {
        #region members
        public string ID;
        public string Nom;
        public Sprite Icone;
        public string Description;
        #endregion members
    }

    public enum RessourceEnum
    {
        POPULATION = 0,
        NOURRITURE = 1,
        BOIS = 2,
        MINERAUX = 3,
    }
}
