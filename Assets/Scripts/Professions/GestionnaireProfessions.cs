using Batiment;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Profession
{
    public sealed class GestionnaireProfessions
    {
        #region members
        private static readonly GestionnaireProfessions _instance = new();
        public Dictionary<ProfessionEnum, AbstraitProfessionConfig> ProfessionDict;
        #endregion members

        public GestionnaireProfessions()
        {
            ProfessionDict = new Dictionary<ProfessionEnum, AbstraitProfessionConfig>
            {
                { ProfessionEnum.NATALITE, new Natalite() },
                { ProfessionEnum.FERMIER,  new Fermier()  },
                { ProfessionEnum.BUCHERON, new Bucheron() },
                { ProfessionEnum.MINEUR,   new Mineur()   },
                { ProfessionEnum.MACON,    new Macon()    }
            };
        }

        public static GestionnaireProfessions Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// Retourne le pourcentage attribué à la profession
        /// </summary>
        /// <param name="profession"></param>
        /// <returns></returns>
        public float AccederPourcent(ProfessionEnum profession)
        {
            float pourcent = 0f;

            try
            {
                pourcent = ProfessionDict[profession].ProfessionPourcent;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
            }

            return pourcent;
        }

        /// <summary>
        /// Attribut un pourcentage à la profession.
        /// Aucune validation!
        /// </summary>
        /// <param name="profession">La profession que l'ont veut attribuer un pourcentage</param>
        /// <param name="pourcent">Le pourcentage à attribuer</param>
        public void AttribuerPourcent(ProfessionEnum profession, float pourcent)
        {
            try
            {
                ProfessionDict[profession].ProfessionPourcent = pourcent;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
            }
        }

        /// <summary>
        /// Attribuer un pourcentage de population à une profession
        /// La modification est ajustée si le pourcentage total de toutes les professions
        /// dépasserait 100% ensemble. Elle l'est aussi si le pourcentage est plus petit que 0.
        /// On ne peut attribuer un pourcentage à une profession verrouillée.
        /// </summary>
        /// <param name="profession">La profession</param>
        /// <param name="pourcent">Le pourcentage</param>
        /// <returns>Vrai si la modification respecte les lmites</returns>
        public bool AttribuerPourcentValide(ProfessionEnum profession, float pourcent)
        {
            float pourcentLibre = PourcentPopLibre();
            bool estValide = true;
            
            if (!EstDeverrouille(profession) || pourcent < 0)
            {
                pourcent = 0;
                estValide = false;
            } else if ((pourcent - AccederPourcent(profession)) > pourcentLibre)
            {
                pourcent = PourcentPopLibre();
                estValide = false;
            }

            AttribuerPourcent(profession, pourcent);
            return estValide;
        }

        /// <summary>
        /// Le pourcentage de population sans profession
        /// </summary>
        /// <returns>Le pourcentage de population libre</returns>
        public float PourcentPopLibre()
        {
            float pourcentPopLibre = 1.0f;
            foreach (ProfessionEnum profession in Enum.GetValues(typeof(ProfessionEnum)))
            {
                pourcentPopLibre -= ProfessionDict[profession].ProfessionPourcent;
            }

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
                prerequis = ProfessionDict[profession].Prerequis;
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
