using Batiment;
using System.Collections.Generic;
using UnityEngine;

namespace Profession
{
    public sealed class GestionnaireProfessions
    {
        #region members
        private static readonly GestionnaireProfessions _instance = new();
        private LotProfessions _professions;
        public Dictionary<ProfessionEnum, AbstraitProfessionConfig> ProfessionDictConfig;

        private const float _pcMax = 1f;
        private const float _pcMin = 0f;
        private const float _pcPrecision = 0.001f;
        #endregion members

        public GestionnaireProfessions()
        {
            ProfessionDictConfig = new Dictionary<ProfessionEnum, AbstraitProfessionConfig>
            {
                { ProfessionEnum.NATALITE, new NataliteConfig() },
                { ProfessionEnum.FERMIER,  new FermierConfig()  },
                { ProfessionEnum.BUCHERON, new BucheronConfig() },
                { ProfessionEnum.MINEUR,   new MineurConfig()   },
                { ProfessionEnum.MACON,    new MaconConfig()    }
            };

            _professions = new LotProfessions(ProfessionDictConfig[ProfessionEnum.NATALITE].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.FERMIER].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.BUCHERON].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.MINEUR].ProfessionPourcent,
                                              ProfessionDictConfig[ProfessionEnum.MACON].ProfessionPourcent);
        }

        public static GestionnaireProfessions Instance
        {
            get
            {
                return _instance;
            }
        }

        #region accesseurs_mutateurs
        /// <summary>
        /// Retourne le pourcentage attribué à la profession
        /// </summary>
        /// <param name="profession"></param>
        /// <returns></returns>
        public float AccederPourcent(ProfessionEnum profession)
        {
            return _professions.AccesPourcentProfession(profession);
        }

        /// <summary>
        /// Attribut un pourcentage à la profession.
        /// Aucune validation!
        /// </summary>
        /// <param name="profession">La profession que l'ont veut attribuer un pourcentage</param>
        /// <param name="pourcent">Le pourcentage à attribuer</param>
        public void AttribuerPourcent(ProfessionEnum profession, float pourcent)
        {
            _professions.AttribuerPourcentProfession(profession, pourcent);
        }

        /// <summary>
        /// Attribue le pourcentage de chaque profession du lot à celui-ci.
        /// Aucune validation.
        /// </summary>
        /// <param name="pcProfessions">Le lot de professions avec les pourcentages</param>
        public void AttribuerPourcent(LotProfessions pcProfessions)
        {
            _professions.AttribuerPourcentProfession(pcProfessions);
        }

        /// <summary>
        /// Attribuer un pourcentage de population à une profession.
        /// La modification est ajustée si le pourcentage total de toutes les professions
        /// dépasserait 100% ensemble. Elle l'est aussi si le pourcentage est plus petit que 0.
        /// On ne peut attribuer un pourcentage à une profession verrouillée.
        /// </summary>
        /// <param name="profession">La profession</param>
        /// <param name="pourcent">Le pourcentage</param>
        /// <returns>Vrai si la modification respecte les limites</returns>
        public bool AttribuerPourcentValide(ProfessionEnum profession, float pourcent)
        {
            float pourcentLibre = PourcentPopLibre();
            bool estValide = true;
            
            if (!EstDeverrouille(profession) || pourcent < (_pcMin - _pcPrecision))
            {
                pourcent = _pcMin;
                estValide = false;
            } else if ((pourcent - AccederPourcent(profession)) > (pourcentLibre + _pcPrecision))
            {
                pourcent = PourcentPopLibre();
                estValide = false;
            }

            AttribuerPourcent(profession, pourcent);
            return estValide;
        }

        /// <summary>
        /// Attribuer le pourcentage de population décrit par le lot pour les professions.
        /// La modification est ajustée si le pourcentage total de toutes les professions
        /// dépasserait 100% ensemble. Elle l'est aussi si le pourcentage est plus petit que 0.
        /// On ne peut attribuer un pourcentage à une profession verrouillée.
        /// </summary>
        /// <param name="pcProfessions"></param>
        /// <returns>Vrai si les modifications respectent les limites</returns>
        public bool AttribuerPourcentValide(LotProfessions pcProfessions)
        {
            bool estValide = true;
            foreach (ProfessionEnum profession in ProfessionDictConfig.Keys)
                estValide &= AttribuerPourcentValide(profession, pcProfessions.AccesPourcentProfession(profession));

            return estValide;
        }
        #endregion accesseurs_mutateurs

        /// <summary>
        /// Incrémente le pourcentage de 1, max 100%.
        /// </summary>
        /// <param name="profession">La profession à incrémenter le poucentage.</param>
        /// <returns>Vrai si le pourcentage a été incrémenté de 1%.</returns>
        public bool IncrementerPourcent(ProfessionEnum profession)
        {
            float pourcent = AccederPourcent(profession);
            return AttribuerPourcentValide(profession, ++pourcent);
        }

        /// <summary>
        /// Décrémente le pourcentage de 1, min 0%.
        /// </summary>
        /// <param name="profession">La profession à incrémenter le poucentage.</param>
        /// <returns>Vrai si le pourcentage a été décrémenté de 1%.</returns>
        public bool DecrementerPourcent(ProfessionEnum profession)
        {
            float pourcent = AccederPourcent(profession);
            return AttribuerPourcentValide(profession, --pourcent);
        }

        /// <summary>
        /// Le pourcentage de population sans profession
        /// </summary>
        /// <returns>Le pourcentage de population libre</returns>
        public float PourcentPopLibre()
        {
            float pourcentPopLibre = 1.0f;
            foreach (ProfessionEnum profession in ProfessionDictConfig.Keys)
            {
                pourcentPopLibre -= _professions.AccesPourcentProfession(profession);
            }

            if (pourcentPopLibre > (_pcMax + _pcPrecision) || pourcentPopLibre < (_pcMin - _pcPrecision))
                Debug.LogError($"Le pourcentage de population libre est {pourcentPopLibre:0.000}, alors qu'il devrait être dans [0,1].");

            return pourcentPopLibre;
        }

        /// <summary>
        /// Indique si la profession est déverrouillée.
        /// Si le prérequis de la profession n'est pas rencontré, celle-ci est verrouillée.
        /// </summary>
        /// <param name="profession">La profession dont on valide le prérequis.</param>
        /// <returns>Vrai si la profession est déverrouillée.</returns>
        public bool EstDeverrouille(ProfessionEnum profession)
        {
            LotBatiments prerequis;
            try
            {
                prerequis = ProfessionDictConfig[profession].Prerequis;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
                return false;
            }

            return GestionnaireBatiments.Instance.PrerequisEstRespecte(prerequis);
        }
    }
}
