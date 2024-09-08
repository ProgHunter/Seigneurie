using Batiment;
using UnityEngine;

namespace Profession
{
    public abstract class AbstraitProfessionConfig
    {
        #region membres
        private string _id;
        private string _nom;
        private Sprite _icone;
        private string _description;
        /// <summary>
        /// <see cref="ProfessionPourcent"/> Pourcentage de la population affecté à la profession
        ///                                  TODO : À changer de place éventuellement. 
        ///                                  Créer un dict de config séparé d'un dict de poucents.
        /// </summary>
        private float _professionPourcent;
        private LotBatiments _prerequis;
        #endregion membres

        #region accesseurs_mutateurs
        public string Id { get => _id; protected set => _id = value; }
        public string Nom { get => _nom; protected set => _nom = value; }
        public Sprite Icone { get => _icone; protected set => _icone = value; }
        public string Description { get => _description; protected set => _description = value; }
        public float ProfessionPourcent { get => _professionPourcent; protected set => _professionPourcent = value; }
        public LotBatiments Prerequis { get => _prerequis.Clone(); protected set => _prerequis = value.Clone(); }
        #endregion accesseurs_mutateurs
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
