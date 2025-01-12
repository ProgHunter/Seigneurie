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
        private string _id;
        private string _nom;
        private Sprite _icone;
        private string _description;
        private Qte _qte;
        private LotBatiments _prerequis;
        #endregion membres

        #region accesseurs_mutateurs
        public string Id { get => _id; protected set => _id = value; }
        public string Nom { get => _nom; protected set => _nom = value; }
        public Sprite Icone { get => _icone; protected set => _icone = value; }
        public string Description { get => _description; protected set => _description = value; }
        public Qte Qte { get => _qte.Clone(); protected set => _qte = value.Clone(); }
        public LotBatiments Prerequis { get => _prerequis.Clone(); protected set => _prerequis = value.Clone(); }
        #endregion accesseurs_mutateurs
    }

    public enum RessourceEnum
    {
        POPULATION = 0,
        NOURRITURE = 1,
        BOIS = 2,
        MINERAUX = 3,
    }
}
