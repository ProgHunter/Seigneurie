using UnityEngine;
using Ressource;
using Utils;

namespace Batiment
{
    public abstract class AbstraitBatimentConfig
    {
        #region membres
        /// <summary>
        /// <see cref="Prerequis"/> Indique les bâtiments prérequis pour débloquer celui-ci.
        /// </summary>
        private string _id;
        private string _nom;
        private Sprite _icone;
        private string _description;
        private Qte _qte;
        private LotRessources _coutConstruction;
        private int _effortConstruction;
        private LotBatiments _prerequis;
        #endregion membres

        #region accesseurs_mutateurs
        public string Id { get => _id; protected set => _id = value; }
        public string Nom { get => _nom; protected set => _nom = value; }
        public Sprite Icone { get => _icone; protected set => _icone = value; }
        public string Description { get => _description; protected set => _description = value; }
        public Qte Qte { get => _qte.Clone(); protected set => _qte = value.Clone(); }
        public LotRessources CoutConstruction { get => _coutConstruction.Clone(); protected set => _coutConstruction = value.Clone(); }
        public int EffortConstruction { get => _effortConstruction; protected set => _effortConstruction = value; }
        public LotBatiments Prerequis { get => _prerequis.Clone(); protected set => _prerequis = value.Clone(); }
        #endregion accesseurs_mutateurs
    }

    public enum BatimentEnum
    {
        NUL = 0,
        MAISON = 1,
        FERME = 2,
        SCIERIE = 3,
        MINE = 4,
        HOTELDEVILLE = 5,
        CHATEAU = 6
    }
}
