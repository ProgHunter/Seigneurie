using Batiment;
using UnityEngine;
using Utils;

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
        /// <see cref="ProfessionPourcent"/> Pourcentage de la population affecté de base à la profession.
        /// </summary>
        private Qte _professionPourcent;
        private LotBatiments _prerequis;
        #endregion membres

        #region accesseurs_mutateurs
        public string Id { get => _id; protected set => _id = value; }
        public string Nom { get => _nom; protected set => _nom = value; }
        public Sprite Icone { get => _icone; protected set => _icone = value; }
        public string Description { get => _description; protected set => _description = value; }
        public Qte ProfessionPourcent { get => _professionPourcent.Clone(); protected set => _professionPourcent = value.Clone(); }
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
