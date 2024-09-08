using System.Collections.Generic;
using UnityEngine;

namespace Profession
{
    public class LotProfessions
    {
        /// <summary>
        /// <see cref="_professionsDict"/> Chaque profession a un pourcentage attribué [0,1].
        ///                                Celui-ci impacte la production de ressources.
        /// </summary>
        private Dictionary<ProfessionEnum, float> _professionsDict;

        public LotProfessions(float pcNatalite, float pcFermier, float pcBucheron, float pcMineur, float pcMacon)
        {
            _professionsDict = new Dictionary<ProfessionEnum, float>
            {
                { ProfessionEnum.NATALITE,  pcNatalite },
                { ProfessionEnum.FERMIER,   pcFermier  },
                { ProfessionEnum.BUCHERON,  pcBucheron },
                { ProfessionEnum.MINEUR,    pcMineur   },
                { ProfessionEnum.MACON,     pcMacon    }
            };
        }

        /// <summary>
        /// Donne accès au pourcentage d'une profession.
        /// </summary>
        /// <param name="profession">La profession</param>
        /// <returns>Le pourcentage [0,1]</returns>
        public float AccesPourcentProfession(ProfessionEnum profession)
        {
            float pcProfession = 0;

            try
            {
                pcProfession = _professionsDict[profession];
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
            }

            return pcProfession;
        }

        /// <summary>
        /// Attribue un pourcentage à une profession.
        /// Aucune validation.
        /// </summary>
        /// <param name="profession">La profession</param>
        /// <param name="pcProfession">Le pourcentage à attribuer [0, 1].</param>
        public void AttribuerPourcentProfession(ProfessionEnum profession, float pcProfession)
        {
            try
            {
                _professionsDict[profession] = pcProfession;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
            }
        }

        /// <summary>
        /// Attribue le pourcentage de chaque profession du lot à celui-ci.
        /// Aucune validation.
        /// </summary>
        /// <param name="pcProfessions">Le lot de professions avec les pourcentages</param>
        public void AttribuerPourcentProfession(LotProfessions pcProfessions)
        {
            foreach (ProfessionEnum profession in _professionsDict.Keys)
                _professionsDict[profession] = pcProfessions._professionsDict[profession];
        }
    }
}