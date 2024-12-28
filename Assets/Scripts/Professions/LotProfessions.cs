using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Profession
{
    public class LotProfessions
    {
        /// <summary>
        /// <see cref="_professionsDict"/> Chaque profession a un pourcentage attribu� [0,100].
        ///                                Celui-ci impacte la production de ressources.
        /// </summary>
        private Dictionary<ProfessionEnum, Qte> _professionsDict;

        public LotProfessions(Qte pcNatalite, Qte pcFermier, Qte pcBucheron, Qte pcMineur, Qte pcMacon)
        {
            _professionsDict = new Dictionary<ProfessionEnum, Qte>
            {
                { ProfessionEnum.NATALITE,  pcNatalite.Clone() },
                { ProfessionEnum.FERMIER,   pcFermier.Clone()  },
                { ProfessionEnum.BUCHERON,  pcBucheron.Clone() },
                { ProfessionEnum.MINEUR,    pcMineur.Clone()   },
                { ProfessionEnum.MACON,     pcMacon.Clone()    }
            };
        }

        public LotProfessions(long pcNatalite = 0, long pcFermier = 0, long pcBucheron = 0, long pcMineur = 0, long pcMacon = 0)
        {
            _professionsDict = new Dictionary<ProfessionEnum, Qte>
            {
                { ProfessionEnum.NATALITE,  new Qte(pcNatalite) },
                { ProfessionEnum.FERMIER,   new Qte(pcFermier)  },
                { ProfessionEnum.BUCHERON,  new Qte(pcBucheron) },
                { ProfessionEnum.MINEUR,    new Qte(pcMineur)   },
                { ProfessionEnum.MACON,     new Qte(pcMacon)    }
            };
        }

        /// <summary>
        /// Cr�e un clone profond du lot de professions
        /// </summary>
        /// <returns>Nouvel objet LotProfessions avec les m�mes valeurs</returns>
        public LotProfessions Clone()
        {
            return new LotProfessions(_professionsDict[ProfessionEnum.NATALITE].Clone(),
                                      _professionsDict[ProfessionEnum.FERMIER].Clone(),
                                      _professionsDict[ProfessionEnum.BUCHERON].Clone(),
                                      _professionsDict[ProfessionEnum.MINEUR].Clone(),
                                      _professionsDict[ProfessionEnum.MACON].Clone());
        }

        /// <summary>
        /// Donne accès au pourcentage d'une profession.
        /// </summary>
        /// <param name="profession">La profession</param>
        /// <returns>Le pourcentage [0,100]</returns>
        public long AccesPourcentProfession(ProfessionEnum profession)
        {
            long pcProfession = 0;

            try
            {
                pcProfession = _professionsDict[profession].qte;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
            }

            return pcProfession;
        }

        /// <summary>
        /// Donne acc�s au pourcentage d'une profession en fraction [0,1].
        /// </summary>
        /// <param name="profession">La profession</param>
        /// <returns>Le pourcentage [0,1]</returns>
        public float AccesPourcentProfessionFraction(ProfessionEnum profession)
        {
            return (float)AccesPourcentProfession(profession) / 100;
        }

        /// <summary>
        /// Attribue un pourcentage � une profession.
        /// Aucune validation.
        /// </summary>
        /// <param name="profession">La profession</param>
        /// <param name="pcProfession">Le pourcentage � attribuer [0, 100].</param>
        public void AttribuerPourcentProfession(ProfessionEnum profession, long pcProfession)
        {
            try
            {
                _professionsDict[profession].qte = pcProfession;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
            }
        }

        /// <summary>
        /// Attribue le pourcentage de chaque profession du lot � celui-ci.
        /// Aucune validation.
        /// </summary>
        /// <param name="pcProfessions">Le lot de professions avec les pourcentages</param>
        public void AttribuerPourcentProfession(LotProfessions pcProfessions)
        {
            foreach (ProfessionEnum profession in _professionsDict.Keys)
                _professionsDict[profession].qte = pcProfessions._professionsDict[profession].qte;
        }

        public long AccesPourcentMin(ProfessionEnum profession)
        {
            long pcMin = 0;

            try
            {
                pcMin = _professionsDict[profession].qteMin;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
            }

            return pcMin;
        }

        public void AttribuerPourcentMin()
        {
            foreach (ProfessionEnum profession in _professionsDict.Keys)
                _professionsDict[profession].qte = _professionsDict[profession].qteMin;
        }

        public long AccesPourcentMax(ProfessionEnum profession)
        {
            long pcMax = 0;

            try
            {
                pcMax = _professionsDict[profession].qteMax;
            }
            catch (KeyNotFoundException)
            {
                Debug.LogError($"La profession {profession} n'est pas dans le dictionnaire.");
            }

            return pcMax;
        }
    }
}
